using Core.Models;
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(DbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Student> GetStudentById(long id)
        {
            var result = await GetAsync(new { ID = 6 }).ConfigureAwait(false);
            return result;
        }
    }
}
