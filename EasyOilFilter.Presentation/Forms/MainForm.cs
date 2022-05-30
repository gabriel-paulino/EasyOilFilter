using EasyOilFilter.Domain.Contracts.Services;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class MainForm : Form
    {
        private readonly IOilService _oilService;
        private readonly IFilterService _filterService;
        private readonly ISaleService _saleService;


        public MainForm(IOilService oilService, IFilterService filterService, ISaleService saleService)
        {
            _oilService = oilService;
            _filterService = filterService;
            _saleService = saleService;
            InitializeComponent();
        }


        private void ButtonLubs_Click(object sender, EventArgs e)
        {
            using var oilForm = new OilForm(_oilService);
            oilForm.ShowDialog();
        }

        private void ButtonFilters_Click(object sender, EventArgs e)
        {
            using var filterForm = new FilterForm(_filterService);
            filterForm.ShowDialog();
        }

        private void ButtonSale_Click(object sender, EventArgs e)
        {
            using var saleForm = new SaleListForm(_saleService);
            saleForm.ShowDialog();
        }
    }
}