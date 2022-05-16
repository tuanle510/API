using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Respositories
{
    public interface IBaseRepository<T>
    {
        List<T> Get();

        int Insert(T entity);

        int Update(Guid entityId, T entity);

        int Delete(Guid entityId);
    }
}
