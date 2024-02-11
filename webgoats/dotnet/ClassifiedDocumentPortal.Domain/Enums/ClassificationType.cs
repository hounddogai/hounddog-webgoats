using System.ComponentModel;

namespace ClassifiedDocumentPortal.Domain.Enums
{
    public enum ClassificationType
    {
        [Description("Top Secret")] // level 3
        TopSecret,

        [Description("Secret")] // level 2
        Secret,

        [Description("Confidential")] // level 1
        Confidential
    }
}
