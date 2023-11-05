using Lira.Application.Enums;

namespace Lira.Application.Specifications;

public interface ISpecification<in T> where T : struct
{
    StatusCode StatusCode { get; protected set; }
    ICollection<string> ErrorMessages { get; }
    bool IsSatisfiedBy(T data);
}
