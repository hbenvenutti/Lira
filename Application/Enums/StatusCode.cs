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

    # region ---- data ---------------------------------------------------------

    PasswordsDoNotMatch = 440001,
    PasswordIsInvalid = 440002,
    NameIsInvalid = 440003,
    SurnameIsInvalid = 440004,
    CpfIsInvalid = 440005,
    UsernameIsInvalid = 440006,
    SeveralInvalidFields = 440090,
    AdminCodeIsInvalid = 440099,

    # endregion

    # region ---- error --------------------------------------------------------

    AdminAlreadyExists = 540001,
    UnexpectedError = 950001

    # endregion
}
