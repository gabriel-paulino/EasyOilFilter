using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Extensions;
using EasyOilFilter.Domain.Shared.Utils;
using EasyOilFilter.Domain.ViewModels.OilViewModel;
using EasyOilFilter.Presentation.Enums;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class OilForm : Form
    {
        private readonly IProductService _productService;

        public OilForm(IProductService productService)
        {
            _productService = productService;
            InitializeComponent();
        }

        private async void OilForm_Load(object sender, EventArgs e)
        {
            ConfigureComponents();
            ConfigureGrid();

            var oils = await _productService.GetAllOils();
            if (oils?.Any() ?? false)
                DataGridView.DataSource = oils.ToList();
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
                    (bool Sucess, string Message) = await _productService.ChangePriceOfAllOilsByPercentage(percentage);

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
                    (bool Sucess, string Message) = await _productService.ChangePriceOfAllOilsByAbsoluteValue(value);

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
            decimal.TryParse(DataGridView.CurrentRow.Cells["DefaultPrice"].Value.ToString(), out decimal defaultPrice);
            decimal.TryParse(DataGridView.CurrentRow.Cells["AlternativePrice"].Value.ToString(), out decimal alternativePrice);
            decimal.TryParse(DataGridView.CurrentRow.Cells["StockQuantity"].Value.ToString(), out decimal stockQuantity);

            OilViewModel selectedOilModel = new()
            {
                Id = id,
                Name = DataGridView.CurrentRow.Cells["Name"].Value.ToString(),
                Viscosity = DataGridView.CurrentRow.Cells["Viscosity"].Value?.ToString() ?? string.Empty,
                Api = DataGridView.CurrentRow.Cells["Api"].Value?.ToString() ?? string.Empty,
                DefaultPrice = defaultPrice,
                AlternativePrice = alternativePrice,
                StockQuantity = stockQuantity,
                OilType = DataGridView.CurrentRow.Cells["OilType"].Value.ToString(),
                DefaultUoM = DataGridView.CurrentRow.Cells["DefaultUoM"].Value.ToString(),
                AlternativeUoM = DataGridView.CurrentRow.Cells["AlternativeUoM"].Value.ToString(),
                HasAlternative = (bool)DataGridView.CurrentRow.Cells["HasAlternative"].Value,
            };

            bool wasProcessed = false;

            using (var detail = new OilDetailForm(_productService))
            {
                detail.Mode = FormMode.Update;
                detail.Model = selectedOilModel;
                detail.ShowDialog();
                wasProcessed = detail.IsUpdate || detail.IsDelete;
            }

            if (wasProcessed)
                SearchLubs();
        }

        private void ButtonAddOil_Click(object sender, EventArgs e)
        {
            bool isAdd = false;

            using (var detail = new OilDetailForm(_productService))
            {
                detail.Mode = FormMode.Add;
                detail.ShowDialog();
                isAdd = detail.IsAdd;
            }

            if (isAdd)
                SearchLubs();
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
                OilType = oilType
            };

            var oils = await _productService.Get(model);

            if (oils?.Any() ?? false)
            {
                DataGridView.DataSource = oils.ToList();
                return;
            }

            DataGridView.DataSource = new List<OilViewModel>();
        }

        private void ConfigureGrid()
        {
            DataGridView.DataSource = new List<OilViewModel>();
            DataGridView.ReadOnly = true;
            DataGridView.Columns["Id"].Visible = false;
            DataGridView.Columns["DefaultPrice"].Visible = false;
            DataGridView.Columns["DefaultUoM"].Visible = false;
            DataGridView.Columns["AlternativePrice"].Visible = false;
            DataGridView.Columns["AlternativeUoM"].Visible = false;
            DataGridView.Columns["HasAlternative"].Visible = false;
            DataGridView.Columns["Name"].HeaderText = "Lubrificante";
            DataGridView.Columns["Viscosity"].HeaderText = "Visc.";
            DataGridView.Columns["Api"].HeaderText = "Api";
            DataGridView.Columns["StockQuantity"].HeaderText = "Estoque";
            DataGridView.Columns["PriceUoM"].HeaderText = "Preço";
            DataGridView.Columns["OilType"].HeaderText = "Tipo";
            DataGridView.Columns["StockQuantity"].DefaultCellStyle.Format = "F2";
            DataGridView.Columns["Name"].MinimumWidth = 190;
            DataGridView.Columns["OilType"].MinimumWidth = 105;
            DataGridView.Columns["PriceUoM"].MinimumWidth = 240;

            DataGridView.AutoResizeColumns();
        }
    }
}