using BrazilianTypes.Types;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Common.Types;

namespace Lira.Application.Specifications.Person;

public class PersonSpecification : ISpecification<PersonSpecificationDto>
{
    # region ---- properties ---------------------------------------------------

    public StatusCode StatusCode { get; set; } = StatusCode.Empty;
    public ICollection<string> ErrorMessages { get; init; } = new List<string>();

    # endregion

    # region ---- specification ------------------------------------------------

    public bool IsSatisfiedBy(PersonSpecificationDto data)
    {
        var errors = 0;

        if (!Name.TryParse(data.Name, out _))
        {
            StatusCode = StatusCode.NameIsInvalid;
            ErrorMessages.Add(item: NameErrorMessages.NameIsInvalid);
            errors++;
        }

        if (!Name.TryParse(data.Surname, out _))
        {
            StatusCode = StatusCode.SurnameIsInvalid;
            ErrorMessages.Add(item: NameErrorMessages.SurnameIsInvalid);
            errors++;
        }

        if (!Cpf.TryParse(data.Cpf, out _))
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
