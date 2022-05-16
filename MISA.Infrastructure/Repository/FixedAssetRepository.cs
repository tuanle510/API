using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Respositories;
using MySqlConnector;
using System.Text.RegularExpressions;

namespace MISA.Infrastructure.Repository
{
    public class FixedAssetRepository : BaseRepository<FixedAsset>, IFixedAssetRepository
    {

        public FixedAssetRepository(IConfiguration configuration) : base(configuration)
        {

        }

        /// <summary>
        /// Kiểm tra dữ lệu có đã tồn tại trong hệ thống hay chưa
        /// </summary>
        /// <param name="fixedAssetCode"></param>
        /// <returns></returns>
        public bool CheckFixedAssetExist(string fixedAssetCode, Guid id, int mode)
        {
            var sqlQueryCheckDuplicateCode = "";
            if (mode == 1)
            {
                sqlQueryCheckDuplicateCode = $"SELECT FixedAssetCode FROM FixedAsset WHERE fixed_asset_code=@fixedAssetCode";
            }

            if (mode == 2)
            {
                sqlQueryCheckDuplicateCode = $"SELECT FixedAssetCode FROM FixedAsset WHERE FixedAssetCode=@fixedAssetCode AND FixedAssetId <> @FixedAssetId";
            }
            // Kiểm tra xem mã đã tồn tại hay chưa?
            var parametersDup = new DynamicParameters();
            parametersDup.Add("@fixedAssetCode", fixedAssetCode);
            parametersDup.Add("@fixedAssetId", id);
            var fixedAssetCodeDuplicate = _sqlConnection.QueryFirstOrDefault<string>(sqlQueryCheckDuplicateCode, param: parametersDup);

            if (fixedAssetCodeDuplicate != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Delete(Guid id)
        {
            //Thực hiện xóa dữ liệu
            var sqlQuery = $"DELETE FROM fixed_asset WHERE fixed_asset_id = @fixedAssetId";
            var parameters = new DynamicParameters();
            parameters.Add("@fixedAssetId", id);
            var res = _sqlConnection.Execute(sqlQuery, param: parameters);
            return res;
        }

        public FixedAsset GetById(Guid id)
        {
            // Thực hiện lấy dữ liệu
            var sqlQuery = $"SELECT * FROM fixed_asset WHERE fixed_asset_id = @fixed_asset_id";
            var parameters = new DynamicParameters();
            parameters.Add("@fixed_asset_id", id);
            var asset = _sqlConnection.QueryFirstOrDefault<FixedAsset>(sqlQuery, param: parameters);
            //Trả về dữ liệu cho clien
            return asset;
        }

        /// <summary>
        /// Xử lí tạo ra mã mới
        /// </summary>
        /// <returns></returns>
        public string GetNewFixedAssetCode()
        {

            var sqlQuery = "SELECT FixedAssetCode FROM FixedAsset ORDER BY created_date DESC";

            var lastCode = _sqlConnection.QueryFirstOrDefault<String>(sqlQuery);
            //var strCode = lastCode.Substring(0, 2);
            //var numberCode = int.Parse(lastCode.Substring(2));
            //Regex.Match(subjectString, @"\d+").Value;
            var newCode = lastCode.Substring(0, 2) + (int.Parse(lastCode.Substring(2)) + 1);
            //var newCode = Regex.Match(lastCode, @"\d+").Value;


            return newCode;
        }

        public List<FixedAsset> GetPaging(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public int Insert(FixedAsset fixedAsset)
        {
            //Tạo id mới
            fixedAsset.FixedAssetId = Guid.NewGuid();

            // Thực hiện thêm mới dữ liệu
            var sqlQuery = $"INSERT INTO FixedAsset(FixedAssetId,FixedAssetCode, FixedAssetName, department_id,  department_code, department_name, fixed_asset_category_id, fixed_asset_category_code,fixed_asset_category_name, purchase_date, cost, quantity, depreciation_rate, tracked_year, life_time , production_year) VALUES (@fixed_asset_id,@fixed_asset_code, @fixed_asset_name, @department_id,  @department_code, @department_name, @fixed_asset_category_id, @fixed_asset_category_code,@fixed_asset_category_name, @purchase_date, @cost, @quantity, @depreciation_rate, @tracked_year, @life_time , @production_year)";
            var parameters = new DynamicParameters();
            parameters.Add("@fixed_asset_id", fixedAsset.FixedAssetId);
            parameters.Add("@fixed_asset_code", fixedAsset.FixedAssetCode);
            parameters.Add("@fixed_asset_name", fixedAsset.FixedAssetName);
            parameters.Add("@department_id", fixedAsset.DepartmentId);
            parameters.Add("@department_code", fixedAsset.DepartmentCode);
            parameters.Add("@department_name", fixedAsset.DepartmentName);
            parameters.Add("@fixed_asset_category_id", fixedAsset.FixedAssetCategoryId);
            parameters.Add("@fixed_asset_category_code", fixedAsset.FixedAssetCategoryCode);
            parameters.Add("@fixed_asset_category_name", fixedAsset.FixedAssetCategoryName);
            parameters.Add("@purchase_date", fixedAsset.PurchaseDate);
            parameters.Add("@cost", fixedAsset.Cost);
            parameters.Add("@quantity", fixedAsset.Quantity);
            parameters.Add("@depreciation_rate", fixedAsset.DepreciationRate);
            parameters.Add("@tracked_year", fixedAsset.TrackedYear);
            parameters.Add("@life_time", fixedAsset.Lifetime);
            parameters.Add("@production_year", fixedAsset.ProductionYear);

            var res = _sqlConnection.Execute(sqlQuery, param: parameters);
            return res;
        }

        public int Update(Guid id, FixedAsset fixedAsset)
        {
            // Thực hiện thêm mới dữ liệu
            var sqlQuery = $"UPDATE FixedAsset SET FixedAssetCode=@fixed_asset_code, FixedAssetName=@fixed_asset_name, department_id=@department_id,  department_code = @department_code, department_name = @department_name, fixed_asset_category_id=@fixed_asset_category_id, fixed_asset_category_code=@fixed_asset_category_code,fixed_asset_category_name=@fixed_asset_category_name, purchase_date=@purchase_date, cost=@cost, quantity=@quantity, depreciation_rate=@depreciation_rate, tracked_year=@tracked_year, life_time=@life_time , production_year=@production_year  WHERE fixed_asset_id = @fixed_asset_id";
            var parameters = new DynamicParameters();
            parameters.Add("@fixed_asset_id", id);
            parameters.Add("@fixed_asset_code", fixedAsset.FixedAssetCode);
            parameters.Add("@fixed_asset_name", fixedAsset.FixedAssetName);
            parameters.Add("@department_id", fixedAsset.DepartmentId);
            parameters.Add("@department_code", fixedAsset.DepartmentCode);
            parameters.Add("@department_name", fixedAsset.DepartmentName);
            parameters.Add("@fixed_asset_category_id", fixedAsset.FixedAssetCategoryId);
            parameters.Add("@fixed_asset_category_code", fixedAsset.FixedAssetCategoryCode);
            parameters.Add("@fixed_asset_category_name", fixedAsset.FixedAssetCategoryName);
            parameters.Add("@purchase_date", fixedAsset.PurchaseDate);
            parameters.Add("@cost", fixedAsset.Cost);
            parameters.Add("@quantity", fixedAsset.Quantity);
            parameters.Add("@depreciation_rate", fixedAsset.DepreciationRate);
            parameters.Add("@tracked_year", fixedAsset.TrackedYear);
            parameters.Add("@life_time", fixedAsset.Lifetime);
            parameters.Add("@production_year", fixedAsset.ProductionYear);

            var res = _sqlConnection.Execute(sqlQuery, param: parameters);
            return res;
        }
    }
}
