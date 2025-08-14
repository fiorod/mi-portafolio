using FixGo.Models;
using FixGo.Services;

namespace FixGo.Views
{
    public partial class WorkersListPage : ContentPage
    {
        private List<Worker> trabajadores = new();

        public WorkersListPage()
        {
            InitializeComponent();
            CargarTrabajadores();
        }

        private async void CargarTrabajadores()
        {
            var api = new ApiService();
            trabajadores = await api.GetTrabajadoresAsync();
            trabajadoresCollection.ItemsSource = trabajadores;
        }

        private async void OnMenuClicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var trabajador = (Worker)imageButton.CommandParameter;

            string opcion = await DisplayActionSheet("Opciones", "Cancelar", null, "Eliminar");

            if (opcion == "Eliminar")
            {
                var confirmar = await DisplayAlert("Confirmar", $"¿Desea eliminar a {trabajador.NombreCompleto}?", "Sí", "No");
                if (confirmar)
                {
                    var api = new ApiService();
                    var eliminado = await api.EliminarTrabajadorAsync(trabajador.IdTrabajador);

                    if (eliminado)
                    {
                        trabajadores.Remove(trabajador);
                        trabajadoresCollection.ItemsSource = null;
                        trabajadoresCollection.ItemsSource = trabajadores;
                        await DisplayAlert("Éxito", "Trabajador eliminado", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo eliminar", "OK");
                    }
                }
            }
        }

        private async void OnAgregarTrabajadorClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkerRegisterPage());
        }
    }
}

