using MargamParkArchives.Core.Database;
using MargamParkArchives.Core.Database.PasswordManagement.Storage;
using MargamParkArchives.Core.Database.PasswordManagement.Validation;
using Microsoft.Extensions.Options;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace MargamParkArchives.Windows.UI.Dialogs;

public class PasswordDialogService(IOptions<DatabaseOptions> databaseOptions,
    IDatabasePasswordValidationService passwordValidationService,
    IPasswordStorageService passwordStorageService)
{
    private const string DialogTitle = "Database Password Required";
    private const string ErrorExpanderHeader = "Show Error Details";
    private const int DialogWidth = 400;

    // Password validation error messages
    private const string InitialPasswordPrompt = "Enter the database password for user {0} to continue.";
    private const string IncorrectPasswordPrompt = "The password entered for user {0} is incorrect. Please try again.";
    private const string ServerUnreachablePrompt = "The database server is currently unreachable. Please check that" +
        "the database service is running and you have followed all database configuration steps.";
    private const string OtherValidationErrorPrompt = "An unexpected error occurred while validating the database password. " +
        "Please check the error details then try again.";

    // Password storage error messages
    private const string EncryptionFailedMessage = "The password was correct but could not be stored securely. " +
                                "Please the error details and try again.";
    private const string FileWriterErrorMessage = "The password was correct but could not be saved. Please check the " +
        "application has permission to write to the ProgramData folder. Check the error details below for more details" +
        "then try again.";
    private const string UnknownStorageErrorMessage = "The password was correct could not be saved. Please check the " +
        "error details then try again.";

    // Services
    private readonly DatabaseOptions _databaseOptions = databaseOptions.Value;
    private readonly IDatabasePasswordValidationService _passwordValidationService = passwordValidationService;
    private readonly IPasswordStorageService _passwordStorageService = passwordStorageService;

    // UI elements
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

        PasswordValidationResponse validationResponse =
            await _passwordValidationService.ValidatePasswordAsync(passwordBox.Password);
        //switch (validationResult.ValidationResult)
        switch (validationResponse.Result)
        {
            // Store password and close dialog (if storage is successful)
            case PasswordValidationResult.Correct:
                PasswordStorageResponse storageResponse =
                    await _passwordStorageService.SavePasswordAsync(passwordBox.Password);
                switch (storageResponse.Result)
                {
                    case PasswordStorageResult.Success:
                        dialog.Hide();
                        return;

                    case PasswordStorageResult.EncryptionFailed:
                        UpdatePasswordPrompt(EncryptionFailedMessage, storageResponse.ExceptionThrown);
                        return;

                    case PasswordStorageResult.FileWriterError:
                        UpdatePasswordPrompt(FileWriterErrorMessage, storageResponse.ExceptionThrown);
                        return;

                    case PasswordStorageResult.UnknownError:
                        UpdatePasswordPrompt(UnknownStorageErrorMessage, storageResponse.ExceptionThrown);
                        return;
                }
                break;

            // Show incorrect password prompt
            case PasswordValidationResult.Incorrect:
                UpdatePasswordPrompt(string.Format(IncorrectPasswordPrompt, _databaseOptions.Uid),
                    validationResponse.ExceptionThrown);
                break;

            // Show server unreachable message
            case PasswordValidationResult.ServerUnreachable:
                UpdatePasswordPrompt(ServerUnreachablePrompt, validationResponse.ExceptionThrown);
                break;

            // Show other error message
            case PasswordValidationResult.OtherError:
                UpdatePasswordPrompt(OtherValidationErrorPrompt, validationResponse.ExceptionThrown);
                break;
        }
    }

    private void UpdatePasswordPrompt(string message, Exception exception)
    {
        passwordPrompt.Text = message;
        LoadErrorDetailsIntoExpander(exception);
        dialog.Content = passwordPromptPanel;
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
        else
        {
            errorDetailsText.Text = string.Empty;
            errorExpander.Visibility = Visibility.Collapsed;
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
