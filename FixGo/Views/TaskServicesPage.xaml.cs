using FixGo.Helpers;
using FixGo.Models;
using FixGo.Services;

namespace FixGo.Views;

public partial class TaskServicesPage : ContentPage
{
	public TaskServicesPage()
	{
		InitializeComponent();
        CargarUsuario();
        CargarTrabajosFinalizados();
    }

    private void CargarUsuario()
    {
        userNameLabel.Text = AppSession.NombreCompleto;
        userLocationLabel.Text = AppSession.Empresa;
    }

    private void CargarTrabajosFinalizados()
    {
        // Datos simulados
        var trabajos = new List<TaskResponse>
            {
                new TaskResponse
                {
                    Servicio = "Plomer�a",
                    Subcategoria = "Fugas",
                    Dia = "19/02/2025",
                    Hora = "5:00 pm",
                    Encargado = "Juan Garc�a",
                    Empresa = "Electric S.A"
                },
                new TaskResponse
                {
                    Servicio = "Electricidad",
                    Subcategoria = "Instalaci�n",
                    Dia = "20/02/2025",
                    Hora = "10:30 am",
                    Encargado = "Carlos Rivera",
                    Empresa = "Luz Total CR"
                },
                new TaskResponse
                {
                    Servicio = "Carpinter�a",
                    Subcategoria = "Muebles",
                    Dia = "21/02/2025",
                    Hora = "2:00 pm",
                    Encargado = "Pedro Jim�nez",
                    Empresa = "Maderas Pro"
                }
            };

        TaskList.ItemsSource = trabajos;
    }

    private void OnNotificationsClicked(object sender, EventArgs e)
    {
        DisplayAlert("Notificaciones", "Aqu� se mostrar�n las notificaciones.", "OK");
    }
}