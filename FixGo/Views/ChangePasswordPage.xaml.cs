using FixGo.Helpers;
using FixGo.Models;
using FixGo.Services;
using System.Text;
using System.Text.Json;

namespace FixGo.Views;

public partial class ChangePasswordPage : ContentPage
{
    public ChangePasswordPage()
    {
        InitializeComponent();
        userNameLabel.Text = AppSession.NombreCompleto;
        userLocationLabel.Text = AppSession.Direccion ?? AppSession.Empresa;
    }

    private async void OnNotificationsClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Notificaciones", "Aquí irán tus notificaciones.", "OK");
    }

    private async void OnChangePasswordClicked(object sender, EventArgs e)
    {
        var oldPassword = oldPasswordEntry.Text;
        var newPassword = newPasswordEntry.Text;
        var confirmPassword = confirmPasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(oldPassword) ||
            string.IsNullOrWhiteSpace(newPassword) ||
            string.IsNullOrWhiteSpace(confirmPassword))
        {
            await DisplayAlert("Error", "Por favor complete todos los campos.", "OK");
            return;
        }

        if (newPassword != confirmPassword)
        {
            await DisplayAlert("Error", "La nueva contraseña y su confirmación no coinciden.", "OK");
            return;
        }

        try
        {
            ApiService api = new ApiService();
            var response = await api.ChangePassword(AppSession.UsuarioID, newPassword, oldPassword);
            var Msg = response?.Message ?? "Error desconocido.";
            await DisplayAlert("\n", Msg, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Excepción: {ex.Message}", "OK");
        }
    }
}