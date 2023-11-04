using Lira.Domain.Enums;

namespace Lira.Data.Entities;

public class EmailEntity : BaseEntity
{
    # region ---- properties --------------------------------------------------

    public required string Address { get; set; }
    public required EmailType Type { get; set; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public required Guid PersonId { get; set; }
    public PersonEntity? Person { get; set; }

    # endregion
}
