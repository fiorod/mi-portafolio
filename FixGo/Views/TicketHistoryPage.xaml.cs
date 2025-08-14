using CommunityToolkit.Maui.Views;
using FixGo.Helpers;
using FixGo.Models;
using FixGo.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;

namespace FixGo.Views;

public partial class TicketHistoryPage : ContentPage
{
    private readonly List<Ticket> tickets = new();
    public TicketHistoryPage()
    {
        InitializeComponent();
        CargarDatosDeUsuario();
        //LoadTickets();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        LoadTickets();
    }

    private void CargarDatosDeUsuario()
    {
        userNameLabel.Text = AppSession.NombreCompleto ?? "Usuario";
        userLocationLabel.Text = AppSession.Direccion ?? "Ubicaci�n no definida";
    }

    private async void LoadTickets()
    {
        try
        {
            int _clienteId = 0;
            if (AppSession.RolID == null)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                _clienteId = (int)AppSession.RolID;
            }
            var api = new ApiService();
            var ticketsApi = await api.ObtenerHistorialTicketsAsync(_clienteId);
            ticketContainer.Children.Clear();

            foreach (var t in ticketsApi)
            {
                var ticket = new Ticket(
                    idPeticion: t.idPeticion,
                    idCliente: t.idCliente,
                    descripcion: t.descripcion,
                    idCita: t.idCita,
                    descripcionPeticion: t.descripcionPeticion,
                    direccionCliente: t.direccionCliente,
                    estado: t.estado,
                    Categoria: t.Categoria,
                    SubCategoria: t.SubCategoria,
                    fecha: DateTime.Parse(t.fechaCita).ToString("dd/MM/yyyy"),
                    hora: t.horaCita[..5],
                    Encargado: t.nombreEncargado,
                    empresa: t.empresaTrabjador,
                    idTiquete: t.idTiquete,
                    idTrabajador : t.idTrabajador
                );

                ticketContainer.Children.Add(CreateTicketCard(ticket));
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo cargar el historial: {ex.Message}", "OK");
        }
    }

    private View CreateTicketCard(Ticket ticket)
    {
        var grid = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto }
            },
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto }
            }
        };

        var left = new VerticalStackLayout
        {
            Children =
            {
                new Label { Text = $"Categoria: {ticket.Categoria}" , FontAttributes = FontAttributes.Bold},
                new Label { Text = $"Subcategoria: {ticket.SubCategoria}" },
                new Label { Text = $"Descripcion: {ticket.descripcionPeticion}" },
                new Label { Text = $"Fecha: {ticket.fecha}", FontAttributes = FontAttributes.Bold },
                new Label { Text = $"Hora: {ticket.hora}" },
                new Label { Text = $"Cliente: {ticket.Encargado}" }
            }
        };
        Grid.SetColumn(left, 0);
        Grid.SetRow(left, 0);
        grid.Children.Add(left);

        //var right = new VerticalStackLayout
        //{
        //    HorizontalOptions = LayoutOptions.End,
        //    Children =
        //    {
        //        new Label { Text = $"Fecha: {ticket.fecha}", FontAttributes = FontAttributes.Bold },
        //        new Label { Text = $"Hora: {ticket.hora}" },
        //        new Label { Text = $"Cliente: {ticket.Encargado}" }
        //    }
        //};
        //Grid.SetColumn(right, 0);
        //Grid.SetRow(right, 0);
        //grid.Children.Add(right);

        // �cono de eliminar
        var deleteButton = new ImageButton
        {
            Source = "delete_50.svg", 
            WidthRequest = 6,
            HeightRequest = 6,
            BackgroundColor = Colors.Transparent,
            HorizontalOptions = LayoutOptions.End,
        };

        Grid.SetColumn(deleteButton, 0);
        Grid.SetRow(deleteButton, 1); // debajo del stack derecho
        grid.Children.Add(deleteButton);

        // �cono de rese�a
        var reseniaButton = new ImageButton
        {
            Source = "star_50.svg",
            WidthRequest = 6,
            HeightRequest = 6,
            BackgroundColor = Colors.Transparent,
            HorizontalOptions = LayoutOptions.End
        };

        Grid.SetColumn(reseniaButton, 1);
        Grid.SetRow(reseniaButton, 1); // debajo del stack derecho
        grid.Children.Add(reseniaButton);

        Border? border = null;

        deleteButton.Clicked += async (s, e) =>
        {
            bool confirm = await DisplayAlert("Eliminar", "�Dese�s eliminar este tiquete?", "S�", "No");
            if (confirm && border != null)
            {
                var api = new ApiService();
                bool eliminado = await api.EliminarTicketAsync(ticket.idTiquete);
                if (eliminado)
                {
                    ticketContainer.Children.Remove(border);
                    tickets.Remove(ticket);
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo eliminar el tiquete. Verific� el ID.", "OK");
                }
            }
        };

        reseniaButton.Clicked += (s, e) =>
        {
            var popup = new ResenaPopup(ticket.idTrabajador);
            this.ShowPopup(popup);
        };

        border = new Border
        {
            Stroke = Colors.LightGray,
            StrokeShape = new RoundRectangle { CornerRadius = 20 },
            BackgroundColor = Color.FromArgb("#F8FAFC"),
            Padding = 15,
            Content = grid
        };

        return border;
    }

    private async void OnNotificationsClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Notificaciones", "Aqu� ir�n tus notificaciones pronto.", "OK");
    }

    private record Ticket(int idTiquete, int idPeticion, int idCliente, string descripcion, int idTrabajador, int idCita, string fecha, string hora, string SubCategoria, string Categoria, string descripcionPeticion, string direccionCliente, string Encargado, string empresa, string estado);
}