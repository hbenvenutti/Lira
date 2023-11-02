namespace Lira.Application.Enums;

public enum StatusCode
{
    Empty = 0,

    # region ---- login --------------------------------------------------------

    SignInSuccess = 120001,
    SignInFailed  = 140001,

    # endregion

    # region ---- created ------------------------------------------------------

    CreatedOne = 220001,
    CreatedMany = 220002,
    CreatedTransaction = 220003,
    CreatedWithErrors = 240001,

    # endregion

    # region ---- missing resources --------------------------------------------

    ManagerNotFound = 340001,

    # endregion

    # region ---- invalid data -------------------------------------------------

    PasswordsDoNotMatch = 440001,
    PasswordIsInvalid = 440002,
    NameIsInvalid = 440003,
    SurnameIsInvalid = 440004,
    CpfIsInvalid = 440005,
    UsernameIsInvalid = 440006,
    UfIsInvalid = 440007,
    ZipCodeIsInvalid = 440008,
    StreetIsInvalid = 440010,

    IdIsInvalid = 440089,
    SeveralInvalidFields = 440090,
    AdminCodeIsInvalid = 440099,
    OneOrMoreSpecificationsFailed = 440100,

    # endregion

    # region ---- conflict -----------------------------------------------------

    AdminAlreadyExists = 540001,
    PersonAlreadyExists = 540002,

    # endregion

    # region ---- error --------------------------------------------------------

    UnexpectedError = 950001

    # endregion
}
