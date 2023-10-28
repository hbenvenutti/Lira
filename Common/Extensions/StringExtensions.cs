using Lira.Common.Exceptions;

namespace Lira.Common.Extensions;

public static class StringExtensions
{
    # region ---- letters ------------------------------------------------------

    public static bool IsAlphabetic(this string value) => value
        .All(char.IsLetter);

    public static bool HasAllCharsEqual(this string value) => value
        .All(@char => @char == value[0]);

    # endregion

    # region ---- numbers ------------------------------------------------------

    public static bool IsNumeric(this string value) => value.All(char.IsDigit);

    # endregion

    # region ---- enums --------------------------------------------------------

    public static T ParseToEnum<T>(this string value) where T : struct, Enum
    {
        var result = Enum.TryParse<T>(
            value: value,
            ignoreCase: true,
            result: out var @enum
        );

        if (!result) throw new InvalidEnumException(nameof(value));

        return @enum;
    }

    public static bool IsEnum<T>(this string enumValue) =>
        Enum.IsDefined(typeof(T), enumValue);

    # endregion
}
