namespace Aliasys.Domain.Entities.BaseEntities
{
    public abstract class BaseEntity<TKey>
    {
        public abstract TKey Id { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime UpdatedDateTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDateTime { get; set; }
    }
}
