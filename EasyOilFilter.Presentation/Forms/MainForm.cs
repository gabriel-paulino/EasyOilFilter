using EasyOilFilter.Domain.Contracts.Services;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class MainForm : Form
    {
        private readonly IProductService _productService;
        private readonly ISaleService _saleService;


        public MainForm(IProductService productService, ISaleService saleService)
        {
            _productService = productService;
            _saleService = saleService;
            InitializeComponent();
        }


        private void ButtonLubs_Click(object sender, EventArgs e)
        {
            using var oilForm = new OilForm(_productService);
            oilForm.ShowDialog();
        }

        private void ButtonFilters_Click(object sender, EventArgs e)
        {
            using var filterForm = new FilterForm(_productService);
            filterForm.ShowDialog();
        }

        private void ButtonSale_Click(object sender, EventArgs e)
        {
            using var saleForm = new SaleListForm(_saleService, _productService);
            saleForm.ShowDialog();
        }
    }
}