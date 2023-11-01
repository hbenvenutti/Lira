namespace Lira.Common.Exceptions;

public class InvalidEnumException : ArgumentException
{
    private const string ErrorMessage = "Not a valid enum.";

    public InvalidEnumException(string property)
        : base(message: ErrorMessage, paramName: property)
    {
    }
}
