using FixGo.Views;

namespace FixGo;

public partial class WorkerShell : Shell
{
	public WorkerShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("TaskServicesPage", typeof(TaskServicesPage));
        Routing.RegisterRoute("AssignWorkerPage", typeof(AssignWorkerPage));
    }
}