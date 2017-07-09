using System.Collections.Generic;
using System.Threading.Tasks;
using web.Models;
public interface IClassRepository
{
    Task<IEnumerable<Student>> GetStudents();   
    Task<Profile> GetProfile(string id);
    Task<Profile> UpdateProfile(string id, Profile profile);
}