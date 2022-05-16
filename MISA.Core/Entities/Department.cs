namespace MISA.Core.Entities
{
    public class Department
    {
        public Guid DepartmentId { get; set; }

        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string description { get; set; }
        public string organization_id { get; set; }
    }
}
