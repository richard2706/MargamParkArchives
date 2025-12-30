using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace MargamParkArchives.SharedUI;

public class PasswordDialogService(XamlRoot xamlRoot)
{
    private readonly XamlRoot _xamlRoot = xamlRoot;

    public async void ShowDialog()
    {
        ContentDialog dialog = new()
        {
            Title = "Database Password Required",
            Content = "Please enter the database account password to continue.",
            PrimaryButtonText = "Continue",
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary,
            XamlRoot = _xamlRoot
        };
        ContentDialogResult result = await dialog.ShowAsync();
    }
}
