namespace MISA.Core.Entities
{
    public class Department
    {
        public Guid department_id { get; set; }

        public string department_code { get; set; }
        public string department_name { get; set; }
        public string description { get; set; }
        public string organization_id { get; set; }
    }
}
