using Lira.Application.Responses;
using MediatR;

namespace Lira.Application.CQRS.Emails.Commands.CreateEmail;

public class CreateEmailRequest : IRequest<Response<CreateEmailResponse>>
{
    public Guid PersonId { get; init; }
    public string Address { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
}
