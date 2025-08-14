using FixGo.Helpers;

namespace FixGo.Views;

public partial class MainWorkPage : ContentPage
{
    public MainWorkPage()
    {
        InitializeComponent();
        userNameLabel.Text = AppSession.NombreCompleto;
        userLocationLabel.Text = AppSession.Empresa;
    }

    private void OnNotificationsClicked(object sender, EventArgs e)
    {
        DisplayAlert("Notificaciones", "Aquí se mostrarán tus notificaciones.", "OK");
    }

    private async void OnTusTrabajosClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TaskServicesPage());
        //Application.Current.MainPage = new NavigationPage(new TaskServicesPage());
        //Application.Current.MainPage = new WorkerShell();
        //await Shell.Current.GoToAsync("TaskServicesPage");
    }

    private async void OnNuevosTrabajosClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AssignWorkerPage());
        //Application.Current.MainPage = new NavigationPage(new AssignWorkerPage());
        //Application.Current.MainPage = new WorkerShell();
        //await Shell.Current.GoToAsync("AssignWorkerPage");
    }
}