using FixGo.Models;
using FixGo.Services;

namespace FixGo.Views;

public partial class AdminRequestsPage : ContentPage
{
	public AdminRequestsPage()
	{
		InitializeComponent();
        LoadPendingRequests();
    }

    private async void LoadPendingRequests()
    {
        var api = new ApiService();
        var pending = await api.GetAllUnassignedRequestsAsync();
        adminRequestList.ItemsSource = pending;
    }

    private async void OnAssignClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var ticket = button?.CommandParameter as Ticket;

        if (ticket != null)
        {
            await Navigation.PushAsync(new AssignWorkerPage());
        }
    }
}