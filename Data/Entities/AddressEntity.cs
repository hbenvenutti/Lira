namespace Lira.Data.Entities;

public class AddressEntity : BaseEntity
{
    # region ---- properties ---------------------------------------------------

    public required string Street { get; set; }
    public required string Number { get; set; }
    public required string Neighborhood { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string ZipCode { get; set; }
    public string? Complement { get; set; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public Guid PersonId { get; set; }
    public PersonEntity? Person { get; set; }

    # endregion
}
