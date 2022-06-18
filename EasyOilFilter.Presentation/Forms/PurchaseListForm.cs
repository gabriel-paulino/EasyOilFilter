using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.ViewModels.PurchaseViewModel;
using EasyOilFilter.Presentation.Enums;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class PurchaseListForm : Form
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IProductService _productService;
        private IEnumerable<PurchaseViewModel> _purchases = new List<PurchaseViewModel>();

        public PurchaseListForm(IPurchaseService purchaseService, IProductService productService)
        {
            _purchaseService = purchaseService;
            _productService = productService;
            InitializeComponent();
        }

        private void PurchaseForm_Load(object sender, EventArgs e)
        {
            SetDateInfo(DateTime.Today);
            ConfigureGrid();
            SearchPurchases(DateTime.Today);
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            SearchPurchases(DateTimePickerSearch.Value);
        }

        private void ResetList()
        {
            SetTotalLabel(default);
            DataGridView.DataSource = new List<PurchaseViewModel>();
        }

        private void SetDateInfo(DateTime date)
        {
            LabelDate.Text = date.ToString("d");
            DateTimePickerSearch.Value = date;
        }

        private void SetTotalLabel(decimal total)
        {
            LabelTotal.Text = total.ToString("C2");
        }

        private void SetDateLabel(DateTime date)
        {
            LabelDate.Text = date.ToString("d");
        }

        private void ConfigureGrid()
        {
            DataGridView.DataSource = new List<PurchaseViewModel>();
            DataGridView.ReadOnly = true;
            DataGridView.Columns["Id"].Visible = false;
            DataGridView.Columns["Date"].Visible = false;
            DataGridView.Columns["Items"].Visible = false;
            DataGridView.Columns["Status"].Visible = false;
            DataGridView.Columns["Provider"].HeaderText = "Fornecedor";
            DataGridView.Columns["Remarks"].HeaderText = "Observações";
            DataGridView.Columns["Total"].HeaderText = "Valor";
            DataGridView.Columns["Total"].DefaultCellStyle.Format = "C2";
            DataGridView.Columns["Provider"].MinimumWidth = 310;
            DataGridView.Columns["Remarks"].MinimumWidth = 155;
            DataGridView.Columns["Total"].MinimumWidth = 140;
            DataGridView.AutoResizeColumns();
        }

        private void ButtonAddPurchase_Click(object sender, EventArgs e)
        {
            bool isAdd = false;
            DateTime purchaseDate = DateTimePickerSearch.Value;

            using (var purchaseForm = new PurchaseForm(_purchaseService, _productService))
            {
                purchaseForm.Mode = FormMode.Add;
                purchaseForm.ShowDialog();
                isAdd = purchaseForm.IsAdd;
                purchaseDate = purchaseForm.Date;
            }

            if (isAdd)
                SearchPurchases(purchaseDate.Date);
        }

        private async void SearchPurchases(DateTime date)
        {
            _purchases = await _purchaseService.Get(date);
            SetDateLabel(date);

            if (_purchases?.Any() ?? false)
            {
                DataGridView.DataSource = _purchases.ToList();
                SetTotalLabel(_purchases.Sum(purchase => purchase.Total));
                return;
            }

            ResetList();
        }

        private void DataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var purchaseId = DataGridView
            .Rows[e.RowIndex]
            .Cells["Id"]
            .Value.ToString();

            Guid.TryParse(purchaseId, out Guid id);

            var selectedPurchaseModel = _purchases.FirstOrDefault(purchase => purchase.Id == id);

            if (selectedPurchaseModel is null)
                return;

            bool isCanceled = false;
            DateTime purchaseDate = DateTimePickerSearch.Value;

            using (var purchaseForm = new PurchaseForm(_purchaseService, _productService))
            {
                purchaseForm.Model = selectedPurchaseModel;
                purchaseForm.Mode = FormMode.OnlyReadCanCancel;
                purchaseForm.ShowDialog();
                isCanceled = purchaseForm.IsCanceled;
                purchaseDate = purchaseForm.Date;
            }

            if (isCanceled)
                SearchPurchases(purchaseDate.Date);
        }
    }
}