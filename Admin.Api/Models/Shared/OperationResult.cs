namespace Admin.Api.Models
{
    public class OperationResult<T>
    {
        public Result State { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }

        public static OperationResult<T> Success(T data)
        {
            return new OperationResult<T> { State = Result.Success, Data = data };
        }
    }
}


