using Microsoft.AspNetCore.Components;

namespace Lira.Ui.Components.Inputs;

public partial class LoginInput
{
    [Parameter] public required string Type { get; set; }
    [Parameter] public required string Name { get; set; }
    [Parameter] public required string Placeholder { get; set; }
    [Parameter] public required string Id { get; set; }
}
