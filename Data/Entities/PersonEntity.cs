namespace Lira.Data.Entities;

public class PersonEntity : BaseEntity
{
    # region ---- properties ---------------------------------------------------

    public required string Cpf { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public required IEnumerable<EmailEntity> Emails { get; set; }
    public required IEnumerable<PhoneEntity> Phones { get; set; }
    public required IEnumerable<AddressEntity> Addresses { get; set; }

    # endregion
}
