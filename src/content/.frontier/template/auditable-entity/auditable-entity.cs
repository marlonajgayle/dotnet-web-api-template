using __PROJECT_NAME__.Domain.Shared;

namespace __PROJECT_NAME__.Domain.Entities
{
    public class __ENTITY_NAME__ : AuditableEntity
    {
        public int Id { get; set; }
        public string Property1 { get; set; } = String.Empty;
        public string Property2 { get; set; } = String.Empty;
        public string Property3 { get; set; } = String.Empty;
    }
}