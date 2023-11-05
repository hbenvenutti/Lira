namespace Lira.Application.Specifications.Person;

public struct PersonSpecificationDto
{
    public string Name { get; init; }
    public string Surname { get; init; }
    public string Cpf { get; init; }

    public PersonSpecificationDto(
        string name,
        string surname,
        string cpf
    )
    {
        Name = name;
        Surname = surname;
        Cpf = cpf;
    }
}
