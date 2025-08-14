using FixGo.Helpers;
using FixGo.Models;
using FixGo.Services;

namespace FixGo.Views;

public partial class MainMenuPage : ContentPage
{
    public MainMenuPage()
    {
        InitializeComponent();
        CargarDatosDeUsuario();
        CargarCategoriasDesdeApi();
    }

    private readonly Dictionary<int, string> categoriaIconos = new()
    {
        { 13, "potted_50.svg" },          // Jardinería
        { 14, "plumbing_50.svg" },        // Fontanería
        { 15, "electrical_services_50.svg" }, // Electricista
        { 16, "construction_50.svg" },    // Albañil
        { 17, "carpenter_50.svg" },       // Carpintería
        { 18, "key_50.svg" }            // Mantenimiento
    };

    private const string IconoDefault = "apps.svg";

    private void CargarDatosDeUsuario()
    {
        userNameLabel.Text = AppSession.NombreCompleto ?? "Usuario";
        userLocationLabel.Text = AppSession.Direccion ?? "Ubicación no definida";
    }

    private async void CargarCategoriasDesdeApi()
    {
        var api = new ApiService();
        var categorias = await api.GetCategoriasAsync();

        if (categorias == null || categorias.Count == 0)
        {
            await DisplayAlert("Error", "No se encontraron categorías", "OK");
            return;
        }

        servicesGrid.Children.Clear();

        int columnas = 2;
        int row = 0, col = 0;

        foreach (var categoria in categorias)
        {
            string icono = categoriaIconos.ContainsKey(categoria.IdCategoria)
                ? categoriaIconos[categoria.IdCategoria]
                : IconoDefault;

            var boton = new ServiceButton
            {
                Icon = icono,
                Label = categoria.Nombre,
                FontSize = 14,
                Command = new Command(() => OnCategoriaSeleccionada(categoria)),
                CommandParameter = categoria
            };

            servicesGrid.Add(boton, col, row);

            col++;
            if (col >= columnas)
            {
                col = 0;
                row++;
            }
        }
    }

    private async void OnCategoriaSeleccionada(Categoria categoria)
    {
        await Navigation.PushAsync(new RequestServicePage(categoria.IdCategoria, categoria.Nombre));
    }

    private void OnNotificationsClicked(object sender, EventArgs e)
    {
        // Aquí podés abrir una página de notificaciones o mostrar un mensaje
        DisplayAlert("Notificaciones", "Aquí irán tus notificaciones más adelante.", "OK");
    }
}