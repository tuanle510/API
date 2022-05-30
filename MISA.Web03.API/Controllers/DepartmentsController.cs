using Microsoft.AspNetCore.Mvc;
using MISA.Core.Interfaces.Respositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.Web03.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        IConfiguration _configuration;
        IDepartmentRepository _departmentRepository;

        public DepartmentsController(IConfiguration configuration, IDepartmentRepository departmentRepository)
        {
            _configuration = configuration;
            _departmentRepository = departmentRepository;
        }
        // GET: api/<DepartmentController>
        [HttpGet]
        public IActionResult Get()
        {
            var departments = _departmentRepository.Get();
            return Ok(departments);
        }

        // GET api/<DepartmentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DepartmentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DepartmentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DepartmentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
