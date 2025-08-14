using FixGo.Models;

namespace FixGo.Views;

public partial class ServiceDetailPage : ContentPage
{
    private Ticket currentTicket;
    public ServiceDetailPage(Ticket ticket)
    {
        InitializeComponent();
        currentTicket = ticket;

        lblServicio.Text = $"Servicio: {ticket.Servicio}";
        lblSubcategoria.Text = $"Subcategor�a: {ticket.Subcategoria}";
        lblDia.Text = $"D�a: {ticket.Dia}";
        lblHora.Text = $"Hora: {ticket.Hora}";
    }

    private async void OnCompleteClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Completado", "El servicio fue marcado como finalizado.", "OK");
        // Aqu� ir�a una llamada real al API para actualizar el estado
        await Navigation.PopAsync(); // Regresar a la lista
    }
}