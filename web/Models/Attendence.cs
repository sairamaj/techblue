using Newtonsoft.Json;
namespace web.Models
{
  class AttendanceData{
    public string Name {get; set;}
  }
  public class Attendence
  {
    public string Id {get; set;}
    public string  Name { get; set;}
    
    public string  Date { get; set;}
    public string Data { set{
        System.Console.WriteLine("data:" + value);
        var extraData =  JsonConvert.DeserializeObject<AttendanceData>(value);
        this.Name = extraData.Name;
    }}
  }  
}
