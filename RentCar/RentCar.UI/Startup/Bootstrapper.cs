using Autofac;
using Microsoft.EntityFrameworkCore;
using Prism.Events;
using RentCar.DataAccess;
using RentCar.DataAccess.Data.Repositories;
using RentCar.DataAccess.Data.Repositories.Lookups;
using RentCar.Domain.Interfaces;
using RentCar.Domain.Services.PreflightService;
using RentCar.UI.Services.MessageDialogService;
using RentCar.UI.View.Services;
using RentCar.UI.ViewModel;
using System.Configuration;

namespace RentCar.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            var options = new DbContextOptionsBuilder<RentCarDbContext>()
                          .UseSqlServer(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString)
                          .Options;
            builder.Register(x => new RentCarDbContext(options)).As<RentCarDbContext>();


            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<MainWindow>().AsSelf();

            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();

            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<AvailableCarsViewModel>()
                  .Keyed<IViewModel>(nameof(AvailableCarsViewModel));
            builder.RegisterType<LoginViewModel>()
              .Keyed<IViewModel>(nameof(LoginViewModel));

            builder.RegisterType<LookupDataRepository>().AsImplementedInterfaces();
            builder.RegisterType<CarRepository>().As<ICarRepository>();
            builder.RegisterType<BookingRepository>().As<IBookingRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<PreflightService>().As<IPreflightService>();

            return builder.Build();
        }
    }
}
