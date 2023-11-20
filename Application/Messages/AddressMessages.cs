namespace Lira.Application.Messages;

public readonly struct AddressMessages
{
    public static string StreetIsInvalid => "Street is invalid.";
    public static string AddressNumberIsInvalid => "Number is invalid.";
    public static string NeighborhoodIsInvalid => "Neighborhood is invalid.";
    public static string CityIsInvalid => "City is invalid.";
    public static string ZipCodeIsInvalid => "Zip code is invalid.";
    public static string StateIsInvalid => "State is invalid.";
}
