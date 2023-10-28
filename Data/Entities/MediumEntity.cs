namespace Lira.Data.Entities;

public class MediumEntity : BaseEntity
{
    # region ---- properties ---------------------------------------------------

    public required DateTime? FirstAmaci { get; set; }
    public required DateTime? LastAmaci { get; set; }

    # endregion

    # region ---- relationships ------------------------------------------------

    public required Guid PersonId { get; set; }
    public PersonEntity? Person { get; set; }

    # endregion
}
