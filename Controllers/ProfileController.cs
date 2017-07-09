using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using web.Models;

namespace web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public const string ObjectIdentifier = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        public ProfileController(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        IClassRepository _classRepository;
        public async Task<IActionResult> Index()
        {
            var profile = await _classRepository.GetProfile(GetClaimValue(ObjectIdentifier));
            if( profile == null)
            {
                profile = new Profile();
            }

            profile.Name = User.Identity.Name;
            profile.Email = GetClaimValue("emails");
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Profile profile)
        {
            profile.Id = GetClaimValue(ObjectIdentifier);
            profile.Name = User.Identity.Name;
            System.Console.WriteLine("Updating:" + profile.Id);
            await _classRepository.UpdateProfile(profile.Id, profile);
            return View();
        }

        private string GetClaimValue(string claimType)
        {
            foreach(var c in User.Claims)
            {
                System.Console.WriteLine("{0} - {1}", c.Value, c.Type);
            }

            var claim = User.Claims.FirstOrDefault(c => c.Type == claimType);
            if( claim == null)
            {
                return null;
            }
            return claim.Value;
        }
    }
}
