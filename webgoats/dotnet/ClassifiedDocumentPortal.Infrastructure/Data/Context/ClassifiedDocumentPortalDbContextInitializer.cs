using ClassifiedDocumentPortal.Domain.Entities;
using ClassifiedDocumentPortal.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ClassifiedDocumentPortal.Infrastructure.Data.Context
{
    public static class ClassifiedDocumentPortalDbContextInitializer
    {
        public static void Initialize(ClassifiedDocumentPortalDbContext context, IServiceProvider serviceProvider)
        {
            context.Database.EnsureCreated();

            SeedRoles(context);
            SeedUsers(context, serviceProvider);
            SeedDocuments(context);
        }

        private static void SeedRoles(ClassifiedDocumentPortalDbContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new IdentityRole
                    {
                        Name = "TopSecret",
                        NormalizedName = "TOPSECRET"
                    },
                    new IdentityRole
                    {
                        Name = "Confidential",
                        NormalizedName = "CONFIDENTIAL"
                    },
                    new IdentityRole
                    {
                        Name = "Secret",
                        NormalizedName = "SECRET"
                    });

                context.SaveChanges();
            }
        }

        private static void SeedUsers(ClassifiedDocumentPortalDbContext context, IServiceProvider serviceProvider)
        {
            if (!context.Users.Any())
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<PortalUser>>();

                SeedUserWithRole(
                    userManager,
                    "matthewparker@example.com",
                    "matthewparker",
                    "matthewparker",
                    "Matthew Parker",
                    "4364 Hewes Avenue",
                    true,
                    ClassificationType.TopSecret,
                    "443-588-3886",
                    "213-49-1111",
                    "TopSecret");

                SeedUserWithRole(
                    userManager,
                    "johndoe@example.com",
                    "johndoe",
                    "johndoe",
                    "John Doe",
                    "2672 James Martin Circle",
                    false,
                    ClassificationType.Secret,
                    "443-588-5555",
                    "213-49-2222",
                    "Secret");

                SeedUserWithRole(
                    userManager,
                    "janedoe@example.com",
                    "janedoe",
                    "janedoe",
                    "Jane Doe",
                    "4179 Arbutus Drive",
                    true,
                    ClassificationType.Confidential,
                    "443-588-9999",
                    "213-49-3333",
                    "Confidential");
            }
        }

        private static void SeedUserWithRole(
            UserManager<PortalUser> userManager,
            string email,
            string username,
            string password,
            string name,
            string address,
            bool backgroundCheckStatusCompleted,
            ClassificationType securityClearance,
            string departmentOfDefenseContractorNumber,
            string usFederalContractorRegistrationNumber,
            string roleName)
        {
            var user = new PortalUser
            {
                Email = email,
                UserName = username,
                Name = name,
                Address = address,
                BackgroundCheckStatusCompleted = backgroundCheckStatusCompleted,
                SecurityClearance = securityClearance,
                DepartmentOfDefenseContractorNumber = departmentOfDefenseContractorNumber,
                USFederalContractorRegistrationNumber = usFederalContractorRegistrationNumber,
            };

            var result = userManager.CreateAsync(user, password).Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, roleName).Wait();
            }
        }

        private static void SeedDocuments(ClassifiedDocumentPortalDbContext context)
        {
            if (!context.Documents.Any())
            {
                context.Documents.AddRange(
                    new Document
                    {
                        Name = "Military Plans Report",
                        Classification = ClassificationType.TopSecret,
                        Category = CategoryType.A14,
                        DatePublished = "15 Jan 2022",
                        DocumentId = "39842-230"
                    },
                    new Document
                    {
                        Name = "Foreign Government Report",
                        Classification = ClassificationType.TopSecret,
                        Category = CategoryType.B14,
                        DatePublished = "22 Feb 2022",
                        DocumentId = "39842-234"
                    },
                    new Document
                    {
                        Name = "Intelligence Activities Report",
                        Classification = ClassificationType.TopSecret,
                        Category = CategoryType.C14,
                        DatePublished = "11 Jan 2022",
                        DocumentId = "39842-229"
                    },
                    new Document
                    {
                        Name = "Foreign Relations Report",
                        Classification = ClassificationType.Confidential,
                        Category = CategoryType.D14,
                        DatePublished = "25 Feb 2022",
                        DocumentId = "39842-233"
                    },
                    new Document
                    {
                        Name = "Scientific Research Report",
                        Classification = ClassificationType.Secret,
                        Category = CategoryType.E14,
                        DatePublished = "20 Jan 2022",
                        DocumentId = "39842-231"
                    },
                    new Document
                    {
                        Name = "Technology Research Report",
                        Classification = ClassificationType.Secret,
                        Category = CategoryType.E14,
                        DatePublished = "22 Feb 2022",
                        DocumentId = "39842-232"
                    },
                    new Document
                    {
                        Name = "Nuclear Materials Report",
                        Classification = ClassificationType.Confidential,
                        Category = CategoryType.F14,
                        DatePublished = "28 Feb 2022",
                        DocumentId = "39842-235"
                    },
                    new Document
                    {
                        Name = "National Security Report",
                        Classification = ClassificationType.TopSecret,
                        Category = CategoryType.G14,
                        DatePublished = "26 Feb 2022",
                        DocumentId = "39842-236"
                    });

                context.SaveChanges();
            }
        }
    }
}
