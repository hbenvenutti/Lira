namespace Lira.Domain.Domains.PersonOrixa;

public interface IPersonOrixaRepository
{
    Task<PersonOrixaDomain> CreateAsync(PersonOrixaDomain domain);
}
