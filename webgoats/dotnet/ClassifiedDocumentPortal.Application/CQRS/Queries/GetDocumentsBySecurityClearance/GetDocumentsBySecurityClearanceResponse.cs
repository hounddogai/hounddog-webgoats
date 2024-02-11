using ClassifiedDocumentPortal.Domain.Entities;

namespace ClassifiedDocumentPortal.Application.CQRS.Queries.GetDocumentsBySecurityClearance
{
    public sealed record GetDocumentsBySecurityClearanceResponse(List<Document> Documents);
}
