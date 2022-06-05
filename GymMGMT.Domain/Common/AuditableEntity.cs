namespace GymMGMT.Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTimeOffset CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTimeOffset? LastModifiedAt { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}