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
    public class FixedAssetsController : MISABaseController<FixedAsset>
    {
        IFixedAssetRepository _fixedAssetRepository;
        IFixedAssetService _fixedAssetService;

        public FixedAssetsController(IFixedAssetRepository fixedAssetRepository, IFixedAssetService fixedAssetService) : base(fixedAssetService, fixedAssetRepository)
        {
            _fixedAssetRepository = fixedAssetRepository;
            _fixedAssetService = fixedAssetService;
        }

        /// <summary>
        /// Xử lí tạo mã tài sản mới
        /// </summary>
        /// <returns></returns>
        //[HttpGet("NewFixedAssetCode")]
        //public IActionResult GetNewAssetCode()
        //{
        //    try
        //    {
        //        var newFixedAssetCode = _fixedAssetRepository.GetNewFixedAssetCode();
        //        return Ok(newFixedAssetCode);
        //    }
        //    catch (Exception ex)
        //    {

        //        return HandleException(ex);
        //    }
        //}

        /// <summary>
        /// Xử lí xóa nhiều
        /// </summary>
        /// <param name="fixedAssetIdList"></param>
        /// <returns></returns>
        [HttpDelete("DeleteMulti")]
        public IActionResult DeleteMulti([FromBody] Guid[] fixedAssetIdList)

        {
            try
            {
                var res = _fixedAssetRepository.DeleteMulti(fixedAssetIdList);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xử lí phân trang và filter
        /// </summary>
        /// <param name="FixedAssetCategoryName"></param>
        /// <param name="DepartmentName"></param>
        /// <param name="FixedAssetFilter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("Filter")]
        public IActionResult Filter(string? FixedAssetCategoryName, string? DepartmentName, string? FixedAssetFilter = "", int pageIndex = 1, int pageSize = 20)
        {
            try
            {
                // List data đã phân trang:
                var FilterList = _fixedAssetRepository.Filter( FixedAssetCategoryName, DepartmentName, FixedAssetFilter, pageIndex, pageSize);
               
                return StatusCode(200, FilterList);
            }
            catch (Exception ex)
            {
               return HandleException(ex);
            }
        }

        [HttpGet("GetRestAsetList")]
        public IActionResult GetRestAsetList([FromQuery] Guid[] fixedAssetList, int pageIndex = 1, int pageSize = 20)
        {
            try
            {
                var res = _fixedAssetRepository.GetRestFixedAssetList(fixedAssetList, pageIndex, pageSize);
                return StatusCode(200, res);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
