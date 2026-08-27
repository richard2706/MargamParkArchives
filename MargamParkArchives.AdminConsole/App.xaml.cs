using MargamParkArchives.Core.Database;
using MargamParkArchives.Core.Database.PasswordManagement;
using MargamParkArchives.Core.Database.PasswordManagement.Validation;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Services;
using MargamParkArchives.Windows;
using MargamParkArchives.Windows.UI;
using MargamParkArchives.Windows.UI.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.UI.Xaml;
using System;
using System.Diagnostics;
using Windows.ApplicationModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MargamParkArchives.AdminConsole;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    private Window? _window;

    public static IHost? AppHost { get; private set; }

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();
        App.ConfigureAppHost();
    }

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        this.Start();
    }

    /// <summary>
    /// Starts the AppHost and launches the MainWindow
    /// </summary>
    private async void Start()
    {
        try
        {
            await App.AppHost!.StartAsync();
        }
        catch (OptionsValidationException ex)
        {
            Debug.WriteLine(ex.Message);
            const string ErrorTitle = "Database Configuration Error";
            const string ErrorDetails = "The database configuration is invalid. Please check the settings in appsettings.json.";
            new StartupErrorDialogService(ErrorTitle, ErrorDetails).ShowDialog(ex);
            return;
        }

        _window = App.AppHost.Services.GetRequiredService<MainWindow>();
        _window.Activate();
    }

    /// <summary>
    /// Configures the app host with services
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private static void ConfigureAppHost()
    {
        App.AppHost = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
        {
            // General services
            services.AddSingleton<MainWindow>();
            services.AddSingleton<PasswordDialogService>();
            services.AddSingleton<INavigationService, AdminConsoleNavigationService>();

            // ViewModels
            services.AddTransient<AdminArtefactSearchViewModel>();

            // App specific database options and services
            services.AddOptions<DatabaseOptions>()
                .Bind(hostContext.Configuration.GetSection("DatabaseOptions"))
                .ValidateDataAnnotations()
                .ValidateOnStart();
            services.AddSingleton<IPasswordFilePathProvider, DatabasePasswordFilePathProvider>(_ =>
                new DatabasePasswordFilePathProvider(AdminConsoleConstants.DatabasePasswordFileName,
                Package.Current.DisplayName));
            services.AddSingleton<IPasswordProvider, DatabasePasswordProvider>();
            services.AddTransient<IConnectionStringProvider, MySqlConnectionStringProvider>();
            services.AddSingleton<IDatabasePasswordValidationService, MySqlPasswordValidationService>();
            services.AddSingleton<IPasswordStorageService, DatabasePasswordStorageService>();

            // Data Access Services
            services.AddTransient<IMySqlDataAccess, MySqlDataAccess>();
            services.AddTransient<IArtefactSearchService, MySqlAdminArtefactSearchService>();
        }).Build();
    }
}
