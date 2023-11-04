using Lira.Data.Contexts;
using Lira.Data.Entities;
using Lira.Domain.Domains.Phones;

namespace Lira.Data.Repositories;

public class PhoneRepository : IPhoneRepository
{
    # region ---- properties ---------------------------------------------------

    private readonly IDbContext _dbContext;

    # endregion

    # region ---- constructor --------------------------------------------------

    public PhoneRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    # endregion

    public async Task<PhoneDomain> CreateAsync(PhoneDomain domain)
    {
        var phone = (PhoneEntity) domain;

        await _dbContext.Phones.AddAsync(phone);

        await _dbContext.SaveChangesAsync();

        return (PhoneDomain) phone;
    }
}
