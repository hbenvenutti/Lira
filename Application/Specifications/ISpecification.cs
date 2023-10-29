using Lira.Application.Enums;

namespace Lira.Application.Specifications;

public interface ISpecification
{
    StatusCode StatusCode { get; protected set; }
    ICollection<string> ErrorMessages { get; init; }
    bool IsSatisfiedBy();
}
