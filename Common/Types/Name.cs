using BrazilianTypes.Interfaces;
using Lira.Common.Extensions;
using Lira.Common.Structs;

namespace Lira.Common.Types;

public readonly struct Name : IType<Name>
{
    # region ---- properties ---------------------------------------------------

    public static string ErrorMessage => "Name is invalid.";
    private readonly string _value;

    # endregion

    # region ---- constructor --------------------------------------------------

    private Name(string value)
    {
        _value = value;
    }

    # endregion

    # region ---- parse --------------------------------------------------------

    private static Name Parse(string value)
    {
        if (!TryParse(value, out var name))
        {
            throw new ArgumentException(
                message: ErrorMessage,
                paramName: nameof(value)
            );
        }

        return name;
    }

    public static bool TryParse(string value, out Name name)
    {
        if (!IsValid(value))
        {
            name = default;
            return false;
        }

        name = new Name(value.Trim());

        return true;
    }

    # endregion

    # region ---- validation ---------------------------------------------------

    private static bool IsValid(string value)
    {
        value = RegexPatterns.RemoveWhiteSpace(value);

        if (value.Length is < 2 or > 50) { return false; }

        if (!value.IsAlphabetic()) { return false; }

        return true;
    }

    # endregion

    # region ---- operators ----------------------------------------------------

    public static implicit operator Name(string value) => Parse(value);

    public static implicit operator string(Name name) => name._value;

    # endregion
}
