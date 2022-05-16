using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Exceptions;
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
            
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns></returns>
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

        // GET api/<FixedAssetController>/id
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var fixedAsset = _fixedAssetRepository.GetById(id);
                return Ok(fixedAsset);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("NewFixedAssetCode")]
        public IActionResult GetNewAssetCode()
        {
            try
            {
                var newFixedAssetCode = _fixedAssetRepository.GetNewFixedAssetCode();
                return Ok(newFixedAssetCode);
            }
            catch (Exception)
            {

                throw;
            }
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
        public IActionResult Put(Guid id, [FromBody] FixedAsset fixedAsset)
        {
            try
            {
                var res = _fixedAssetService.UpadteService(id, fixedAsset);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // DELETE api/<FixedAssetController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var res = _fixedAssetRepository.Delete(id);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xử lí lỗi Exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns> Thông tin lỗi Exception </returns>
        /// CreatedBy: LTTUAN (09/05/2022)
        private IActionResult HandleException(Exception ex)
        {
            var res = new
            {
                devMsg = ex.Message,
                userMsg = "Có lỗi xấy ra vui lòng liên hệ MISA để được hỗ trợ 2",
                errorCode = "001",
                data = ex.Data
            };
            if (ex is MISAValidateException)
                return StatusCode(400, res); //Lỗi từ server trả về 500
            else
                return StatusCode(500, res);
        }
    }
}
