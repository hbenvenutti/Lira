using System.Text.RegularExpressions;

namespace Lira.Common.Structs;

public readonly partial struct RegexPatterns
{
    # region ---- numbers ------------------------------------------------------

    [GeneratedRegex(pattern: @"[^\d]")]
    private static partial Regex OnlyNumbers();

    public static string GetOnlyNumbers(string value) => OnlyNumbers()
        .Replace(input: value, replacement: "");

    # endregion

    # region ---- remove white space -------------------------------------------

    [GeneratedRegex(pattern: @"\s+")]
    private static partial Regex WhiteSpace();

    public static string RemoveWhiteSpace(string value) => WhiteSpace()
        .Replace(input: value, replacement: "");

    # endregion
}
