namespace Lira.Data.Entities;

public class PersonEntity : BaseEntity
{
    # region ---- properties ---------------------------------------------------

    public required string Cpf { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public ManagerEntity? Manager { get; set; }

    public IEnumerable<EmailEntity>? Emails { get; set; }
    public IEnumerable<PhoneEntity>? Phones { get; set; }
    public IEnumerable<AddressEntity>? Addresses { get; set; }
    public IEnumerable<MediumEntity>? Mediums { get; set; }
    public IEnumerable<PersonOrixaEntity>? PersonOrixas { get; set; }

    # endregion
}
