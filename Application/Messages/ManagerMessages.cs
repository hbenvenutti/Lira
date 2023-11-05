namespace Lira.Application.Messages;

public readonly struct ManagerMessages
{
    public const string AdminAlreadyExists = "Admin already exists.";
    public const string InvalidAdminCode = "Admin code is invalid.";
    public const string InvalidUsernameOrPassword = "User or password is invalid.";
    public const string PasswordsDoNotMatch = "Passwords do not match";
    public const string InvalidPassword =
        "Password must contain 1 letter and at least 8 characters long";
}
