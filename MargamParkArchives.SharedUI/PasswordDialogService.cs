using MargamParkArchives.Core;
using Microsoft.Extensions.Options;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace MargamParkArchives.SharedUI;

public class PasswordDialogService(IOptions<DatabaseOptions> databaseOptions)
{
    private const string DialogTitle = "Database Password Required";

    private readonly DatabaseOptions _databaseOptions = databaseOptions.Value;

    public async void ShowDialog(XamlRoot xamlRoot)
    {
        ContentDialog dialog = new()
        {
            Title = DialogTitle,
            Content = CreateDialogUI(),
            PrimaryButtonText = "Continue",
            DefaultButton = ContentDialogButton.Primary,
            XamlRoot = xamlRoot
        };
        ContentDialogResult result = await dialog.ShowAsync();
    }

    private StackPanel CreateDialogUI()
    {
        TextBlock passwordPrompt = new()
        {
            Text = $"Enter the database password for {_databaseOptions.Uid} to continue."
        };
        PasswordBox passwordBox = new()
        {
            PlaceholderText = "Enter password"
        };

        StackPanel panel = new()
        {
            Spacing = 12
        };
        panel.Children.Add(passwordPrompt);
        panel.Children.Add(passwordBox);
        return panel;
    }
}
