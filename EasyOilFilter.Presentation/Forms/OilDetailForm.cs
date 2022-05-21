﻿using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Extensions;
using EasyOilFilter.Domain.Shared.Utils;
using EasyOilFilter.Domain.ViewModels.OilViewModel;
using EasyOilFilter.Presentation.Enums;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class OilDetailForm : Form
    {
        private readonly IOilService _oilService;
        public OilViewModel Model { get; set; }
        public FormMode Mode { get; set; }
        public bool IsUpdate { get; private set; }
        public bool IsAdd { get; private set; }

        public OilDetailForm(IOilService oilService)
        {
            _oilService = oilService;
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
                ButtonSave.Text = "Atualizar";
                FillFieldsWithOilDetails();
                return;
            }
            ButtonSave.Text = "Adicionar";
        }

        private void FillFieldsWithOilDetails()
        {
            TextBoxName.Text = Model.Name;
            TextBoxViscosity.Text = Model.Viscosity;
            TextBoxPrice.Text = Model.Price.ToString("C2");
            TextBoxStockQuantity.Text = Model.StockQuantity.ToString("F2");
            ComboBoxType.SelectedIndex = (int)EnumUtility.GetEnumByDescription<OilType>(Model.Type);
            ComboBoxUoM.SelectedIndex = (int)EnumUtility.GetEnumByDescription<UoM>(Model.UnitOfMeasurement);
        }

        private void LoadOilTypeComboBox()
        {
            foreach (var type in EnumUtility.EnumToList<OilType>())
                ComboBoxType.Items.Add(type.GetDescription());
        }

        private void LoadUoMComboBox()
        {
            foreach (var type in EnumUtility.EnumToList<UoM>())
                ComboBoxUoM.Items.Add(type.GetDescription());
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
                MessageBox.Show(message);

            var oils = await _oilService.GetByName(model.Name);

            if (oils?.Any() ?? false)
            {
                MessageBox.Show($"O lubrificante: {TextBoxName.Text} já está adicionado na base de dados.");
                return;
            }

            var result = await _oilService.Create(model);

            if (result == default)
            {
                MessageBox.Show($"Falha ao adicionar lubrificante: {TextBoxName.Text}.");
                return;
            }

            IsAdd = true;
            MessageBox.Show($"Lubrificante: {TextBoxName.Text} adicionado com sucesso.");
        }

        private async void UpdateOil()
        {
            var (model, message) = GetOilViewModel();

            if (!string.IsNullOrEmpty(message))
                MessageBox.Show(message);

            bool anyChange = HasChangedAnyField(model);

            if (!anyChange)
                return;

            var result = await _oilService.Update(Model.Id, model);

            if (result == default)
            {
                MessageBox.Show($"Falha ao atualizar lubrificante: {TextBoxName.Text}.");
                return;
            }

            IsUpdate = true;
            MessageBox.Show($"Lubrificante: {TextBoxName.Text} atualizado com sucesso.");
        }

        private (OilViewModel model, string message) GetOilViewModel()
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

            return (new OilViewModel()
            {
                Id = Model.Id,
                Name = TextBoxName.Text,
                Viscosity = TextBoxViscosity.Text.FixTextToManageDataBaseResult(allowWithSpaces: false),
                Price = price,
                StockQuantity = stockQuantity,
                Type = ComboBoxType.SelectedItem.ToString(),
                UnitOfMeasurement = ComboBoxUoM.SelectedItem.ToString()
            }, message);
        }

        private (AddOilViewModel model, string message) GetAddOilViewModel()
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

            return (new AddOilViewModel()
            {
                Name = TextBoxName.Text,
                Viscosity = TextBoxViscosity.Text.FixTextToManageDataBaseResult(allowWithSpaces: false),
                Price = price,
                StockQuantity = stockQuantity,
                Type = ComboBoxType.SelectedItem.ToString(),
                UnitOfMeasurement = ComboBoxUoM.SelectedItem.ToString()
            }, message);
        }

        private bool HasChangedAnyField(OilViewModel updatedOil)
        {
            return
                updatedOil.Name != Model.Name ||
                updatedOil.Viscosity != Model.Viscosity ||
                updatedOil.StockQuantity != Model.StockQuantity ||
                updatedOil.Type != Model.Type ||
                updatedOil.UnitOfMeasurement != Model.UnitOfMeasurement ||
                updatedOil.Price != Model.Price;
        }
    }
}