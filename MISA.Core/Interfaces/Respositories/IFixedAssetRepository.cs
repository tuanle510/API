using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Respositories
{
    public interface IFixedAssetRepository: IBaseRepository<FixedAsset>
    {
        //FixedAsset GetById(Guid id);

        //string GetNewFixedAssetCode();

        /// <summary>
        /// Xử lí xóa nhiều
        /// </summary>
        /// <param name="fixedAssetIdList"> Danh sách id gửi lên từ body </param>
        /// <returns> Số bản ghi xóa thành công </returns>
        int DeleteMulti(Guid[] fixedAssetIdList);

        /// <summary>
        /// xử lí phân trang, filtter
        /// </summary>
        /// <param name="FixedAssetCategoryName"> Tên loại tài sản </param>
        /// <param name="DepartmentName"> Tên bộ phận sử dụng </param>
        /// <param name="FixedAssetFilter"> Input filter </param>
        /// <param name="pageIndex"> Số trang </param>
        /// <param name="pageSize"> Sô bản ghi trong 1 trang </param>
        /// <returns> Trả về danh sách đã filter và phân trang </returns>
        object Filter( string? FixedAssetCategoryName, string? DepartmentName,string FixedAssetFilter,  int pageIndex, int pageSize );


        /// <summary>
        /// Lấy tất cả bản ghi còn lại (trừ những bản ghi được gửi lên)
        /// </summary>
        /// <returns></returns>
        object GetRestFixedAssetList(Guid[] fixedAssetList, int pageIndex, int pageSize);

    }
}
