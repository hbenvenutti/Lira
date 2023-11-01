using BrazilianTypes.Types;
using Lira.Application.Enums;
using Lira.Application.Errors;
using Lira.Common.Types;

namespace Lira.Application.Specifications.Person;

public class PersonSpecification : ISpecification
{
    # region ---- properties ---------------------------------------------------

    private readonly string _name;
    private readonly string _surname;
    private readonly string _cpf;

    public StatusCode StatusCode { get; set; }
    public ICollection<string> ErrorMessages { get; init; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public PersonSpecification(
        string name,
        string surname,
        string cpf
    )
    {
        _name = name;
        _surname = surname;
        _cpf = cpf;
        StatusCode = StatusCode.Empty;
        ErrorMessages = new List<string>();
    }

    # endregion

    # region ---- specification ------------------------------------------------

    public bool IsSatisfiedBy()
    {
        var errors = 0;

        if (!Name.TryParse(_name, out _))
        {
            StatusCode = StatusCode.NameIsInvalid;
            ErrorMessages.Add(item: NameErrorMessages.NameIsInvalid);
            errors++;
        }

        if (!Name.TryParse(_surname, out _))
        {
            StatusCode = StatusCode.SurnameIsInvalid;
            ErrorMessages.Add(item: NameErrorMessages.SurnameIsInvalid);
            errors++;
        }

        if (!Cpf.TryParse(_cpf, out _))
        {
            StatusCode = StatusCode.CpfIsInvalid;
            ErrorMessages.Add(item: Cpf.ErrorMessage);
            errors++;
        }

        if (errors > 1) { StatusCode = StatusCode.SeveralInvalidFields; }

        return true;
    }

    # endregion
}
