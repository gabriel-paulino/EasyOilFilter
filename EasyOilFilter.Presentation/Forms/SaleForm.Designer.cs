namespace EasyOilFilter.Presentation.Forms
{
    partial class SaleForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.LabelDescription = new System.Windows.Forms.Label();
            this.TextBoxDescription = new System.Windows.Forms.TextBox();
            this.LabelPaymentMethod = new System.Windows.Forms.Label();
            this.ComboBoxPaymentMethod = new System.Windows.Forms.ComboBox();
            this.LabelDiscount = new System.Windows.Forms.Label();
            this.TextBoxDiscountPercentage = new System.Windows.Forms.TextBox();
            this.LabelPercentageDiscount = new System.Windows.Forms.Label();
            this.TextBoxDiscountValue = new System.Windows.Forms.TextBox();
            this.LabelTotal = new System.Windows.Forms.Label();
            this.TextBoxTotal = new System.Windows.Forms.TextBox();
            this.LabelRemarks = new System.Windows.Forms.Label();
            this.TextBoxRemarks = new System.Windows.Forms.TextBox();
            this.LabelDate = new System.Windows.Forms.Label();
            this.DateTimePickerSaleDate = new System.Windows.Forms.DateTimePicker();
            this.GroupBoxContent = new System.Windows.Forms.GroupBox();
            this.Grid = new System.Windows.Forms.DataGridView();
            this.ButtonProcessSale = new System.Windows.Forms.Button();
            this.GroupBoxContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelDescription
            // 
            this.LabelDescription.AutoSize = true;
            this.LabelDescription.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelDescription.Location = new System.Drawing.Point(15, 19);
            this.LabelDescription.Name = "LabelDescription";
            this.LabelDescription.Size = new System.Drawing.Size(56, 19);
            this.LabelDescription.TabIndex = 0;
            this.LabelDescription.Text = "Veículo";
            // 
            // TextBoxDescription
            // 
            this.TextBoxDescription.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextBoxDescription.Location = new System.Drawing.Point(164, 19);
            this.TextBoxDescription.Name = "TextBoxDescription";
            this.TextBoxDescription.Size = new System.Drawing.Size(181, 27);
            this.TextBoxDescription.TabIndex = 1;
            // 
            // LabelPaymentMethod
            // 
            this.LabelPaymentMethod.AutoSize = true;
            this.LabelPaymentMethod.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelPaymentMethod.Location = new System.Drawing.Point(15, 66);
            this.LabelPaymentMethod.Name = "LabelPaymentMethod";
            this.LabelPaymentMethod.Size = new System.Drawing.Size(146, 19);
            this.LabelPaymentMethod.TabIndex = 2;
            this.LabelPaymentMethod.Text = "Forma de pagamento";
            // 
            // ComboBoxPaymentMethod
            // 
            this.ComboBoxPaymentMethod.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ComboBoxPaymentMethod.FormattingEnabled = true;
            this.ComboBoxPaymentMethod.Location = new System.Drawing.Point(164, 66);
            this.ComboBoxPaymentMethod.Name = "ComboBoxPaymentMethod";
            this.ComboBoxPaymentMethod.Size = new System.Drawing.Size(181, 27);
            this.ComboBoxPaymentMethod.TabIndex = 3;
            // 
            // LabelDiscount
            // 
            this.LabelDiscount.AutoSize = true;
            this.LabelDiscount.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelDiscount.Location = new System.Drawing.Point(510, 396);
            this.LabelDiscount.Name = "LabelDiscount";
            this.LabelDiscount.Size = new System.Drawing.Size(70, 19);
            this.LabelDiscount.TabIndex = 4;
            this.LabelDiscount.Text = "Desconto";
            // 
            // TextBoxDiscountPercentage
            // 
            this.TextBoxDiscountPercentage.Location = new System.Drawing.Point(586, 396);
            this.TextBoxDiscountPercentage.Name = "TextBoxDiscountPercentage";
            this.TextBoxDiscountPercentage.Size = new System.Drawing.Size(56, 23);
            this.TextBoxDiscountPercentage.TabIndex = 5;
            this.TextBoxDiscountPercentage.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxDiscountPercentage_Validating);
            this.TextBoxDiscountPercentage.Validated += new System.EventHandler(this.TextBoxDiscountPercentage_Validated);
            // 
            // LabelPercentageDiscount
            // 
            this.LabelPercentageDiscount.AutoSize = true;
            this.LabelPercentageDiscount.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelPercentageDiscount.Location = new System.Drawing.Point(645, 400);
            this.LabelPercentageDiscount.Name = "LabelPercentageDiscount";
            this.LabelPercentageDiscount.Size = new System.Drawing.Size(20, 19);
            this.LabelPercentageDiscount.TabIndex = 6;
            this.LabelPercentageDiscount.Text = "%";
            // 
            // TextBoxDiscountValue
            // 
            this.TextBoxDiscountValue.Location = new System.Drawing.Point(668, 396);
            this.TextBoxDiscountValue.Name = "TextBoxDiscountValue";
            this.TextBoxDiscountValue.Size = new System.Drawing.Size(117, 23);
            this.TextBoxDiscountValue.TabIndex = 7;
            // 
            // LabelTotal
            // 
            this.LabelTotal.AutoSize = true;
            this.LabelTotal.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelTotal.Location = new System.Drawing.Point(510, 445);
            this.LabelTotal.Name = "LabelTotal";
            this.LabelTotal.Size = new System.Drawing.Size(41, 19);
            this.LabelTotal.TabIndex = 8;
            this.LabelTotal.Text = "Total";
            // 
            // TextBoxTotal
            // 
            this.TextBoxTotal.Location = new System.Drawing.Point(668, 445);
            this.TextBoxTotal.Name = "TextBoxTotal";
            this.TextBoxTotal.Size = new System.Drawing.Size(117, 23);
            this.TextBoxTotal.TabIndex = 9;
            // 
            // LabelRemarks
            // 
            this.LabelRemarks.AutoSize = true;
            this.LabelRemarks.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelRemarks.Location = new System.Drawing.Point(15, 390);
            this.LabelRemarks.Name = "LabelRemarks";
            this.LabelRemarks.Size = new System.Drawing.Size(93, 19);
            this.LabelRemarks.TabIndex = 10;
            this.LabelRemarks.Text = "Observações";
            // 
            // TextBoxRemarks
            // 
            this.TextBoxRemarks.Location = new System.Drawing.Point(164, 390);
            this.TextBoxRemarks.Multiline = true;
            this.TextBoxRemarks.Name = "TextBoxRemarks";
            this.TextBoxRemarks.Size = new System.Drawing.Size(181, 117);
            this.TextBoxRemarks.TabIndex = 11;
            // 
            // LabelDate
            // 
            this.LabelDate.AutoSize = true;
            this.LabelDate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelDate.Location = new System.Drawing.Point(510, 17);
            this.LabelDate.Name = "LabelDate";
            this.LabelDate.Size = new System.Drawing.Size(103, 19);
            this.LabelDate.TabIndex = 12;
            this.LabelDate.Text = "Data da venda";
            // 
            // DateTimePickerSaleDate
            // 
            this.DateTimePickerSaleDate.CalendarFont = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DateTimePickerSaleDate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DateTimePickerSaleDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTimePickerSaleDate.Location = new System.Drawing.Point(668, 17);
            this.DateTimePickerSaleDate.Name = "DateTimePickerSaleDate";
            this.DateTimePickerSaleDate.Size = new System.Drawing.Size(117, 27);
            this.DateTimePickerSaleDate.TabIndex = 13;
            // 
            // GroupBoxContent
            // 
            this.GroupBoxContent.Controls.Add(this.Grid);
            this.GroupBoxContent.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GroupBoxContent.Location = new System.Drawing.Point(12, 126);
            this.GroupBoxContent.Name = "GroupBoxContent";
            this.GroupBoxContent.Size = new System.Drawing.Size(776, 234);
            this.GroupBoxContent.TabIndex = 14;
            this.GroupBoxContent.TabStop = false;
            this.GroupBoxContent.Text = "Conteúdo";
            // 
            // Grid
            // 
            this.Grid.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Grid.DefaultCellStyle = dataGridViewCellStyle2;
            this.Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid.Location = new System.Drawing.Point(3, 23);
            this.Grid.MultiSelect = false;
            this.Grid.Name = "Grid";
            this.Grid.RowTemplate.Height = 25;
            this.Grid.Size = new System.Drawing.Size(770, 208);
            this.Grid.TabIndex = 0;
            this.Grid.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellValidated);
            this.Grid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.DataGridView_CellValidating);
            this.Grid.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Grid_RowHeaderMouseDoubleClick);
            // 
            // ButtonProcessSale
            // 
            this.ButtonProcessSale.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ButtonProcessSale.Location = new System.Drawing.Point(12, 551);
            this.ButtonProcessSale.Name = "ButtonProcessSale";
            this.ButtonProcessSale.Size = new System.Drawing.Size(93, 28);
            this.ButtonProcessSale.TabIndex = 15;
            this.ButtonProcessSale.UseVisualStyleBackColor = true;
            this.ButtonProcessSale.Click += new System.EventHandler(this.ButtonProcessSale_Click);
            // 
            // SaleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 601);
            this.Controls.Add(this.ButtonProcessSale);
            this.Controls.Add(this.GroupBoxContent);
            this.Controls.Add(this.DateTimePickerSaleDate);
            this.Controls.Add(this.LabelDate);
            this.Controls.Add(this.TextBoxRemarks);
            this.Controls.Add(this.LabelRemarks);
            this.Controls.Add(this.TextBoxTotal);
            this.Controls.Add(this.LabelTotal);
            this.Controls.Add(this.TextBoxDiscountValue);
            this.Controls.Add(this.LabelPercentageDiscount);
            this.Controls.Add(this.TextBoxDiscountPercentage);
            this.Controls.Add(this.LabelDiscount);
            this.Controls.Add(this.ComboBoxPaymentMethod);
            this.Controls.Add(this.LabelPaymentMethod);
            this.Controls.Add(this.TextBoxDescription);
            this.Controls.Add(this.LabelDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SaleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Venda";
            this.Load += new System.EventHandler(this.SaleForm_Load);
            this.GroupBoxContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label LabelDescription;
        private TextBox TextBoxDescription;
        private Label LabelPaymentMethod;
        private ComboBox ComboBoxPaymentMethod;
        private Label LabelDiscount;
        private TextBox TextBoxDiscountPercentage;
        private Label LabelPercentageDiscount;
        private TextBox TextBoxDiscountValue;
        private Label LabelTotal;
        private TextBox TextBoxTotal;
        private Label LabelRemarks;
        private TextBox TextBoxRemarks;
        private Label LabelDate;
        private DateTimePicker DateTimePickerSaleDate;
        private GroupBox GroupBoxContent;
        private DataGridView Grid;
        private Button ButtonProcessSale;
    }
}