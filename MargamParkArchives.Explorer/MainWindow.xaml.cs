using Microsoft.UI.Xaml;
using System.Threading.Tasks;

using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Services;
using MargamParkArchives.Windows.UI;
using MargamParkArchives.Windows.UI.Dialogs;
using MargamParkArchives.Windows.UI.Pages;

namespace MargamParkArchives.Explorer;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    private bool _showPasswordDialog = false;
    private bool _uiLoaded = false;

    // Services
    //private readonly ErrorDialogService? _databaseErrorDialogService;
    private readonly INavigationService _navigationService;
    private readonly IMySqlDataAccess _dataAccess;
    private readonly PasswordDialogService _passwordDialogService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="navigationService"></param>
    /// <param name="dataAccess"></param>
    /// <param name="passwordDialogService"></param>
    public MainWindow(INavigationService navigationService, IMySqlDataAccess dataAccess, PasswordDialogService passwordDialogService)
    {
        this.InitializeComponent();
        _navigationService = navigationService;
        _passwordDialogService = passwordDialogService;
        _dataAccess = dataAccess;

        this.ExtendsContentIntoTitleBar = true;
        this.SetTitleBar(this.AppTitleBar);

        _navigationService.Initialise(this.ContentFrame);
        _navigationService.NavigateTo(typeof(ArtefactSearchPage));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Earliest point where XamlRoot is available
    /// </remarks>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void RootGrid_Loaded(object sender, RoutedEventArgs e)
    {
        //_uiLoaded = true;
        //if (_showPasswordDialog)
        //{
        //    await ShowPasswordDialogThenReloadAsync();
        //}

        _ = this.ShowDatabasePasswordPromptIfRequired();
    }

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

    /// <summary>
    /// Verifies the database connection is working and triggers a password prompt if the connection fails due to a missing or invalid password
    /// </summary>
    private async Task ShowDatabasePasswordPromptIfRequired()
    {
        DatabaseConnectionCheckService dbConnectionCheck = new(this._dataAccess);
        bool isPasswordPromptRequired = !(await dbConnectionCheck.IsStoredPasswordValidAsync());
        if (isPasswordPromptRequired)
        {
            await this._passwordDialogService.ShowDialog(this.Content.XamlRoot);
        }
    }
}
