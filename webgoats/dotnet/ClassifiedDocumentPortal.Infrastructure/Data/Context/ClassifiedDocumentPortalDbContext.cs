using ClassifiedDocumentPortal.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedDocumentPortal.Infrastructure.Data.Context
{
    public class ClassifiedDocumentPortalDbContext : IdentityDbContext<PortalUser>
    {
        public ClassifiedDocumentPortalDbContext(DbContextOptions<ClassifiedDocumentPortalDbContext> options) : base(options)
        {

        }

        public DbSet<Document> Documents { get; set; }
    }
}
