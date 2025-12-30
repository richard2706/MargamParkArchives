using MargamParkArchives.Core;
using Microsoft.Extensions.Options;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace MargamParkArchives.SharedUI;

public class PasswordDialogService(IOptions<DatabaseOptions> databaseOptions,
    IDatabasePasswordValidationService passwordValidationService)
{
    private const string DialogTitle = "Database Password Required";

    private readonly DatabaseOptions _databaseOptions = databaseOptions.Value;
    private readonly IDatabasePasswordValidationService _passwordValidationService = passwordValidationService;

    private ContentDialog dialog;
    private PasswordBox passwordBox;
    private StackPanel passwordPromptPanel;
    private StackPanel passwordValidatingPanel;

    public async void ShowDialog(XamlRoot xamlRoot)
    {
        passwordPromptPanel = CreatePasswordPromptUI();
        passwordValidatingPanel = CreatePasswordValidatingUI();

        dialog = new()
        {
            Title = DialogTitle,
            Content = passwordPromptPanel,
            PrimaryButtonText = "Continue",
            DefaultButton = ContentDialogButton.Primary,
            XamlRoot = xamlRoot
        };
        dialog.PrimaryButtonClick += PrimaryButton_Clicked;
        ContentDialogResult result = await dialog.ShowAsync();
    }

    private void PrimaryButton_Clicked(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        args.Cancel = true; // Prevent dialog from closing automatically
        dialog.Content = passwordValidatingPanel;
    }

    private StackPanel CreatePasswordPromptUI()
    {
        TextBlock passwordPrompt = new()
        {
            Text = $"Enter the database password for {_databaseOptions.Uid} to continue."
        };
        passwordBox = new()
        {
            PlaceholderText = "Enter password"
        };

        passwordPromptPanel = new()
        {
            Spacing = 12
        };
        passwordPromptPanel.Children.Add(passwordPrompt);
        passwordPromptPanel.Children.Add(passwordBox);
        return passwordPromptPanel;
    }

    private StackPanel CreatePasswordValidatingUI()
    {
        ProgressRing progressRing = new()
        {
            IsActive = true,
            Width = 28,
            Height = 28
        };
        TextBlock validatingMessage = new()
        {
            Text = "Checking password..."
        };
        Grid messageContainer = new()
        {
            VerticalAlignment = VerticalAlignment.Center
        };
        messageContainer.Children.Add(validatingMessage);

        passwordValidatingPanel = new()
        {
            Orientation = Orientation.Horizontal,
            Spacing = 12
        };
        passwordValidatingPanel.Children.Add(progressRing);
        passwordValidatingPanel.Children.Add(messageContainer);
        return passwordValidatingPanel;
    }
}
