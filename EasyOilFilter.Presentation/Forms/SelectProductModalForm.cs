using EasyOilFilter.Domain.ViewModels.SaleViewModel;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class SelectProductModalForm : Form
    {
        public IEnumerable<SaleItemViewModel> DataSource { get; set; }
        public SaleItemViewModel? SelectedData { get; set; }

        public SelectProductModalForm()
        {
            DataSource = new List<SaleItemViewModel>();
            InitializeComponent();
        }

        private void SelectProductModalForm_Load(object sender, EventArgs e)
        {
            DataGridView.DataSource = DataSource.ToList();
            ConfigureDataGrid();
        }

        private void DataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs cell)
        {
            string productId = DataGridView
                .Rows[cell.RowIndex]
                .Cells["ProductId"]
                .Value?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(productId))
                return;

            if (Guid.TryParse(productId, out Guid id))
            {
                SelectedData = DataSource.FirstOrDefault(product => product.ProductId == id);
                this.Close();
            }
        }

        private void ConfigureDataGrid()
        {
            DataGridView.ReadOnly = true;
            DataGridView.Columns["Id"].Visible = false;
            DataGridView.Columns["SaleId"].Visible = false;
            DataGridView.Columns["ProductId"].Visible = false;
            DataGridView.Columns["Quantity"].Visible = false;
            DataGridView.Columns["UnitOfMeasurement"].Visible = false;
            DataGridView.Columns["TotalItem"].Visible = false;
            DataGridView.Columns["ItemDescription"].HeaderText = "Item";
            DataGridView.Columns["UnitaryPrice"].HeaderText = "Preço unitário";
            DataGridView.Columns["UnitaryPrice"].DefaultCellStyle.Format = "C2";
            DataGridView.Columns["ItemDescription"].MinimumWidth = 200;
            DataGridView.Columns["UnitaryPrice"].MinimumWidth = 105;
            DataGridView.AutoResizeColumns();
        }
    }
}