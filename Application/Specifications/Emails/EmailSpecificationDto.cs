namespace Lira.Application.Specifications.Emails;

public readonly struct EmailSpecificationDto
{
    public string Address { get; init; }

    public EmailSpecificationDto(string address)
    {
        Address = address;
    }
}
