using Lira.Domain.Religion.Enums;

namespace Lira.Data.Entities;

public class PersonOrixaEntity : BaseEntity
{
    public required OrixaType Type { get; set; }

    # region ---- relationship -------------------------------------------------

    public required Guid PersonId { get; set; }
    public PersonEntity? Person { get; set; }

    public required Guid OrixaId { get; set; }
    public OrixaEntity? Orixa { get; set; }

    # endregion
}
