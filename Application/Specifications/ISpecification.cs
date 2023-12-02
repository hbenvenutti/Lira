using Lira.Application.Enums;

namespace Lira.Application.Specifications;

public interface ISpecification<in T> where T : struct
{
    AppStatusCode AppStatusCode { get; protected set; }
    List<string> ErrorMessages { get; }
    bool IsSatisfiedBy(T data);
}
