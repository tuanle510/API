using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Respositories;
using MISA.Core.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.Web03.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LicensesController : MISABaseController<License>
    {
        public LicensesController(IBaseService<License> baseService, IBaseRepository<License> baseRepository) : base(baseService, baseRepository)
        {
        }
    }
}
