using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace MargamParkArchives.SharedUI;

public static partial class DatabaseConfigErrorDialogService
{
    public static async void ShowDialog(Exception exceptionThrown)
    {
        // Create a temporary window to host the dialog
        Window tempWindow = new();
        var root = new Grid(); // Temporary content to get a XamlRoot
        tempWindow.Content = root;
        tempWindow.Activate();

        // Wait for the root to load so XamlRoot is guaranteed
        var tcs = new TaskCompletionSource();
        root.Loaded += (_, _) => tcs.SetResult();
        await tcs.Task;

        ContentDialog dialog = new()
        {
            Title = "Database Configuration Error",
            Content = "The database configuration is invalid. Please check the settings in appsettings.json.",
            PrimaryButtonText = "Open settings file",
            SecondaryButtonText = "Copy error",
            CloseButtonText = "Close",
            DefaultButton = ContentDialogButton.Primary,
            XamlRoot = tempWindow.Content.XamlRoot
        };
        ContentDialogResult result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            OpenDatabaseSettingsFile();
        }
        else if (result == ContentDialogResult.Secondary)
        {
            await CopyDatabaseErrorToClipboard(exceptionThrown, tempWindow);
        }

        tempWindow.Close();
    }

    private static async Task CopyDatabaseErrorToClipboard(Exception exceptionThrown, Window tempWindow)
    {
        DataPackage dataPackage = new();
        dataPackage.RequestedOperation = DataPackageOperation.Copy;
        dataPackage.SetText($"{exceptionThrown.GetType().Name}: {exceptionThrown.Message}");
        Clipboard.SetContent(dataPackage);

        tempWindow.AppWindow.Hide();
        await Task.Delay(500); // Wait long enough for copying to complete
    }

    /// <summary>
    /// Opens appsettings.json in the default application or asks the user which application they want to use.
    /// </summary>
    private static void OpenDatabaseSettingsFile()
    {
        string appSettingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        Process openFileProcess = new()
        {
            StartInfo = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = appSettingsPath
            }
        };
        openFileProcess.Start();
    }
}
