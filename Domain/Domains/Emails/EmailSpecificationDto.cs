namespace Lira.Domain.Domains.Emails;

public readonly struct EmailSpecificationDto
{
    public string Address { get; init; }

    public EmailSpecificationDto(string address)
    {
        Address = address;
    }
}
