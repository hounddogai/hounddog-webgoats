using ClassifiedDocumentPortal.Application.Messaging;
using ClassifiedDocumentPortal.Domain.Interfaces.Repositories;

namespace ClassifiedDocumentPortal.Application.CQRS.Queries.GetDocumentsBySecurityClearance
{
    internal sealed class GetDocumentsBySecurityClearanceQueryHandler : IQueryHandler<GetDocumentsBySecurityClearanceQuery, GetDocumentsBySecurityClearanceResponse>
    {
        private readonly IDocumentRepository _documentRepository;

        public GetDocumentsBySecurityClearanceQueryHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<GetDocumentsBySecurityClearanceResponse> Handle(GetDocumentsBySecurityClearanceQuery request, CancellationToken cancellationToken = default)
        {
            var documents = await _documentRepository.GetDocumentsBySecurityClearanceAsync(request.SecurityClearance, cancellationToken);

            return new GetDocumentsBySecurityClearanceResponse(documents);
        }
    }
}
