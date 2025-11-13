using Microsoft.AspNetCore.Components;
namespace BlazorPlayGround.Components.Pages._07_CascadingParameters;

public partial class CascadingParametersDemo : ComponentBase
{
    private CascadingState CascadingStateValue { get; set; } = new CascadingState();

    private void OnThemeChanged(ChangeEventArgs e)
    {
        CascadingStateValue.Theme = e.Value?.ToString() ?? "light";
    }

    private void OnTextSizeChanged(ChangeEventArgs e)
    {

    }

    public class CascadingState
    {
        public string Theme { get; set; } = "light";
        public string TextSize { get; set; } = "medium";
    }
}