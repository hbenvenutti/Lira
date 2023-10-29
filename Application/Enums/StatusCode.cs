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
    SeveralInvalidFields = 440090,
    AdminCodeIsInvalid = 440099,

    # endregion

    # region ---- error --------------------------------------------------------

    UnexpectedError = 950001

    # endregion
}
