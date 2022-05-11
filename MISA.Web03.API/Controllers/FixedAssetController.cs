using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Respositories;
using MISA.Core.Interfaces.Services;
using MISA.Infrastructure.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.Web03.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FixedAssetController : ControllerBase
    {
        IConfiguration _configuration;
        IFixedAssetRepository _fixedAssetRepository;
        IFixedAssetService _fixedAssetService;
        public FixedAssetController(IConfiguration configuration, IFixedAssetRepository fixedAssetRepository, IFixedAssetService fixedAssetService)
        {
            _configuration = configuration;
            _fixedAssetRepository = fixedAssetRepository;
            _fixedAssetService = fixedAssetService;
        }
        // GET: api/<FixedAssetController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var fixedAssets = _fixedAssetRepository.Get();
                return Ok(fixedAssets);

            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // GET api/<FixedAssetController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FixedAssetController>
        [HttpPost]
        public IActionResult Post([FromBody] FixedAsset fixedAsset)
        {
            try
            {
                var res = _fixedAssetService.InsertService(fixedAsset);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // PUT api/<FixedAssetController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FixedAssetController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        /// <summary>
        /// Xử lí lỗi Exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns> Thông tin lỗi Exception </returns>
        /// CreatedBy: LTTUAN (09/05/2022)
        private IActionResult HandleException(Exception ex)
        {
            var validateError = new ValidateError();
            validateError.DevMsg = ex.Message;
            validateError.UserMsg = "Có lỗi xảy ra, vui lòng liên hệ MISA để hỗ trợ";
            validateError.Data = "001";
            return StatusCode(500, validateError); //Lỗi từ server trả về 500

        }
    }
}
