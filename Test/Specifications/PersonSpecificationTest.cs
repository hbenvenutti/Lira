using System.Diagnostics.CodeAnalysis;
using BrazilianTypes.Types;
using Lira.Application.Specifications.Person;
using Lira.Common.Enums;
using Lira.Domain.Domains.Person;

namespace Lira.Test.Specifications;

[ExcludeFromCodeCoverage]
public class PersonSpecificationTest
{
    private readonly PersonSpecification _personSpecification = new();

    private static readonly Name Name = "John";
    private static readonly Name Surname = "Doe";
    private static readonly Cpf Document = Cpf.Generate();
    private const byte Invalid = 0;
    private const byte Valid = 1;

    # region ---- success ------------------------------------------------------

    [Fact]
    public void ItShouldBeSatisfied()
    {
        var personSpecificationDto = new PersonSpecificationDto(
            name: Name,
            surname: Surname,
            cpf: Document
        );

        var result = _personSpecification.IsSatisfiedBy(personSpecificationDto);

        Assert.True(result);

        Assert.Equal(
            expected: AppStatusCode.Empty,
            actual: _personSpecification.AppStatusCode
        );

        Assert.NotNull(_personSpecification.ErrorMessages);
        Assert.Empty(_personSpecification.ErrorMessages);
    }

    # endregion

    # region ---- Name ---------------------------------------------------------

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void ItShouldNotBeSatisfiedWhenNameIsInvalid(string name)
    {
        var personSpecificationDto = new PersonSpecificationDto(
            name: name,
            surname: Surname,
            cpf: Document
        );

        var result = _personSpecification.IsSatisfiedBy(personSpecificationDto);

        Assert.False(result);

        Assert.Equal(
            expected: AppStatusCode.InvalidName,
            actual: _personSpecification.AppStatusCode
        );

        Assert.NotNull(_personSpecification.ErrorMessages);
        Assert.NotEmpty(_personSpecification.ErrorMessages);
        Assert.Single(_personSpecification.ErrorMessages);
        Assert.Contains(
            expected: PersonMessages.InvalidName,
            collection: _personSpecification.ErrorMessages
        );
    }

    # endregion

    # region ---- Surname ------------------------------------------------------

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void ItShouldNotBeSatisfiedWhenSurnameIsInvalid(string surname)
    {
        var personSpecificationDto = new PersonSpecificationDto(
            name: Name,
            surname: surname,
            cpf: Document
        );

        var result = _personSpecification.IsSatisfiedBy(personSpecificationDto);

        Assert.False(result);

        Assert.Equal(
            expected: AppStatusCode.InvalidSurname,
            actual: _personSpecification.AppStatusCode
        );

        Assert.NotNull(_personSpecification.ErrorMessages);
        Assert.NotEmpty(_personSpecification.ErrorMessages);
        Assert.Single(_personSpecification.ErrorMessages);
        Assert.Contains(
            expected: PersonMessages.InvalidSurname,
            collection: _personSpecification.ErrorMessages
        );
    }

    # endregion

    # region ---- Document -----------------------------------------------------

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    // [InlineData(null)] todo fix lib to handle null
    [InlineData("00000000000")]
    public void ItShouldNotBeSatisfiedWhenDocumentIsInvalid(string document)
    {
        var personSpecificationDto = new PersonSpecificationDto(
            name: Name,
            surname: Surname,
            cpf: document
        );

        var result = _personSpecification.IsSatisfiedBy(personSpecificationDto);

        Assert.False(result);

        Assert.Equal(
            expected: AppStatusCode.InvalidCpf,
            actual: _personSpecification.AppStatusCode
        );

        Assert.NotNull(_personSpecification.ErrorMessages);
        Assert.NotEmpty(_personSpecification.ErrorMessages);
        Assert.Single(_personSpecification.ErrorMessages);
        Assert.Contains(
            expected: PersonMessages.InvalidDocument,
            collection: _personSpecification.ErrorMessages
        );
    }

    # endregion

    # region ---- SeveralInvalidFields -----------------------------------------

    [Theory]
    [InlineData(Invalid, Invalid, Invalid)]
    [InlineData(Invalid, Invalid, Valid)]
    [InlineData(Invalid, Valid, Invalid)]
    [InlineData(Valid, Invalid, Invalid)]
    public void ItShouldNotBeSatisfiedWhenSeveralFieldsAreInvalid(
        byte name,
        byte surname,
        byte document
    )
    {
        var personSpecificationDto = new PersonSpecificationDto(
            name: name == Invalid ? string.Empty : Name,
            surname: surname == Invalid ? string.Empty : Surname,
            cpf: document == Invalid ? string.Empty : Document
        );

        var result = _personSpecification.IsSatisfiedBy(personSpecificationDto);

        Assert.False(result);

        Assert.Equal(
            expected: AppStatusCode.SeveralInvalidFields,
            actual: _personSpecification.AppStatusCode
        );

        Assert.NotNull(_personSpecification.ErrorMessages);
        Assert.NotEmpty(_personSpecification.ErrorMessages);

        if (name == Invalid)
        {
            Assert.Contains(
                expected: PersonMessages.InvalidName,
                collection: _personSpecification.ErrorMessages
            );
        }

        if (surname == Invalid)
        {
            Assert.Contains(
                expected: PersonMessages.InvalidSurname,
                collection: _personSpecification.ErrorMessages
            );
        }

        if (document == Invalid)
        {
            Assert.Contains(
                expected: PersonMessages.InvalidDocument,
                collection: _personSpecification.ErrorMessages
            );
        }
    }

    # endregion
}
