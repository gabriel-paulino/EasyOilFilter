using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Extensions;
using EasyOilFilter.Domain.Shared.Utils;
using EasyOilFilter.Domain.ViewModels.OilViewModel;

namespace EasyOilFilter.Presentation.Forms
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
            ConfigureComponents();

            //To-Do: Usar paginação
            var oils = await _oilService.GetAll();
            if (oils?.Any() ?? false)
            {
                DataGridView.DataSource = oils.ToList();
                ConfigureGrid();
            }
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            SearchLubs();   
        }

        private void CheckBoxChangePricePercentage_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabledChangePricePercentageComponents(CheckBoxChangePricePercentage.Checked);
        }

        private void CheckBoxChangePriceValue_CheckedChanged(object sender, EventArgs e)
        {
            SetEnabledChangePriceValueComponents(CheckBoxChangePriceValue.Checked);
        }

        private async void ButtonChangePricePercentage_Click(object sender, EventArgs e)
        {
            string userInput = TextBoxChangePricePercentage.Text.Replace('.', ',');

            if (decimal.TryParse(userInput, out decimal percentage))
            {
                var choice = MessageBox.Show(
                    "Alterar os preços dos lubrificantes é um processo irreversível. Deseja continuar?",
                    "Alerta",
                    MessageBoxButtons.YesNo);

                if (choice == DialogResult.Yes)
                {
                    (bool Sucess, string Message) = await _oilService.ChangePriceOfAllOilsByPercentage(percentage);

                    MessageBox.Show(Sucess
                        ? "Os preços dos lubrificantes foram alterados com sucesso."
                        : Message);

                    if (Sucess)
                        SearchLubs();
                }
                return;
            }

            MessageBox.Show($"A porcentagem '{userInput}' é inválida.{Environment.NewLine} Preencha com um valor numérico.");
        }

        private async void ButtonChangePriceValue_Click(object sender, EventArgs e)
        {
            string userInput = TextBoxChangePriceValue.Text.Replace('.', ',');

            if (decimal.TryParse(userInput, out decimal value))
            {
                var choice = MessageBox.Show(
                    "Alterar os preços dos lubrificantes é um processo irreversível. Deseja continuar?",
                    "Alerta",
                    MessageBoxButtons.YesNo);

                if (choice == DialogResult.Yes)
                {
                    (bool Sucess, string Message) = await _oilService.ChangePriceOfAllOilsByAbsoluteValue(value);

                    MessageBox.Show(Sucess
                        ? "Os preços dos lubrificantes foram alterados com sucesso."
                        : Message);

                    if (Sucess)
                        SearchLubs();
                }
                return;
            }

            MessageBox.Show($"O valor '{userInput}' é inválido.{Environment.NewLine} Preencha com um valor numérico.");
        }

        private void DataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Guid.TryParse(DataGridView.CurrentRow.Cells["Id"].Value.ToString(), out Guid id);
            decimal.TryParse(DataGridView.CurrentRow.Cells["Price"].Value.ToString(), out decimal price);
            decimal.TryParse(DataGridView.CurrentRow.Cells["StockQuantity"].Value.ToString(), out decimal stockQuantity);

            OilViewModel selectedOilModel = new()
            {
                Id = id,
                Name = DataGridView.CurrentRow.Cells["Name"].Value.ToString(),
                Viscosity = DataGridView.CurrentRow.Cells["Viscosity"].Value.ToString(),
                Price = price,
                StockQuantity = stockQuantity,
                Type = DataGridView.CurrentRow.Cells["Type"].Value.ToString(),
                UnitOfMeasurement = DataGridView.CurrentRow.Cells["UnitOfMeasurement"].Value.ToString()
            };

            using (var detail = new OilDetailForm())
            {
                detail.Model = selectedOilModel;
                detail.ShowDialog();
            }
        }

        private void ConfigureComponents()
        {
            SetEnabledChangePricePercentageComponents(false);
            SetEnabledChangePriceValueComponents(false);
            LoadOilTypeComboBox();
        }

        private void SetEnabledChangePricePercentageComponents(bool enabled)
        {
            TextBoxChangePricePercentage.Enabled = enabled;
            ButtonChangePricePercentage.Enabled = enabled;
        }

        private void SetEnabledChangePriceValueComponents(bool enabled)
        {
            TextBoxChangePriceValue.Enabled = enabled;
            ButtonChangePriceValue.Enabled = enabled;
        }

        private void LoadOilTypeComboBox()
        {
            foreach (var type in EnumUtility.EnumToList<OilType>())
                ComboType.Items.Add(type.GetDescription());

            ComboType.SelectedIndex = 0;
        }

        private async void SearchLubs()
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
                DataGridView.DataSource = oils.ToList();
                return;
            }

            DataGridView.DataSource = new List<OilViewModel>();
        }

        private void ConfigureGrid()
        {
            DataGridView.Columns["Id"].Visible = false;

            DataGridView.Columns["Name"].HeaderText = "Lubrificante";
            DataGridView.Columns["Viscosity"].HeaderText = "Viscosidade";
            DataGridView.Columns["Price"].HeaderText = "Preço";
            DataGridView.Columns["StockQuantity"].HeaderText = "Estoque";
            DataGridView.Columns["Type"].HeaderText = "Tipo";
            DataGridView.Columns["UnitOfMeasurement"].HeaderText = "Embalagem";

            DataGridView.Columns["Price"].DefaultCellStyle.Format = "C2";
            DataGridView.Columns["StockQuantity"].DefaultCellStyle.Format = "F2";

            DataGridView.AutoResizeColumns();
            DataGridView.ReadOnly = true;
            DataGridView.Columns["Name"].MinimumWidth = 188;
            DataGridView.Columns["Price"].MinimumWidth = 100;
            DataGridView.Columns["StockQuantity"].MinimumWidth = 100;
        }
    }
}