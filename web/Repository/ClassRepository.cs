using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

class ClassRepository : IClassRepository
{
    public async Task<IEnumerable<Student>> GetStudents()
    {
        
        var client = new HttpClient();
        var jsonData = await client.GetStringAsync("https://basicjavaclass.azurewebsites.net/api/GetStudents");
        return JsonConvert.DeserializeObject<List<Student>>(jsonData);        
    }
}