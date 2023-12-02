namespace Lira.Common.Enums;

public enum AppStatusCode
{
    Empty = 0,

    # region ---- login 10_xx-xxx ----------------------------------------------

    SignInSuccess = 10_00_200,
    SignInFailed  = 10_00_404,
    PasswordsDoNotMatch = 10_10_400,
    InvalidPassword = 10_11_400,

    # endregion

    # region ---- generic 11-xx-xxx --------------------------------------------

    CreatedOne = 11_00_201,
    CreatedMany = 11_01_201,
    CreatedTransaction = 11_03_201,
    CreatedWithErrors = 11_04_201,

    SeveralInvalidFields = 11_01_400,

    # endregion

    # region ---- person 20-xx-xxx ---------------------------------------------

    CreatedPerson = 20_00_201,
    UpdatedPerson = 20_01_200,
    DeletedPerson = 20_02_204,

    InvalidCpf = 20_10_400,
    InvalidName = 20_11_400,
    InvalidSurname = 20_12_400,
    CpfInUse = 20_10_409,

    PersonNotFound = 20_20_404,
    PersonAlreadyExists = 20_20_409,

    # endregion

    # region ---- manager 21-xx-xxx --------------------------------------------

    CreatedManager = 21_00_201,
    UpdatedManager = 21_01_200,
    DeletedManager = 21_02_204,

    InvalidUsername = 21_10_400,
    UsernameAlreadyExists = 21_10_409,

    ManagerNotFound = 21_20_404,

    AdminAlreadyExists = 21_90_409,
    InvalidAdminCode = 21_90_400,

    # endregion

    # region ---- email 22_xx-xxx ----------------------------------------------

    CreatedEmail = 22_00_201,
    UpdatedEmail = 22_01_200,
    DeletedEmail = 22_02_204,
    InvalidEmailAddress = 22_10_400,
    EmailAlreadyExists = 22_10_409,
    EmailNotFound = 22_20_404,

    # endregion

    # region ---- address 23_xx-xxx --------------------------------------------

    CreatedAddress = 23_00_201,
    UpdatedAddress = 23_01_200,
    DeletedAddress = 23_02_204,

    InvalidUf = 23_10_400,
    InvalidStreet = 23_11_400,
    InvalidZipCode = 23_12_400,
    InvalidNeighborhood = 23_13_400,
    InvalidAddressNumber = 23_14_400,
    InvalidCity = 23_15_400,

    AddressNotFound = 23_20_404,

    # endregion

    # region ---- phone 24-xx-xxx ----------------------------------------------

    CreatedPhone = 24_00_201,
    UpdatedPhone = 24_01_200,
    DeletedPhone = 24_02_204,

    InvalidPhone = 24_10_400,

    PhoneNotFound = 24_20_404,

    # endregion

    # region ---- orixa 25-xx-xxx ----------------------------------------------

    CreatedOrixa = 25_00_201,
    UpdatedOrixa = 25_01_200,
    DeletedOrixa = 25_02_204,

    InvalidOrixa = 25_10_400,

    OrixaNotFound = 25_20_404,

    # endregion

    # region ---- person orixa 26-xx-xxx ---------------------------------------

    CreatedPersonOrixa = 26_00_201,
    UpdatedPersonOrixa = 26_01_200,
    DeletedPersonOrixa = 26_02_204,

    InvalidPersonOrixa = 26_10_400,

    PersonOrixaNotFound = 26_20_404,

    # endregion

    # region ---- error 9x_xx-xxx ----------------------------------------------

    UnexpectedError = 90_00_500,
    BadGateway = 90_00_502

    # endregion
}
