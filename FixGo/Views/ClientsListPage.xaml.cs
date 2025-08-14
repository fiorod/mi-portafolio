using FixGo.Models;
using FixGo.Services;

namespace FixGo.Views;

public partial class ClientsListPage : ContentPage
{
    private List<FixGo.Models.Cliente> clientes = new();



    public ClientsListPage()
    {
        InitializeComponent();
        CargarClientes();
    }

    private async void CargarClientes()
    {
        var api = new ApiService();
        clientes = await api.GetClientesAsync();
        clientesCollection.ItemsSource = clientes;
    }

    private async void OnMenuClicked(object sender, EventArgs e)
    {
        var imageButton = (ImageButton)sender;
        var cliente = (Cliente)imageButton.CommandParameter;

        string opcion = await DisplayActionSheet("Opciones", "Cancelar", null, "Eliminar");

        if (opcion == "Eliminar")
        {
            var confirmar = await DisplayAlert("Confirmar", $"¿Desea eliminar a {cliente.NombreCompleto}?", "Sí", "No");
            if (confirmar)
            {
                var api = new ApiService();
                var eliminado = await api.EliminarClienteAsync(cliente.IdCliente);

                if (eliminado)
                {
                    clientes.Remove(cliente);
                    clientesCollection.ItemsSource = null;
                    clientesCollection.ItemsSource = clientes;
                    await DisplayAlert("Éxito", "Cliente eliminado", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo eliminar", "OK");
                }
            }
        }
    }

    private async void OnAgregarClienteClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ClientRegisterPage(true));
    }
}
