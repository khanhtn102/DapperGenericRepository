using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<Student> GetStudentById(long id);
    }
}
