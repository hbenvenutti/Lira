using System.Globalization;
using System.Text.RegularExpressions;
using Lira.Common.Extensions;

namespace Lira.Common.Types;

public readonly partial struct Cpf
{
    # region ---- constants ----------------------------------------------------

    private const string Pattern = @"^\d{3}\.\d{3}\.\d{3}-\d{2}$";
    private const string Replacement = @"$1.$2.$3-$4";

    public const string ErrorMessage = "CPF is invalid.";

    private static readonly byte[] Mult1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
    private static readonly byte[] Mult2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

    # endregion

    # region ---- properties ---------------------------------------------------

    private readonly string _value;

    public string Numbers => RemoveMask(_value);
    public string Digits => _value[10..];

    # endregion

    # region ---- constructor --------------------------------------------------

    private Cpf(string value)
    {
        _value = Format(value);
    }

    # endregion

    # region ---- parse --------------------------------------------------------

    public static Cpf Parse(string value)
    {
        if (!TryParse(value, out var cpf))
        {
            throw new ArgumentException(
                message: ErrorMessage,
                paramName: nameof(value)
            );
        }

        return cpf;
    }

    public static bool TryParse(string value, out Cpf cpf)
    {
        value = value.Trim();
        value = RemoveMask(value);

        if (!IsValid(value))
        {
            cpf = default;

            return false;
        }

        cpf = new Cpf(value);

        return true;
    }

    # endregion

    # region ---- validation ---------------------------------------------------

    private static bool IsValid(string value )
    {
        var culture = CultureInfo.InvariantCulture;

        var cpf = value
            .Trim()
            .Replace(oldValue: ".", newValue: "")
            .Replace(oldValue: "-", newValue: "");

        if (!cpf.IsNumeric()) { return false; }

        if (cpf.Length != 11) { return false; }

        for (var j = 0; j < 10; j++)
        {

            var @char = j
                .ToString(culture)
                .PadLeft(
                    totalWidth: 11,
                    paddingChar: char.Parse(j.ToString(culture))
                );

            if (@char == cpf) { return false; }
        }

        var tempCpf = cpf[..9];

        var sum = Sum(tempCpf, Mult1);

        var rest = GetRest(sum);

        var digit = rest.ToString(culture);

        tempCpf += digit;

        sum = Sum(tempCpf, Mult2);

        rest = GetRest(sum);

        digit += rest.ToString(culture);

        return cpf.EndsWith(digit, StringComparison.InvariantCulture);
    }

    # endregion

    # region ---- auxiliars ----------------------------------------------------

    private static int Sum(string tempCpf, byte[] mult)
    {
        var sum = 0;

        for (var i = 0; i < mult.Length; i++ )
        {
            sum += (tempCpf[i] - '0') * mult[i];
        }

        return sum;
    }

    private static int GetRest(int sum)
    {
        var rest = sum % 11;

        return rest < 2
            ? 0
            : 11 - rest;
    }

    private static string RemoveMask(string value) => value
        .Replace(oldValue: ".", newValue: "")
        .Replace(oldValue: "-", newValue: "");

    # endregion

    # region ---- mask ---------------------------------------------------------

    private static string Format(string value)
    {
        return MyRegex().Replace(
            input: value,
            replacement: Replacement
        );
    }

    [GeneratedRegex(Pattern)]
    private static partial Regex MyRegex();

    # endregion ----------------------------------------------------------------

    # region ---- implicit operators -------------------------------------------

    public static implicit operator Cpf(string value) => Parse(value);

    public static implicit operator string(Cpf cpf) => cpf._value;

    # endregion
}