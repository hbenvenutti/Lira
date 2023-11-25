namespace Lira.Application.Enums;

public enum AppStatusCode
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
    PersonNotFound = 340002,
    OrixaNotFound = 340008,
    PersonOrixaNotFound = 340009,

    # endregion

    # region ---- invalid data -------------------------------------------------

    PasswordsDoNotMatch = 440001,
    InvalidPassword = 440002,
    InvalidName = 440003,
    InvalidSurname = 440004,
    InvalidCpf = 440005,
    InvalidUsername = 440006,
    InvalidUf = 440007,
    InvalidZipCode = 440008,
    InvalidStreet = 440010,
    InvalidPhone = 440011,
    InvalidAddressNumber = 440012,
    InvalidNeighborhood = 440013,
    InvalidCity = 440014,
    InvalidEmailAddress = 440015,

    SeveralInvalidFields = 440090,
    InvalidAdminCode = 440099,
    OneOrMoreSpecificationsFailed = 440100,

    # endregion

    # region ---- conflict -----------------------------------------------------

    AdminAlreadyExists = 540001,
    PersonAlreadyExists = 540002,
    UsernameAlreadyExists = 540003,
    EmailAlreadyExists = 540004,

    # endregion

    # region ---- error --------------------------------------------------------

    UnexpectedError = 950001

    # endregion
}
