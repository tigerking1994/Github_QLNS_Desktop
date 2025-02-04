using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;
using VTS.QLNS.CTC.App.DIConfigure;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ILog _logger;
        private LoginViewModel _loginViewModel;

        public IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                // DI configure
                IServiceCollection services = new ServiceCollection();
                services
                    .AddConfiguration()
                    //.AddRepositories()
                    //.AddServices()
                    //.AddViewModels();
                    .AddSingletonFromNamespace("VTS.QLNS.CTC.App", "VTS.QLNS.CTC.App.Service")
                    .AddSingletonFromNamespace("VTS.QLNS.CTC.Core", "VTS.QLNS.CTC.Core.Service")
                    .AddSingletonFromNamespace("VTS.QLNS.CTC.Core", "VTS.QLNS.CTC.Core.Repository")
                    .AddTransientFromNamespace()
                    .AddAutoMapper();
                ServiceProvider = services.BuildServiceProvider();

                // Get services from DI
                _logger = ServiceProvider.GetRequiredService<ILog>();
                _loginViewModel = ServiceProvider.GetService<LoginViewModel>();

                // this will not handle exception come from other window (except main window)
                DispatcherUnhandledException += App_DispatcherUnhandledException;

                bool isCreated = true;
                try
                {
                    // Reset migrate version 1.9.0.0 If exits
                    IMigrationDataService migrateService = ServiceProvider.GetRequiredService<IMigrationDataService>();
                    migrateService.ResetMigrateVersion190();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message, ex);
                    isCreated = false;
                }


                // Migrate database
                ApplicationDbContextFactory contextFactory = ServiceProvider.GetRequiredService<ApplicationDbContextFactory>();
                IDatabaseService databaseService = ServiceProvider.GetRequiredService<IDatabaseService>();

                IConfiguration configuration = ServiceProvider.GetRequiredService<IConfiguration>();
                string path = configuration.GetSection(ConfigHelper.CONFIG_UPDATE_SETTING_PATH).Value;
                ConnectionType connectionType = configuration.GetSection("DbSettings:ConnectionType").Value == ConnectionType.SqlServer.ToString() ? ConnectionType.SqlServer : ConnectionType.LocalDb;
                string connectionString = configuration.GetConnectionString(connectionType.ToString());
                string currentLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                if (isCreated)
                {
                    using (ApplicationDbContext ctx = contextFactory.CreateDbContext())
                    {
                        System.Collections.Generic.List<string> migrated = ctx.Database.GetAppliedMigrations().OrderBy(x => x).ToList();
                        if (!migrated.Any() || (migrated.Count < 10))
                        {
                            databaseService.RestoreLocal(connectionString, Path.Combine(currentLocation, @"AppData\Standard_MDF\QLNS_TIEUCHUAN.zip"), "");
                        }
                        else
                        {
                            try
                            {
                                UpdateSetting updateSetting = ConfigHelper.ReadSetting<UpdateSetting>(path);
                                if (updateSetting is object && updateSetting.IsOverrideDatabase)
                                {
                                    databaseService.RestoreLocal(connectionString, Path.Combine(currentLocation, @"AppData\Standard_MDF\QLNS_TIEUCHUAN.zip"), "");
                                    updateSetting.IsOverrideDatabase = false;
                                    ConfigHelper.UpdateSetting(path, updateSetting);
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.Error(ex.Message, ex);
                            }

                        }
                    }

                    Task.Run(async () =>
                    {
                        try
                        {
                            using ApplicationDbContext ctx = contextFactory.CreateDbContext();
                            await ctx.Database.MigrateAsync();
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex.Message, ex);
                        } finally
                        {
                            Current.Dispatcher.Invoke(delegate
                            {
                                OnShowLogin();
                            });
                        }
                    });

                }
                else
                {

                    // chắc chắn tạo được cơ sở dữ liệu dựa trên cả file mdf
                    Task.Run(async () =>
                    {
                        try
                        {
                            using ApplicationDbContext ctx = contextFactory.CreateDbContext();
                            await ctx.Database.MigrateAsync(new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token);
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex.Message, ex);
                        }
                    }).ContinueWith(task =>
                    {
                        if (databaseService.CheckConnection(connectionString))
                        {
                            databaseService.RestoreLocal(connectionString, Path.Combine(currentLocation, @"AppData\Standard_MDF\QLNS_TIEUCHUAN.zip"), "");
                        }
                    }).ContinueWith(async (task) =>
                    {
                        try
                        {
                            using ApplicationDbContext ctx = contextFactory.CreateDbContext();
                            await ctx.Database.MigrateAsync();
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex.Message, ex);
                        }
                        finally
                        {
                            Current.Dispatcher.Invoke(delegate
                            {
                                OnShowLogin();
                            });
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                Current.Shutdown();
            }

            base.OnStartup(e);
        }

        private void OnShowLogin()
        {
            try
            {
                // Set region
                string cultureName = "vi-VN";
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
                FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }

            _loginViewModel.Init();
            _loginViewModel.Show();
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            _logger.Error(e.Exception.Message);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _logger.Error("Good bye!");
            base.OnExit(e);
        }
    }
}
