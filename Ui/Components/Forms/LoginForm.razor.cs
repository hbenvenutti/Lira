namespace Lira.Ui.Components.Forms;

public partial class LoginForm
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    private void Submit()
    {
        Console.WriteLine("Login form submitted");
        Console.WriteLine($"Username: {Username}");
        Console.WriteLine($"Password: {Password}");
    }
}
