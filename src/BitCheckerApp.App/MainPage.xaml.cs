using System;
using System.Runtime.InteropServices;
using Microsoft.Maui.Controls;

#if ANDROID
using Android.OS;
#endif

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
            string message = "";

#if ANDROID
            // Get supported ABIs on Android
            string[] supportedAbis = Build.SupportedAbis.ToArray();
            string abiList = string.Join(", ", supportedAbis);
            message += $"Supported ABIs: {abiList}\n";
#endif

            // Determine 32-bit or 64-bit from .NET architecture
            var arch = RuntimeInformation.ProcessArchitecture;
            string bitness = arch switch
            {
                Architecture.X64 or Architecture.Arm64 => "64-bit",
                Architecture.X86 or Architecture.Arm => "32-bit",
                _ => "Unknown"
            };

            message += $"\n\nDevice is running: {bitness} ({arch})";

            architectureLabel.Text = message;
            architectureLabel.IsVisible = true;
            checkButton.IsVisible = false;
        }
    }
}
