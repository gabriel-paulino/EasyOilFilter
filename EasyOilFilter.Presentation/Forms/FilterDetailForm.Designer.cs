namespace EasyOilFilter.Presentation.Forms
{
    partial class FilterDetailForm
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
            this.LabelCode = new System.Windows.Forms.Label();
            this.TextBoxCode = new System.Windows.Forms.TextBox();
            this.LabelPrice = new System.Windows.Forms.Label();
            this.TextBoxPrice = new System.Windows.Forms.TextBox();
            this.LabelTipo = new System.Windows.Forms.Label();
            this.TextBoxStockQuantity = new System.Windows.Forms.TextBox();
            this.LabelStockQuantity = new System.Windows.Forms.Label();
            this.ComboBoxType = new System.Windows.Forms.ComboBox();
            this.ButtonProcess = new System.Windows.Forms.Button();
            this.TextBoxManufacturer = new System.Windows.Forms.TextBox();
            this.LabelManufacturer = new System.Windows.Forms.Label();
            this.ButtonDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabelCode
            // 
            this.LabelCode.AutoSize = true;
            this.LabelCode.Location = new System.Drawing.Point(27, 14);
            this.LabelCode.Name = "LabelCode";
            this.LabelCode.Size = new System.Drawing.Size(54, 19);
            this.LabelCode.TabIndex = 0;
            this.LabelCode.Text = "Código";
            // 
            // TextBoxCode
            // 
            this.TextBoxCode.Location = new System.Drawing.Point(27, 36);
            this.TextBoxCode.Name = "TextBoxCode";
            this.TextBoxCode.Size = new System.Drawing.Size(356, 27);
            this.TextBoxCode.TabIndex = 0;
            // 
            // LabelPrice
            // 
            this.LabelPrice.AutoSize = true;
            this.LabelPrice.Location = new System.Drawing.Point(27, 119);
            this.LabelPrice.Name = "LabelPrice";
            this.LabelPrice.Size = new System.Drawing.Size(45, 19);
            this.LabelPrice.TabIndex = 4;
            this.LabelPrice.Text = "Preço";
            // 
            // TextBoxPrice
            // 
            this.TextBoxPrice.Location = new System.Drawing.Point(27, 141);
            this.TextBoxPrice.Name = "TextBoxPrice";
            this.TextBoxPrice.Size = new System.Drawing.Size(356, 27);
            this.TextBoxPrice.TabIndex = 2;
            // 
            // LabelTipo
            // 
            this.LabelTipo.AutoSize = true;
            this.LabelTipo.Location = new System.Drawing.Point(27, 229);
            this.LabelTipo.Name = "LabelTipo";
            this.LabelTipo.Size = new System.Drawing.Size(37, 19);
            this.LabelTipo.TabIndex = 8;
            this.LabelTipo.Text = "Tipo";
            // 
            // TextBoxStockQuantity
            // 
            this.TextBoxStockQuantity.Location = new System.Drawing.Point(27, 197);
            this.TextBoxStockQuantity.Name = "TextBoxStockQuantity";
            this.TextBoxStockQuantity.Size = new System.Drawing.Size(356, 27);
            this.TextBoxStockQuantity.TabIndex = 3;
            // 
            // LabelStockQuantity
            // 
            this.LabelStockQuantity.AutoSize = true;
            this.LabelStockQuantity.Location = new System.Drawing.Point(27, 175);
            this.LabelStockQuantity.Name = "LabelStockQuantity";
            this.LabelStockQuantity.Size = new System.Drawing.Size(85, 19);
            this.LabelStockQuantity.TabIndex = 6;
            this.LabelStockQuantity.Text = "Em estoque";
            // 
            // ComboBoxType
            // 
            this.ComboBoxType.FormattingEnabled = true;
            this.ComboBoxType.Location = new System.Drawing.Point(27, 251);
            this.ComboBoxType.Name = "ComboBoxType";
            this.ComboBoxType.Size = new System.Drawing.Size(356, 27);
            this.ComboBoxType.TabIndex = 4;
            // 
            // ButtonProcess
            // 
            this.ButtonProcess.Location = new System.Drawing.Point(27, 368);
            this.ButtonProcess.Name = "ButtonProcess";
            this.ButtonProcess.Size = new System.Drawing.Size(90, 30);
            this.ButtonProcess.TabIndex = 6;
            this.ButtonProcess.UseVisualStyleBackColor = true;
            this.ButtonProcess.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // TextBoxManufacturer
            // 
            this.TextBoxManufacturer.Location = new System.Drawing.Point(27, 89);
            this.TextBoxManufacturer.Name = "TextBoxManufacturer";
            this.TextBoxManufacturer.Size = new System.Drawing.Size(356, 27);
            this.TextBoxManufacturer.TabIndex = 1;
            // 
            // LabelManufacturer
            // 
            this.LabelManufacturer.AutoSize = true;
            this.LabelManufacturer.Location = new System.Drawing.Point(27, 67);
            this.LabelManufacturer.Name = "LabelManufacturer";
            this.LabelManufacturer.Size = new System.Drawing.Size(77, 19);
            this.LabelManufacturer.TabIndex = 14;
            this.LabelManufacturer.Text = "Fabricante";
            // 
            // ButtonDelete
            // 
            this.ButtonDelete.Location = new System.Drawing.Point(293, 368);
            this.ButtonDelete.Name = "ButtonDelete";
            this.ButtonDelete.Size = new System.Drawing.Size(90, 30);
            this.ButtonDelete.TabIndex = 15;
            this.ButtonDelete.Text = "Deletar";
            this.ButtonDelete.UseVisualStyleBackColor = true;
            this.ButtonDelete.Visible = false;
            this.ButtonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // FilterDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 422);
            this.Controls.Add(this.ButtonDelete);
            this.Controls.Add(this.TextBoxManufacturer);
            this.Controls.Add(this.LabelManufacturer);
            this.Controls.Add(this.ButtonProcess);
            this.Controls.Add(this.ComboBoxType);
            this.Controls.Add(this.LabelTipo);
            this.Controls.Add(this.TextBoxStockQuantity);
            this.Controls.Add(this.LabelStockQuantity);
            this.Controls.Add(this.TextBoxPrice);
            this.Controls.Add(this.LabelPrice);
            this.Controls.Add(this.TextBoxCode);
            this.Controls.Add(this.LabelCode);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "FilterDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtro";
            this.Load += new System.EventHandler(this.FilterDetailForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label LabelCode;
        private TextBox TextBoxCode;
        private Label LabelPrice;
        private TextBox TextBoxPrice;
        private Label LabelTipo;
        private TextBox TextBoxStockQuantity;
        private Label LabelStockQuantity;
        private ComboBox ComboBoxType;
        private Button ButtonProcess;
        private TextBox TextBoxManufacturer;
        private Label LabelManufacturer;
        private Button ButtonDelete;
    }
}