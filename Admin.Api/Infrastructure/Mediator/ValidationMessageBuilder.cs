using Admin.Api.Infrastructure.Mediator;
using Admin.Api.Models;

public abstract class BaseValidator<TRequest,TResponse>: IValidator<TRequest,TResponse> where TResponse : new()
{
    public abstract Message[] GetMessages(TRequest request);
    public virtual Task<OperationResult<TResponse>> Validate(TRequest request)
    {
        var messages = GetMessages(request);
        if(messages.Any()){
            var result = new OperationResult<TResponse>
            {
                State = OperationState.Error,
                Messages = messages,
                Data = default(TResponse)
            };
            return Task.FromResult(result);
        }

        return Task.FromResult(OperationResult<TResponse>.Success(new TResponse()));
    }   
}

public class ValidationMessageBuilder{
    private List<Message> messages;
    public ValidationMessageBuilder(){
        messages = new List<Message>();
    }

    public ValidationMessageBuilder WithNullEmpty(string text,OperationState state,String message){
        if (string.IsNullOrWhiteSpace(text))
        {
            messages.Add(new Message { Text = message, State = state });
        }
        return this;
    }    

    public Message[] Build(){
        return messages.ToArray();
    }
}