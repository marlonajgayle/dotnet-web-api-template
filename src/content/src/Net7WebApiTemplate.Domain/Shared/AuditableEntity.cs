namespace Net7WebApiTemplate.Domain.Shared
{
    public class AuditableEntity
    {
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; } = string.Empty;
        public DateTime? LastModifiedOn { get; set; }
    }
}