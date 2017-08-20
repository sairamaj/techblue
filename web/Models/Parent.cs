using System.Collections.Generic;

namespace web.Models
{
  public class Parent : User
  {
      public Parent()
      {
          Students = new List<Student>();
      }
      public IEnumerable<Student> Students{get;set;}
  }  
}