using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Respositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.Web03.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixedAssetCategoryController : ControllerBase
    {
        IConfiguration _configuration;
        IFixedAssetCategoryRepository _fixedAssetCategoryRepository;
        public FixedAssetCategoryController(IConfiguration configuration, IFixedAssetCategoryRepository fixedAssetCategoryRepository)
        {
            _configuration = configuration;
            _fixedAssetCategoryRepository = fixedAssetCategoryRepository;
        }

        // GET: api/<FixedAssetCategoryController>
        [HttpGet]
        public IActionResult Get()
        {
            var fixedAssetCategorys = _fixedAssetCategoryRepository.Get();
            return Ok(fixedAssetCategorys);
        }

        // GET api/<FixedAssetCategoryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FixedAssetCategoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FixedAssetCategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FixedAssetCategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
