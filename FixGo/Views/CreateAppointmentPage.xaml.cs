using FixGo.Helpers;
using FixGo.Models;
using FixGo.Services;

namespace FixGo.Views;

public partial class CreateAppointmentPage : ContentPage
{
    private readonly ApiService _api = new();
    private int _peticionId;// = 15; // dummy
    //private int _trabajadorId = 5; // dummy

    private List<DateTime> _fechas;
    private List<TimeSpan> _horas;

    public CreateAppointmentPage(int idPeticion, int idCliente, int idCategoria, string categoriaDesc, int idSubCategoria, string subcategoriaDesc)
    {
        InitializeComponent();
        _peticionId = idPeticion;
        CargarDatosUsuario();
        //CargarDiasDisponibles();
        CargarPeticionAsync(idCliente, idCategoria, categoriaDesc, idSubCategoria, subcategoriaDesc);
    }

    private void CargarDatosUsuario()
    {
        userNameLabel.Text = AppSession.NombreCompleto ?? "Usuario";
        userLocationLabel.Text = AppSession.Empresa;
    }

    //private void CargarDiasDisponibles()
    //{
    //    dayPicker.ItemsSource = new List<string> { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo" };
    //}

    private async void CargarPeticionAsync(int idCliente, int idCategoria, string categoriaDesc, int idSubCategoria, string subcategoriaDesc)
    {
        var request = new ConsultaPeticionIdRequest
        {
            IdPeticion = _peticionId,
            IdCliente = idCliente,
            idCategoria = idCategoria,
            IdSubCategoria = idSubCategoria
        };

        var response = await _api.ObtenerPeticionPorIdAsync(request);

        if (response?.resultado == true && response.listaPeticiones.Any())
        {
            var peticion = response.listaPeticiones.First();
            servicioLabel.Text = idCategoria.ToString();//categoriaDesc; // "Jardiner�a";
            subcategoriaLabel.Text = idSubCategoria.ToString();//subcategoriaDesc; // "Cortar c�sped";
            direccionLabel.Text = AppSession.Direccion;
            descripcionLabel.Text = peticion.descripcion;

            // Separar por comas
            var fechas = peticion.fechasPosibles.Split(',');
            var horas = peticion.horasPosibles.Split(',');

            List<string> combinaciones = new();
            List<DateTime> fechasSeleccionadas = new();
            List<TimeSpan> horasSeleccionadas = new();

            for (int i = 0; i < fechas.Length && i < horas.Length; i++)
            {
                // Convertir fecha y hora
                if (DateTime.TryParse(fechas[i], out DateTime fecha) &&
                    TimeSpan.TryParse(horas[i], out TimeSpan hora))
                {
                    // Combinar en formato amigable
                    DateTime fechaHora = fecha.Date + hora;
                    combinaciones.Add(fechaHora.ToString("dd/MM/yyyy - hh:mm tt"));
                    fechasSeleccionadas.Add(fecha);
                    horasSeleccionadas.Add(hora);
                }
            }
            fechaHoraCollection.ItemsSource = combinaciones;
            fechaHoraPicker.ItemsSource = combinaciones;

            this._fechas = fechasSeleccionadas;
            this._horas = horasSeleccionadas;
        }
        else
        {
            await DisplayAlert("Error", "No se pudo cargar la petici�n.", "OK");
        }
    }

    private async void OnCreateJobClicked(object sender, EventArgs e)
    {
        int index = fechaHoraPicker.SelectedIndex;

        if (index < 0)
        {
            await DisplayAlert("Error", "Por favor selecciona una fecha y hora.", "OK");
            return;
        }

        // Extraer fecha y hora
        DateTime fechaSeleccionada = _fechas[index];
        TimeSpan horaSeleccionada = _horas[index];

        var nuevaCita = new CrearCitaRequest
        {
            cita = new Cita
            {
                IdCita = 0,
                fecha = fechaSeleccionada,
                hora = horaSeleccionada, //hora,
                IdPeticion = _peticionId,
                IdTrabajador = AppSession.RolID
            }
        };

        var respuesta = await _api.CrearCitaAsync(nuevaCita);

        if (respuesta?.Resultado == true)
        {
            await DisplayAlert("�xito", "Cita registrada correctamente.", "OK");
            await Shell.Current.GoToAsync("..", true);
        }
        else
        {
            var error = respuesta?.Error?.FirstOrDefault()?.Message ?? "Error desconocido";
            await DisplayAlert("Error", error, "OK");
        }
    }

    private async void OnNotificationsClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Notificaciones", "Aqu� aparecer�n las notificaciones m�s adelante.", "OK");
    }
}