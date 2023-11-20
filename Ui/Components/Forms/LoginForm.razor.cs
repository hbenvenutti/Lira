using Lira.Domain.Authentication.Manager;
using Microsoft.AspNetCore.Components.Forms;

namespace Lira.Ui.Components.Forms;

public partial class LoginForm
{
    public readonly ManagerCredentials Credentials = new();

    public void Submit(EditContext context)
    {
        Console.WriteLine("Login form submitted");
        Console.WriteLine($"Username: {Credentials.Username}");
        Console.WriteLine($"Password: {Credentials.Password}");
        Console.WriteLine($"Context: {context.Model}");
        Console.WriteLine($"Password: {((ManagerCredentials)context.Model).Password}");
        Console.WriteLine($"Username: {((ManagerCredentials)context.Model).Username}");
    }
}
