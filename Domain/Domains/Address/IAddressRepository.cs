namespace Lira.Domain.Domains.Address;

public interface IAddressRepository
{
    Task<AddressDomain> CreateAsync(AddressDomain addressDomain);
}
