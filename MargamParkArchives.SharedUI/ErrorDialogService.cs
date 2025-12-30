using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.ApplicationModel.DataTransfer;

namespace MargamParkArchives.SharedUI;

public class ErrorDialogService(string errorTitle, string errorDetails)
{
    private readonly string _errorTitle = errorTitle;
    private readonly string _errorDetails = errorDetails;

    /// <summary>
    /// Displays the dialog with error information and an option to copy the details to clipboard.
    /// </summary>
    /// <param name="xamlRoot">The XamlRoot location to display the dialog.</param>
    public async void ShowDialog(XamlRoot xamlRoot)
    {
        ContentDialog errorDialog = new()
        {
            Title = _errorTitle,
            Content = _errorDetails,
            PrimaryButtonText = "Copy",
            CloseButtonText = "Close",
            XamlRoot = xamlRoot
        };
        ContentDialogResult result = await errorDialog.ShowAsync();

        // Copy error details to clipboard if user clicked copy button
        if (result == ContentDialogResult.Primary)
        {
            CopyErrorToClipboard();
        }
    }

    private void CopyErrorToClipboard()
    {
        DataPackage dataPackage = new();
        dataPackage.RequestedOperation = DataPackageOperation.Copy;
        dataPackage.SetText($"{_errorTitle}: {_errorDetails}");
        Clipboard.SetContent(dataPackage);
    }
}
