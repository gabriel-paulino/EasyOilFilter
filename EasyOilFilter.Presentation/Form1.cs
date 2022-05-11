using EasyOilFilter.Domain.Contracts.Services;

namespace EasyOilFilter.Presentation
{
    public partial class Form1 : Form
    {
        private readonly IOilService _oilService;

        public Form1(IOilService oilService)
        {
            _oilService = oilService;

            InitializeComponent();
        }
    }
}