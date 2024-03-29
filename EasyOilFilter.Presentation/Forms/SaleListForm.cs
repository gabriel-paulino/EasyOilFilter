﻿using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.ViewModels.SaleViewModel;
using EasyOilFilter.Presentation.Enums;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class SaleListForm : Form
    {
        private readonly ISaleService _saleService;
        private readonly IProductService _productService;
        private IEnumerable<SaleViewModel> _sales = new List<SaleViewModel>();

        public SaleListForm(ISaleService saleService, IProductService productService)
        {
            _saleService = saleService;
            _productService = productService;
            InitializeComponent();
        }

        private void SaleForm_Load(object sender, EventArgs e)
        {
            SetDateInfo(DateTime.Today);
            ConfigureGrid();
            SearchSales(DateTime.Today);
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            SearchSales(DateTimePickerSearch.Value);
        }

        private void ResetList()
        {
            SetTotalLabel(default);
            DataGridView.DataSource = new List<SaleViewModel>();
        }

        private void SetDateInfo(DateTime date)
        {
            LabelDate.Text = date.ToString("d");
            DateTimePickerSearch.Value = date;
        }

        private void SetTotalLabel(decimal total)
        {
            LabelTotal.Text = total.ToString("C2");
        }

        private void SetDateLabel(DateTime date)
        {
            LabelDate.Text = date.ToString("d");
        }

        private void ConfigureGrid()
        {
            DataGridView.DataSource = new List<SaleViewModel>();
            DataGridView.ReadOnly = true;
            DataGridView.Columns["Id"].Visible = false;
            DataGridView.Columns["Discount"].Visible = false;
            DataGridView.Columns["Date"].Visible = false;
            DataGridView.Columns["Remarks"].Visible = false;
            DataGridView.Columns["Items"].Visible = false;
            DataGridView.Columns["Status"].Visible = false;
            DataGridView.Columns["Description"].HeaderText = "Descrição";
            DataGridView.Columns["PaymentMethod"].HeaderText = "Forma de pagamento";
            DataGridView.Columns["Total"].HeaderText = "Valor";
            DataGridView.Columns["Total"].DefaultCellStyle.Format = "C2";
            DataGridView.Columns["Description"].MinimumWidth = 310;
            DataGridView.Columns["PaymentMethod"].MinimumWidth = 155;
            DataGridView.Columns["Total"].MinimumWidth = 140;
            DataGridView.AutoResizeColumns();
        }

        private void ButtonAddSale_Click(object sender, EventArgs e)
        {
            bool isAdd = false;
            DateTime saleDate = DateTimePickerSearch.Value;

            using (var saleForm = new SaleForm(_saleService, _productService))
            {
                saleForm.Mode = FormMode.Add;
                saleForm.ShowDialog();
                isAdd = saleForm.IsAdd;
                saleDate = saleForm.Date;
            }

            if (isAdd)
                SearchSales(saleDate.Date);
        }

        private async void SearchSales(DateTime date)
        {
            _sales = await _saleService.Get(date);
            SetDateLabel(date);

            if (_sales?.Any() ?? false)
            {
                DataGridView.DataSource = _sales.ToList();
                SetTotalLabel(_sales.Sum(sale => sale.Total));
                return;
            }

            ResetList();
        }

        private void DataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var saleId = DataGridView
            .Rows[e.RowIndex]
            .Cells["Id"]
            .Value.ToString();

            Guid.TryParse(saleId, out Guid id);

            var selectedSaleModel = _sales.FirstOrDefault(sale => sale.Id == id);

            if (selectedSaleModel is null)
                return;

            bool isCanceled = false;
            DateTime saleDate = DateTimePickerSearch.Value;

            using (var saleForm = new SaleForm(_saleService, _productService))
            {
                saleForm.Model = selectedSaleModel;
                saleForm.Mode = FormMode.OnlyReadCanCancel;
                saleForm.ShowDialog();
                isCanceled = saleForm.IsCanceled;
                saleDate = saleForm.Date;
            }

            if (isCanceled)
                SearchSales(saleDate.Date);
        }
    }
}