using BrazilianTypes.Types;
using Lira.Domain.Domains.Emails;

namespace Lira.Data.Repositories.Interfaces;

public interface IEmailRepository
{
    Task<EmailDomain> CreateAsync(EmailDomain email);
    Task<EmailDomain?> FindByIdAsync(Guid id, bool includePerson);
    Task<EmailDomain?> FindByAddressAsync(Email address, bool includePerson);
}
