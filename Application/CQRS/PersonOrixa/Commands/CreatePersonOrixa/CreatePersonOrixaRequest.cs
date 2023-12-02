using Lira.Application.Responses;
using Lira.Domain.Enums;
using MediatR;

namespace Lira.Application.CQRS.PersonOrixa.Commands.CreatePersonOrixa;

public class CreatePersonOrixaRequest :
    IRequest<IHandlerResponse<CreatePersonOrixaResponse>>
{
    public Guid PersonId { get; init; }
    public Guid OrixaId { get; init; }
    public OrixaType Type { get; init; }
    public bool ValidatePerson { get; init; }

    public CreatePersonOrixaRequest(
        Guid personId,
        Guid orixaId,
        OrixaType type,
        bool validatePerson = true
    )
    {
        PersonId = personId;
        OrixaId = orixaId;
        Type = type;
        ValidatePerson = validatePerson;
    }
}
