namespace MISA.Core.Entities
{
    public class FixedAssetCategory
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid FixedAssetCategoryId { get; set; }

        public string FixedAssetCategoryIdName { get; set; }

        public string FixedAssetCategoryCode { get; set; }

        //public string organization_id { get; set; }

        public string depreciation_rate { get; set; }

        public int life_time { get; set; }
        
        public string? description { get; set; }
    }
}
