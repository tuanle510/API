namespace MISA.Core.Entities
{
    public class ValidateError
    {
        // note: ? có thể để rỗng
        public string? DevMsg { get; set; }
        public string? UserMsg { get; set; }

        public object? Data { get; set; }

        public string? ErrorCode { get; set; }

    }
}
