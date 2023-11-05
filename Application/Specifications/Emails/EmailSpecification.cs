using BrazilianTypes.Types;
using Lira.Application.Enums;

namespace Lira.Application.Specifications.Emails;

public class EmailSpecification : ISpecification<EmailSpecificationDto>
{
    public StatusCode StatusCode { get; set; } = StatusCode.Empty;
    public ICollection<string> ErrorMessages { get; init; } = new List<string>();

    public bool IsSatisfiedBy(EmailSpecificationDto data)
    {
        return Email.TryParse(data.Address, out _);
    }
}
