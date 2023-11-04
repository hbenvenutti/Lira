using Lira.Application.Responses;
using Lira.Domain.Enums;
using MediatR;

namespace Lira.Application.CQRS.Emails.Commands.CreateEmail;

public class CreateEmailRequest : IRequest<Response<CreateEmailResponse>>
{
    public Guid PersonId { get; init; }
    public string Address { get; init; }
    public EmailType Type { get; init; }

    public CreateEmailRequest(
        Guid personId,
        string address,
        EmailType type
    )
    {
        PersonId = personId;
        Address = address;
        Type = type;
    }
}
