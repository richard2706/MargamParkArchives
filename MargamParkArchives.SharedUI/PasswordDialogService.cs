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
    private const string InitialPasswordPrompt = "Enter the database password for user {0} to continue.";
    private const string IncorrectPasswordPrompt = "The password entered for user {0} is incorrect. Please try again.";
    private const string ServerUnreachablePrompt = "The database server is currently unreachable. Please check that" +
        "the database service is running and you have followed all database configuration steps.";
    private const string OtherErrorPrompt = "An unexpected error occurred while validating the database password. " +
        "Please check the error details then try again.";
    private const string ErrorExpanderHeader = "Show Error Details";
    private const int DialogWidth = 400;

    private readonly DatabaseOptions _databaseOptions = databaseOptions.Value;
    private readonly IDatabasePasswordValidationService _passwordValidationService = passwordValidationService;

    private ContentDialog dialog;
    private TextBlock passwordPrompt;
    private Expander errorExpander;
    private TextBlock errorDetailsText;
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

    private async void PrimaryButton_Clicked(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        args.Cancel = true; // Prevent dialog from closing automatically
        dialog.Content = passwordValidatingPanel;

        PasswordValidationResponse validationResult =
            await _passwordValidationService.ValidatePasswordAsync(passwordBox.Password);
        switch (validationResult.ValidationResult)
        {
            // Close dialog if password is valid
            case PasswordValidationResult.Correct:
                dialog.Hide();
                break;

            // Show incorrect password prompt
            case PasswordValidationResult.Incorrect:
                passwordPrompt.Text = string.Format(IncorrectPasswordPrompt, _databaseOptions.Uid);
                LoadErrorDetailsIntoExpander(validationResult.exceptionThrown);
                dialog.Content = passwordPromptPanel;
                break;

            // Show server unreachable message
            case PasswordValidationResult.ServerUnreachable:
                passwordPrompt.Text = ServerUnreachablePrompt;
                LoadErrorDetailsIntoExpander(validationResult.exceptionThrown);
                dialog.Content = passwordPromptPanel;
                break;

            // Show other error message
            case PasswordValidationResult.OtherError:
                passwordPrompt.Text = OtherErrorPrompt;
                LoadErrorDetailsIntoExpander(validationResult.exceptionThrown);
                dialog.Content = passwordPromptPanel;
                break;
        }
    }

    private void LoadErrorDetailsIntoExpander(Exception exceptionThrown)
    {
        if (exceptionThrown != null)
        {
            string exceptionName = exceptionThrown.GetType().Name;
            string exceptionMessage = exceptionThrown.Message;
            errorDetailsText.Text = $"{exceptionName}: {exceptionMessage}";
            errorExpander.Visibility = Visibility.Visible;
        }
    }

    private StackPanel CreatePasswordPromptUI()
    {
        passwordPrompt = new()
        {
            Text = string.Format(InitialPasswordPrompt, _databaseOptions.Uid),
            TextWrapping = TextWrapping.WrapWholeWords
        };
        errorDetailsText = new()
        {
            TextWrapping = TextWrapping.WrapWholeWords
        };
        errorExpander = new()
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Visibility = Visibility.Collapsed,
            Header = ErrorExpanderHeader,
            Content = errorDetailsText,
            Padding = new Thickness(0, 10, 0, 10)
        };
        passwordBox = new()
        {
            PlaceholderText = "Enter password"
        };

        passwordPromptPanel = new()
        {
            Width = DialogWidth,
            Spacing = 12
        };
        passwordPromptPanel.Children.Add(passwordPrompt);
        passwordPromptPanel.Children.Add(errorExpander);
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
            Width = DialogWidth,
            Orientation = Orientation.Horizontal,
            Spacing = 12
        };
        passwordValidatingPanel.Children.Add(progressRing);
        passwordValidatingPanel.Children.Add(messageContainer);
        return passwordValidatingPanel;
    }
}
