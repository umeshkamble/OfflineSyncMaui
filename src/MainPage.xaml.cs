using OfflineSyncMauiSolution.Services;

namespace OfflineSyncMauiSolution
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSendClicked(object sender, EventArgs e)
        {
            StatusLabel.Text = "Status: sending...";

            var data = new { Timestamp = DateTime.UtcNow, Value = "Hello" };
            await SyncService.SendOrQueueAsync("https://httpbin.org/post", HttpMethod.Post, data);

            StatusLabel.Text = "Done. Check queue.";
        }
    }
}
