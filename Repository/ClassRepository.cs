using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json;
using web.Models;

class ClassRepository : IClassRepository
{
    const string StudentProfileApiUrl = "https://d4htq98825.execute-api.us-east-1.amazonaws.com/prod/students/profile/";

    public async Task<IEnumerable<Student>> GetStudents()
    {
        var client = new HttpClient();
        var jsonData = await client.GetStringAsync("https://basicjavaclass.azurewebsites.net/api/students");
        return JsonConvert.DeserializeObject<List<Student>>(jsonData);
    }

    public async Task<Profile> GetProfile(string id)
    {
        var client = new HttpClient();
        var jsonData = await client.GetStringAsync(StudentProfileApiUrl + id);
        return JsonConvert.DeserializeObject<Profile>(jsonData);
    }

    public async Task<Profile> UpdateProfile(string id, Profile profile)
    {
        var client = new HttpClient();
        var settings = new JsonSerializerSettings();
        settings.ContractResolver = new LowercaseContractResolver();
        var json = JsonConvert.SerializeObject(profile, Formatting.Indented, settings);
        System.Console.WriteLine("updating :" + json);
        var result = await client.PostAsync(StudentProfileApiUrl + id, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
        System.Console.WriteLine(result.StatusCode);
        if (result.StatusCode != HttpStatusCode.OK)
        {
            throw new System.Exception("Error:" + result.RequestMessage);
        }
        System.Console.WriteLine("Updated successfully");
        return profile;
    }
}