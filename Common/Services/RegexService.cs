using System.Text.RegularExpressions;

namespace Lira.Common.Services;

public static partial class RegexService
{
    # region ---- numbers ------------------------------------------------------

    [GeneratedRegex(pattern: @"[^\d]")]
    private static partial Regex OnlyNumbersRegex();

    public static string GetOnlyNumbers(string value) => OnlyNumbersRegex()
        .Replace(input: value, replacement: "");

    # endregion

    # region ---- remove white space -------------------------------------------

    [GeneratedRegex(pattern: @"\s+")]
    private static partial Regex WhiteSpaceRegex();

    public static string RemoveWhiteSpace(string value) => WhiteSpaceRegex()
        .Replace(input: value, replacement: "");

    # endregion

    # region ---- username -----------------------------------------------------

    [GeneratedRegex(pattern: @"^[a-zA-Z]\w{2,14}$")]
    public static partial Regex UsernameRegex();

    # endregion
}
