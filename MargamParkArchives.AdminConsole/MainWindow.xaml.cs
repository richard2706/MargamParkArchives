using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Services;
using MargamParkArchives.Windows.UI;
using MargamParkArchives.Windows.UI.Dialogs;
using MargamParkArchives.Windows.UI.SharedViews;
using Microsoft.UI.Xaml;
using System.Threading.Tasks;

namespace MargamParkArchives.AdminConsole;

/// <summary>
/// Main Window for the Admin Console application. Contains the top level NavigationView and handles cross page functionality
/// such as notifications and password prompts
/// </summary>
public sealed partial class MainWindow : Window
{
    private readonly INavigationService _navigationService;
    private readonly IMySqlDataAccess _dataAccess;
    private readonly PasswordDialogService _passwordDialogService;

    /// <summary>
    /// Creates a new instance of the MainWindow class and initializes the navigation service
    /// </summary>
    /// <param name="navigationService"></param>
    /// <param name="dataAccess"></param>
    /// <param name="passwordDialogService"></param>
    public MainWindow(INavigationService navigationService, IMySqlDataAccess dataAccess, PasswordDialogService passwordDialogService)
    {
        this.InitializeComponent();
        this._navigationService = navigationService;
        this._dataAccess = dataAccess;
        this._passwordDialogService = passwordDialogService;

        this.ExtendsContentIntoTitleBar = true;
        //this.SetTitleBar(this.AppTitleBar);

        this._navigationService.Initialise(this.ContentFrame);
        this._navigationService.NavigateTo(typeof(ArtefactSearchPage));
    }

    /// <summary>
    /// Called when the RootGrid is loaded
    /// </summary>
    /// <remarks>
    /// This is the earliest point where XamlRoot is available, and can be used to trigger any actions that require the UI to be fully loaded.
    /// </remarks>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RootGrid_Loaded(object sender, RoutedEventArgs e)
    {
        _ = this.ShowDatabasePasswordPromptIfRequired();
    }

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
