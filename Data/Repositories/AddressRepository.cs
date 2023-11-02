using Lira.Data.Contexts;
using Lira.Data.Entities;
using Lira.Domain.Domains.Address;

namespace Lira.Data.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly IDbContext _dbContext;

    public AddressRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AddressDomain> CreateAsync(AddressDomain addressDomain)
    {
        var address = (AddressEntity) addressDomain;

        await _dbContext.Addresses.AddAsync(address);

        await _dbContext.SaveChangesAsync();

        return (AddressDomain) address;
    }
}
