using BrazilianTypes.Types;
using Lira.Application.Enums;
using Lira.Application.Messages;

namespace Lira.Application.Specifications.Person;

public class PersonSpecification : ISpecification<PersonSpecificationDto>
{
    # region ---- properties ---------------------------------------------------

    public AppStatusCode AppStatusCode { get; set; } = AppStatusCode.Empty;
    public List<string> ErrorMessages { get; } = new List<string>();

    # endregion

    # region ---- specification ------------------------------------------------

    public bool IsSatisfiedBy(PersonSpecificationDto data)
    {
        var errors = 0;
        var isSatisfiedBy = true;

        if (!Name.TryParse(data.Name, out _))
        {
            AppStatusCode = AppStatusCode.InvalidName;
            ErrorMessages.Add(item: PersonMessages.InvalidName);
            errors++;

            isSatisfiedBy = false;
        }

        if (!Name.TryParse(data.Surname, out _))
        {
            AppStatusCode = AppStatusCode.InvalidSurname;
            ErrorMessages.Add(item: PersonMessages.InvalidSurname);
            errors++;

            isSatisfiedBy = false;
        }

        if (!Cpf.TryParse(data.Cpf, out _))
        {
            AppStatusCode = AppStatusCode.InvalidCpf;
            ErrorMessages.Add(item: PersonMessages.InvalidDocument);
            errors++;

            isSatisfiedBy = false;
        }

        if (errors > 1) { AppStatusCode = AppStatusCode.SeveralInvalidFields; }

        return isSatisfiedBy;
    }

    # endregion
}
