using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Extensions;
using EasyOilFilter.Domain.Shared.Utils;
using EasyOilFilter.Domain.ViewModels.OilViewModel;
using EasyOilFilter.Presentation.Enums;
using System.Globalization;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class OilDetailForm : Form
    {
        private readonly IProductService _productService;
        public OilViewModel Model { get; set; }
        public FormMode Mode { get; set; }
        public bool IsUpdate { get; private set; }
        public bool IsAdd { get; private set; }

        public OilDetailForm(IProductService productService)
        {
            _productService = productService;
            Model = new OilViewModel();
            IsUpdate = false;
            IsAdd = false;
            InitializeComponent();
        }

        private void OilDetailForm_Load(object sender, EventArgs e)
        {
            LoadOilTypeComboBox();
            LoadUoMComboBox();

            if (Mode == FormMode.Update)
            {
                ButtonProcess.Text = "Atualizar";
                FillFieldsWithOilDetails();
                return;
            }
            ButtonProcess.Text = "Adicionar";
        }

        private void FillFieldsWithOilDetails()
        {
            TextBoxName.Text = Model.Name;
            TextBoxViscosity.Text = Model.Viscosity;
            TextBoxApi.Text = Model.Api;
            TextBoxDefaultPrice.Text = Model.DefaultPrice.ToString("C2");
            TextBoxStockQuantity.Text = Model.StockQuantity.ToString("F2");
            ComboBoxType.SelectedIndex = (int)EnumUtility.GetEnumByDescription<OilType>(Model.OilType);
            ComboBoxDefaultUoM.SelectedIndex = (int)EnumUtility.GetEnumByDescription<UoM>(Model.DefaultUoM);
            CheckBoxAlternative.Checked = Model.HasAlternative;

            if (Model.HasAlternative)
            {
                SetAlternativeComponentsVisible(true);               
                TextBoxAlternativePrice.Text = Model.AlternativePrice.ToString("C2");
                ComboBoxAlternativeUoM.SelectedIndex = (int)EnumUtility.GetEnumByDescription<UoM>(Model.AlternativeUoM);
            }
        }

        private void LoadOilTypeComboBox()
        {
            foreach (var type in EnumUtility.EnumToList<OilType>())
                ComboBoxType.Items.Add(type.GetDescription());
        }

        private void LoadUoMComboBox()
        {
            foreach (var type in EnumUtility.EnumToList<UoM>())
            {
                ComboBoxDefaultUoM.Items.Add(type.GetDescription());
                ComboBoxAlternativeUoM.Items.Add(type.GetDescription());
            }            
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            switch (Mode)
            {
                case FormMode.Add:
                    AddOil();
                    break;
                case FormMode.Update:
                    UpdateOil();
                    break;
                default:
                    break;
            }
        }

        private async void AddOil()
        {
            var (model, message) = GetAddOilViewModel();

            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message);
                return;
            }
                
            var oils = await _productService.GetOilsByName(model.Name);

            if (oils?.Any() ?? false)
            {
                MessageBox.Show($"O lubrificante: {model.Name} já está adicionado na base de dados.");
                return;
            }

            var result = await _productService.Create(model);

            if (result == default)
            {
                MessageBox.Show($"Falha ao adicionar lubrificante: {model.Name}.");
                return;
            }

            IsAdd = true;
            MessageBox.Show($"Lubrificante {model.OilType}: {model.Name} adicionado com sucesso.");
            ResetOilDetailForm();
        }

        private void ResetOilDetailForm()
        {
            TextBoxName.Clear();
            TextBoxViscosity.Clear();
            TextBoxApi.Clear();
            TextBoxDefaultPrice.Clear();
            TextBoxAlternativePrice.Clear();
            TextBoxStockQuantity.Clear();
            ComboBoxType.SelectedIndex = (int)OilType.None;
            ComboBoxDefaultUoM.SelectedIndex = -1;
            ComboBoxAlternativeUoM.SelectedIndex = -1;
        }

        private async void UpdateOil()
        {
            var (model, message) = GetOilViewModel();

            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message);
                return;
            }
                
            bool anyChange = HasChangedAnyField(model);

            if (!anyChange)
                return;

            var result = await _productService.Update(Model.Id, model);

            if (result == default)
            {
                MessageBox.Show($"Falha ao atualizar lubrificante: {TextBoxName.Text}.");
                return;
            }

            IsUpdate = true;
            MessageBox.Show($"{model.OilType}: {TextBoxName.Text} atualizado com sucesso.");
        }

        private (OilViewModel model, string message) GetOilViewModel()
        {
            string message = GetValidateFormMessage();

            if (!string.IsNullOrEmpty(message))
                return (new OilViewModel(), message);

            return (new OilViewModel()
            {
                Id = Model.Id,
                Name = TextBoxName.Text,
                Viscosity = TextBoxViscosity.Text.FixTextToManageDataBaseResult(allowWithSpaces: false),
                Api = TextBoxApi.Text.FixTextToManageDataBaseResult(allowWithSpaces: false),
                DefaultPrice = GetDecimalValueOnTextBox(TextBoxDefaultPrice),
                AlternativePrice = GetDecimalValueOnTextBox(TextBoxAlternativePrice),
                StockQuantity = decimal.Parse(TextBoxStockQuantity.Text.Replace('.', ',')),
                OilType = ComboBoxType.SelectedItem?.ToString(),
                DefaultUoM = ComboBoxDefaultUoM.SelectedItem?.ToString(),
                AlternativeUoM = ComboBoxAlternativeUoM.SelectedItem?.ToString() ?? string.Empty,
                HasAlternative = CheckBoxAlternative.Checked
            }, message);
        }

        private (AddOilViewModel model, string message) GetAddOilViewModel()
        {
            string message = GetValidateFormMessage();
            
            if (!string.IsNullOrEmpty(message))
                return (new AddOilViewModel(), message);

            return (new AddOilViewModel()
            {
                Name = TextBoxName.Text,
                Viscosity = TextBoxViscosity.Text.FixTextToManageDataBaseResult(allowWithSpaces: false),
                Api = TextBoxApi.Text.FixTextToManageDataBaseResult(allowWithSpaces: false),
                DefaultPrice = GetDecimalValueOnTextBox(TextBoxDefaultPrice),
                AlternativePrice = GetDecimalValueOnTextBox(TextBoxAlternativePrice),
                StockQuantity = decimal.Parse(TextBoxStockQuantity.Text.Replace('.', ',')),
                OilType = ComboBoxType.SelectedItem?.ToString(),
                DefaultUoM = ComboBoxDefaultUoM.SelectedItem?.ToString(),
                AlternativeUoM = ComboBoxAlternativeUoM.SelectedItem?.ToString() ?? string.Empty,
                HasAlternative = CheckBoxAlternative.Checked
            }, message);
        }

        private decimal GetDecimalValueOnTextBox(TextBox textBox)
        {
            decimal.TryParse(textBox.Text.Replace("R$",string.Empty), out decimal value);
            return value;
        }

        private string GetValidateFormMessage()
        {
            string validationMessage = string.Empty;
            decimal defaultPrice = GetDecimalValueOnTextBox(TextBoxDefaultPrice);
            decimal.TryParse(TextBoxStockQuantity.Text.Replace('.', ','), out decimal stockQuantity);

            bool invalidDefaultPrice = defaultPrice == 0.0m;
            bool invalidStockQuantity = stockQuantity == 0;

            if (string.IsNullOrEmpty(TextBoxName.Text))
                validationMessage += $"O preenchimento do campo 'Nome' é obrigatório.{Environment.NewLine}";

            if (string.IsNullOrEmpty(TextBoxViscosity.Text))
                validationMessage += $"O preenchimento do campo 'Viscosidade' é obrigatório.{Environment.NewLine}";

            if (string.IsNullOrEmpty(TextBoxApi.Text))
                validationMessage += $"O preenchimento do campo 'API' é obrigatório.{Environment.NewLine}";

            if (string.IsNullOrEmpty(ComboBoxType.SelectedItem?.ToString()))
                validationMessage += $"O preenchimento do campo 'Tipo' é obrigatório.{Environment.NewLine}";

            if (string.IsNullOrEmpty(ComboBoxDefaultUoM.SelectedItem?.ToString()))
                validationMessage += $"O preenchimento do campo 'Embalagem' é obrigatório.{Environment.NewLine}";

            if (invalidDefaultPrice)
                validationMessage = $"O valor do campo 'Preço' é inválido.{Environment.NewLine}";

            if (invalidStockQuantity)
                validationMessage += $"O valor do campo 'Em estoque' é inválido.{Environment.NewLine}";

            if (CheckBoxAlternative.Checked)
            {
                decimal alternativePrice = GetDecimalValueOnTextBox(TextBoxAlternativePrice);
                bool invalidSecondPrice = alternativePrice == 0.0m;

                if (invalidSecondPrice)
                    validationMessage = $"O valor do campo 'Preço' é inválido.{Environment.NewLine}";

                if (string.IsNullOrEmpty(ComboBoxAlternativeUoM.SelectedItem?.ToString()))
                    validationMessage += $"O preenchimento do campo 'Embalagem alternativa' é obrigatório.{Environment.NewLine}";

                bool defaultUoMIsEqualAlternativeUoM =
                    (ComboBoxDefaultUoM.SelectedItem?.ToString() ?? string.Empty) ==
                    (ComboBoxAlternativeUoM.SelectedItem?.ToString() ?? string.Empty);

                if (defaultUoMIsEqualAlternativeUoM)
                    validationMessage += $" O valor do campo 'Embalagem' deve ser diferente da 'Embalagem alternativa'.{Environment.NewLine}";
            }

            return validationMessage;
        }

        private bool HasChangedAnyField(OilViewModel updatedOil)
        {
            return
                updatedOil.Name != Model.Name ||
                updatedOil.Viscosity != Model.Viscosity ||
                updatedOil.Api != Model.Api ||
                updatedOil.StockQuantity != Model.StockQuantity ||
                updatedOil.OilType != Model.OilType ||
                updatedOil.DefaultUoM != Model.DefaultUoM ||
                updatedOil.AlternativeUoM != Model.AlternativeUoM ||
                updatedOil.DefaultPrice != Model.DefaultPrice ||
                updatedOil.AlternativePrice != Model.AlternativePrice;
        }

        private void CheckBoxAlternative_CheckedChanged(object sender, EventArgs e)
        {
            SetAlternativeComponentsVisible(CheckBoxAlternative.Checked);
        }

        private void SetAlternativeComponentsVisible(bool isVisible)
        {
            LabelAlternativePrice.Visible = isVisible;
            LabelAlternativeUoM.Visible = isVisible;
            TextBoxAlternativePrice.Visible = isVisible;
            ComboBoxAlternativeUoM.Visible = isVisible;
        }

        private void ComboBoxDefaultUoM_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectedValue = ComboBoxDefaultUoM.SelectedItem?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(selectedValue))
                return;

            LabelDefaultPrice.Text = $"Preço ({selectedValue})";
        }

        private void ComboBoxAlternativeUoM_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectedValue = ComboBoxAlternativeUoM.SelectedItem?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(selectedValue))
                return;

            LabelAlternativePrice.Text = $"Preço ({selectedValue})";
        }
    }
}