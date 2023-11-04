namespace Lira.Application.Specifications.Manager;

public readonly struct ManagerSpecificationDto
{
    # region ---- properties ---------------------------------------------------

    public string Username { get; init; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public ManagerSpecificationDto(string username)
    {
        Username = username;
    }

    # endregion
}
