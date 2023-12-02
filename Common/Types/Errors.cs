namespace Lira.Common.Types;

public struct Errors
{
    private readonly IEnumerable<string> _values;

    # region ---- constructor --------------------------------------------------

    private Errors(IEnumerable<string> values)
    {
        _values = values.ToList();
    }

    # endregion

    # region ---- operators ----------------------------------------------------

    public static implicit operator Errors(string message) =>
        new(new List<string> { message });

    public static implicit operator Errors(List<string> messages) =>
        new(messages);

    public static implicit operator List<string>(Errors errors) =>
        errors._values.ToList();

    # endregion
}
