using System.Collections.Generic;
using System.Threading.Tasks;
public interface IClassRepository
{
    Task<IEnumerable<Student>> GetStudents();   
}