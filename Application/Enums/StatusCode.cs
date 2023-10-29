namespace Lira.Application.Enums;

public enum StatusCode
{
    Empty = 0,

    # region ---- login --------------------------------------------------------

    SignInSuccess = 120001,
    SignInFailed  = 140001,

    # endregion

    # region ---- error --------------------------------------------------------

    UnexpectedError = 950001

    # endregion
}
