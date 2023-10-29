namespace Lira.Application.Errors;

public readonly struct NameErrorMessages
{
    public const string NameLength = "Name must be between 2 and 50 characters.";
    public const string NameFormat = "Name must contain only letters.";
}
