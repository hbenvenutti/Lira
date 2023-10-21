namespace Lira.Data.Entities;

public class PersonEntity : BaseEntity
{
    public required string Cpf { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
}
