namespace FixGo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new Views.LoginPage()));
            //return new Window(new AppShell());
        }
    }
}