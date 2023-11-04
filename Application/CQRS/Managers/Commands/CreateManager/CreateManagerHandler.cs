using System.Net;
using Lira.Application.Dto;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Application.Responses;
using Lira.Application.Specifications.Manager;
using Lira.Application.Specifications.Password;
using Lira.Domain.Domains.Manager;
using Lira.Domain.Domains.Person;
using MediatR;

namespace Lira.Application.CQRS.Managers.Commands.CreateManager;

public class CreateManagerHandler :
    IRequestHandler<CreateManagerRequest, Response<CreateManagerResponse>>
{
    # region ---- properties ---------------------------------------------------

    private readonly IManagerRepository _managerRepository;
    private readonly IPersonRepository _personRepository;

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreateManagerHandler(
        IManagerRepository managerRepository,
        IPersonRepository personRepository
    )
    {
        _managerRepository = managerRepository;
        _personRepository = personRepository;
    }

    # endregion

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

        # region ---- person ---------------------------------------------------

        var person = await _personRepository.FindByIdAsync(request.PersonId);

        if (person is null)
        {
            return new Response<CreateManagerResponse>(
                httpStatusCode: HttpStatusCode.NotFound,
                statusCode: StatusCode.PersonNotFound,
                error: new ErrorDto(
                    messages: new List<string>
                    {
                        NotFoundMessages.PersonNotFound
                    }
                )
            );
        }

        # endregion

        # region ---- manager --------------------------------------------------

        var managerWithUsername = await _managerRepository.FindByUsernameAsync(
            username: request.Username
        );

        if (managerWithUsername is not null)
        {
            return new Response<CreateManagerResponse>(
                httpStatusCode: HttpStatusCode.BadRequest,
                statusCode: StatusCode.UsernameAlreadyExists,
                error: new ErrorDto(
                    messages: new List<string>
                    {
                        ManagerMessages.UsernameIsInUse
                    }
                )
            );
        }

        var manager = ManagerDomain.Create(
            personId: request.PersonId,
            username: request.Username,
            password: request.Password
        );

        manager = await _managerRepository.CreateAsync(manager);

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
