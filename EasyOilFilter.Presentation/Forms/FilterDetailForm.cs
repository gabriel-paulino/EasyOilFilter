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
        private readonly IProductService _productService;
        public FilterViewModel Model { get; set; }
        public FormMode Mode { get; set; }
        public bool IsUpdate { get; private set; }
        public bool IsDelete { get; private set; }
        public bool IsAdd { get; private set; }

        public FilterDetailForm(IProductService productService)
        {
            _productService = productService;
            Model = new FilterViewModel();
            IsUpdate = false;
            IsDelete = false;
            IsAdd = false;
            InitializeComponent();
        }

        private void FilterDetailForm_Load(object sender, EventArgs e)
        {
            LoadFilterTypeComboBox();

            if (Mode == FormMode.Update)
            {
                ButtonProcess.Text = "Atualizar";
                ButtonDelete.Visible = true;
                FillFieldsWithFilterDetails();
                return;
            }
            ButtonProcess.Text = "Adicionar";
        }

        private void FillFieldsWithFilterDetails()
        {
            TextBoxCode.Text = Model.Name;
            TextBoxManufacturer.Text = Model.Manufacturer;
            TextBoxPrice.Text = Model.DefaultPrice.ToString("C2");
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

        private async void ButtonDelete_Click(object sender, EventArgs e)
        {
            var choice = MessageBox.Show(
                    "Deletar um filtro é um processo irreversível. Deseja continuar?",
                    "Alerta",
                    MessageBoxButtons.YesNo);

            if (choice == DialogResult.No)
                return;

            (bool sucess, string errorMessage) = await _productService.Delete(Model.Id);

            MessageBox.Show(sucess
                ? $"Filtro {Model.Name} removido com sucesso."
                : $"Falha ao deletar filtro {Model.Name}.{Environment.NewLine}" +
                  $"Retorno: {errorMessage}");

            if (sucess)
            {
                IsDelete = true;
                this.Close();
            }
        }

        private async void AddFilter()
        {
            var (model, message) = GetAddFilterViewModel();

            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message);
                return;
            }

            var filters = await _productService.GetFiltersByName(model.Name);

            if (filters.Any())
            {
                MessageBox.Show($"O filtro: {model.Name} já está adicionado na base de dados.");
                return;
            }

            var (result, error) = await _productService.Create(model);

            if (result == default)
            {
                MessageBox.Show($"Falha ao adicionar filtro: {model.Name}. Retorno: {error}");
                return;
            }

            IsAdd = true;
            MessageBox.Show($"Filtro de {model.FilterType}: {model.Name} adicionado com sucesso.");
            ResetOilDetailForm();
        }

        private void ResetOilDetailForm()
        {
            TextBoxCode.Clear();
            TextBoxManufacturer.Clear();
            TextBoxPrice.Clear();
            TextBoxStockQuantity.Clear();
            ComboBoxType.SelectedIndex = (int)FilterType.None;
        }

        private async void UpdateFilter()
        {
            var (model, message) = GetFilterViewModel();

            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message);
                return;
            }

            bool anyChange = HasChangedAnyField(model);

            if (!anyChange)
                return;

            var (result, error) = await _productService.Update(Model.Id, model);

            if (result == default)
            {
                MessageBox.Show($"Falha ao atualizar filtro: {TextBoxCode.Text}. Retorno: {error}");
                return;
            }

            IsUpdate = true;
            MessageBox.Show($"{model.FilterType}: {TextBoxCode.Text} atualizado com sucesso.");
        }

        private (FilterViewModel? model, string message) GetFilterViewModel()
        {
            string message = GetValidateFormMessage();

            if (!string.IsNullOrEmpty(message))
                return (new FilterViewModel(), message);

            return (new FilterViewModel(

                Id: Model.Id,
                Name: TextBoxCode.Text,
                Manufacturer: TextBoxManufacturer.Text.FixTextToManageDataBaseResult(allowWithSpaces: true),
                DefaultPrice: GetDecimalValueOnTextBox(TextBoxPrice),
                StockQuantity: decimal.Parse(TextBoxStockQuantity.Text.Replace('.', ',')),
                FilterType: ComboBoxType?.SelectedItem?.ToString()
            ), message);
        }

        private (AddFilterViewModel model, string message) GetAddFilterViewModel()
        {
            string message = GetValidateFormMessage();

            if (!string.IsNullOrEmpty(message))
                return (new AddFilterViewModel(), message);

            return (new AddFilterViewModel()
            {
                Name = TextBoxCode.Text,
                Manufacturer = TextBoxManufacturer.Text.FixTextToManageDataBaseResult(allowWithSpaces: true),
                DefaultPrice = GetDecimalValueOnTextBox(TextBoxPrice),
                StockQuantity = decimal.Parse(TextBoxStockQuantity.Text.Replace('.', ',')),
                FilterType = ComboBoxType.SelectedItem?.ToString() ?? string.Empty
            }, message);
        }

        private decimal GetDecimalValueOnTextBox(TextBox textBox)
        {
            decimal.TryParse(textBox.Text.Replace("R$", string.Empty), out decimal value);
            return value;
        }

        private string GetValidateFormMessage()
        {
            string validationMessage = string.Empty;
            decimal defaultPrice = GetDecimalValueOnTextBox(TextBoxPrice);
            decimal.TryParse(TextBoxStockQuantity.Text.Replace('.', ','), out decimal stockQuantity);

            bool invalidDefaultPrice = defaultPrice == 0.0m;
            bool invalidStockQuantity = stockQuantity == 0;

            if (string.IsNullOrEmpty(TextBoxCode.Text))
                validationMessage += $"O preenchimento do campo 'Código' é obrigatório.{Environment.NewLine}";

            if (string.IsNullOrEmpty(TextBoxManufacturer.Text))
                validationMessage += $"O preenchimento do campo 'Fabricante' é obrigatório.{Environment.NewLine}";

            if (string.IsNullOrEmpty(ComboBoxType.SelectedItem?.ToString()))
                validationMessage += $"O preenchimento do campo 'Tipo' é obrigatório.{Environment.NewLine}";

            if (invalidDefaultPrice)
                validationMessage += $"O valor do campo 'Preço' é inválido.{Environment.NewLine}";

            if (invalidStockQuantity)
                validationMessage += $"O valor do campo 'Em estoque' é inválido.{Environment.NewLine}";

            return validationMessage;
        }

        private bool HasChangedAnyField(FilterViewModel updatedFilter)
        {
            return
                updatedFilter.Name != Model.Name ||
                updatedFilter.Manufacturer != Model.Manufacturer ||
                updatedFilter.StockQuantity != Model.StockQuantity ||
                updatedFilter.FilterType != Model.FilterType ||
                updatedFilter.DefaultPrice != Model.DefaultPrice;
        }
    }
}