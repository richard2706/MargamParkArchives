using MargamParkArchives.Core.DataAccess.ArtefactEntity;
using MargamParkArchives.Core.Database.PasswordManagement;
using MargamParkArchives.Core.Entities.ArtefactDetailsReadModel;
using MargamParkArchives.Windows.UI.Dialogs;
using Microsoft.UI.Xaml;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MargamParkArchives.Explorer;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    private ArtefactDetailsReadModel[] _artefacts = [];
    private bool _showPasswordDialog = false;
    private bool _uiLoaded = false;

    // Services
    private readonly IArtefactDetailsReader _artefactReader;
    private readonly PasswordDialogService _passwordDialogService;
    private readonly ErrorDialogService? _databaseErrorDialogService;

    public MainWindow(IArtefactDetailsReader artefactReader, PasswordDialogService passwordDialogService)
    {
        this.InitializeComponent();
        _artefactReader = artefactReader;
        _passwordDialogService = passwordDialogService;

        this.ExtendsContentIntoTitleBar = true;
        this.SetTitleBar(this.AppTitleBar);
        //LoadRandomArtefacts();
    }

    // Earliest point where XamlRoot is available
    //private async void RootGrid_Loaded(object sender, RoutedEventArgs e)
    //{
    //    _uiLoaded = true;
    //    if (_showPasswordDialog)
    //    {
    //        await ShowPasswordDialogThenReloadAsync();
    //    }
    //}

    //private async void LoadRandomArtefacts()
    //{
    //    try
    //    {
    //        _artefacts = await _artefactReader.GetRandomArtefactsAsync();
    //    }
    //    catch (Exception ex) when (ex is PasswordFileMissingException or DatabasePasswordInvalidException)
    //    {
    //        await ShowPasswordDialogThenReloadAsync();
    //        return;
    //    }
    //    catch (Exception ex)
    //    {
    //        // Display Error Message info bar
    //        Debug.WriteLine(ex.Message);
    //        DatabaseConnectionFailedInfoBar.IsOpen = true;

    //        string errorTitle = ex.GetType().Name;
    //        string errorDetails = $"{ex.Message}\nStack Trace: {ex.StackTrace}\nSource: {ex.Source}\nInnerException: {ex.InnerException}";
    //        _databaseErrorDialogService = new ErrorDialogService(errorTitle, errorDetails);

    //        return;
    //    }
    //}

    /// <summary>
    /// Attempts to show the password dialog then reload the artefacts if the dialog returns successfully. If the UI is
    /// not yet loaded, sets the _showPasswordDialog flag to true.
    /// </summary>
    /// <returns></returns>
    //private async Task ShowPasswordDialogThenReloadAsync()
    //{
    //    if (_uiLoaded && await _passwordDialogService.ShowDialog(Content.XamlRoot))
    //    {
    //        LoadRandomArtefacts();
    //    }
    //    else
    //    {
    //        _showPasswordDialog = true;
    //    }
    //}

    //private void ViewErrorButton_Click(object sender, RoutedEventArgs e)
    //{
    //    _databaseErrorDialogService?.ShowDialog(Content.XamlRoot);
    //}
}
