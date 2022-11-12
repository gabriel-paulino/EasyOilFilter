using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.ViewModels.PaymentViewModel;
using EasyOilFilter.Domain.ViewModels.PurchaseViewModel;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class PaymentForm : Form
    {
        private readonly IPaymentService _paymentService;

        public PurchaseViewModel CurrentPurchase { get; set; } = new PurchaseViewModel();
        public bool IsAdd { get; private set; } = false;

        public PaymentForm(IPaymentService paymentService)
        {
            _paymentService = paymentService;
            InitializeComponent();
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            PaymentDateTimePicker.Value = DateTime.Today;
        }

        private async void AddPaymentButton_Click(object sender, EventArgs e)
        {
            var choice = MessageBox.Show(
                    $"Adicionar uma pagamento é um processo irreversível.{Environment.NewLine}" +
                    $"Deseja continuar?",
                    "Alerta",
                    MessageBoxButtons.YesNo);

            if (choice == DialogResult.No)
                return;

            var viewModel = new AddPaymentViewModel()
            {
                PaymentDate = PaymentDateTimePicker.Value,
                PurchaseId = CurrentPurchase.Id,
                AmountPaid = NumericUpDownPaymentValue.Value,
                PurchaseTotal = CurrentPurchase.Total
            };

            var (isAdd, errors) = await _paymentService.Add(viewModel);

            MessageBox.Show(isAdd
            ? "Pagamento adicionado com sucesso."
            : $"Erro ao adicionar pagamento.{Environment.NewLine}" +
              $"Retorno:{Environment.NewLine}" +
              $"{string.Join(Environment.NewLine, errors)}");

            if (isAdd)
            {
                IsAdd = true;
                Close();
            }
        }

        private void CancelPaymentButton_Click(object sender, EventArgs e) => Close();
    }
}