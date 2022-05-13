using EasyOilFilter.Domain.ViewModels.OilViewModel;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class OilDetailForm : Form
    {
        public OilViewModel Model { get; set; }
        public OilDetailForm()
        {
            Model = new OilViewModel();
            InitializeComponent();
        }

        private void OilDetailForm_Load(object sender, EventArgs e)
        {
            FillFieldsWithOilInfo();
        }

        private void FillFieldsWithOilInfo()
        {
            TextBoxId.Text = Model.Id.ToString();
            TextBoxName.Text = Model.Name;
            TextBoxViscosity.Text = Model.Viscosity;
            TextBoxPrice.Text = Model.Price.ToString("C2");
            TextBoxStockQuantity.Text = Model.StockQuantity.ToString("F2");

            //ToDo: Preencher Combos
        }
    }
}