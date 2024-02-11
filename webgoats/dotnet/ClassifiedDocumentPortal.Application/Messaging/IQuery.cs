using MediatR;

namespace ClassifiedDocumentPortal.Application.Messaging
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {

    }
}
