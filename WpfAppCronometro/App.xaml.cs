using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAppCronometro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IClockController, ClockController>();
            services.AddSingleton<IClockView, ClockView>();
            services.AddSingleton<MainWindow>();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
        //private void OnStartup(object sender, StartupEventArgs e)
        //{
        //    ServiceCollection services = new ServiceCollection();

        //    services.AddSingleton<IClockController, ClockController>();
        //    services.AddSingleton<IClockView, ClockView>();
        //    services.AddSingleton<MainWindow>();

        //    var serviceProvider = services.BuildServiceProvider();

        //    //var mainWindow = serviceProvider.GetService<MainWindow>();
        //    var mainWindow = services.BuildServiceProvider().GetService<MainWindow>();
        //    mainWindow.Show();

        //}

