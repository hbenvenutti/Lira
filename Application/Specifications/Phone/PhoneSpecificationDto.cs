namespace Lira.Application.Specifications.Phone;

public readonly struct PhoneSpecificationDto
{
    public string Phone { get; init; }

    public PhoneSpecificationDto(string phone)
    {
        Phone = phone;
    }
}
