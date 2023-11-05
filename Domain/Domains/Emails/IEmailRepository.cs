using BrazilianTypes.Types;

namespace Lira.Domain.Domains.Emails;

public interface IEmailRepository
{
    Task<EmailDomain> CreateAsync(EmailDomain email);
    Task<EmailDomain?> FindByIdAsync(Guid id, bool includePerson = false);
    Task<EmailDomain?> FindByAddressAsync(Email address, bool includePerson = false);
}
