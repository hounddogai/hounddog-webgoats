using ClassifiedDocumentPortal.Domain.Enums;

namespace ClassifiedDocumentPortal.Domain.Entities
{
    public class Document
    {
        public string DocumentId { get; set; }

        public string Name { get; set; }

        public ClassificationType Classification { get; set; }

        public CategoryType Category { get; set; }

        public string DatePublished { get; set; }
    }
}
