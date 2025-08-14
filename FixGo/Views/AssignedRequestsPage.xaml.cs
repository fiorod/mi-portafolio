using FixGo.Helpers;
using FixGo.Models;
using FixGo.Services;
using System.Collections.ObjectModel;

namespace FixGo.Views;

public partial class AssignedRequestsPage : ContentPage
{
    public ObservableCollection<AssignWorkerResponse> Works { get; set; }

    public AssignedRequestsPage()
    {
        InitializeComponent();
        CargarUsuario();
        CargarTrabajosDummy();
    }

    private void CargarUsuario()
    {
        //userNameLabel.Text = AppSession.NombreCompleto;
        //userLocationLabel.Text = AppSession.Direccion ?? AppSession.Empresa;
    }

    private void CargarTrabajosDummy()
    {
        Works = new ObservableCollection<AssignWorkerResponse>
            {
                new AssignWorkerResponse
                {
                    Servicio = "Plomer�a",
                    Subcategoria = "Fugas",
                    //Direccion = "Condominio Alturas, Bel�n, Heredia",
                    Descripcion = "Fuga en la tuber�a de la cocina",
                    LunesHoras = "7:00-14:00",
                    MartesHoras = "8:00-10:00",
                    ViernesHoras = "9:00-17:00"
                },
                new AssignWorkerResponse
                {
                    Servicio = "Electricidad",
                    Subcategoria = "Apagones",
                    //Direccion = "Calle 10, San Jos�",
                    Descripcion = "Cortocircuito en la cocina",
                    LunesHoras = "8:00-12:00",
                    MartesHoras = "10:00-14:00",
                    ViernesHoras = "13:00-16:00"
                }
            };

        //workerList.ItemsSource = Works;
    }

    private async void OnTakeJobClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var trabajo = button?.BindingContext as AssignWorkerResponse;

        if (trabajo != null)
        {
            // Aqu� podr�as hacer una llamada al API REST para asignar el trabajo
            await DisplayAlert("Trabajo Asignado", $"Has tomado el trabajo de {trabajo.Servicio} - {trabajo.Subcategoria}", "OK");
        }
    }

    private void OnNotificationsClicked(object sender, EventArgs e)
    {
        DisplayAlert("Notificaciones", "Aqu� se mostrar�n las notificaciones futuras.", "OK");
    }
}