using Lira.Data.Contexts;
using Lira.Data.Entities;
using Lira.Domain.Domains.PersonOrixa;

namespace Lira.Data.Repositories;

public class PersonOrixaRepository : IPersonOrixaRepository
{
    private readonly IDbContext _dbContext;

    public PersonOrixaRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PersonOrixaDomain> CreateAsync(PersonOrixaDomain domain)
    {
        var personOrixa = (PersonOrixaEntity) domain;

        await _dbContext.PersonOrixas.AddAsync(personOrixa);
        await _dbContext.SaveChangesAsync();

        return (PersonOrixaDomain) personOrixa;
    }
}
