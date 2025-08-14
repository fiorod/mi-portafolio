using FixGo.Helpers;
using FixGo.Models;
using FixGo.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace FixGo.Views;

public partial class AssignWorkerPage : ContentPage
{
    public ObservableCollection<AssignWorkerResponse> Trabajos { get; set; }

    public AssignWorkerPage()
    {
        InitializeComponent();
        CargarUsuario();
        CargarTrabajosDummy();
    }

    private void CargarUsuario()
    {
        userNameLabel.Text = AppSession.NombreCompleto;
        userLocationLabel.Text = AppSession.Empresa;
    }

    private async void CargarTrabajosDummy()
    {
        try
        {
            PeticionGeneralRequest request = new PeticionGeneralRequest();
            request.IdPeticion = null;
            request.IdCliente = null;
            request.IdCategoria = null;
            request.IdSubCategoria = null;

            var apiService = new ApiService();
            var resultado = await apiService.GetPeticionesGeneralesAsync(request);

            var trabajos = new ObservableCollection<AssignWorkerResponse>();

            foreach (var peticion in resultado)
            {
                var fechas = peticion.fechasPosibles.Split(',', StringSplitOptions.TrimEntries);
                var horas = peticion.horasPosibles.Split(',', StringSplitOptions.TrimEntries);

                trabajos.Add(new AssignWorkerResponse
                {
                    idServicio = peticion.idCategoria,
                    idSubcategoria = peticion.idSubcategoria,
                    Cliente = peticion.idCliente.ToString(),
                    Descripcion = peticion.descripcion,
                    LunesHoras = fechas.Length > 0 && horas.Length > 0 ? $"{fechas[0]} - {horas[0]}" : "",
                    MartesHoras = fechas.Length > 1 && horas.Length > 1 ? $"{fechas[1]} - {horas[1]}" : "",
                    ViernesHoras = fechas.Length > 2 && horas.Length > 2 ? $"{fechas[2]} - {horas[2]}" : "",
                    idPeticion = peticion.idPeticion,
                    idCliente = peticion.idCliente
                });
            }

            Trabajos = trabajos;
            workerList.ItemsSource = Trabajos;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo cargar los trabajos: {ex.Message}", "OK");
        }
    }

    private async void OnTakeJobClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var trabajo = button?.BindingContext as AssignWorkerResponse;

        if (trabajo != null)
        {
            //await DisplayAlert("Trabajo Asignado", $"Has tomado el trabajo de {trabajo.Servicio} - {trabajo.Subcategoria}", "OK");
            await Navigation.PushAsync(new CreateAppointmentPage(trabajo.idPeticion, trabajo.idCliente, trabajo.idServicio, trabajo.Servicio, trabajo.idSubcategoria, trabajo.Subcategoria));
        }
    }

    private void OnNotificationsClicked(object sender, EventArgs e)
    {
        DisplayAlert("Notificaciones", "Aqu� se mostrar�n las notificaciones futuras.", "OK");
    }
}