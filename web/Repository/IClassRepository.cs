using System.Collections.Generic;
using System.Threading.Tasks;
using web.Models;
public interface IClassRepository
{
    Task<IEnumerable<Student>> GetStudents();   
}