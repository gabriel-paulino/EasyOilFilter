using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Extensions;
using EasyOilFilter.Domain.Shared.Utils;
using EasyOilFilter.Domain.ViewModels.FilterViewModel;
using EasyOilFilter.Presentation.Enums;
using System.Globalization;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class FilterDetailForm : Form
    {
        private readonly IProductService _productService;
        public FilterViewModel Model { get; set; }
        public FormMode Mode { get; set; }
        public bool IsUpdate { get; private set; }
        public bool IsAdd { get; private set; }

        public FilterDetailForm(IProductService productService)
        {
            _productService = productService;
            Model = new FilterViewModel();
            IsUpdate = false;
            IsAdd = false;
            InitializeComponent();
        }

        private void FilterDetailForm_Load(object sender, EventArgs e)
        {
            LoadFilterTypeComboBox();

            if (Mode == FormMode.Update)
            {
                ButtonProcess.Text = "Atualizar";
                FillFieldsWithFilterDetails();
                return;
            }
            ButtonProcess.Text = "Adicionar";
        }

        private void FillFieldsWithFilterDetails()
        {
            TextBoxCode.Text = Model.Name;
            TextBoxManufacturer.Text = Model.Manufacturer;
            TextBoxPrice.Text = Model.Price.ToString("C2");
            TextBoxStockQuantity.Text = Model.StockQuantity.ToString("F2");
            ComboBoxType.SelectedIndex = (int)EnumUtility.GetEnumByDescription<FilterType>(Model.FilterType);
        }

        private void LoadFilterTypeComboBox()
        {
            foreach (var type in EnumUtility.EnumToList<FilterType>())
                ComboBoxType.Items.Add(type.GetDescription());
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            switch (Mode)
            {
                case FormMode.Add:
                    AddFilter();
                    break;
                case FormMode.Update:
                    UpdateFilter();
                    break;
                default:
                    break;
            }
        }

        private async void AddFilter()
        {
            var (model, message) = GetAddFilterViewModel();

            if (!string.IsNullOrEmpty(message))
                MessageBox.Show(message);

            var filters = await _productService.GetFiltersByName(model.Name);

            if (filters?.Any() ?? false)
            {
                MessageBox.Show($"O filtro: {TextBoxCode.Text} já está adicionado na base de dados.");
                return;
            }

            var result = await _productService.Create(model);

            if (result == default)
            {
                MessageBox.Show($"Falha ao adicionar filtro: {TextBoxCode.Text}.");
                return;
            }

            IsAdd = true;
            MessageBox.Show($"{model.FilterType}: {TextBoxCode.Text} adicionado com sucesso.");
        }

        private async void UpdateFilter()
        {
            var (model, message) = GetFilterViewModel();

            if (!string.IsNullOrEmpty(message))
                MessageBox.Show(message);

            bool anyChange = HasChangedAnyField(model);

            if (!anyChange)
                return;

            var result = await _productService.Update(Model.Id, model);

            if (result == default)
            {
                MessageBox.Show($"Falha ao atualizar filtro: {TextBoxCode.Text}.");
                return;
            }

            IsUpdate = true;
            MessageBox.Show($"{model.FilterType}: {TextBoxCode.Text} atualizado com sucesso.");
        }

        private (FilterViewModel model, string message) GetFilterViewModel()
        {
            string message = string.Empty;

            decimal price = string.IsNullOrEmpty(TextBoxPrice.Text) ? 0 : decimal.Parse(TextBoxPrice.Text, NumberStyles.Currency);
            decimal.TryParse(TextBoxStockQuantity.Text.Replace('.', ','), out decimal stockQuantity);

            bool invalidPrice = price == 0 && Model.Price != 0;
            bool invalidStockQuantity = stockQuantity == 0 && Model.StockQuantity != 0;

            if (invalidPrice)
                message = $"O valor do campo 'Preço' é inválido.{Environment.NewLine}";

            if (invalidStockQuantity)
                message += $"O valor do campo 'Em estoque' é inválido.{Environment.NewLine}";

            if (!string.IsNullOrEmpty(message))
                message += "Preencha com um valor numérico.";

            return (new FilterViewModel()
            {
                Id = Model.Id,
                Name = TextBoxCode.Text,
                Manufacturer = TextBoxManufacturer.Text.FixTextToManageDataBaseResult(allowWithSpaces: true),
                Price = price,
                StockQuantity = stockQuantity,
                FilterType = ComboBoxType.SelectedItem.ToString()
            }, message);
        }

        private (AddFilterViewModel model, string message) GetAddFilterViewModel()
        {
            string message = string.Empty;
            decimal price = string.IsNullOrEmpty(TextBoxPrice.Text) ? 0 : decimal.Parse(TextBoxPrice.Text, NumberStyles.Currency);
            decimal.TryParse(TextBoxStockQuantity.Text.Replace('.', ','), out decimal stockQuantity);

            bool invalidPrice = price == 0 && Model.Price != 0;
            bool invalidStockQuantity = stockQuantity == 0 && Model.StockQuantity != 0;

            if (invalidPrice)
                message = $"O valor do campo 'Preço' é inválido.{Environment.NewLine}";

            if (invalidStockQuantity)
                message += $"O valor do campo 'Em estoque' é inválido.{Environment.NewLine}";

            if (!string.IsNullOrEmpty(message))
                message += "Preencha com um valor numérico.";

            return (new AddFilterViewModel()
            {
                Name = TextBoxCode.Text,
                Manufacturer = TextBoxManufacturer.Text.FixTextToManageDataBaseResult(allowWithSpaces: true),
                Price = price,
                StockQuantity = stockQuantity,
                FilterType = ComboBoxType.SelectedItem.ToString()
            }, message);
        }

        private bool HasChangedAnyField(FilterViewModel updatedFilter)
        {
            return
                updatedFilter.Name != Model.Name ||
                updatedFilter.Manufacturer != Model.Manufacturer ||
                updatedFilter.StockQuantity != Model.StockQuantity ||
                updatedFilter.FilterType != Model.FilterType ||
                updatedFilter.Price != Model.Price;
        }
    }
}