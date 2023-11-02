namespace Lira.Domain.Domains.Phone;

public interface IPhoneRepository
{
    Task<PhoneDomain> CreateAsync(PhoneDomain domain);
}
