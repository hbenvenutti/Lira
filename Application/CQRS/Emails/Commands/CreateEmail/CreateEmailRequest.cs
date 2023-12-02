using Lira.Application.Responses;
using Lira.Domain.Enums;
using MediatR;

namespace Lira.Application.CQRS.Emails.Commands.CreateEmail;

public class CreateEmailRequest : IRequest<IHandlerResponse<CreateEmailResponse>>
{
    public string Address { get; init; }
    public EmailType Type { get; init; }
    public Guid PersonId { get; init; }
    public bool ValidatePerson { get; init; }

    public CreateEmailRequest(
        string address,
        EmailType type,
        Guid personId,
        bool validatePerson = true
    )
    {
        Address = address;
        Type = type;
        PersonId = personId;
        ValidatePerson = validatePerson;
    }
}
