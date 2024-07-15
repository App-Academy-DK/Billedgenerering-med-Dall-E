using DallEService;
using System.Timers;


namespace DallE
{

    public partial class MainPage : ContentPage
    {
        private System.Timers.Timer _timer;

        public MainPage()
        {
            InitializeComponent();

            UpdateStatus();
            StartTimer();
        }
       
        private async void UpdateStatus()
        {           
            WeatherService weatherService = new();
            StatusService statusService = new();
            ImageService imageService = new(Token.token);

            int currentHours = DateTime.Now.Hour;

            ImagePrompt copenhagenPrompt = new ImagePrompt
            {
                City = "København",
                Weather = weatherService.GetWeather("København"),
                Time = currentHours,
                Status = statusService.IsOnline("København")
            };

            ImagePrompt parisPrompt = new ImagePrompt
            {
                City = "Paris",
                Weather = weatherService.GetWeather("Paris"),
                Time = currentHours,
                Status = statusService.IsOnline("Paris")
            };

            ImagePrompt newYorkPrompt = new ImagePrompt
            {
                City = "New York",
                Weather = weatherService.GetWeather("New York"),
                Time = currentHours > 7 ? currentHours - 7 : currentHours + 24 - 7,
                Status = statusService.IsOnline("New York")
            };

            Task<string> taskCopenhagen = imageService.GenerateImageAsync(copenhagenPrompt.Prompt);
            Task<string> taskParis = imageService.GenerateImageAsync(parisPrompt.Prompt);
            Task<string> taskNewYork = imageService.GenerateImageAsync(newYorkPrompt.Prompt);

            await Task.WhenAll(taskCopenhagen, taskParis, taskNewYork);

            MainThread.BeginInvokeOnMainThread(() =>
            {

                CopenhagenStatus.Text = copenhagenPrompt.Status ? "Online" : "Offline";
                ParisStatus.Text = parisPrompt.Status ? "Online" : "Offline";
                NewYorkStatus.Text = newYorkPrompt.Status ? "Online" : "Offline";

                CopenhagenImage.Source = taskCopenhagen.Result;
                ParisImage.Source = taskParis.Result;
                NewYorkImage.Source = taskNewYork.Result;
            });
        }

        private void StartTimer()
        {
            _timer = new System.Timers.Timer(60 * 60 * 1000);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            UpdateStatus();
        }
    }
}