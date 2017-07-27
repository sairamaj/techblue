using System.Collections.Generic;
using System.Threading.Tasks;
using web.Models;
public interface IClassRepository
{
    Task<IEnumerable<Student>> GetStudents();   
    Task<Profile> GetProfile(string id);
    Task<Profile> UpdateProfile(string id, Profile profile);
    Task UpdateAttendance(string id, string name, System.DateTime date);
    Task<bool> IsClassRunningToday();

    Task<IEnumerable<Attendence>> GetAttendance(System.DateTime dt);
    Task<IEnumerable<Attendence>> GetAttendance(string id);
    Task<bool> CheckAttendanceExists(string id, System.DateTime dt);
    Task<IEnumerable<Class>> GetClasses();
}