using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Identity.Client;
using web.Models;
using System.Security.Claims;

namespace web
{
    public class OpenIdConnectOptionsSetup : IConfigureOptions<OpenIdConnectOptions>
    {
        IClassRepository classRepository;
        public OpenIdConnectOptionsSetup(IOptions<AzureAdB2COptions> b2cOptions, IClassRepository classRepository)
        {
            AzureAdB2COptions = b2cOptions.Value;
            ClassRepository = classRepository;
        }

        public AzureAdB2COptions AzureAdB2COptions { get; set; }
        public IClassRepository ClassRepository { get; set; }

        public void Configure(OpenIdConnectOptions options)
        {
            options.ClientId = AzureAdB2COptions.ClientId;
            options.Authority = AzureAdB2COptions.Authority;
            options.UseTokenLifetime = true;
            options.TokenValidationParameters = new TokenValidationParameters() { NameClaimType = "name" };

            options.Events = new OpenIdConnectEvents()
            {
                OnRedirectToIdentityProvider = OnRedirectToIdentityProvider,
                OnRemoteFailure = OnRemoteFailure,
                OnAuthorizationCodeReceived = OnAuthorizationCodeReceived,
                OnTokenValidated = SecurityTokenValidated
            };
        }

        public Task OnRedirectToIdentityProvider(RedirectContext context)
        {
            var defaultPolicy = AzureAdB2COptions.DefaultPolicy;
            if (context.Properties.Items.TryGetValue(AzureAdB2COptions.PolicyAuthenticationProperty, out var policy) &&
                !policy.Equals(defaultPolicy))
            {
                context.ProtocolMessage.Scope = OpenIdConnectScope.OpenIdProfile;
                context.ProtocolMessage.ResponseType = OpenIdConnectResponseType.IdToken;
                context.ProtocolMessage.IssuerAddress = context.ProtocolMessage.IssuerAddress.ToLower().Replace(defaultPolicy.ToLower(), policy.ToLower());
                context.Properties.Items.Remove(AzureAdB2COptions.PolicyAuthenticationProperty);
            }
            else if (!string.IsNullOrEmpty(AzureAdB2COptions.ApiUrl))
            {
                context.ProtocolMessage.Scope += $" offline_access {AzureAdB2COptions.ApiScopes}";
                context.ProtocolMessage.ResponseType = OpenIdConnectResponseType.CodeIdToken;
            }
            return Task.FromResult(0);
        }

        public Task OnRemoteFailure(FailureContext context)
        {
            context.HandleResponse();
            // Handle the error code that Azure AD B2C throws when trying to reset a password from the login page 
            // because password reset is not supported by a "sign-up or sign-in policy"
            if (context.Failure is OpenIdConnectProtocolException && context.Failure.Message.Contains("AADB2C90118"))
            {
                // If the user clicked the reset password link, redirect to the reset password route
                context.Response.Redirect("/Session/ResetPassword");
            }
            else if (context.Failure is OpenIdConnectProtocolException && context.Failure.Message.Contains("access_denied"))
            {
                context.Response.Redirect("/");
            }
            else
            {
                context.Response.Redirect("/Home/Error?message=" + context.Failure.Message);
            }
            return Task.FromResult(0);
        }

        public async Task OnAuthorizationCodeReceived(AuthorizationCodeReceivedContext context)
        {
            // Use MSAL to swap the code for an access token
            // Extract the code from the response notification
            var code = context.ProtocolMessage.Code;

            foreach (var claim in context.Ticket.Principal.Claims)
            {
                System.Console.WriteLine(claim.Type + "-->" + claim.Value);
            }

            string signedInUserID = context.Ticket.Principal.FindFirst(ClaimTypes.NameIdentifier).Value;
            TokenCache userTokenCache = new MSALSessionCache(signedInUserID, context.HttpContext).GetMsalCacheInstance();
            ConfidentialClientApplication cca = new ConfidentialClientApplication(AzureAdB2COptions.ClientId, AzureAdB2COptions.Authority, AzureAdB2COptions.RedirectUri, new ClientCredential(AzureAdB2COptions.ClientSecret), userTokenCache, null);
            try
            {
                AuthenticationResult result = await cca.AcquireTokenByAuthorizationCodeAsync(code, AzureAdB2COptions.ApiScopes.Split(' '));


                context.HandleCodeRedemption(result.AccessToken, result.IdToken);
            }
            catch (Exception ex)
            {
                //TODO: Handle
                throw;
            }
        }

        private Task SecurityTokenValidated(TokenValidatedContext context)
        {
            // resource: https://stackoverflow.com/questions/40302231/authorize-by-group-in-azure-active-directory-b2c

            Console.WriteLine("In SecurityTokenValidated....");
            var oidClaim = context.SecurityToken.Claims.FirstOrDefault(c => c.Type == "oid");
            System.Console.WriteLine("oidClaim is:" + oidClaim);
            if (oidClaim.Value == "766e375f-360a-4945-904e-cb4aaac5d1e7")
            {
                // This is temporary. here we are going to query the Groups for this user and add accordingly.
                ((ClaimsIdentity)context.Ticket.Principal.Identity).AddClaim(new Claim(ClaimTypes.Role, "Administrators", ClaimValueTypes.String));
            }
            Console.WriteLine("Administrators claim has been added");


            if( ClassRepository.GetParents().Result.FirstOrDefault(p=> p.Id == oidClaim.Value) != null)
            {
                Console.WriteLine("Parent claim has been added");
                ((ClaimsIdentity)context.Ticket.Principal.Identity).AddClaim(new Claim(ClaimTypes.Role, "Parents", ClaimValueTypes.String));
            }

            return Task.FromResult(0);
        }
    }
}
