namespace Lira.Application.Specifications.Passwords;

public readonly struct PasswordSpecificationDto
{
    public string Password { get; init; }
    public string Confirmation { get; init; }

    public PasswordSpecificationDto(
        string password,
        string confirmation
    )
    {
        Password = password;
        Confirmation = confirmation;
    }
}
