using MargamParkArchives.Data;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Explorer.DatabaseConnection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;

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
        var builder = Host.CreateDefaultBuilder();
        builder.ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<MainWindow>();

                // App specific database options and services
                services.AddOptions<DatabaseOptions>()
                    .Bind(hostContext.Configuration.GetSection("DatabaseOptions"))
                    .ValidateDataAnnotations()
                    .ValidateOnStart();
                services.AddTransient<IConnectionStringProvider, MySqlConnectionStringProvider>();

                // Data Access Services
                services.AddSingleton<IMySqlConnectionFactory, MySqlConnectionFactory>();
                services.AddTransient<IArtefactDataAccess, MySqlArtefactDataAccess>();
                services.AddTransient<IArtefactReader, MySqlArtefactReader>();
            });

        AppHost = builder.Build();
    }

    private async void Start()
    {
        await AppHost!.StartAsync();

        m_window = AppHost.Services.GetRequiredService<MainWindow>();
        m_window.Activate();
    }
}
