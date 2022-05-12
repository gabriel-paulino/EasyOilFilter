using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Extensions;
using EasyOilFilter.Domain.Shared.Utils;
using EasyOilFilter.Domain.ViewModels.OilViewModel;

namespace EasyOilFilter.Presentation
{
    public partial class OilForm : Form
    {
        private readonly IOilService _oilService;

        public OilForm(IOilService oilService)
        {
            _oilService = oilService;

            InitializeComponent();
        }


        private async void OilForm_Load(object sender, EventArgs e)
        {
            LoadOilTypeComboBox();

            //To-Do: Usar paginação
            var oils = await _oilService.GetAll();
            if (oils?.Any() ?? false)
            {
                dataGridView.DataSource = oils.ToList();
                ConfigureGrid();
            }        
        }

        private void dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private async void ButtonSearch_Click(object sender, EventArgs e)
        {
            string selectedType = ComboType?.SelectedItem?.ToString() ?? "Todos";
            var oilType = EnumUtility.GetEnumByDescription<OilType>(selectedType);

            var model = new SearchOilViewModel()
            {
                Name = TextName.Text.FixTextToManageDataBaseResult(),
                Viscosity = TextViscosity.Text.FixTextToManageDataBaseResult(allowWithSpaces: false),
                Type = oilType
            };

            var oils = await _oilService.Get(model);

            if (oils?.Any() ?? false)
            {
                dataGridView.DataSource = oils.ToList();
                return;
            }

            dataGridView.DataSource = new List<OilViewModel>();
        }

        private void LoadOilTypeComboBox()
        {
            foreach (var type in EnumUtility.EnumToList<OilType>())
                ComboType.Items.Add(type.GetDescription());
            
            ComboType.SelectedIndex = 0;
        }

        private void ConfigureGrid()
        {
            dataGridView.Columns["Id"].Visible = false;

            dataGridView.Columns["Name"].HeaderText = "Lubrificante";
            dataGridView.Columns["Viscosity"].HeaderText = "Viscosidade";
            dataGridView.Columns["Price"].HeaderText = "Preço";
            dataGridView.Columns["StockQuantity"].HeaderText = "Estoque";
            dataGridView.Columns["Type"].HeaderText = "Tipo";
            dataGridView.Columns["UnitOfMeasurement"].HeaderText = "Embalagem";

            dataGridView.AutoResizeColumns();
            dataGridView.ReadOnly = true;
            dataGridView.Columns["Name"].MinimumWidth = 188;
            dataGridView.Columns["Price"].MinimumWidth = 100;
            dataGridView.Columns["StockQuantity"].MinimumWidth = 100;
        }
    }
}