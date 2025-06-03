using System.Runtime.InteropServices;

namespace BitCheckerApp.App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCheckClicked(object sender, EventArgs e)
        {
            var arch = RuntimeInformation.ProcessArchitecture;
            string bitness = arch switch
            {
                Architecture.X64 or Architecture.Arm64 => "64-bit",
                Architecture.X86 or Architecture.Arm => "32-bit",
                _ => "Unknown"
            };

            architectureLabel.Text = $"Device is running: {bitness} ({arch})";
            architectureLabel.IsVisible = true;
            checkButton.IsVisible = false;
        }

    }
}
