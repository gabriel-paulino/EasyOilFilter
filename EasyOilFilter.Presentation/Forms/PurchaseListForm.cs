using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.ViewModels.PurchaseViewModel;
using EasyOilFilter.Presentation.Enums;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class PurchaseListForm : Form
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IProductService _productService;
        private readonly IPaymentService _paymentService;
        private IEnumerable<PurchaseViewModel> _purchases = new List<PurchaseViewModel>();

        public PurchaseListForm
        (
            IPurchaseService purchaseService,
            IProductService productService,
            IPaymentService paymentService
        )
        {
            _purchaseService = purchaseService;
            _productService = productService;
            _paymentService = paymentService;
            InitializeComponent();
        }

        private void PurchaseForm_Load(object sender, EventArgs e)
        {
            var today = DateTime.Today;
            var firstDayMonth = new DateTime(today.Year, today.Month, 1);

            SetDateInfo(firstDayMonth, today);
            ConfigureGrid();
            SearchPurchases(firstDayMonth.Date, today.Date);
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            SearchPurchases(DateTimePickerStart.Value, DateTimePickerEnd.Value);
        }

        private void ResetList()
        {
            SetTotalLabel(default);
            DataGridView.DataSource = new List<PurchaseViewModel>();
        }

        private void SetDateInfo(DateTime startDate, DateTime endDate)
        {
            DateTimePickerStart.Value = startDate;
            DateTimePickerEnd.Value = endDate;
        }

        private void SetTotalLabel(decimal total)
        {
            LabelTotal.Text = total.ToString("C2");
        }

        private void SetDateLabel(string rangeDate)
        {
            LabelDate.Text = rangeDate;
        }

        private void ConfigureGrid()
        {
            DataGridView.DataSource = new List<PurchaseViewModel>();
            DataGridView.ReadOnly = true;
            DataGridView.Columns["Id"].Visible = false;
            DataGridView.Columns["Date"].Visible = true;
            DataGridView.Columns["Items"].Visible = false;
            DataGridView.Columns["Status"].Visible = false;
            DataGridView.Columns["PaymentDone"].Visible = false;
            DataGridView.Columns["Date"].HeaderText = "Data";
            DataGridView.Columns["Provider"].HeaderText = "Fornecedor";
            DataGridView.Columns["Remarks"].HeaderText = "Observações";
            DataGridView.Columns["Total"].HeaderText = "Valor";
            DataGridView.Columns["Total"].DefaultCellStyle.Format = "C2";
            DataGridView.Columns["Provider"].MinimumWidth = 230;
            DataGridView.Columns["Date"].MinimumWidth = 80;
            DataGridView.Columns["Remarks"].MinimumWidth = 205;
            DataGridView.Columns["Total"].MinimumWidth = 90;
            DataGridView.Columns["Date"].DisplayIndex = 0;
            DataGridView.AutoResizeColumns();
        }

        private void ButtonAddPurchase_Click(object sender, EventArgs e)
        {
            bool isAdd = false;
            DateTime startDate = DateTimePickerStart.Value;
            DateTime endDate = DateTimePickerEnd.Value;

            using (var purchaseForm = new PurchaseForm(_purchaseService, _productService, _paymentService))
            {
                purchaseForm.Mode = FormMode.Add;
                purchaseForm.ShowDialog();
                isAdd = purchaseForm.IsAdd;
                startDate = purchaseForm.Date;
            }

            if (isAdd)
                SearchPurchases(startDate.Date, endDate.Date);
        }

        private async void SearchPurchases(DateTime startDate, DateTime endDate)
        {
            _purchases = await _purchaseService.Get(startDate, endDate);
            SetDateLabel($"{startDate:d} - {endDate:d}");

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
            DateTime startDate = DateTimePickerStart.Value;
            DateTime endDate = DateTimePickerStart.Value;

            using (var purchaseForm = new PurchaseForm(_purchaseService, _productService, _paymentService))
            {
                purchaseForm.Model = selectedPurchaseModel;
                purchaseForm.Mode = FormMode.OnlyReadCanCancel;
                purchaseForm.ShowDialog();
                isCanceled = purchaseForm.IsCanceled;
                startDate = purchaseForm.Date;
            }

            if (isCanceled)
                SearchPurchases(startDate.Date, endDate.Date);
        }
    }
}