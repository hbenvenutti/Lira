using BrazilianTypes.Interfaces;
using Lira.Common.Services;

namespace Lira.Common.Types;

public readonly struct Username : IType<Username>
{
    # region ---- properties ---------------------------------------------------

    public static string ErrorMessage => "Username is invalid.";
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
        username = default;

        if (string.IsNullOrWhiteSpace(value)) { return false; }

        value = value.Trim();

        if (!IsValid(value)) { return false; }

        username = new Username(value);

        return true;
    }

    # endregion

    # region ---- validation ---------------------------------------------------

    private static bool IsValid(string value)
    {
        return RegexService.UsernameRegex().IsMatch(value);
    }

    # endregion

    # region ---- operators ----------------------------------------------------

    public static implicit operator Username(string value) => Parse(value);

    public static implicit operator string(Username username) => username._value;

    #endregion
}
