using Core.Models;
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;

namespace DapperGenericRepository.Controllers
{
    public class TestController : ApiController
    {
        private readonly IStudentRepository _studentRepository;

        public TestController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [Route("api/Test/GetStudentById")]
        [HttpGet]
        public async Task<Student> GetStudentById(long id)
        {
            var result = await _studentRepository.GetStudentById(id).ConfigureAwait(false);
            return result;
        }
    }
}
