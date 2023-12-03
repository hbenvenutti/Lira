using Lira.Common.Enums;

namespace Lira.Domain.Domains.Base;

public interface ISpecification<in T> where T : struct
{
    AppStatusCode AppStatusCode { get; protected set; }
    List<string> ErrorMessages { get; }
    bool IsSatisfiedBy(T data);
}
