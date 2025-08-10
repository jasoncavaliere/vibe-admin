namespace Admin.Api.Models
{
    public class Message
    {
        public required string Text { get; set; }
        public required OperationState State { get; set; }
    }
}


