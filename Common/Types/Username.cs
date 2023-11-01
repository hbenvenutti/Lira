using System.Text.RegularExpressions;

namespace Lira.Common.Types;

public readonly partial struct Username
{
    # region ---- properties ---------------------------------------------------

    private const string Pattern = @"^[a-zA-Z]\w{2,14}$";
    public const string ErrorMessage = "Username is invalid.";
    private readonly string _value;

    # endregion

    # region ---- constructor --------------------------------------------------

    private Username(string value)
    {
        _value = value;
    }

    # endregion

    # region ---- parse --------------------------------------------------------

    private static Username Parse(string value)
    {
        if (!TryParse(value, out var username))
        {
            throw new ArgumentException(
                message: ErrorMessage,
                paramName: nameof(value)
            );
        }

        return username;
    }

    public static bool TryParse(string value, out Username username)
    {
        value = value.Trim();

        if (!IsValid(value))
        {
            username = default;

            return false;
        }

        username = new Username(value);

        return true;
    }

    # endregion

    # region ---- validation ---------------------------------------------------

    private static bool IsValid(string value)
    {
        return MyRegex().IsMatch(value);
    }

    [GeneratedRegex(Pattern)]
    private static partial Regex MyRegex();

    # endregion

    # region ---- operators ----------------------------------------------------

    public static implicit operator Username(string value) => Parse(value);

    public static implicit operator string(Username username) => username._value;

    #endregion
}
