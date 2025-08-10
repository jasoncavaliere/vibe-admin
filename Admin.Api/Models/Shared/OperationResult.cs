namespace Admin.Api.Models
{
    public class OperationResult<T>
    {
        public Result State { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
    }
}


