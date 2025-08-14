using FixGo.Helpers;
using FixGo.Models;
using FixGo.Services;
using System.Data;

namespace FixGo.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
	{
		InitializeComponent();
	}

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var username = usernameEntry.Text?.Trim();
        var password = passwordEntry.Text?.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Por favor ingrese usuario y contraseña.", "OK");
            return;
        }

        var api = new ApiService();
        var request = new LoginRequest
        {
            NombreUsuario = username,
            Contrasenia = password
        };

        var response = await api.LoginUserAsync(request);

        if (response != null && response.resultado)
        {
            switch (response.TipoRol.ToLower())
            {
                case "cliente":
                    saveGlobal(response);
                    Application.Current.Windows[0].Page = new AppShell();
                    break;
                case "trabajador":
                    saveGlobal(response);
                    Application.Current.Windows[0].Page = new WorkerShell();
                    break;
                case "administrador":
                    saveGlobal(response);
                    Application.Current.Windows[0].Page = new AdminShell();
                    break;
                default:
                    await DisplayAlert("Aviso", "Rol desconocido: " + response.TipoRol, "OK");
                    break;
            }
        }
        else
        {
            var errores = response?.error?.Select(e => e.Message) ?? new List<string> { "Login fallido" };
            await DisplayAlert("Error", string.Join("\n", errores), "OK");
        }
    }

    public void saveGlobal(LoginResponse response)
    {
        AppSession.UsuarioID = response.UsuarioID;
        AppSession.PersonaID = response.PersonaID;
        AppSession.NombreUsuario = response.NombreUsuario;
        AppSession.NombreCompleto = response.NombreCompleto;
        AppSession.RolID = response.RolID;
        AppSession.TipoRol = response.TipoRol;
        AppSession.Email = response.Email;
        AppSession.Telefono = response.Telefono;
        AppSession.Direccion = response.Direccion;
        AppSession.Empresa = response.Empresa;
    }

    private async void OnGoToRegister(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterTypePage());
    }
}