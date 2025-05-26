namespace Product.Models.DTO
{
    public class Response
    {
        public bool IsSuccess { get; set; } = true;

        public int StatusCode { get; set; } = 200;

        public object? Message { get; set; } = null;

        public object? Res { get; set; } = null;
    }
}
