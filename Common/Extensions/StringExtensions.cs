using Lira.Common.Exceptions;

namespace Lira.Common.Extensions;

public static class StringExtensions
{
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
