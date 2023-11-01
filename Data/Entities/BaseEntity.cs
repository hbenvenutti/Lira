using Lira.Data.Enums;

namespace Lira.Data.Entities;

public class BaseEntity
{
    public required Guid Id { get; set; }
    public required EntityStatus Status { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
