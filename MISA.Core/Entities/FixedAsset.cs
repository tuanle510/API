using MISA.Core.MISAAttribute;

namespace MISA.Core.Entities
{
    public class FixedAsset: BaseEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        public Guid FixedAssetId { get; set; }

        public string FixedAssetCode { get; set; }

        public string FixedAssetName { get; set; }

        public Guid? DepartmentId { get; set; }

        public string DepartmentCode { get; set; }

        public string DepartmentName { get; set; }

        public Guid? FixedAssetCategoryId { get; set; }

        public string FixedAssetCategoryCode { get; set; }

        public string FixedAssetCategoryName { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public decimal Cost { get; set; }

        public int Quantity { get; set; }

        public float DepreciationRate { get; set; }

        public int? TrackedYear { get; set; }

        public int Lifetime { get; set; }

        public int? ProductionYear { get; set; }

        //public Guid organization_id { get; set; }

        //public string organization_code { get; set; }

        //public string organization_name { get; set; }

    }
}
