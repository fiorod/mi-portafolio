using CommunityToolkit.Maui.Views;
using FixGo.Models;
using FixGo.Services;

namespace FixGo.Views;

public partial class ResenaPopup : Popup
{
    private readonly int idTrabajador;
    private readonly List<ImageButton> estrellas = new();
    private int calificacionSeleccionada = 0;

    public ResenaPopup(int idTrabajador)
    {
        InitializeComponent();
        this.idTrabajador = idTrabajador;
        CrearEstrellas();
    }

    private void CrearEstrellas()
    {
        for (int i = 1; i <= 5; i++)
        {
            var estrella = new ImageButton
            {
                Source = "star_50.svg",
                WidthRequest = 40,
                HeightRequest = 40,
                BackgroundColor = Colors.Transparent,
                CommandParameter = i
            };

            estrella.Clicked += (s, e) =>
            {
                calificacionSeleccionada = (int)((ImageButton)s).CommandParameter;
                for (int j = 0; j < estrellas.Count; j++)
                {
                    estrellas[j].Source = (j < calificacionSeleccionada) ? "star_20.svg" : "star_50.svg";
                }
            };

            estrellas.Add(estrella);
            EstrellasLayout.Children.Add(estrella);
        }
    }

    private async void OnEnviarClicked(object sender, EventArgs e)
    {
        if (calificacionSeleccionada == 0)
        {
            await Application.Current.MainPage.DisplayAlert("Aviso", "Seleccion� una calificaci�n.", "OK");
            return;
        }

        var req = new ReqCreateResenia
        {
            resenia = new Resenia
            {
                titulo = tituloEntry.Text,
                descripcion = descripcionEditor.Text,
                calificacion = calificacionSeleccionada,
                IdTrabajador = idTrabajador
            }
        };

        var api = new ApiService();
        var success = await api.CrearReseniaAsync(req);

        if (success)
        {
            await Application.Current.MainPage.DisplayAlert("Gracias", "Rese�a enviada con �xito.", "OK");
            Close(); // Cierra el popup
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo enviar la rese�a.", "OK");
        }
    }
}