using Autofac;
using EngineDataAccess;
using EngineDataAccess.Repositories;
using EngineUIComponents.Services;
using EngineUIComponents.ViewModels;
using Microsoft.EntityFrameworkCore;
using Prism.Events;
using System.Configuration;
using System.DirectoryServices.ActiveDirectory;
using System.Windows;

namespace EngineStartup
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ContainerBuilder();
            builder.RegisterType<MessageDialogService>().AsImplementedInterfaces();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.Register(c =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<EngineDbContext>()
                .UseSqlServer(ConfigurationManager.ConnectionStrings["EngineDatabase"].ConnectionString);
                return optionsBuilder.Options;
            });

            builder.RegisterType<EngineDbContext>().AsSelf();
            builder.RegisterType<EngineRepository>().As<IEngineRepository>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<EngineTypeRepository>().As<IEngineTypeRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<Repositories>().As<IRepositories>();

            builder.RegisterType<MainViewModel>();
            builder.RegisterType<MainWindowViewModel>();
            builder.RegisterType<MainWindow>().AsSelf();



            var container = builder.Build();
            ExceptionHandler.Register(container.Resolve<IMessageDialogService>());

            var context = container.Resolve<EngineDbContext>();
            //context.Database.EnsureCreated();
            //context.Database.Migrate();

            using (var scope = container.BeginLifetimeScope())
            {
                var main = scope.Resolve<MainWindow>();
                main.ShowDialog();
            }
        }
    }
}
