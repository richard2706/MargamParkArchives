using MargamParkArchivesData;
using MargamParkArchivesData.Entities;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using Windows.ApplicationModel.DataTransfer;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MargamParkArchivesApp;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    private const string DatabaseErrorTitle = "A database error occurred";

    private readonly IArtefactReader _artefactReader;
    private Artefact[] _artefacts = [];

    private string _errorTitle = string.Empty;
    private string _errorDetails = string.Empty;

    public MainWindow(IArtefactReader artefactReader)
    {
        this.InitializeComponent();
        _artefactReader = artefactReader;
        LoadRandomArtefacts();
    }

    private void LoadRandomArtefacts()
    {
        try
        {
            _artefacts = _artefactReader.GetRandomArtefacts();
            DatabaseConnectionSuccessPopup.Message = $"{_artefacts.Length} artefacts loaded.";
            DatabaseConnectionSuccessPopup.IsOpen = true;
        }
        catch (Exception ex)
        {
            // Display Error Message
            Debug.WriteLine(ex.Message);
            DatabaseConnectionFailedPopup.IsOpen = true;

            _errorTitle = ex.GetType().Name;
            _errorDetails = $"{ex.Message}\nStack Trace: {ex.StackTrace}\nSource: {ex.Source}\nInnerException: {ex.InnerException}";
        }
    }

    private void ViewErrorButton_Click(object sender, RoutedEventArgs e)
    {
        DisplayErrorDetailsDialog();
    }

    private async void DisplayErrorDetailsDialog()
    {
        ContentDialog errorDialog = new()
        {
            Title = DatabaseErrorTitle,
            Content = $"{_errorTitle}\n{_errorDetails}",
            PrimaryButtonText = "Copy",
            CloseButtonText = "Close",
            XamlRoot = this.Content.XamlRoot
        };
        ContentDialogResult result = await errorDialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            DataPackage dataPackage = new();
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            dataPackage.SetText($"{_errorTitle}: {_errorDetails}");
            Clipboard.SetContent(dataPackage);
        }
    }
}
