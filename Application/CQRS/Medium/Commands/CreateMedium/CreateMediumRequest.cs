using Lira.Application.Responses;
using MediatR;

namespace Lira.Application.CQRS.Medium.Commands.CreateMedium;

public class CreateMediumRequest : IRequest<IHandlerResponse<CreateMediumResponse>>
{
    public Guid PersonId { get; init; }
    public DateTime? FirstAmaci { get; init; }
    public DateTime? LastAmaci { get; init; }
    public bool ValidatePerson { get; init; }

    public CreateMediumRequest(
        Guid personId,
        DateTime? firstAmaci = null,
        DateTime? lastAmaci = null,
        bool validatePerson = true
    )
    {
        PersonId = personId;
        FirstAmaci = firstAmaci;
        LastAmaci = lastAmaci;
        ValidatePerson = validatePerson;
    }
}
