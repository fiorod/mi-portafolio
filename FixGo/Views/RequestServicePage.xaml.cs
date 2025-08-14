using FixGo.Helpers;
using FixGo.Models;
using FixGo.Services;
using Microsoft.Maui.Platform;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using System;

namespace FixGo.Views;

public partial class RequestServicePage : ContentPage
{
    private string selectedService;
    private readonly int _idCategoria;
    private readonly string _nombreCategoria;
    private List<Subcategoria> subcategorias = new();

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (AppSession.RolID == null)
        {
            // Redirige al Login si no hay sesión activa
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
            //SyncDayStates();
    }

    public RequestServicePage(int idCategoria, string nombreCategoria)
    {
        InitializeComponent();
        CargarDatosDeUsuario();
        _idCategoria = idCategoria;
        _nombreCategoria = nombreCategoria;

        Title = $"Servicio: {nombreCategoria}";
        serviceEntry.Text = nombreCategoria;

        CargarSubcategoriasDesdeApi(_idCategoria);
    }

    private void CargarDatosDeUsuario()
    {
        userNameLabel.Text = AppSession.NombreCompleto ?? "Usuario";
        userLocationLabel.Text = AppSession.Direccion ?? "Ubicación no definida";
    }

    private async void CargarSubcategoriasDesdeApi(int idCategoria)
    {
        var api = new ApiService();
        subcategorias = await api.GetSubcategoriasPorCategoriaAsync(idCategoria);

        if (subcategorias != null && subcategorias.Count > 0)
        {
            subcategoryPicker.ItemsSource = subcategorias.Select(s => s.Nombre).ToList();
            subcategoryPicker.SelectedIndex = 0;
        }
        else
        {
            await DisplayAlert("Sin opciones", "No hay subcategorías disponibles.", "OK");
        }
    }

    private void OnDayCheckChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender == oneDayCheck) { onefechaPicker.IsEnabled = e.Value; onehoraPicker.IsEnabled = e.Value; }
        else if (sender == twoDayCheck) { twofechaPicker.IsEnabled = e.Value; twohoraPicker.IsEnabled = e.Value; }
        else if (sender == threeDayCheck) { threefechaPicker.IsEnabled = e.Value; threehoraPicker.IsEnabled = e.Value; }
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        var subNombre = subcategoryPicker.SelectedItem?.ToString();
        var subcategoria = subcategorias.FirstOrDefault(s => s.Nombre == subNombre);
        // Recoger días seleccionados y horas
        if (subcategoria == null)
        {
            await DisplayAlert("Error", "Debe seleccionar una subcategoría", "OK");
            return;
        }

        // Recoger días y horas seleccionadas
        var fechas = new List<string>();
        var horas = new List<string>();

        if (oneDayCheck.IsChecked) { fechas.Add(onefechaPicker.Date.ToShortDateString()); horas.Add(onehoraPicker.Time.ToString(@"hh\:mm")); }
        if (twoDayCheck.IsChecked) { fechas.Add(twofechaPicker.Date.ToShortDateString()); horas.Add(twohoraPicker.Time.ToString(@"hh\:mm")); }
        if (threeDayCheck.IsChecked) { fechas.Add(threefechaPicker.Date.ToShortDateString()); horas.Add(threehoraPicker.Time.ToString(@"hh\:mm")); }

        if (!fechas.Any())
        {
            await DisplayAlert("Error", "Debe seleccionar al menos una fecha y hora", "OK");
            return;
        }

        var request = new PeticionRequest
        {
            peticion = new PeticionData
            {
                idCliente = (int)AppSession.RolID,
                fechasPosibles = string.Join(", ", fechas),
                horasPosibles = string.Join(", ", horas),
                descripcion = $"Solicitud para {serviceEntry.Text}",
                idCategoria = _idCategoria,
                idSubcategoria = subcategoria.IdSubCategoria
            }
        };

        var api = new ApiService();
        var response = await api.SubmitRequestAsync(request);

        if (response?.resultado == true)
        {
            await DisplayAlert("Éxito", "Solicitud enviada.","Ok");// ID: ", response.IdPeticion.ToString());
        }
        else
        {
            var errores = response?.error?.Select(e => e.Message) ?? new List<string> { "Error al enviar solicitud" };
            await DisplayAlert("Error", string.Join("\n", errores), "OK");
        }
    }

    private async void OnNotificationsClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Notificaciones", "Aquí irán tus notificaciones pronto.", "OK");
    }

}