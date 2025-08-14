using FixGo.Helpers;
using FixGo.Services;

namespace FixGo.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
        CargarDatosDeUsuario();
        LoadProfile();
    }

    private void CargarDatosDeUsuario()
    {
        userNameLabel.Text = AppSession.NombreCompleto ?? "Usuario";
        userLocationLabel.Text = AppSession.Direccion ?? "Ubicaci�n no definida";
    }

    private void LoadProfile()
    {
        nameEntry.Text = AppSession.NombreCompleto;
        emailEntry.Text = AppSession.Email;
        phoneEntry.Text = AppSession.Telefono;
        dirLabel.Text = AppSession.Direccion;
        //compannyEntry.Text = AppSession.Empresa;
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditarPerfilPage());
    }

    private async void OnChangePasswordClicked(object sender, EventArgs e)
    {
        //await DisplayAlert("Cambiar Contrase�a", "Esta funci�n estar� disponible pr�ximamente.", "OK");
        await Navigation.PushAsync(new ChangePasswordPage());
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert("Cerrar Sesi�n", "�Est�s seguro que deseas salir?", "S�", "Cancelar");

        if (confirmar)
        {
            AppSession.LimpiarSesion(); 
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }

    private async void OnNotificationsClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Notificaciones", "Aqu� aparecer�n las notificaciones m�s adelante.", "OK");
    }
}