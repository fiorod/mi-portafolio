using FixGo.Helpers;
using FixGo.Models;
using FixGo.Services;

namespace FixGo.Views;

public partial class EditarPerfilPage : ContentPage
{
    public EditarPerfilPage()
    {
        InitializeComponent();
        CargarDatosDesdeSesion();
        CargarDatosDeUsuario();
    }

    private void CargarDatosDeUsuario()
    {
        userNameLabel.Text = AppSession.NombreCompleto ?? "Usuario";
        userLocationLabel.Text = AppSession.Direccion ?? "Ubicación no definida";
    }

    private void CargarDatosDesdeSesion()
    {
        // Mostrar nombre completo
        usernameEntry.Text = AppSession.NombreCompleto;

        // Cargar datos en campos
        TelefonoEntry.Text = AppSession.Telefono;
        DireccionEntry.Text = AppSession.Direccion;
        emailEntry.Text = AppSession.Email;

        //SenasEntry.Text = AppSession.Senas ?? string.Empty;
        //NumeroCasaEntry.Text = AppSession.NumeroCasa ?? string.Empty;
    }

    private async void OnGuardarPerfilClicked(object sender, EventArgs e)
    {
        var request = new UpdateUserRequest
        {
            usuario = new UsuarioDto
            {
                ID = AppSession.UsuarioID,
                NombreUsuario = AppSession.NombreUsuario,
                //Contrasenia = AppSession.Contrasenia ?? "temporal123", // si no se permite omitir
                Email = AppSession.Email,
                PersonaID = AppSession.PersonaID,
                Persona = new PersonaDto
                {
                    IdPersona = AppSession.PersonaID,
                    nombre = ObtenerNombreDesdeCompleto(AppSession.NombreCompleto),
                    nombre2 = "",
                    apellido1 = ObtenerApellidoDesdeCompleto(AppSession.NombreCompleto, 1),
                    apellido2 = ObtenerApellidoDesdeCompleto(AppSession.NombreCompleto, 2)
                }
            },
            TipoRol = AppSession.TipoRol,
            Telefono = TelefonoEntry.Text,
            Direccion = DireccionEntry.Text,
            //Senas = SenasEntry.Text,
            //NumeroCasa = NumeroCasaEntry.Text,
            Empresa = AppSession.Empresa,
            CategoriaID = 1, // reemplazar si lo tienes en sesión
            RolID = AppSession.RolID ?? 1
        };
        ApiService api = new ApiService();
        var response = await api.UpdateProfileAsync(request);

        if (response.resultado)
        {
            // Actualizar la sesión
            AppSession.Telefono = TelefonoEntry.Text;
            AppSession.Direccion = DireccionEntry.Text;
            //AppSession.Senas = SenasEntry.Text;
            //AppSession.NumeroCasa = NumeroCasaEntry.Text;

            await DisplayAlert("Éxito", "Perfil actualizado correctamente", "OK");
        }
        else
        {
            string errorMsg = response.mensaje?.FirstOrDefault() ?? "Error desconocido";
            await DisplayAlert("Error", $"No se pudo actualizar el perfil: {errorMsg}", "OK");
        }
    }

    private string ObtenerNombreDesdeCompleto(string nombreCompleto)
    {
        return nombreCompleto.Split(' ').FirstOrDefault() ?? "";
    }

    private string ObtenerApellidoDesdeCompleto(string nombreCompleto, int indicador)
    {
        return nombreCompleto.Split(' ').Skip(indicador).FirstOrDefault() ?? "";
    }

    private async void OnNotificationsClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Notificaciones", "Aquí aparecerán las notificaciones más adelante.", "OK");
    }
}