namespace Lira.Data.Entities;

public class PhoneEntity : BaseEntity
{
    # region ---- properties ---------------------------------------------------

    public required string Number { get; set; }
    public required string Ddd { get; set; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public required Guid PersonId { get; set; }
    public PersonEntity? Person { get; set; }

    # endregion
}
