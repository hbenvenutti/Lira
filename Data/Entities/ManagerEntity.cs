namespace Lira.Data.Entities;

public class ManagerEntity : BaseEntity
{
    # region ---- properties ---------------------------------------------------

    public required string Username { get; set; }
    public required string Password { get; set; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public required Guid PersonId { get; set; }
    public PersonEntity? Person { get; set; }

    # endregion
}
