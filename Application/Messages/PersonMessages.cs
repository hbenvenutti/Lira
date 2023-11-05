namespace Lira.Application.Messages;

public readonly struct PersonMessages
{
    public const string InvalidDocument = "Invalid document";
    public const string InvalidName =
        "Name must be only letters and have more than 2 characters";
    public const string InvalidSurname =
        "Surname must be only letters and have more than 2 characters";
}
