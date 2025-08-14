using FixGo.Models;
using FixGo.Services;

namespace FixGo.Views;

public partial class ClientRegisterPage : ContentPage
{
    private readonly bool _esAdmin;

    public ClientRegisterPage(bool esAdmin = false)
    {
        InitializeComponent();
        _esAdmin = esAdmin;

        if (_esAdmin)
        {
            pageTitle.Text = "Agregar Cliente";
            this.Title = "Agregar Cliente";
        }
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        var request = new RegisterRequest
        {
            TipoRol = "Cliente",
            Telefono = phoneEntry.Text ?? "",
            Direccion = addressEntry.Text ?? "",
            Senas = senasEntry.Text ?? "",
            NumeroCasa = houseEntry.Text ?? "",
            usuario = new Usuario
            {
                NombreUsuario = usernameEntry.Text ?? "",
                Contrasenia = passwordEntry.Text ?? "",
                Email = emailEntry.Text ?? "",
                Persona = new Persona
                {
                    nombre = nameEntry.Text ?? "",
                    apellido1 = "",
                    apellido2 = ""
                }
            }

        };


        var api = new ApiService();
        var resultado = await api.RegisterUserAsync(request);

        if (resultado != null && resultado.resultado)
        {
            if (_esAdmin)
            {
                await DisplayAlert("Éxito", "Cliente creado correctamente.", "OK");
                await Navigation.PopAsync(); // Volver a ClientesListPage
            }
            else
            {
                await DisplayAlert("Éxito", "Registro completado.", "OK");
                await Navigation.PushAsync(new LoginPage());
            }
        }
        else
        {
            var errorMsg = resultado?.error?.FirstOrDefault()?.Message ?? "Error desconocido.";
            await DisplayAlert("Error", errorMsg, "OK");
        }
    }
    private async void OnAgregarClienteClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ClientRegisterPage(true));
    }

}
