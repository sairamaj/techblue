using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using web.Models;

class ClassRepository : IClassRepository
{
    public async Task<IEnumerable<Student>> GetStudents()
    {
        
        var client = new HttpClient();
        var jsonData = await client.GetStringAsync("https://basicjavaclass.azurewebsites.net/api/students");
        return JsonConvert.DeserializeObject<List<Student>>(jsonData);        
    }
}