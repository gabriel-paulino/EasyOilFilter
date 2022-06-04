namespace EasyOilFilter.Presentation.Forms
{
    public partial class ChooseFromList : Form
    {
        public IEnumerable<object>? DataSource { get; set; }
        public object? Data { get; set; }

        public ChooseFromList()
        {
            InitializeComponent();
        }

        private void SelectProductModalForm_Load(object sender, EventArgs e)
        {
            DataGridView.DataSource = DataSource?.ToList();
            ConfigureDataGrid();
        }

        private void DataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs cell)
        {
            string id = DataGridView
                .Rows[cell.RowIndex]
                .Cells["Id"]
                .Value?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(id))
                return;

            Data = DataSource?.FirstOrDefault(data => GetIdValue(data) == id);
            this.Close();
        }

        private string GetIdValue(object data)
        {
            var type = data.GetType();

            var property = type.GetProperty("Id");

            if (property is null)
                return string.Empty;

            var value = property.GetValue(data);

            return value?.ToString() ?? string.Empty;
        }

        private void ConfigureDataGrid()
        {
            if (DataSource is null)
                return;

            var type = DataSource.FirstOrDefault()?.GetType();

            if (type is null)
                return;

            var properties = type.GetProperties();

            if (properties is null)
                return;

            foreach (var property in properties)
            {

                if (property.Name == "Name")
                {
                    DataGridView.Columns[property.Name].MinimumWidth = 340;
                    DataGridView.Columns[property.Name].HeaderText = "Item";
                    continue;
                }

                DataGridView.Columns[property.Name].Visible = false;
            }

            DataGridView.ReadOnly = true;
        }
    }
}