using System.Collections.Generic;

namespace web.Models
{
  public class Student : User
  {
    public string GitUrl{get; set;}
    public IEnumerable<Reward> Rewards{get; set;}
  }  
}