using ClassifiedDocumentPortal.Domain.Entities;
using ClassifiedDocumentPortal.Domain.Enums;
using ClassifiedDocumentPortal.Domain.Interfaces.Repositories;
using ClassifiedDocumentPortal.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedDocumentPortal.Infrastructure.Data.Repositories
{
    internal sealed class DocumentRepository : IDocumentRepository
    {
        private readonly ClassifiedDocumentPortalDbContext _context;

        public DocumentRepository(ClassifiedDocumentPortalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Document>> GetDocumentsBySecurityClearanceAsync(ClassificationType securityClearance, CancellationToken cancellationToken = default)
        {
            var documents = _context.Set<Document>();

            return securityClearance switch
            {
                ClassificationType.TopSecret => await documents.Where(x =>
                    x.Classification == ClassificationType.TopSecret ||
                    x.Classification == ClassificationType.Secret ||
                    x.Classification == ClassificationType.Confidential)
                .ToListAsync(),

                ClassificationType.Secret => await documents.Where(x =>
                    x.Classification == ClassificationType.Secret ||
                    x.Classification == ClassificationType.Confidential)
                .ToListAsync(),

                ClassificationType.Confidential => await documents.Where(x =>
                    x.Classification == ClassificationType.Confidential)
                .ToListAsync(),

                _ => await documents.Where(x => x.Classification == ClassificationType.Confidential)
                .ToListAsync()
            };
        }
    }
}
