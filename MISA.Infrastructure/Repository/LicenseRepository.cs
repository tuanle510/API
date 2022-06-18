using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Respositories;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repository
{
    public class LicenseRepository : BaseRepository<License>, ILicenseRepository
    {

        public LicenseRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public object FilterLicenseDetail(string? searchLicense, int pageIndex, int pageSize)
        {
            // Nếu trả về null thì gắn vào giá trị "":
            if (string.IsNullOrEmpty(searchLicense))
            {
                searchLicense = "";
            }
            // Tính giá trị tổng offset:
            var totalOffset = (pageIndex - 1) * pageSize;
            // Khởi tạo câu truy vấn: 
            var sqlQuery = "SELECT * FROM License WHERE (LicenseCode LIKE CONCAT('%',@searchLicense,'%') OR Description LIKE CONCAT('%',@searchLicense,'%')) ";
            var parameters = new DynamicParameters();
            parameters.Add("@searchLicense", searchLicense);
            var count = _sqlConnection.Query<License>(sqlQuery, param: parameters);

            // Phân trang: 
            sqlQuery += "ORDER BY CreatedDate DESC LIMIT @pageSize OFFSET @totalOffset";
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@totalOffset", totalOffset);
            var list = _sqlConnection.Query<License>(sqlQuery, param: parameters);

            // Trả về đối tượng:
            var res = new
            {
                List = (List<License>)list.ToList(),
                Count = (int)count.Count()
            };

            // Trả về dữ liệu dạng List:
            return res;
        }
    }
}
