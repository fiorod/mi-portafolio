using FixGo.Models;
using FixGo.Services;
using Microsoft.Maui.Storage;

namespace FixGo.Views;

public partial class RegisterPage : ContentPage
{
    private List<Categoria> categorias = new();
    public RegisterPage()
	{
		InitializeComponent();
        CargarCategorias();
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
            await DisplayAlert("Error", "No se pudieron cargar las categorías", "OK");
        }
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        int selectedIndex = categoryPicker.SelectedIndex;
        if (selectedIndex < 0)
        {
            await DisplayAlert("Error", "Debe seleccionar una categoría.", "OK");
            return;
        }

        int idCategoriaSeleccionada = categorias[selectedIndex].IdCategoria;
        string nombreCategoriaSeleccionada = categorias[selectedIndex].Nombre;

        var request = new RegisterRequest
        {
            TipoRol = rolePicker.SelectedItem?.ToString() ?? "Cliente",
            Telefono = phoneEntry.Text ?? "",
            Direccion = addressEntry.Text ?? "",
            Senas = senasEntry.Text ?? "",
            NumeroCasa = houseEntry.Text ?? "",
            Empresa = companyEntry.Text ?? "",
            CategoriaID = idCategoriaSeleccionada,
            NombreCategoria = nombreCategoriaSeleccionada,

            usuario = new Usuario
            {
                NombreUsuario = usernameEntry.Text ?? "",
                Contrasenia = passwordEntry.Text ?? "",
                Email = emailEntry.Text ?? "",
                Persona = new Persona
                {
                    nombre = nameEntry.Text ?? "",
                    nombre2 = secondNameEntry.Text ?? "",
                    apellido1 = lastName1Entry.Text ?? "",
                    apellido2 = lastName2Entry.Text ?? ""
                }
            }
        };

        var api = new ApiService();
        var result = await api.RegisterUserAsync(request);

        if (result != null && result.resultado)
        {
            await DisplayAlert("Éxito", "Registro completado.", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            var errorMsg = result?.error?.FirstOrDefault()?.Message ?? "Error desconocido.";
            await DisplayAlert("Error", errorMsg, "OK");
        }
    }
}