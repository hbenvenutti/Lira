namespace Lira.Application.Specifications.Phones;

public readonly struct PhoneSpecificationDto
{
    public string Phone { get; init; }

    public PhoneSpecificationDto(string phone)
    {
        Phone = phone;
    }
}
