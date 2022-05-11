﻿namespace MISA.Core.Entities
{
    public class FixedAsset
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid fixed_asset_id { get; set; }

        public string fixed_asset_code { get; set; }

        public string fixed_asset_name { get; set; }

        //public Guid organization_id { get; set; }

        //public string organization_code { get; set; }

        //public string organization_name { get; set; }

        public Guid department_id { get; set; }

        public string department_code { get; set; }

        public string department_name { get; set; }

        public Guid fixed_asset_category_id { get; set; }

        public string fixed_asset_category_code { get; set; }

        public string fixed_asset_category_name { get; set; }

        public DateTime purchase_date { get; set; }

        public decimal cost { get; set; }

        public int quantity { get; set; }

        public float depreciation_rate { get; set; }

        public int tracked_year { get; set; }

        public int life_time { get; set; }

        public int production_year { get; set; }
    }
}