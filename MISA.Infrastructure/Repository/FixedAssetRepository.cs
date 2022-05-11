using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Respositories;
using MySqlConnector;

namespace MISA.Infrastructure.Repository
{
    public class FixedAssetRepository : IFixedAssetRepository
    {
        /// <summary>
        /// Xử lí kết nốt với database 
        /// </summary>
        IConfiguration _configuration;
        public FixedAssetRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool CheckFixedAssetExist(string fixedAssetCode)
        {
            throw new NotImplementedException();
        }

        public int Delete(Guid fixedAssetId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Lấy tất cả dữ liệu tài sản về 
        /// </summary>
        /// <returns></returns>
        public List<FixedAsset> Get()
        {
            // Khai báo thông tin database:
            var connectionString = _configuration.GetConnectionString("LTTUAN");

            // Khỏi tạo kết nối đến database --> sử dụng mySqlConnector
            var sqlConnection = new MySqlConnection(connectionString);

            // Thực hiện lấy dữ liệu --> Dapper
            var data = sqlConnection.Query<FixedAsset>("SELECT * FROM fixed_asset");

            //Trả về dữ liệu cho clien
            return data.ToList();
        } 

        public string GetNewFixedAssetCode()
        {
            // Kết nốt đến database

            // Lấy mã tài sản mới nhất về (tính theo thời gian)

            // Trả về mã nhân viên mới
            throw new NotImplementedException();
        }

        public List<FixedAsset> GetPaging(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public int Insert(FixedAsset fixedAsset)
        {
            //Tạo id mới
            fixedAsset.fixed_asset_id = Guid.NewGuid();

            // Khai báo thông tin database:
            var connectionString = _configuration.GetConnectionString("LTTUAN");

            // Khỏi tạo kết nối đến database --> sử dụng mySqlConnector
            var sqlConnection = new MySqlConnection(connectionString);

            // Thực hiện thêm mới dữ liệu
            var sqlQuery = $"INSERT INTO fixed_asset(fixed_asset_id,fixed_asset_code, fixed_asset_name, department_id,  department_code, department_name, fixed_asset_category_id, fixed_asset_category_code,fixed_asset_category_name, purchase_date, cost, quantity, depreciation_rate, tracked_year, life_time , production_year) VALUES (@fixed_asset_id,@fixed_asset_code, @fixed_asset_name, @department_id,  @department_code, @department_name, @fixed_asset_category_id, @fixed_asset_category_code,@fixed_asset_category_name, @purchase_date, @cost, @quantity, @depreciation_rate, @tracked_year, @life_time , @production_year)";
            //var sqlQuery = $"INSERT INTO fixed_asset(fixed_asset_id,fixed_asset_code, fixed_asset_name,   department_code, department_name, fixed_asset_category_code,fixed_asset_category_name, purchase_date, cost, quantity, depreciation_rate, tracked_year, life_time , production_year) VALUES (@fixed_asset_id,@fixed_asset_code, @fixed_asset_name, @department_code, @department_name, @fixed_asset_category_code,@fixed_asset_category_name, @purchase_date, @cost, @quantity, @depreciation_rate, @tracked_year, @life_time , @production_year)";
            var parameters = new DynamicParameters();
            parameters.Add("@fixed_asset_id", fixedAsset.fixed_asset_id);
            parameters.Add("@fixed_asset_code", fixedAsset.fixed_asset_code);
            parameters.Add("@fixed_asset_name", fixedAsset.fixed_asset_name);
            parameters.Add("@department_id", fixedAsset.department_id);
            parameters.Add("@department_code", fixedAsset.department_code);
            parameters.Add("@department_name", fixedAsset.department_name);
            parameters.Add("@fixed_asset_category_id", fixedAsset.fixed_asset_category_id);
            parameters.Add("@fixed_asset_category_code", fixedAsset.fixed_asset_category_code);
            parameters.Add("@fixed_asset_category_name", fixedAsset.fixed_asset_category_name);
            parameters.Add("@purchase_date", fixedAsset.purchase_date);
            parameters.Add("@cost", fixedAsset.cost);
            parameters.Add("@quantity", fixedAsset.quantity);
            parameters.Add("@depreciation_rate", fixedAsset.depreciation_rate);
            parameters.Add("@tracked_year", fixedAsset.tracked_year);
            parameters.Add("@life_time", fixedAsset.life_time);
            parameters.Add("@production_year", fixedAsset.production_year);
         
            var res = sqlConnection.Execute(sqlQuery, param: parameters);
            return res;
        }

        public int Update(FixedAsset fixedAsset)
        {
            throw new NotImplementedException();
        }
    }
}
