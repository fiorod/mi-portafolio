using FixGo.Helpers;
using FixGo.Models;
using FixGo.Services;
using Microsoft.Maui.Controls.Shapes;

namespace FixGo.Views;

public partial class SearchWorkersPage : ContentPage
{
    private List<Categoria> categorias = new();
    public SearchWorkersPage()
    {
        InitializeComponent();
        CargarDatosDeUsuario();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarCategorias();
        await LoadWorkers(); 
    }

    private async Task CargarCategorias()
    {
        var api = new ApiService();
        categorias = await api.GetCategoriasAsync();

        if (categorias != null)
        {
            categoryPicker.ItemsSource = categorias.Select(c => c.Nombre).ToList();
        }
    }

    private void CargarDatosDeUsuario()
    {
        userNameLabel.Text = AppSession.NombreCompleto ?? "Usuario";
        userLocationLabel.Text = AppSession.Direccion ?? AppSession.Empresa;
    }

    private async Task LoadWorkers(int? idCategoria = null)
    {
        workersStack.Children.Clear();

        var api = new ApiService();
        var trabajadores = await api.GetTrabajadoresAsync(idCategoria); // debes implementar esto

        foreach (var trabajador in trabajadores)
        {
            var card = new VerticalStackLayout
            {
                Padding = 15,
                BackgroundColor = Color.FromArgb("#F8FAFC"),
                Margin = new Thickness(0),
                Spacing = 8,
                Shadow = new Shadow { Radius = 10, Opacity = 0.2f }
            };

            card.Children.Add(new Label { Text = $"Trabajador: {trabajador.NombreCompleto}", FontAttributes = FontAttributes.Bold });
            card.Children.Add(new Label { Text = $"Empresa: {trabajador.Empresa}" });
            card.Children.Add(new Label { Text = $"Teléfono: {trabajador.Telefono}" });
            card.Children.Add(new Label { Text = $"Categoría: {trabajador.Categoria}" });
            card.Children.Add(new Label { Text = "Reseñas", FontAttributes = FontAttributes.Bold });

            foreach (var resena in await GetResenasDesdeApiAsync(trabajador.IdTrabajador))
            {
                var resenaCard = new Border
                {
                    BackgroundColor = Colors.White,
                    StrokeShape = new RoundRectangle { CornerRadius = 15 },
                    Padding = 10,
                    Stroke = Color.FromArgb("#E0E0E0"),
                    Content = new VerticalStackLayout
                    {
                        Spacing = 4,
                        Children =
                    {
                        new HorizontalStackLayout
                        {
                            Spacing = 5,
                            Children =
                            {
                                new Label
                                {
                                    Text = resena.titulo,
                                    FontAttributes = FontAttributes.Bold,
                                    VerticalOptions = LayoutOptions.Center,
                                    FontSize = 12
                                },
                                new Label
                                {
                                    Text = resena.calificacion.ToString("0.0"),
                                    FontAttributes = FontAttributes.Bold,
                                    VerticalOptions = LayoutOptions.Center,
                                    FontSize = 12
                                },
                                new Image
                                {
                                    Source = "star_20.svg", 
                                    HeightRequest = 16,
                                    WidthRequest = 16,
                                    VerticalOptions = LayoutOptions.Center
                                }
                            }
                        },

                        new Label
                        {
                            Text = resena.descripcion,
                            FontSize = 10,
                            TextColor = Colors.Gray
                        }
                    }
                    },
                    Shadow = new Shadow { Brush = Brush.Black, Radius = 5, Offset = new Point(2, 2), Opacity = 0.2f }
                };

                card.Children.Add(resenaCard);
            }

            workersStack.Children.Add(card);
        }
    }

    private async void OnBuscarClicked(object sender, EventArgs e)
    {
        var selectedIndex = categoryPicker.SelectedIndex;
        int? idCategoria = selectedIndex >= 0 ? categorias[selectedIndex].IdCategoria : null;

        await LoadWorkers(idCategoria);
    }

    private async void OnLimpiarClicked(object sender, EventArgs e)
    {
        categoryPicker.SelectedIndex = -1;
        await LoadWorkers(null);
    }

    private async Task<List<FeedbackResponse>> GetResenasDesdeApiAsync(int idTrabajador)
    {
        try
        {
            ApiService api = new ApiService();

            var request = new FeedbackRequest
            {
                IdTrabajador = idTrabajador
            };

            var resennas = await api.GetFeedbackAsync(request);

            var listaResenas = resennas.Select(r => new FeedbackResponse
            {
                titulo = r.titulo,
                calificacion = r.calificacion,
                descripcion = r.descripcion
            }).ToList();

            return listaResenas;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error al obtener reseñas", ex.Message, "OK");
            return new List<FeedbackResponse>();
        }
    }

    private async void OnNotificationsClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Notificaciones", "Aquí aparecerán las notificaciones más adelante.", "OK");
    }
}