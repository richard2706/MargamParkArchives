using MargamParkArchives.Core;
using MargamParkArchives.Data;
using MargamParkArchives.Data.Entities;
using MargamParkArchives.SharedUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using Windows.ApplicationModel.DataTransfer;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MargamParkArchives.Explorer;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    // Services
    private readonly IArtefactReader _artefactReader;
    private ErrorDialogService? _databaseErrorDialogService;

    private Artefact[] _artefacts = [];
    private bool _showPasswordDialog = false;

    public MainWindow(IArtefactReader artefactReader)
    {
        this.InitializeComponent();
        _artefactReader = artefactReader;
        LoadRandomArtefacts();
    }

    // Earliest point where XamlRoot is available
    private void RootGrid_Loaded(object sender, RoutedEventArgs e)
    {
        if (_showPasswordDialog)
        {
            ShowPasswordDialog();
        }
    }

    private void LoadRandomArtefacts()
    {
        try
        {
            _artefacts = _artefactReader.GetRandomArtefacts();
        }
        catch (PasswordMissingException)
        {
            _showPasswordDialog = true;
            return;
        }
        catch (Exception ex)
        {
            // Display Error Message
            Debug.WriteLine(ex.Message);
            DatabaseConnectionFailedPopup.IsOpen = true;

            string errorTitle = ex.GetType().Name;
            string errorDetails = $"{ex.Message}\nStack Trace: {ex.StackTrace}\nSource: {ex.Source}\nInnerException: {ex.InnerException}";
            _databaseErrorDialogService = new ErrorDialogService(errorTitle, errorDetails);

            return;
        }

        // Display success message
        DatabaseConnectionSuccessPopup.Message = $"{_artefacts.Length} artefacts loaded.";
        DatabaseConnectionSuccessPopup.IsOpen = true;
    }

    private void ViewErrorButton_Click(object sender, RoutedEventArgs e)
    {
        _databaseErrorDialogService?.ShowDialog(Content.XamlRoot);
    }

    private void ShowPasswordDialog()
    {
        new PasswordDialogService(Content.XamlRoot).ShowDialog();
        _showPasswordDialog = false;
    }
}
