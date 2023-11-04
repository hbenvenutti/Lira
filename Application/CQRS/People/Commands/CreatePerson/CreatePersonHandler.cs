using System.Net;
using Lira.Application.Dto;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Application.Responses;
using Lira.Application.Specifications.Person;
using Lira.Domain.Domains.Person;
using MediatR;

namespace Lira.Application.CQRS.People.Commands.CreatePerson;

public class CreatePersonHandler :
    IRequestHandler<CreatePersonRequest, Response<CreatePersonResponse>>
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

    public async Task<Response<CreatePersonResponse>> Handle(
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
            return new Response<CreatePersonResponse>(
                httpStatusCode: HttpStatusCode.BadRequest,
                statusCode: personSpecification.StatusCode,
                error: new ErrorDto(personSpecification.ErrorMessages)
            );
        }

        # endregion

        # region ---- person ---------------------------------------------------

        var person = await _personRepository
            .FindByCpfAsync(request.Document);

        if (person is not null)
        {
            return new Response<CreatePersonResponse>(
                httpStatusCode: HttpStatusCode.Conflict,
                statusCode: StatusCode.PersonAlreadyExists,
                error: new ErrorDto(PersonMessages.PersonAlreadyExists)
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

        return new Response<CreatePersonResponse>(
            isSuccess: true,
            httpStatusCode: HttpStatusCode.Created,
            statusCode: StatusCode.CreatedOne,
            data: new CreatePersonResponse(person.Id)
        );

        # endregion
    }
}
