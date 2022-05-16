using MISA.Core.Interfaces.Respositories;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class BaseService<T> : IBaseService<T>
    {
        IBaseRepository<T> _baseRepository;
        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public int InsertService(T entity)
        {
            //Xứ lí nghiêp vụ đặc thù

            //Thực hiện thêm mới
            var res = _baseRepository.Insert(entity);
            return res;
        }
    }
}
