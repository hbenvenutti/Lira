namespace Lira.Domain.Domains.Phones;

public interface IPhoneRepository
{
    Task<PhoneDomain> CreateAsync(PhoneDomain domain);
}
