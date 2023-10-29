using System.Net;
using Lira.Application.Dto;
using Lira.Application.Enums;
using Lira.Application.Responses;
using Lira.Application.Specifications.Manager;
using Lira.Application.Specifications.Password;
using Lira.Application.Specifications.Person;
using Lira.Application.Transactions;
using Lira.Common.Exceptions;
using Lira.Domain.Domains.Manager;
using Lira.Domain.Domains.Person;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Lira.Application.CQRS.Managers.Commands.CreateAdmin;

public class CreateAdminHandler
    : IRequestHandler<CreateAdminRequest, Response<CreateAdminResponseDto>>
{
    # region ---- properties ---------------------------------------------------

    private readonly IConfiguration _configuration;
    private readonly ITransaction _transaction;

    private readonly IPersonRepository _personRepository;
    private readonly IManagerRepository _managerRepository;

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreateAdminHandler(
        IConfiguration configuration,
        IPersonRepository personRepository,
        IManagerRepository managerRepository,
        ITransaction transaction
    )
    {
        _configuration = configuration;
        _personRepository = personRepository;
        _managerRepository = managerRepository;
        _transaction = transaction;
    }

    # endregion

    public async Task<Response<CreateAdminResponseDto>> Handle(
        CreateAdminRequest request,
        CancellationToken cancellationToken
    )
    {
        # region ---- code validation ------------------------------------------

        var code = _configuration["Admin:Code"]
            ?? throw new MissingEnvironmentVariableException("Admin:Code");

        var codeIsValid = request.Code == code;

        if (!codeIsValid)
        {
            return new Response<CreateAdminResponseDto>(
                httpStatusCode: HttpStatusCode.BadRequest,
                statusCode: StatusCode.AdminCodeIsInvalid
            );
        }

        # endregion

        # region ---- data validation ------------------------------------------

        var personSpecification = new PersonSpecification(
            name: request.Name,
            surname: request.Surname,
            cpf: request.Cpf
        );

        var passwordSpecification = new PasswordSpecification(
            password: request.Password,
            confirmation: request.PasswordConfirmation
        );

        var managerSpecification = new ManagerSpecification(
            username: request.Username
        );

        if (!personSpecification.IsSatisfiedBy())
        {
            return new Response<CreateAdminResponseDto>(
                httpStatusCode: HttpStatusCode.BadRequest,
                statusCode: personSpecification.StatusCode,
                error: new ErrorDto(personSpecification.ErrorMessages)
            );
        }

        if (!passwordSpecification.IsSatisfiedBy())
        {
            return new Response<CreateAdminResponseDto>(
                httpStatusCode: HttpStatusCode.BadRequest,
                statusCode: passwordSpecification.StatusCode,
                error: new ErrorDto(passwordSpecification.ErrorMessages)
            );
        }

        if (!managerSpecification.IsSatisfiedBy())
        {
            return new Response<CreateAdminResponseDto>(
                httpStatusCode: HttpStatusCode.BadRequest,
                statusCode: managerSpecification.StatusCode,
                error: new ErrorDto(managerSpecification.ErrorMessages)
            );
        }

        # endregion

        # region ---- managers -------------------------------------------------

        var managers = await _managerRepository.FindAllAsync();

        if (managers.Any())
        {
            return new Response<CreateAdminResponseDto>(
                httpStatusCode: HttpStatusCode.UnprocessableEntity,
                statusCode: StatusCode.AdminAlreadyExists
            );
        }

        # endregion

        # region ---- person ---------------------------------------------------

        var person = await _personRepository.FindByCpfAsync(request.Cpf)
            ?? PersonDomain.Create(
                cpf: request.Cpf,
                name: request.Name,
                surname: request.Surname
            );

        # endregion

        # region ---- manager --------------------------------------------------

        var manager = ManagerDomain.Create(
            username: request.Username,
            password: request.Password,
            personId: person.Id
        );

        # endregion

        # region ---- response -------------------------------------------------

        _transaction.Commit();

        return new Response<CreateAdminResponseDto>(
            httpStatusCode: HttpStatusCode.Created,
            statusCode: StatusCode.CreatedTransaction,
            isSuccess: true,
            data: new CreateAdminResponseDto(manager.Id)
        );

        # endregion
    }
}
