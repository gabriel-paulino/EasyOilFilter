using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Extensions;
using EasyOilFilter.Domain.Shared.Utils;
using EasyOilFilter.Domain.ViewModels.FilterViewModel;
using EasyOilFilter.Presentation.Enums;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class FilterDetailForm : Form
    {
        private readonly IFilterService _filterService;
        public FilterViewModel Model { get; set; }
        public FormMode Mode { get; set; }
        public bool IsUpdate { get; private set; }
        public bool IsAdd { get; private set; }

        public FilterDetailForm(IFilterService filterService)
        {
            _filterService = filterService;
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
                ButtonSave.Text = "Atualizar";
                FillFieldsWithFilterDetails();
                return;
            }
            ButtonSave.Text = "Adicionar";
        }

        private void FillFieldsWithFilterDetails()
        {
            TextBoxCode.Text = Model.Code;
            TextBoxManufacturer.Text = Model.Manufacturer;
            TextBoxPrice.Text = Model.Price.ToString("C2");
            TextBoxStockQuantity.Text = Model.StockQuantity.ToString("F2");
            ComboBoxType.SelectedIndex = (int)EnumUtility.GetEnumByDescription<FilterType>(Model.Type);
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
            var (model, message) = GetAddOilViewModel();

            if (!string.IsNullOrEmpty(message))
                MessageBox.Show(message);

            var oils = await _filterService.GetByCode(model.Code);

            if (oils?.Any() ?? false)
            {
                MessageBox.Show($"O filtro: {TextBoxCode.Text} já está adicionado na base de dados.");
                return;
            }

            var result = await _filterService.Create(model);

            if (result == default)
            {
                MessageBox.Show($"Falha ao adicionar filtro: {TextBoxCode.Text}.");
                return;
            }

            IsAdd = true;
            MessageBox.Show($"Lubrificante: {TextBoxCode.Text} adicionado com sucesso.");
        }

        private async void UpdateFilter()
        {
            var (model, message) = GetFilterViewModel();

            if (!string.IsNullOrEmpty(message))
                MessageBox.Show(message);

            bool anyChange = HasChangedAnyField(model);

            if (!anyChange)
                return;

            var result = await _filterService.Update(Model.Id, model);

            if (result == default)
            {
                MessageBox.Show($"Falha ao atualizar filtro: {TextBoxCode.Text}.");
                return;
            }

            IsUpdate = true;
            MessageBox.Show($"Filtro: {TextBoxCode.Text} atualizado com sucesso.");
        }

        private (FilterViewModel model, string message) GetFilterViewModel()
        {
            string message = string.Empty;

            decimal.TryParse(TextBoxPrice.Text.Replace("R$", string.Empty).Replace('.', ','), out decimal price);
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
                Code = TextBoxCode.Text,
                Manufacturer = TextBoxManufacturer.Text.FixTextToManageDataBaseResult(allowWithSpaces: true),
                Price = price,
                StockQuantity = stockQuantity,
                Type = ComboBoxType.SelectedItem.ToString()
            }, message);
        }

        private (AddFilterViewModel model, string message) GetAddOilViewModel()
        {
            string message = string.Empty;

            decimal.TryParse(TextBoxPrice.Text.Replace("R$", string.Empty).Replace('.', ','), out decimal price);
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
                Code = TextBoxCode.Text,
                Manufacturer = TextBoxManufacturer.Text.FixTextToManageDataBaseResult(allowWithSpaces: true),
                Price = price,
                StockQuantity = stockQuantity,
                Type = ComboBoxType.SelectedItem.ToString()
            }, message);
        }

        private bool HasChangedAnyField(FilterViewModel updatedOil)
        {
            return
                updatedOil.Code != Model.Code ||
                updatedOil.Manufacturer != Model.Manufacturer ||
                updatedOil.StockQuantity != Model.StockQuantity ||
                updatedOil.Type != Model.Type ||
                updatedOil.Price != Model.Price;
        }
    }
}