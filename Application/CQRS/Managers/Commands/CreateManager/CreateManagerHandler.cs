using System.Net;
using Lira.Application.Dto;
using Lira.Application.Enums;
using Lira.Application.Responses;
using Lira.Application.Specifications.Manager;
using Lira.Application.Specifications.Password;
using Lira.Domain.Domains.Manager;
using MediatR;

namespace Lira.Application.CQRS.Managers.Commands.CreateManager;

public class CreateManagerHandler :
    IRequestHandler<CreateManagerRequest, Response<CreateManagerResponse>>
{
    private readonly IManagerRepository _managerRepository;

    public CreateManagerHandler(IManagerRepository managerRepository)
    {
        _managerRepository = managerRepository;
    }

    public async Task<Response<CreateManagerResponse>> Handle(
        CreateManagerRequest request,
        CancellationToken cancellationToken
    )
    {
        # region ---- specification --------------------------------------------

        var passwordSpecification = new PasswordSpecification();

        var managerSpecification = new ManagerSpecification();

        var passwordData = new PasswordSpecificationDto(
            password: request.Password,
            confirmation: request.Confirmation
        );

        var managerData = new ManagerSpecificationDto(
            username: request.Username
        );

        if (!passwordSpecification.IsSatisfiedBy(passwordData))
        {
            return new Response<CreateManagerResponse>(
                httpStatusCode: HttpStatusCode.BadRequest,
                statusCode: passwordSpecification.StatusCode,
                error: new ErrorDto(
                    messages: passwordSpecification.ErrorMessages
                )
            );
        }

        if (!managerSpecification.IsSatisfiedBy(managerData))
        {
            return new Response<CreateManagerResponse>(
                httpStatusCode: HttpStatusCode.BadRequest,
                statusCode: managerSpecification.StatusCode,
                error: new ErrorDto(managerSpecification.ErrorMessages)
            );
        }

        # endregion

        # region ---- manager --------------------------------------------------

        var manager = ManagerDomain.Create(
            personId: request.PersonId,
            username: request.Username,
            password: request.Password
        );

        await _managerRepository.CreateAsync(manager);

        # endregion

        # region ---- response -------------------------------------------------

        return new Response<CreateManagerResponse>(
            isSuccess: true,
            httpStatusCode: HttpStatusCode.Created,
            statusCode: StatusCode.CreatedOne,
            data: new CreateManagerResponse(manager.Id)
        );

        # endregion
    }
}
