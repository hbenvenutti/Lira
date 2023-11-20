using Lira.Ui.Texts;
using Microsoft.AspNetCore.Components;

namespace Lira.Ui.Components.Buttons;

public partial class ConfirmButton
{
    [Parameter] public string Text { get; set; } = "Are you sure?";
    [Parameter] public string Type { get; set; } = @InputTypes.Button;
}
