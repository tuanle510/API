using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Respositories;
using MISA.Core.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.Web03.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public abstract class MISABaseController<T> : ControllerBase
    {
        #region Fiels
        IBaseService<T> _baseService;
        IBaseRepository<T> _baseRepository;
        #endregion

        #region Contructtor
        public MISABaseController(IBaseService<T> baseService, IBaseRepository<T> baseRepository)
        {
            _baseService = baseService;
            _baseRepository = baseRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Xử lí lấy tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        // GET: api/<MISABaseController>
        [HttpGet]
        public virtual IActionResult Get()
        {
            try
            {
                var entitties = _baseRepository.Get();
                return Ok(entitties);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xử lí lấy dữ liệu về theo Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        // GET api/<MISABaseController>/5
        [HttpGet("{entityId}")]
        public IActionResult Get(Guid entityId)
        {
            try
            {
                var entitties = _baseRepository.GetById(entityId);
                return Ok(entitties);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xử lí thêm mới dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        // POST api/<MISABaseController>
        [HttpPost]
        public IActionResult Post([FromBody] T entity)
        {
            try
            {
                var res = _baseService.InsertService(entity);
                return StatusCode(201, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xử lí sửa đối tượng 
        /// </summary>
        /// <param name="entityId"> Id của đôi tượng </param>
        /// <param name="entity"> Dữ liệu mới </param>
        /// <returns></returns>
        // PUT api/<MISABaseController>/5
        [HttpPut("{entityId}")]
        public virtual IActionResult Put(Guid entityId, [FromBody] T entity)
        {
            try
            {
                var res = _baseService.UpdateService(entityId, entity);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xử lí xóa đối tượng theo Id
        /// </summary>
        /// <param name="entityId"> Id của đối tượng </param>
        /// <returns></returns>
        [HttpDelete("{entityId}")]
        public virtual IActionResult Delete(Guid entityId)
        {
            try
            {
                var res = _baseRepository.Delete(entityId);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        #endregion 
        /// <summary>
        /// Xử lí lỗi Exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns> Thông tin lỗi Exception </returns>
        /// CreatedBy: LTTUAN (09/05/2022)
        protected IActionResult HandleException(Exception ex)
        {
            var res = new
            {
                devMsg = ex.Message,
                userMsg = "Có lỗi xấy ra vui lòng liên hệ MISA để được hỗ trợ",
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
