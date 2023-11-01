namespace Lira.Application.CQRS.Managers.Commands.CreateAdmin;

public class CreateAdminResponseDto
{
    public Guid Id { get; set; }

    public CreateAdminResponseDto(Guid id)
    {
        Id = id;
    }
}
