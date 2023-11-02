namespace Lira.Data.Entities;

public class OrixaEntity : BaseEntity
{
    # region ---- properties ---------------------------------------------------

    public required string Name { get; set; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public IEnumerable<PersonOrixaEntity>? PersonOrixas { get; set; }

    # endregion
}
