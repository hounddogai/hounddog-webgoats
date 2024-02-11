using ClassifiedDocumentPortal.Application.Messaging;
using ClassifiedDocumentPortal.Domain.Enums;

namespace ClassifiedDocumentPortal.Application.CQRS.Queries.GetDocumentsBySecurityClearance
{
    public sealed record GetDocumentsBySecurityClearanceQuery(ClassificationType SecurityClearance) : IQuery<GetDocumentsBySecurityClearanceResponse>;
}
