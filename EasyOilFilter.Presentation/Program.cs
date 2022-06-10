using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Contracts.UoW;
using EasyOilFilter.Domain.Implementation;
using EasyOilFilter.Domain.Shared.Contexts;
using EasyOilFilter.Infra.Data.Repositories;
using EasyOilFilter.Infra.Data.Session;
using EasyOilFilter.Infra.Data.UoW;
using EasyOilFilter.Presentation.Forms;
using SimpleInjector;
using SimpleInjector.Diagnostics;

namespace EasyOilFilter.Presentation
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var container = Bootstrap();

            Application.Run(container.GetInstance<MainForm>());
        }

        private static Container Bootstrap()
        {
            var settings = new Settings();

            var container = new Container();

            //To-Do: Validar ajustes necessarios mudança do ciclo de vida para Transient

            container.RegisterInstance(new SqlSettings(settings.ConnectionString));
            container.Register<DbSession>(Lifestyle.Singleton);
            container.Register<NotificationContext>(Lifestyle.Singleton);
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Singleton);

            container.Register<IProductRepository, ProductRepository>(Lifestyle.Singleton);
            container.Register<ISaleRepository, SaleRepository>(Lifestyle.Singleton);
            container.Register<IPurchaseRepository, PurchaseRepository>(Lifestyle.Singleton);

            container.Register<IProductService, ProductService>(Lifestyle.Singleton);
            container.Register<ISaleService, SaleService>(Lifestyle.Singleton);
            container.Register<IPurchaseService, PurchaseService>(Lifestyle.Singleton);

            AutoRegisterWindowsForms(container);

            container.Verify();

            return container;
        }
        private static void AutoRegisterWindowsForms(Container container)
        {
            var types = container.GetTypesToRegister<Form>(typeof(Program).Assembly);

            foreach (var type in types)
            {
                var registration =
                    Lifestyle.Transient.CreateRegistration(type, container);

                registration.SuppressDiagnosticWarning(
                    DiagnosticType.DisposableTransientComponent,
                    "Forms should be disposed by app code; not by the container.");

                container.AddRegistration(type, registration);
            }
        }
    }
}