namespace Lira.Web.Pages.SignIn;

public partial class SignInPage
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string? ErrorMessage { get; set; }
    
    private async Task LoginUser()
    {
        try
        {
            // var result = await _authService.LoginUser(Email, Password);
            //
            // if (result.Succeeded)
            // {
            //     _navigationManager.NavigateTo("/");
            // }
            // else
            // {
            //     ErrorMessage = result.Errors.First().Description;
            // }
        }

        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}
