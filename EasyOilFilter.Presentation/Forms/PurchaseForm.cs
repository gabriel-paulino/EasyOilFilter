
using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.ViewModels;
using EasyOilFilter.Domain.ViewModels.PurchaseViewModel;
using EasyOilFilter.Presentation.Enums;
using System.Globalization;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class PurchaseForm : Form
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IProductService _productService;
        public PurchaseViewModel Model { get; set; }
        public FormMode Mode { get; set; }
        public bool IsAdd { get; private set; }
        public bool IsCanceled { get; private set; }
        public DateTime Date { get; private set; }

        public PurchaseForm(IPurchaseService purchaseService, IProductService productService)
        {
            _purchaseService = purchaseService;
            _productService = productService;
            Model = new PurchaseViewModel();
            IsAdd = false;
            IsCanceled = false;
            InitializeComponent();
        }

        private void PurchaseForm_Load(object sender, EventArgs e)
        {
            ConfigureComponents();

            if (Mode == FormMode.Add)
            {
                ButtonProcessPurchase.Text = "Adicionar";

                SetDateTimePicker(DateTime.Today);
                DisableTotal();

                return;
            }

            ButtonProcessPurchase.Text = "Cancelar compra";
            ButtonProcessPurchase.Width = 150;

            FillFieldsWithPurchaseDetails();
            DisableComponents();
        }

        private void FillFieldsWithPurchaseDetails()
        {
            TextBoxProvider.Text = Model.Provider;
            TextBoxRemarks.Text = Model.Remarks;
            TextBoxTotal.Text = Model.Total.ToString("C2");

            SetDateTimePicker(Model.Date);

            Grid.DataSource = Model.Items.ToList();
        }

        private void DisableComponents()
        {
            TextBoxProvider.Enabled = false;
            TextBoxRemarks.Enabled = false;
            DateTimePickerPurchaseDate.Enabled = false;
            Grid.ReadOnly = true;

            DisableTotal();
        }

        private void DataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs cell)
        {
            if (IsQuantityOrUnitaryPriceCell(cell))
            {
                string formattedValue = cell.FormattedValue?.ToString() ?? string.Empty;

                decimal itemPrice = string.IsNullOrEmpty(formattedValue)
                    ? 0
                    : decimal.Parse(formattedValue, NumberStyles.Currency);

                cell.Cancel = itemPrice <= 0;
            }
        }

        private void DataGridView_CellValidated(object sender, DataGridViewCellEventArgs cell)
        {
            switch (cell.ColumnIndex)
            {
                case 3:
                    FillPurchaseItemLine(cell.RowIndex);
                    UpdateTotalItem(cell.RowIndex);
                    UpdateTotal();
                    break;
                case 4:
                case 6:
                    UpdateTotalItem(cell.RowIndex);
                    UpdateTotal();
                    break;
                default:
                    break;
            }
        }

        private void Grid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (Mode == FormMode.Add)
                AddNewEmptyLineOnGrid();
        }

        private void ButtonProcessPurchase_Click(object sender, EventArgs e)
        {
            switch (Mode)
            {
                case FormMode.Add:
                    AddPurchase();
                    break;
                case FormMode.OnlyReadCanCancel:
                    CancelPurchase();
                    break;
                default:
                    break;
            }
        }

        private async void AddPurchase()
        {
            var (purchase, errorMessage) = GetAddPurchaseViewModel();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage);
                return;
            }

            var (sucess, message) = await _purchaseService.Create(purchase);

            MessageBox.Show(sucess
                ? "Compra adicionada com sucesso."
                : message);

            if (sucess)
            {
                ResetPurchaseForm();
                IsAdd = true;
                Date = purchase.Date;
            }
        }

        private async void CancelPurchase()
        {
            var choice = MessageBox.Show(
                    $"Cancelar uma compra é um processo irreversível.{Environment.NewLine}" +
                    $"Todos os itens utilizados nesta compra, serão removidos do estoque.{Environment.NewLine}" +
                    $"Deseja continuar?",
                    "Alerta",
                    MessageBoxButtons.YesNo);

            if (choice == DialogResult.Yes)
            {
                var (sucess, message) = await _purchaseService.Cancel(Model);

                MessageBox.Show(sucess
                    ? "Compra cancelada com sucesso."
                    : message);

                if (sucess)
                {
                    IsCanceled = true;
                    Date = Model.Date;
                    ButtonProcessPurchase.Enabled = false;
                }
            }
        }

        private void TextBoxDiscountPercentage_Validated(object sender, EventArgs e)
        {
            UpdateTotal();
        }

        private void ConfigureComponents()
        {
            ConfigureGrid();
        }

        private void SetDateTimePicker(DateTime date)
        {
            DateTimePickerPurchaseDate.Value = date;
        }

        private void DisableTotal()
        {
            TextBoxTotal.Enabled = false;
        }

        private void ConfigureGrid()
        {
            Grid.DataSource = new List<PurchaseItemViewModel>() { new PurchaseItemViewModel() };
            Grid.Columns["Id"].Visible = false;
            Grid.Columns["PurchaseId"].Visible = false;
            Grid.Columns["ProductId"].Visible = false;
            Grid.Columns["ItemDescription"].HeaderText = "Item";
            Grid.Columns["Quantity"].HeaderText = "Quantidade";
            Grid.Columns["UnitOfMeasurement"].HeaderText = "Embalagem";
            Grid.Columns["UnitaryPrice"].HeaderText = "Preço unitário";
            Grid.Columns["TotalItem"].HeaderText = "Total item";
            Grid.Columns["UnitaryPrice"].DefaultCellStyle.Format = "C2";
            Grid.Columns["TotalItem"].DefaultCellStyle.Format = "C2";
            Grid.Columns["ItemDescription"].MinimumWidth = 310;
            Grid.Columns["UnitaryPrice"].MinimumWidth = 110;
            Grid.Columns["TotalItem"].MinimumWidth = 110;
            Grid.Columns["TotalItem"].ReadOnly = true;
            Grid.Columns["UnitOfMeasurement"].ReadOnly = true;
            Grid.AutoResizeColumns();
        }

        private async void FillPurchaseItemLine(int rowIndex)
        {
            string name = GetCellValue(rowIndex, "ItemDescription")?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(name))
                return;

            var products = await _productService.GetProducts(name);

            if (products is null)
                return;

            if (products.Count() > 1)
            {
                var selectedProduct = GetSelectedProduct(products);

                if (selectedProduct is null)
                    return;

                SetPurchaseItemOnGrid(rowIndex, selectedProduct);
            }

            else if (products.Count() == 1)
                SetPurchaseItemOnGrid(rowIndex, (PurchaseItemViewModel)products.FirstOrDefault());
        }

        private void AddNewEmptyLineOnGrid()
        {
            var current = (List<PurchaseItemViewModel>)Grid.DataSource;

            int lastRow = Grid.RowCount - 1;
            Guid.TryParse(GetCellValue(lastRow, "ProductId").ToString(), out Guid productId);

            if (productId == Guid.Empty)
                return;

            Grid.DataSource = new
                List<PurchaseItemViewModel>(current)
                {
                    new PurchaseItemViewModel()
                };
        }

        private void UpdateTotalItem(int rowIndex)
        {
            decimal unitaryPrice = (decimal)GetCellValue(rowIndex, "UnitaryPrice");
            decimal quantity = (decimal)GetCellValue(rowIndex, "Quantity");
            decimal total = unitaryPrice * quantity;

            SetCellValue(rowIndex, "TotalItem", total);
        }

        private void UpdateTotal()
        {
            decimal totalItems = GetTotalItems();
            SetTotalPurchase(totalItems);
        }

        public decimal GetTotalItems()
        {
            decimal totalItems = 0.0m;

            foreach (DataGridViewRow row in Grid.Rows)
            {
                decimal.TryParse(row.Cells["TotalItem"].Value?.ToString(), out decimal totalItem);
                totalItems += totalItem;
            }

            return totalItems;
        }

        private void SetTotalPurchase(decimal totalItems)
        {
            TextBoxTotal.Text = totalItems.ToString("C2");
        }

        private ProductViewModel? GetSelectedProduct(IEnumerable<ProductViewModel> dataSource)
        {
            using (var cfl = new ChooseFromList())
            {
                cfl.DataSource = dataSource;
                cfl.ShowDialog();

                return cfl?.Data is null
                      ? default
                      : (ProductViewModel)cfl.Data;
            }
        }

        private void SetPurchaseItemOnGrid(int rowIndex, PurchaseItemViewModel selectedProduct)
        {
            SetCellValue(rowIndex, "ProductId", selectedProduct.ProductId);
            SetCellValue(rowIndex, "ItemDescription", selectedProduct.ItemDescription);
            SetCellValue(rowIndex, "UnitOfMeasurement", selectedProduct.UnitOfMeasurement);
        }

        private object GetCellValue(int rowIndex, string column)
        {
            return Grid
            .Rows[rowIndex]
            .Cells[column]
            .Value;
        }

        private void SetCellValue(int rowIndex, string column, object value)
        {
            Grid
            .Rows[rowIndex]
            .Cells[column]
            .Value = value;
        }

        private bool IsQuantityOrUnitaryPriceCell(DataGridViewCellValidatingEventArgs cell)
            => cell.ColumnIndex == 4 || cell.ColumnIndex == 6;

        private (AddPurchaseViewModel purchase, string errorMessage) GetAddPurchaseViewModel()
        {
            string errorMessage = string.Empty;

            string provider = TextBoxProvider.Text;
            decimal total = string.IsNullOrEmpty(TextBoxTotal.Text) ? 0 : decimal.Parse(TextBoxTotal.Text, NumberStyles.Currency);

            bool isInvalidDescription = string.IsNullOrEmpty(provider);
            bool isInvalidTotal = total <= 0;

            if (isInvalidDescription)
                errorMessage += $"O 'Fornecedor' deve ser preenchido.{Environment.NewLine}";

            if (isInvalidTotal)
                errorMessage += $"O valor do campo 'Total' está inválido.{Environment.NewLine}";

            var (items, message) = GetAddPurchaseItemsViewModel(errorMessage);

            if (!string.IsNullOrEmpty(message))
                return (new AddPurchaseViewModel(), message);

            return (
            new AddPurchaseViewModel()
            {
                Provider = provider,
                Date = SetCurrentTime(DateTimePickerPurchaseDate.Value),
                Remarks = TextBoxRemarks.Text,
                Items = items,
            }, string.Empty);
        }

        private (IEnumerable<AddPurchaseItemViewModel> purchaseItems, string errorMessage) GetAddPurchaseItemsViewModel(string errorMessage)
        {
            var purchaseItems = new List<AddPurchaseItemViewModel>();

            foreach (DataGridViewRow row in Grid.Rows)
            {
                if (ShouldIgnoreRow(row.Index))
                    break;

                Guid.TryParse(row.Cells["ProductId"].Value?.ToString(), out Guid productId);

                if (productId == Guid.Empty)
                    errorMessage += $"O produto da linha {row.Index + 1} não foi selecionado.{Environment.NewLine}";

                decimal.TryParse(row.Cells["Quantity"].Value?.ToString(), out decimal quantity);

                if (quantity <= 0)
                    errorMessage += $"A quantidade da linha {row.Index + 1} não é válida.{Environment.NewLine}";

                decimal.TryParse(row.Cells["UnitaryPrice"].Value?.ToString(), out decimal unitaryPrice);

                if (unitaryPrice <= 0)
                    errorMessage += $"O preço uniário da linha {row.Index + 1} não foi informado.{Environment.NewLine}";

                decimal.TryParse(row.Cells["TotalItem"].Value?.ToString(), out decimal totalItem);

                purchaseItems.Add(
                    new AddPurchaseItemViewModel()
                    {
                        ProductId = productId,
                        ItemDescription = row.Cells["ItemDescription"].Value?.ToString(),
                        UnitOfMeasurement = row.Cells["UnitOfMeasurement"].Value?.ToString(),
                        Quantity = quantity,
                        UnitaryPrice = unitaryPrice,
                        TotalItem = totalItem
                    });
            }

            return (purchaseItems, errorMessage);
        }

        private bool ShouldIgnoreRow(int row)
        {
            bool isLastRow = row == Grid.RowCount - 1;
            bool isFirstRow = row == 0;

            if (!isLastRow || isFirstRow)
                return false;

            Guid.TryParse(GetCellValue(row, "ProductId").ToString(), out Guid productId);
            decimal.TryParse(GetCellValue(row, "Quantity").ToString(), out decimal quantity);

            bool hasNoProductSelected = productId == Guid.Empty;
            bool hasNoQuantityFilled = quantity == 0;

            return hasNoProductSelected && hasNoQuantityFilled;
        }

        private DateTime SetCurrentTime(DateTime date)
        {
            var now = DateTime.Now;

            return new DateTime(
                date.Year, date.Month, date.Day,
                now.Hour, now.Minute, now.Second
                );
        }

        private void ResetPurchaseForm()
        {
            TextBoxProvider.Clear();
            TextBoxTotal.Clear();
            TextBoxRemarks.Clear();
            SetDateTimePicker(DateTime.Today);
            Grid.DataSource = new List<PurchaseItemViewModel>() { new PurchaseItemViewModel() };
        }
    }
}