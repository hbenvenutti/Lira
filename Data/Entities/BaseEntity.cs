using Lira.Data.Enums;

namespace Lira.Data.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public EntityStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}
