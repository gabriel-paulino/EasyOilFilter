namespace EasyOilFilter.Presentation.Forms
{
    partial class PaymentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PaymentDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.PaymentDateLabel = new System.Windows.Forms.Label();
            this.ValuePaymentLabel = new System.Windows.Forms.Label();
            this.AddPaymentButton = new System.Windows.Forms.Button();
            this.CancelPaymentButton = new System.Windows.Forms.Button();
            this.NumericUpDownPaymentValue = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownPaymentValue)).BeginInit();
            this.SuspendLayout();
            // 
            // PaymentDateTimePicker
            // 
            this.PaymentDateTimePicker.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PaymentDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.PaymentDateTimePicker.Location = new System.Drawing.Point(23, 44);
            this.PaymentDateTimePicker.Name = "PaymentDateTimePicker";
            this.PaymentDateTimePicker.Size = new System.Drawing.Size(124, 27);
            this.PaymentDateTimePicker.TabIndex = 2;
            this.PaymentDateTimePicker.Value = new System.DateTime(2022, 5, 30, 19, 12, 59, 0);
            // 
            // PaymentDateLabel
            // 
            this.PaymentDateLabel.AutoSize = true;
            this.PaymentDateLabel.Location = new System.Drawing.Point(23, 26);
            this.PaymentDateLabel.Name = "PaymentDateLabel";
            this.PaymentDateLabel.Size = new System.Drawing.Size(31, 15);
            this.PaymentDateLabel.TabIndex = 3;
            this.PaymentDateLabel.Text = "Data";
            // 
            // ValuePaymentLabel
            // 
            this.ValuePaymentLabel.AutoSize = true;
            this.ValuePaymentLabel.Location = new System.Drawing.Point(23, 92);
            this.ValuePaymentLabel.Name = "ValuePaymentLabel";
            this.ValuePaymentLabel.Size = new System.Drawing.Size(33, 15);
            this.ValuePaymentLabel.TabIndex = 4;
            this.ValuePaymentLabel.Text = "Valor";
            // 
            // AddPaymentButton
            // 
            this.AddPaymentButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AddPaymentButton.Location = new System.Drawing.Point(23, 250);
            this.AddPaymentButton.Name = "AddPaymentButton";
            this.AddPaymentButton.Size = new System.Drawing.Size(80, 32);
            this.AddPaymentButton.TabIndex = 11;
            this.AddPaymentButton.Text = "Confirmar";
            this.AddPaymentButton.UseVisualStyleBackColor = true;
            this.AddPaymentButton.Click += new System.EventHandler(this.AddPaymentButton_Click);
            // 
            // CancelPaymentButton
            // 
            this.CancelPaymentButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CancelPaymentButton.Location = new System.Drawing.Point(265, 250);
            this.CancelPaymentButton.Name = "CancelPaymentButton";
            this.CancelPaymentButton.Size = new System.Drawing.Size(80, 32);
            this.CancelPaymentButton.TabIndex = 12;
            this.CancelPaymentButton.Text = "Cancelar";
            this.CancelPaymentButton.UseVisualStyleBackColor = true;
            this.CancelPaymentButton.Click += new System.EventHandler(this.CancelPaymentButton_Click);
            // 
            // NumericUpDownPaymentValue
            // 
            this.NumericUpDownPaymentValue.DecimalPlaces = 2;
            this.NumericUpDownPaymentValue.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericUpDownPaymentValue.Location = new System.Drawing.Point(23, 110);
            this.NumericUpDownPaymentValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.NumericUpDownPaymentValue.Name = "NumericUpDownPaymentValue";
            this.NumericUpDownPaymentValue.Size = new System.Drawing.Size(124, 27);
            this.NumericUpDownPaymentValue.TabIndex = 14;
            this.NumericUpDownPaymentValue.ThousandsSeparator = true;
            // 
            // PaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 294);
            this.Controls.Add(this.NumericUpDownPaymentValue);
            this.Controls.Add(this.CancelPaymentButton);
            this.Controls.Add(this.AddPaymentButton);
            this.Controls.Add(this.ValuePaymentLabel);
            this.Controls.Add(this.PaymentDateLabel);
            this.Controls.Add(this.PaymentDateTimePicker);
            this.MaximizeBox = false;
            this.Name = "PaymentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pagamentos";
            this.Load += new System.EventHandler(this.PaymentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownPaymentValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DateTimePicker PaymentDateTimePicker;
        private Label PaymentDateLabel;
        private Label ValuePaymentLabel;
        private Button AddPaymentButton;
        private Button CancelPaymentButton;
        private NumericUpDown NumericUpDownPaymentValue;
    }
}