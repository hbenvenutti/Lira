namespace Lira.Common.Exceptions;

public class MissingEnvironmentVariableException : NullReferenceException
{
    public MissingEnvironmentVariableException(string variableName)
        : base($"Environment variable {variableName} not found")
    {
    }
}
