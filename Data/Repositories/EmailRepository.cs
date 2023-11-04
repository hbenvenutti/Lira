using BrazilianTypes.Types;
using Lira.Data.Contexts;
using Lira.Data.Entities;
using Lira.Domain.Domains.Emails;
using Microsoft.EntityFrameworkCore;

namespace Lira.Data.Repositories;

public class EmailRepository : IEmailRepository
{
    # region ---- properties --------------------------------------------------

    private readonly IDbContext _dbContext;

    # endregion

    # region ---- constructor --------------------------------------------------

    public EmailRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    # endregion

    # region ---- create -------------------------------------------------------

    public async Task<EmailDomain> CreateAsync(EmailDomain email)
    {
        var emailEntity = (EmailEntity) email;

        await _dbContext.Emails.AddAsync(emailEntity);

        await _dbContext.SaveChangesAsync();

        return (EmailDomain) emailEntity;
    }

    # endregion

    # region ---- find by id ---------------------------------------------------

    public async Task<EmailDomain?> FindByIdAsync(
        Guid id,
        bool includePerson
    )
    {
        var query = _dbContext.Emails
            .AsNoTracking()
            .AsQueryable();

        if (includePerson)
        {
            query = query
                .AsNoTracking()
                .Include(email => email.Person);
        }

        var emailEntity = await query
            .SingleOrDefaultAsync(email => email.Id == id);

        return emailEntity is null
            ? null
            : (EmailDomain) emailEntity;
    }

    # endregion

    # region ---- find by address ----------------------------------------------

    public async Task<EmailDomain?> FindByAddressAsync(
        Email address,
        bool includePerson
    )
    {
        var query = _dbContext.Emails
            .AsNoTracking()
            .AsQueryable();

        if (includePerson)
        {
            query = query
                .AsNoTracking()
                .Include(email => email.Person);
        }

        var emailEntity = await query
            .SingleOrDefaultAsync(email => email.Address == address);

        return emailEntity is null
            ? null
            : (EmailDomain) emailEntity;
    }

    # endregion
}
