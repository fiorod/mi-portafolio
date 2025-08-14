namespace FixGo.Views;

public partial class RegisterTypePage : ContentPage
{
    public RegisterTypePage()
    {
        InitializeComponent();
    }

    private async void OnClienteClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ClientRegisterPage());
    }

    private async void OnTrabajadorClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WorkerRegisterPage());
    }
}
