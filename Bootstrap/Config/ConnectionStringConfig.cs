using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lira.Bootstrap.Config;

[ExcludeFromCodeCoverage]
public class ConnectionStringConfig
{
    public const string SectionName = "ConnectionStrings";

    [Required]
    public required string DefaultConnection { get; init; }
}
