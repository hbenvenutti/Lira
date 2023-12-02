using BrazilianTypes.Types;
using Lira.Application.Enums;
using Lira.Application.Messages;

namespace Lira.Application.Specifications.Emails;

public class EmailSpecification : ISpecification<EmailSpecificationDto>
{
    public AppStatusCode AppStatusCode { get; set; } = AppStatusCode.Empty;
    public List<string> ErrorMessages { get; } = new List<string>();

    public bool IsSatisfiedBy(EmailSpecificationDto data)
    {
        if (!Email.TryParse(data.Address, out _))
        {
            AppStatusCode = AppStatusCode.InvalidEmailAddress;
            ErrorMessages.Add(item: PersonMessages.InvalidEmail);
            return false;
        }

        return true;
    }
}
