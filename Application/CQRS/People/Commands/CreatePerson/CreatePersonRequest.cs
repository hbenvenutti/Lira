using Lira.Application.Responses;
using MediatR;

namespace Lira.Application.CQRS.People.Commands.CreatePerson;

public class CreatePersonRequest : IRequest<IHandlerResponse<CreatePersonResponse>>
{
    # region ---- properties ---------------------------------------------------

    public string FirstName { get; init; }
    public string Surname { get; init; }
    public string Document { get; init; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreatePersonRequest(
        string firstName,
        string surname,
        string document
    )
    {
        FirstName = firstName;
        Surname = surname;
        Document = document;
    }

    # endregion
}
