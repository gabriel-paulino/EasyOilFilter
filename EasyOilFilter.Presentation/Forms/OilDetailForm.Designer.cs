namespace EasyOilFilter.Presentation.Forms
{
    partial class OilDetailForm
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
            this.LabelName = new System.Windows.Forms.Label();
            this.TextBoxName = new System.Windows.Forms.TextBox();
            this.LabelPrice = new System.Windows.Forms.Label();
            this.TextBoxPrice = new System.Windows.Forms.TextBox();
            this.LabelTipo = new System.Windows.Forms.Label();
            this.TextBoxStockQuantity = new System.Windows.Forms.TextBox();
            this.LabelStockQuantity = new System.Windows.Forms.Label();
            this.ComboBoxType = new System.Windows.Forms.ComboBox();
            this.ButtonProcess = new System.Windows.Forms.Button();
            this.TextBoxViscosity = new System.Windows.Forms.TextBox();
            this.LabelViscosity = new System.Windows.Forms.Label();
            this.LabelUoM = new System.Windows.Forms.Label();
            this.ComboBoxUoM = new System.Windows.Forms.ComboBox();
            this.TextBoxApi = new System.Windows.Forms.TextBox();
            this.LabelApi = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LabelName
            // 
            this.LabelName.AutoSize = true;
            this.LabelName.Location = new System.Drawing.Point(27, 14);
            this.LabelName.Name = "LabelName";
            this.LabelName.Size = new System.Drawing.Size(47, 19);
            this.LabelName.TabIndex = 0;
            this.LabelName.Text = "Nome";
            // 
            // TextBoxName
            // 
            this.TextBoxName.Location = new System.Drawing.Point(27, 36);
            this.TextBoxName.Name = "TextBoxName";
            this.TextBoxName.Size = new System.Drawing.Size(356, 27);
            this.TextBoxName.TabIndex = 0;
            // 
            // LabelPrice
            // 
            this.LabelPrice.AutoSize = true;
            this.LabelPrice.Location = new System.Drawing.Point(27, 173);
            this.LabelPrice.Name = "LabelPrice";
            this.LabelPrice.Size = new System.Drawing.Size(45, 19);
            this.LabelPrice.TabIndex = 4;
            this.LabelPrice.Text = "Preço";
            // 
            // TextBoxPrice
            // 
            this.TextBoxPrice.Location = new System.Drawing.Point(27, 195);
            this.TextBoxPrice.Name = "TextBoxPrice";
            this.TextBoxPrice.Size = new System.Drawing.Size(356, 27);
            this.TextBoxPrice.TabIndex = 3;
            // 
            // LabelTipo
            // 
            this.LabelTipo.AutoSize = true;
            this.LabelTipo.Location = new System.Drawing.Point(27, 283);
            this.LabelTipo.Name = "LabelTipo";
            this.LabelTipo.Size = new System.Drawing.Size(37, 19);
            this.LabelTipo.TabIndex = 8;
            this.LabelTipo.Text = "Tipo";
            // 
            // TextBoxStockQuantity
            // 
            this.TextBoxStockQuantity.Location = new System.Drawing.Point(27, 251);
            this.TextBoxStockQuantity.Name = "TextBoxStockQuantity";
            this.TextBoxStockQuantity.Size = new System.Drawing.Size(356, 27);
            this.TextBoxStockQuantity.TabIndex = 4;
            // 
            // LabelStockQuantity
            // 
            this.LabelStockQuantity.AutoSize = true;
            this.LabelStockQuantity.Location = new System.Drawing.Point(27, 229);
            this.LabelStockQuantity.Name = "LabelStockQuantity";
            this.LabelStockQuantity.Size = new System.Drawing.Size(85, 19);
            this.LabelStockQuantity.TabIndex = 6;
            this.LabelStockQuantity.Text = "Em estoque";
            // 
            // ComboBoxType
            // 
            this.ComboBoxType.FormattingEnabled = true;
            this.ComboBoxType.Location = new System.Drawing.Point(27, 305);
            this.ComboBoxType.Name = "ComboBoxType";
            this.ComboBoxType.Size = new System.Drawing.Size(356, 27);
            this.ComboBoxType.TabIndex = 5;
            // 
            // ButtonProcess
            // 
            this.ButtonProcess.Location = new System.Drawing.Point(27, 452);
            this.ButtonProcess.Name = "ButtonProcess";
            this.ButtonProcess.Size = new System.Drawing.Size(90, 30);
            this.ButtonProcess.TabIndex = 7;
            this.ButtonProcess.UseVisualStyleBackColor = true;
            this.ButtonProcess.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // TextBoxViscosity
            // 
            this.TextBoxViscosity.Location = new System.Drawing.Point(27, 89);
            this.TextBoxViscosity.Name = "TextBoxViscosity";
            this.TextBoxViscosity.Size = new System.Drawing.Size(356, 27);
            this.TextBoxViscosity.TabIndex = 1;
            // 
            // LabelViscosity
            // 
            this.LabelViscosity.AutoSize = true;
            this.LabelViscosity.Location = new System.Drawing.Point(27, 67);
            this.LabelViscosity.Name = "LabelViscosity";
            this.LabelViscosity.Size = new System.Drawing.Size(87, 19);
            this.LabelViscosity.TabIndex = 14;
            this.LabelViscosity.Text = "Viscosidade";
            // 
            // LabelUoM
            // 
            this.LabelUoM.AutoSize = true;
            this.LabelUoM.Location = new System.Drawing.Point(27, 338);
            this.LabelUoM.Name = "LabelUoM";
            this.LabelUoM.Size = new System.Drawing.Size(85, 19);
            this.LabelUoM.TabIndex = 10;
            this.LabelUoM.Text = "Embalagem";
            // 
            // ComboBoxUoM
            // 
            this.ComboBoxUoM.FormattingEnabled = true;
            this.ComboBoxUoM.Location = new System.Drawing.Point(27, 360);
            this.ComboBoxUoM.Name = "ComboBoxUoM";
            this.ComboBoxUoM.Size = new System.Drawing.Size(356, 27);
            this.ComboBoxUoM.TabIndex = 6;
            // 
            // TextBoxApi
            // 
            this.TextBoxApi.Location = new System.Drawing.Point(27, 143);
            this.TextBoxApi.Name = "TextBoxApi";
            this.TextBoxApi.Size = new System.Drawing.Size(356, 27);
            this.TextBoxApi.TabIndex = 2;
            // 
            // LabelApi
            // 
            this.LabelApi.AutoSize = true;
            this.LabelApi.Location = new System.Drawing.Point(27, 121);
            this.LabelApi.Name = "LabelApi";
            this.LabelApi.Size = new System.Drawing.Size(30, 19);
            this.LabelApi.TabIndex = 16;
            this.LabelApi.Text = "API";
            // 
            // OilDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 502);
            this.Controls.Add(this.TextBoxApi);
            this.Controls.Add(this.LabelApi);
            this.Controls.Add(this.TextBoxViscosity);
            this.Controls.Add(this.LabelViscosity);
            this.Controls.Add(this.ButtonProcess);
            this.Controls.Add(this.ComboBoxUoM);
            this.Controls.Add(this.LabelUoM);
            this.Controls.Add(this.ComboBoxType);
            this.Controls.Add(this.LabelTipo);
            this.Controls.Add(this.TextBoxStockQuantity);
            this.Controls.Add(this.LabelStockQuantity);
            this.Controls.Add(this.TextBoxPrice);
            this.Controls.Add(this.LabelPrice);
            this.Controls.Add(this.TextBoxName);
            this.Controls.Add(this.LabelName);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "OilDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lubrificante";
            this.Load += new System.EventHandler(this.OilDetailForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label LabelName;
        private TextBox TextBoxName;
        private Label LabelPrice;
        private TextBox TextBoxPrice;
        private Label LabelTipo;
        private TextBox TextBoxStockQuantity;
        private Label LabelStockQuantity;
        private ComboBox ComboBoxType;
        private Button ButtonProcess;
        private TextBox TextBoxViscosity;
        private Label LabelViscosity;
        private Label LabelUoM;
        private ComboBox ComboBoxUoM;
        private TextBox TextBoxApi;
        private Label LabelApi;
    }
}