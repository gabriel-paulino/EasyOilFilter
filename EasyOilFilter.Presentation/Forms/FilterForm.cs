using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Extensions;
using EasyOilFilter.Domain.Shared.Utils;
using EasyOilFilter.Domain.ViewModels.FilterViewModel;
using EasyOilFilter.Presentation.Enums;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class FilterForm : Form
    {
        private readonly IFilterService _filterService;

        public FilterForm(IFilterService filterService)
        {
            _filterService = filterService;
            InitializeComponent();
        }

        private async void FilterForm_Load(object sender, EventArgs e)
        {
            ConfigureComponents();

            //To-Do: Usar paginação
            var oils = await _filterService.GetAll();
            if (oils?.Any() ?? false)
            {
                DataGridView.DataSource = oils.ToList();
                ConfigureGrid();
            }
        }
        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            SearchFilters();
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
                    (bool Sucess, string Message) = await _filterService.ChangePriceOfAllFiltersByPercentage(percentage);

                    MessageBox.Show(Sucess
                        ? "Os preços dos lubrificantes foram alterados com sucesso."
                        : Message);

                    if (Sucess)
                        SearchFilters();
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
                    (bool Sucess, string Message) = await _filterService.ChangePriceOfAllFiltersByAbsoluteValue(value);

                    MessageBox.Show(Sucess
                        ? "Os preços dos lubrificantes foram alterados com sucesso."
                        : Message);

                    if (Sucess)
                        SearchFilters();
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

            FilterViewModel selectedFilterModel = new()
            {
                Id = id,
                Code = DataGridView.CurrentRow.Cells["Code"].Value.ToString(),
                Manufacturer = DataGridView.CurrentRow.Cells["Manufacturer"].Value.ToString(),
                Price = price,
                StockQuantity = stockQuantity,
                Type = DataGridView.CurrentRow.Cells["Type"].Value.ToString()
            };

            bool isUpdated = false;

            using (var detail = new FilterDetailForm(_filterService))
            {
                detail.Mode = FormMode.Update;
                detail.Model = selectedFilterModel;
                detail.ShowDialog();
                isUpdated = detail.IsUpdate;
            }

            if (isUpdated)
                SearchFilters();
        }

        private void ButtonAddFilter_Click(object sender, EventArgs e)
        {
            bool isAdd = false;

            using (var detail = new FilterDetailForm(_filterService))
            {
                detail.Mode = FormMode.Add;
                detail.ShowDialog();
                isAdd = detail.IsAdd;
            }

            if (isAdd)
                SearchFilters();
        }

        private void ConfigureComponents()
        {
            SetEnabledChangePricePercentageComponents(false);
            SetEnabledChangePriceValueComponents(false);
            LoadFilterTypeComboBox();
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

        private void LoadFilterTypeComboBox()
        {
            foreach (var type in EnumUtility.EnumToList<FilterType>())
                ComboType.Items.Add(type.GetDescription());

            ComboType.SelectedIndex = 0;
        }

        private async void SearchFilters()
        {
            string selectedType = ComboType?.SelectedItem?.ToString() ?? "Todos";
            var filterType = EnumUtility.GetEnumByDescription<FilterType>(selectedType);

            var model = new SearchFilterViewModel()
            {
                Name = TextName.Text.FixTextToManageDataBaseResult(),
                Manufacturer = TextManufacturer.Text.FixTextToManageDataBaseResult(allowWithSpaces: false),
                Type = filterType
            };

            var filters = await _filterService.Get(model);

            if (filters?.Any() ?? false)
            {
                DataGridView.DataSource = filters.ToList();
                return;
            }

            DataGridView.DataSource = new List<FilterViewModel>();
        }

        private void ConfigureGrid()
        {
            DataGridView.Columns["Id"].Visible = false;

            DataGridView.Columns["Code"].HeaderText = "Código";
            DataGridView.Columns["Manufacturer"].HeaderText = "Fabricante";
            DataGridView.Columns["Price"].HeaderText = "Preço";
            DataGridView.Columns["StockQuantity"].HeaderText = "Estoque";
            DataGridView.Columns["Type"].HeaderText = "Tipo";

            DataGridView.Columns["Price"].DefaultCellStyle.Format = "C2";
            DataGridView.Columns["StockQuantity"].DefaultCellStyle.Format = "F2";

            DataGridView.AutoResizeColumns();
            DataGridView.ReadOnly = true;
            DataGridView.Columns["Code"].MinimumWidth = 188;
            DataGridView.Columns["Price"].MinimumWidth = 100;
            DataGridView.Columns["StockQuantity"].MinimumWidth = 100;
        }
    }
}