using FixGo.Models;
using FixGo.Services;

namespace FixGo.Views;

public partial class WorkerRegisterPage : ContentPage
{
    private readonly bool _esAdmin;
    private List<Categoria> categorias = new();

    public WorkerRegisterPage(bool esAdmin = false)
    {
        InitializeComponent();
        _esAdmin = esAdmin;
        CargarCategorias();

        // Cambiar título si es admin
        if (_esAdmin)
        {
            pageTitle.Text = "Agregar Trabajador";
            this.Title = "Agregar Trabajador";
        }
    }

    private async void CargarCategorias()
    {
        var api = new ApiService();
        categorias = await api.GetCategoriasAsync();

        if (categorias != null && categorias.Any())
        {
            categoryPicker.ItemsSource = categorias.Select(c => c.Nombre).ToList();
        }
        else
        {
            await DisplayAlert("Error", "No se pudieron cargar las categorías.", "OK");
        }
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        if (categoryPicker.SelectedIndex < 0)
        {
            await DisplayAlert("Error", "Debe seleccionar una categoría.", "OK");
            return;
        }

        var categoriaSeleccionada = categorias[categoryPicker.SelectedIndex];

        var request = new RegisterRequest
        {
            TipoRol = "Trabajador",
            Telefono = phoneEntry.Text ?? "",
            Empresa = companyEntry.Text ?? "",
            CategoriaID = categoriaSeleccionada.IdCategoria,
            NombreCategoria = categoriaSeleccionada.Nombre,
            usuario = new Usuario
            {
                NombreUsuario = usernameEntry.Text ?? "",
                Contrasenia = passwordEntry.Text ?? "",
                Email = emailEntry.Text ?? "",
                Persona = new Persona
                {
                    nombre = nameEntry.Text ?? "",
                    apellido1 = "",
                    apellido2 = ""
                }
            }
        };

        var api = new ApiService();
        var resultado = await api.RegisterUserAsync(request);

        if (resultado != null && resultado.resultado)
        {
            if (_esAdmin)
            {
                await DisplayAlert("Éxito", "Trabajador creado correctamente.", "OK");
                await Navigation.PopAsync(); // volver a lista de trabajadores
            }
            else
            {
                await DisplayAlert("Éxito", "Registro completado.", "OK");
                await Navigation.PushAsync(new LoginPage());
            }
        }
        else
        {
            var errorMsg = resultado?.error?.FirstOrDefault()?.Message ?? "Error desconocido.";
            await DisplayAlert("Error", errorMsg, "OK");
        }
    }
        private async void OnAgregarTrabajadorClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WorkerRegisterPage(true));
    }

}

