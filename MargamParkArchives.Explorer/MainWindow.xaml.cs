using MargamParkArchives.Core.Database.PasswordManagement;
using MargamParkArchives.Data;
using MargamParkArchives.Data.Entities;
using MargamParkArchives.Windows.UI.Dialogs;
using Microsoft.UI.Xaml;
using System;
using System.Diagnostics;

namespace MargamParkArchives.Explorer;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    private Artefact[] _artefacts = [];
    private bool _showPasswordDialog = false;

    // Services
    private readonly IArtefactReader _artefactReader;
    private readonly PasswordDialogService _passwordDialogService;

    private ErrorDialogService? _databaseErrorDialogService;

    public MainWindow(IArtefactReader artefactReader, PasswordDialogService passwordDialogService)
    {
        this.InitializeComponent();
        _artefactReader = artefactReader;
        _passwordDialogService = passwordDialogService;

        LoadRandomArtefacts();
    }

    // Earliest point where XamlRoot is available
    private async void RootGrid_Loaded(object sender, RoutedEventArgs e)
    {
        if (_showPasswordDialog)
        {
            _showPasswordDialog = false;

            // Load the artefacts again if password was set successfully
            if (await _passwordDialogService.ShowDialog(Content.XamlRoot))
            {
                LoadRandomArtefacts();
            }
        }
    }

    private async void LoadRandomArtefacts()
    {
        try
        {
            _artefacts = await _artefactReader.GetRandomArtefactsAsync();
        }
        catch (PasswordFileMissingException)
        {
            _showPasswordDialog = true;
            return;
        }
        catch (Exception ex)
        {
            // Display Error Message info bar
            Debug.WriteLine(ex.Message);
            DatabaseConnectionFailedInfoBar.IsOpen = true;

            string errorTitle = ex.GetType().Name;
            string errorDetails = $"{ex.Message}\nStack Trace: {ex.StackTrace}\nSource: {ex.Source}\nInnerException: {ex.InnerException}";
            _databaseErrorDialogService = new ErrorDialogService(errorTitle, errorDetails);

            return;
        }

        // Display success message
        ArtefactsLoadedInfoBar.Message = $"{_artefacts.Length} artefacts loaded.";
        ArtefactsLoadedInfoBar.IsOpen = true;
    }

    private void ViewErrorButton_Click(object sender, RoutedEventArgs e)
    {
        _databaseErrorDialogService?.ShowDialog(Content.XamlRoot);
    }
}
