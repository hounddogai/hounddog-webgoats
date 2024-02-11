using ClassifiedDocumentPortal.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace ClassifiedDocumentPortal.Domain.Entities
{
    public class PortalUser : IdentityUser
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public bool BackgroundCheckStatusCompleted { get; set; }

        public ClassificationType SecurityClearance { get; set; }

        public string DepartmentOfDefenseContractorNumber { get; set; }

        public string USFederalContractorRegistrationNumber { get; set; }

        public string ProfilePicture { get; set; }
    }
}
