using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pro_Practice.Models;
using System.Windows;

namespace Pro_Practice
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Employee currentUser = null;
        private readonly IServiceProvider _serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BochagovaProPracticeContext>(options =>
            options.UseSqlServer("Data Source=NATBOK\\MSSQLSERVER2;Initial Catalog=Bochagova_ProPractice;Integrated Security=True;Encrypt=False;"));

            services.AddSingleton<Authorization>();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
        }
    }

}
