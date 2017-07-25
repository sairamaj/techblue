using Newtonsoft.Json;
namespace web.Models
{
  public class Profile
  {
    public string Id {get; set;}
    public string  Name { get; set;}
    
    [JsonIgnore]
    public string  Email { get; set;}
    public string  GitUrl { get; set;}
  }  
}