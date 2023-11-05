using BrazilianTypes.Types;
using Lira.Application.Enums;
using Lira.Application.Messages;

namespace Lira.Application.Specifications.Emails;

public class EmailSpecification : ISpecification<EmailSpecificationDto>
{
    public StatusCode StatusCode { get; set; } = StatusCode.Empty;
    public ICollection<string> ErrorMessages { get; } = new List<string>();

    public bool IsSatisfiedBy(EmailSpecificationDto data)
    {
        if (!Email.TryParse(data.Address, out _))
        {
            StatusCode = StatusCode.InvalidEmailAddress;
            ErrorMessages.Add(item: PersonMessages.InvalidEmail);
            return false;
        }

        return true;
    }
}
