using System.ComponentModel.DataAnnotations;

namespace Lira.Bootstrap.Config;

public class ConnectionStringConfig
{
    public const string SectionName = "ConnectionStrings";

    [Required]
    public required string DefaultConnection { get; init; }
}
