using Lira.Common.Providers.Hash;

namespace Lira.Common.Types;

public readonly struct Password
{
    # region ---- properties ---------------------------------------------------

    public const string ErrorMessage = "Password is invalid.";

    private readonly string _value;
    public string Hash { get; init; }

    # endregion

    # region ---- constructor --------------------------------------------------

    private Password(string value)
    {
        _value = value;
        Hash = new HashService().Hash(value);
    }

    # endregion

    # region ---- compare ------------------------------------------------------

    public bool Compare(string hash) => new HashService()
        .Verify(_value, hash);

    # endregion

    # region ---- parse --------------------------------------------------------

    private static Password Parse(string value)
    {
        if (!TryParse(value, out var password))
        {
            throw new ArgumentException(
                message: ErrorMessage,
                paramName: nameof(value)
            );
        }

        return password;
    }

    public static bool TryParse(string value, out Password password)
    {
        value = value.Trim();

        if (!IsValid(value))
        {
            password = default;

            return false;
        }

        password = new Password(value);

        return true;
    }

    # endregion

    # region ---- validation ---------------------------------------------------

    private static bool IsValid(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) { return false; }

        return value.Length >= 8 && value.Any(char.IsLetter);
    }

    # endregion

    # region ---- implicit operators -------------------------------------------

    public static implicit operator Password(string value) => Parse(value);

    public static implicit operator string(Password password) => password._value;

    # endregion
}
