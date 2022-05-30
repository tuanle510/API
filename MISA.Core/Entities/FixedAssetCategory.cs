namespace MISA.Core.Entities
{
    public class FixedAssetCategory
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid FixedAssetCategoryId { get; set; }
        public string FixedAssetCategoryName { get; set; }
        public string FixedAssetCategoryCode { get; set; }
        public string DepreciationRate { get; set; }
        public int LifeTime { get; set; }
        public string? Description { get; set; }
        //public string organization_id { get; set; }
    }
}
