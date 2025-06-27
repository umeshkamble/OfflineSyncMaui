using OfflineSyncMauiSolution.Services;

namespace OfflineSyncMauiSolution
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        private async void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)
                await SyncService.ProcessQueueAsync();
        }
    }
}