using BrazilianTypes.Types;
using Lira.Common.Enums;
using Lira.Domain.Domains.Base;

namespace Lira.Domain.Domains.Emails;

public class EmailSpecification : ISpecification<EmailSpecificationDto>
{
    public AppStatusCode AppStatusCode { get; set; } = AppStatusCode.Empty;
    public List<string> ErrorMessages { get; } = new();

    public bool IsSatisfiedBy(EmailSpecificationDto data)
    {
        if (!Email.TryParse(data.Address, out _))
        {
            AppStatusCode = AppStatusCode.InvalidEmailAddress;
            ErrorMessages.Add(item: EmailMessages.InvalidEmail);
            return false;
        }

        return true;
    }
}
