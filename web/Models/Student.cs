using System.Collections.Generic;

namespace web.Models
{
  public class Student
  {
    public string Id {get;set;}
    public string  Name { get; set;}
    public string Email {get; set;}
    public string GitUrl{get; set;}

    public IEnumerable<Reward> Rewards{get; set;}
  }  
}