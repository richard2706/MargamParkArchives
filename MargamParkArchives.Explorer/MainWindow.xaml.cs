using MargamParkArchives.Windows.UI;
using MargamParkArchives.Windows.UI.Dialogs;
using MargamParkArchives.Windows.UI.SharedViews;
using Microsoft.UI.Xaml;

namespace MargamParkArchives.Explorer;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    private bool _showPasswordDialog = false;
    private bool _uiLoaded = false;

    // Services
    private readonly PasswordDialogService _passwordDialogService;
    //private readonly ErrorDialogService? _databaseErrorDialogService;
    private readonly INavigationService _navigationService;

    public MainWindow(PasswordDialogService passwordDialogService, INavigationService navigationService)
    {
        this.InitializeComponent();
        _passwordDialogService = passwordDialogService;
        _navigationService = navigationService;
        _navigationService.Initialise(this.rootFrame);

        this.ExtendsContentIntoTitleBar = true;
        this.SetTitleBar(this.AppTitleBar);

        _navigationService.NavigateTo(typeof(ArtefactSearchPage));
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
