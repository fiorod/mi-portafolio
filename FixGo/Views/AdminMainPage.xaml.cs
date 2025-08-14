using FixGo.Helpers;

namespace FixGo.Views;

public partial class AdminMainPage : ContentPage
{
    public AdminMainPage()
    {
        InitializeComponent();
        CargarDatosDeUsuario();
    }

    private async void OnWorkerProfilesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WorkersListPage());
    }

    private void CargarDatosDeUsuario()
    {
        userNameLabel.Text = AppSession.NombreCompleto ?? "Usuario";
        userLocationLabel.Text = AppSession.Direccion ?? "Ubicación no definida";
    }

    private async void OnClientProfilesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ClientsListPage());
    }

    private void OnNotificationsClicked(object sender, EventArgs e)
    {
        // Aquí podrías abrir una página de notificaciones o mostrar un mensaje
        DisplayAlert("Notificaciones", "Aquí irán tus notificaciones más adelante.", "OK");
    }
}

