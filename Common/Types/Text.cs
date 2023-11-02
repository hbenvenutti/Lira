using BrazilianTypes.Interfaces;

namespace Lira.Common.Types;

public readonly struct Text : IType<Text>
{
    public static string ErrorMessage => "Invalid text.";

    private readonly string _value;

    # region ---- constructors -------------------------------------------------

    private Text(string value)
    {
        _value = value;
    }

    # endregion

    # region ---- parse --------------------------------------------------------

    public static bool TryParse(string value, out Text text)
    {
        value = value.Trim();

        if (!IsValid(value))
        {
            text = default;

            return false;
        }

        text = new Text(value);

        return true;
    }

    private static Text Parse(string value)
    {
        if (!TryParse(value, out var text))
        {
            throw new ArgumentException(
                message: ErrorMessage,
                paramName: nameof(value)
            );
        }

        return text;
    }

    # endregion

    # region ---- validation ---------------------------------------------------

    private static bool IsValid(string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    # endregion

    # region ---- operators ----------------------------------------------------

    public static implicit operator Text(string value) => Parse(value);
    public static implicit operator string(Text text) => text._value;

    # endregion
}
