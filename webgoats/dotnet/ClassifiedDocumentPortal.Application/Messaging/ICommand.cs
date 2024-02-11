using MediatR;

namespace ClassifiedDocumentPortal.Application.Messaging
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {

    }
}
