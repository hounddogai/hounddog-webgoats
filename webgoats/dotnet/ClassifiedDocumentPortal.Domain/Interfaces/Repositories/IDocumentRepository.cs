using ClassifiedDocumentPortal.Domain.Entities;
using ClassifiedDocumentPortal.Domain.Enums;

namespace ClassifiedDocumentPortal.Domain.Interfaces.Repositories
{
    public interface IDocumentRepository
    {
        Task<List<Document>> GetDocumentsBySecurityClearanceAsync(ClassificationType securityClearance, CancellationToken cancellationToken = default);
    }
}
