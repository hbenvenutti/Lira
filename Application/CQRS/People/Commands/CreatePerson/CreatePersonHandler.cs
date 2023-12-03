using System.Net;
using Lira.Application.Messages;
using Lira.Application.Responses;
using Lira.Application.Specifications.Person;
using Lira.Common.Enums;
using Lira.Domain.Domains.Person;
using MediatR;

namespace Lira.Application.CQRS.People.Commands.CreatePerson;

public class CreatePersonHandler :
    IRequestHandler<CreatePersonRequest, IHandlerResponse<CreatePersonResponse>>
{
    # region ---- properties ---------------------------------------------------

    private readonly IPersonRepository _personRepository;

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreatePersonHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    # endregion

    public async Task<IHandlerResponse<CreatePersonResponse>> Handle(
        CreatePersonRequest request,
        CancellationToken cancellationToken
    )
    {
        # region ---- specification --------------------------------------------

        var personSpecification = new PersonSpecification();

        var personData = new PersonSpecificationDto(
            name: request.FirstName,
            surname: request.Surname,
            cpf: request.Document
        );

        if (!personSpecification.IsSatisfiedBy(personData))
        {
            return new HandlerResponse<CreatePersonResponse>(
                httpStatusCode: HttpStatusCode.BadRequest,
                appStatusCode: personSpecification.AppStatusCode,
                errors: personSpecification.ErrorMessages
            );
        }

        # endregion

        # region ---- person ---------------------------------------------------

        var person = await _personRepository
            .FindByCpfAsync(request.Document);

        if (person is not null)
        {
            return new HandlerResponse<CreatePersonResponse>(
                httpStatusCode: HttpStatusCode.Conflict,
                appStatusCode: AppStatusCode.PersonAlreadyExists,
                errors: ConflictMessages.PersonAlreadyExists
            );
        }

        person = await _personRepository.CreateAsync(
            PersonDomain.Create(
                cpf: request.Document,
                name: request.FirstName,
                surname: request.Surname
            )
        );

        # endregion

        # region ---- response --------------------------------------------------

        return new HandlerResponse<CreatePersonResponse>(
            isSuccess: true,
            httpStatusCode: HttpStatusCode.Created,
            appStatusCode: AppStatusCode.CreatedOne,
            data: new CreatePersonResponse(person.Id)
        );

        # endregion
    }
}
