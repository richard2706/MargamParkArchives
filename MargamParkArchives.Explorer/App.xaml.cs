using MargamParkArchives.Core.DataAccess.ArtefactEntity;
using MargamParkArchives.Core.DataAccess.CategoryEntity;
using MargamParkArchives.Core.DataAccess.CreatorEntity;
using MargamParkArchives.Core.DataAccess.GeneralLocationEntity;
using MargamParkArchives.Core.DataAccess.IdentifierGroupEntity;
using MargamParkArchives.Core.DataAccess.PeriodEntity;
using MargamParkArchives.Core.DataAccess.SpecificLocationEntity;
using MargamParkArchives.Core.Database;
using MargamParkArchives.Core.Database.PasswordManagement;
using MargamParkArchives.Core.Database.PasswordManagement.Validation;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities;
using MargamParkArchives.Data.Entities.ArtefactEntity;
using MargamParkArchives.Data.Entities.CategoryEntity;
using MargamParkArchives.Data.Entities.CreatorEntity;
using MargamParkArchives.Data.Entities.GeneralLocationEntity;
using MargamParkArchives.Data.Entities.IdentifierGroupEntity;
using MargamParkArchives.Data.Entities.PeriodEntity;
using MargamParkArchives.Data.Entities.SpecificLocationEntity;
using MargamParkArchives.Windows;
using MargamParkArchives.Windows.UI;
using MargamParkArchives.Windows.UI.Dialogs;
using MargamParkArchives.Windows.UI.SharedViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.UI.Xaml;
using System.Diagnostics;
using Windows.ApplicationModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MargamParkArchives.Explorer;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

    private Window? m_window;

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();
        ConfigureAppHost();
    }

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        Start();
    }

    private static void ConfigureAppHost()
    {
        AppHost = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
        {
            // General services
            services.AddSingleton<MainWindow>();
            services.AddSingleton<PasswordDialogService>();
            services.AddSingleton<INavigationService, ExplorerNavigationService>();

            // ViewModels
            services.AddTransient<ArtefactSearchViewModel>();

            // App specific database options and services
            services.AddOptions<DatabaseOptions>()
                .Bind(hostContext.Configuration.GetSection("DatabaseOptions"))
                .ValidateDataAnnotations()
                .ValidateOnStart();
            services.AddSingleton<IPasswordFilePathProvider, DatabasePasswordFilePathProvider>(_ =>
                new DatabasePasswordFilePathProvider(ExplorerConstants.DatabasePasswordFileName,
                Package.Current.DisplayName));
            services.AddSingleton<IPasswordProvider, DatabasePasswordProvider>();
            services.AddTransient<IConnectionStringProvider, MySqlConnectionStringProvider>();
            services.AddSingleton<IDatabasePasswordValidationService, MySqlPasswordValidationService>();
            services.AddSingleton<IPasswordStorageService, DatabasePasswordStorageService>();

            // Data Access Services
            services.AddTransient<IMySqlDataAccess, MySqlDataAccess>();
            services.AddTransient<IArtefactDetailsReader, MySqlArtefactDetailsReader>();
            services.AddTransient<IArtefactReader, MySqlArtefactReader>();
            services.AddTransient<IIdentifierGroupReader, MySqlIdentifierGroupReader>();
            services.AddTransient<ICreatorReader, MySqlCreatorReader>();
            services.AddTransient<ICategoryReader, MySqlCategoryReader>();
            services.AddTransient<IPeriodReader, MySqlPeriodReader>();
            services.AddTransient<IGeneralLocationReader, MySqlGeneralLocationReader>();
            services.AddTransient<ISpecificLocationReader, MySqlSpecificLocationReader>();
        }).Build();
    }

    private async void Start()
    {
        try
        {
            await AppHost!.StartAsync();
        }
        catch (OptionsValidationException ex)
        {
            Debug.WriteLine(ex.Message);
            const string ErrorTitle = "Database Configuration Error";
            const string ErrorDetails = "The database configuration is invalid. Please check the settings in appsettings.json.";
            new StartupErrorDialogService(ErrorTitle, ErrorDetails).ShowDialog(ex);
            return;
        }

        m_window = AppHost.Services.GetRequiredService<MainWindow>();
        m_window.Activate();
    }
}
