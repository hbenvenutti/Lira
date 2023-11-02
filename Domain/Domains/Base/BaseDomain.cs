using Lira.Domain.Enums;

namespace Lira.Domain.Domains.Base;

public class BaseDomain
{
    # region ---- properties ---------------------------------------------------
    
    public Guid Id { get; init; }
    public DomainStatus Status { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public DateTime? DeletedAt { get; init; }
    
    # endregion
    
    # region ---- constructor --------------------------------------------------

    protected BaseDomain(
        Guid id,
        DateTime createdAt, 
        DomainStatus status = DomainStatus.Active, 
        DateTime? updatedAt = null, 
        DateTime? deletedAt = null
    )
    {
        Id = id;
        Status = status;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        DeletedAt = deletedAt;
    }
    
    # endregion
}
