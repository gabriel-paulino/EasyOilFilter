namespace EasyOilFilter.Presentation.Forms
{
    partial class PurchaseForm
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
            this.LabelProvider = new System.Windows.Forms.Label();
            this.TextBoxProvider = new System.Windows.Forms.TextBox();
            this.LabelTotal = new System.Windows.Forms.Label();
            this.TextBoxTotal = new System.Windows.Forms.TextBox();
            this.LabelRemarks = new System.Windows.Forms.Label();
            this.TextBoxRemarks = new System.Windows.Forms.TextBox();
            this.LabelDate = new System.Windows.Forms.Label();
            this.DateTimePickerPurchaseDate = new System.Windows.Forms.DateTimePicker();
            this.GroupBoxContent = new System.Windows.Forms.GroupBox();
            this.Grid = new System.Windows.Forms.DataGridView();
            this.ButtonProcessPurchase = new System.Windows.Forms.Button();
            this.GroupBoxContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelProvider
            // 
            this.LabelProvider.AutoSize = true;
            this.LabelProvider.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelProvider.Location = new System.Drawing.Point(15, 27);
            this.LabelProvider.Name = "LabelProvider";
            this.LabelProvider.Size = new System.Drawing.Size(81, 19);
            this.LabelProvider.TabIndex = 0;
            this.LabelProvider.Text = "Fornecedor";
            // 
            // TextBoxProvider
            // 
            this.TextBoxProvider.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextBoxProvider.Location = new System.Drawing.Point(164, 19);
            this.TextBoxProvider.Name = "TextBoxProvider";
            this.TextBoxProvider.Size = new System.Drawing.Size(181, 27);
            this.TextBoxProvider.TabIndex = 1;
            // 
            // LabelTotal
            // 
            this.LabelTotal.AutoSize = true;
            this.LabelTotal.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelTotal.Location = new System.Drawing.Point(510, 449);
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
            this.LabelDate.Location = new System.Drawing.Point(510, 25);
            this.LabelDate.Name = "LabelDate";
            this.LabelDate.Size = new System.Drawing.Size(40, 19);
            this.LabelDate.TabIndex = 12;
            this.LabelDate.Text = "Data";
            // 
            // DateTimePickerPurchaseDate
            // 
            this.DateTimePickerPurchaseDate.CalendarFont = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DateTimePickerPurchaseDate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DateTimePickerPurchaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTimePickerPurchaseDate.Location = new System.Drawing.Point(668, 17);
            this.DateTimePickerPurchaseDate.Name = "DateTimePickerPurchaseDate";
            this.DateTimePickerPurchaseDate.Size = new System.Drawing.Size(117, 27);
            this.DateTimePickerPurchaseDate.TabIndex = 13;
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
            // ButtonProcessPurchase
            // 
            this.ButtonProcessPurchase.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ButtonProcessPurchase.Location = new System.Drawing.Point(12, 551);
            this.ButtonProcessPurchase.Name = "ButtonProcessPurchase";
            this.ButtonProcessPurchase.Size = new System.Drawing.Size(93, 28);
            this.ButtonProcessPurchase.TabIndex = 15;
            this.ButtonProcessPurchase.UseVisualStyleBackColor = true;
            this.ButtonProcessPurchase.Click += new System.EventHandler(this.ButtonProcessPurchase_Click);
            // 
            // PurchaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 601);
            this.Controls.Add(this.ButtonProcessPurchase);
            this.Controls.Add(this.GroupBoxContent);
            this.Controls.Add(this.DateTimePickerPurchaseDate);
            this.Controls.Add(this.LabelDate);
            this.Controls.Add(this.TextBoxRemarks);
            this.Controls.Add(this.LabelRemarks);
            this.Controls.Add(this.TextBoxTotal);
            this.Controls.Add(this.LabelTotal);
            this.Controls.Add(this.TextBoxProvider);
            this.Controls.Add(this.LabelProvider);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PurchaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compra";
            this.Load += new System.EventHandler(this.PurchaseForm_Load);
            this.GroupBoxContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label LabelProvider;
        private TextBox TextBoxProvider;
        private Label LabelTotal;
        private TextBox TextBoxTotal;
        private Label LabelRemarks;
        private TextBox TextBoxRemarks;
        private Label LabelDate;
        private DateTimePicker DateTimePickerPurchaseDate;
        private GroupBox GroupBoxContent;
        private DataGridView Grid;
        private Button ButtonProcessPurchase;
    }
}