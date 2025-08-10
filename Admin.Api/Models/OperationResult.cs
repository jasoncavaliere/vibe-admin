namespace Admin.Api.Models
{
    public class OperationResult<T>
    {
        public OperationState State { get; set; }
        public T? Data { get; set; }
        public Message[] Messages { get; set; }

        public static OperationResult<T> Success(T data)
        {
            return new OperationResult<T> { State = OperationState.Success, Data = data };
        }
    }
}


