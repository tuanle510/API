using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Respositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.Web03.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]

    public class FixedAssetCategoriesController : ControllerBase
    {
        IConfiguration _configuration;
        IFixedAssetCategoryRepository _fixedAssetCategoryRepository;
        public FixedAssetCategoriesController(IConfiguration configuration, IFixedAssetCategoryRepository fixedAssetCategoryRepository)
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
    }
}
