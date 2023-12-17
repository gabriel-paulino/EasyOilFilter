using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Contracts.UoW;
using EasyOilFilter.Domain.Implementation;
using EasyOilFilter.Infra.Data.Repositories;
using EasyOilFilter.Infra.Data.Session;
using EasyOilFilter.Infra.Data.UoW;
using EasyOilFilter.Infra.Pdf.Services;
using EasyOilFilter.Presentation.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace EasyOilFilter.Presentation
{
    internal static class Program
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            var mainForm = ServiceProvider.GetRequiredService<MainForm>();
            Application.Run(mainForm);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var settings = new Settings();
            var sqlSettings = new SqlSettings(settings.ConnectionString);

            services.AddSingleton(sqlSettings);
            services.AddScoped<DbSession>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ISaleRepository, SaleRepository>();
            services.AddTransient<IPurchaseRepository, PurchaseRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ISaleService, SaleService>();
            services.AddTransient<IPurchaseService, PurchaseService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IPdfService, PdfService>();

            services.AddTransient<MainForm>();
            services.AddTransient<ChooseFromList>();
            services.AddTransient<FilterDetailForm>();
            services.AddTransient<FilterForm>();
            services.AddTransient<OilDetailForm>();
            services.AddTransient<OilForm>();
            services.AddTransient<PaymentForm>();
            services.AddTransient<PurchaseForm>();
            services.AddTransient<PurchaseListForm>();
            services.AddTransient<ReportForm>();
            services.AddTransient<SaleForm>();
            services.AddTransient<SaleListForm>();
        }
    }
}