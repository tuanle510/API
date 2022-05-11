namespace MISA.Core.Entities
{
    public class FixedAssetCategory
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid fixed_asset_category_id { get; set; }

        public string fixed_asset_category_name { get; set; }

        public string fixed_asset_category_code { get; set; }

        //public string organization_id { get; set; }

        public string depreciation_rate { get; set; }

        public int life_time { get; set; }
        
        public string? description { get; set; }
    }
}
