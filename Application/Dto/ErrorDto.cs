namespace Lira.Application.Dto;

public readonly struct ErrorDto
{
    # region ---- properties ---------------------------------------------------

    public IEnumerable<string> Messages { get; init; }

    # endregion

    # region ---- constructors -------------------------------------------------

    public ErrorDto(IEnumerable<string> messages)
    {
        Messages = messages;
    }

    public ErrorDto(string message)
    {
        Messages = new List<string> { message };
    }

    # endregion
}
