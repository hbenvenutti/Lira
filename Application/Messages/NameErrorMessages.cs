namespace Lira.Application.Messages;

public readonly struct NameErrorMessages
{
    public const string NameLength = "Name must be between 2 and 50 characters.";
    public const string NameFormat = "Name must contain only letters.";
    public const string NameIsInvalid = "Name is invalid.";
    public const string SurnameLength = "Surname must be between 2 and 50 characters.";
    public const string SurnameFormat = "Surname must contain only letters.";
    public const string SurnameIsInvalid = "Surname is invalid.";
}
