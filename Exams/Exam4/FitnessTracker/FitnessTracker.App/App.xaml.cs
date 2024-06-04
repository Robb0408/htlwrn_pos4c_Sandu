using FitnessTracker.App.Services;
using FitnessTracker.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FitnessTracker.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public sealed partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the current App instance in use
        /// </summary>
        public new static App Current
                => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> 
        /// instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IBurnedCaloriesService, BurnedCaloriesService>();

            // Viewmodels
            services.AddTransient<MainViewModel>();
            services.AddTransient<ActivityViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
