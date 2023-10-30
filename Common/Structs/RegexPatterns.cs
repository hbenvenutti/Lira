using System.Text.RegularExpressions;

namespace Lira.Common.Structs;

public readonly partial struct RegexPatterns
{
    [GeneratedRegex(pattern: @"[^\d]")]
    public static partial Regex RemoveMaskRegex();

    [GeneratedRegex(pattern: @"^\d{3}\.\d{3}\.\d{3}-\d{2}$")]
    public static partial Regex CpfMaskRegex();
}
