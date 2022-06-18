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
            this.LabelDefaultPrice = new System.Windows.Forms.Label();
            this.TextBoxDefaultPrice = new System.Windows.Forms.TextBox();
            this.LabelTipo = new System.Windows.Forms.Label();
            this.TextBoxStockQuantity = new System.Windows.Forms.TextBox();
            this.LabelStockQuantity = new System.Windows.Forms.Label();
            this.ComboBoxType = new System.Windows.Forms.ComboBox();
            this.ButtonProcess = new System.Windows.Forms.Button();
            this.TextBoxViscosity = new System.Windows.Forms.TextBox();
            this.LabelViscosity = new System.Windows.Forms.Label();
            this.LabelDefaultUoM = new System.Windows.Forms.Label();
            this.ComboBoxDefaultUoM = new System.Windows.Forms.ComboBox();
            this.TextBoxApi = new System.Windows.Forms.TextBox();
            this.LabelApi = new System.Windows.Forms.Label();
            this.ComboBoxAlternativeUoM = new System.Windows.Forms.ComboBox();
            this.LabelAlternativeUoM = new System.Windows.Forms.Label();
            this.TextBoxAlternativePrice = new System.Windows.Forms.TextBox();
            this.LabelAlternativePrice = new System.Windows.Forms.Label();
            this.CheckBoxAlternative = new System.Windows.Forms.CheckBox();
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
            // LabelDefaultPrice
            // 
            this.LabelDefaultPrice.AutoSize = true;
            this.LabelDefaultPrice.Location = new System.Drawing.Point(27, 290);
            this.LabelDefaultPrice.Name = "LabelDefaultPrice";
            this.LabelDefaultPrice.Size = new System.Drawing.Size(45, 19);
            this.LabelDefaultPrice.TabIndex = 4;
            this.LabelDefaultPrice.Text = "Preço";
            // 
            // TextBoxDefaultPrice
            // 
            this.TextBoxDefaultPrice.Location = new System.Drawing.Point(27, 312);
            this.TextBoxDefaultPrice.Name = "TextBoxDefaultPrice";
            this.TextBoxDefaultPrice.Size = new System.Drawing.Size(356, 27);
            this.TextBoxDefaultPrice.TabIndex = 5;
            // 
            // LabelTipo
            // 
            this.LabelTipo.AutoSize = true;
            this.LabelTipo.Location = new System.Drawing.Point(27, 232);
            this.LabelTipo.Name = "LabelTipo";
            this.LabelTipo.Size = new System.Drawing.Size(37, 19);
            this.LabelTipo.TabIndex = 8;
            this.LabelTipo.Text = "Tipo";
            // 
            // TextBoxStockQuantity
            // 
            this.TextBoxStockQuantity.Location = new System.Drawing.Point(27, 200);
            this.TextBoxStockQuantity.Name = "TextBoxStockQuantity";
            this.TextBoxStockQuantity.Size = new System.Drawing.Size(356, 27);
            this.TextBoxStockQuantity.TabIndex = 3;
            // 
            // LabelStockQuantity
            // 
            this.LabelStockQuantity.AutoSize = true;
            this.LabelStockQuantity.Location = new System.Drawing.Point(27, 178);
            this.LabelStockQuantity.Name = "LabelStockQuantity";
            this.LabelStockQuantity.Size = new System.Drawing.Size(85, 19);
            this.LabelStockQuantity.TabIndex = 6;
            this.LabelStockQuantity.Text = "Em estoque";
            // 
            // ComboBoxType
            // 
            this.ComboBoxType.FormattingEnabled = true;
            this.ComboBoxType.Location = new System.Drawing.Point(27, 254);
            this.ComboBoxType.Name = "ComboBoxType";
            this.ComboBoxType.Size = new System.Drawing.Size(356, 27);
            this.ComboBoxType.TabIndex = 4;
            // 
            // ButtonProcess
            // 
            this.ButtonProcess.Location = new System.Drawing.Point(27, 588);
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
            // LabelDefaultUoM
            // 
            this.LabelDefaultUoM.AutoSize = true;
            this.LabelDefaultUoM.Location = new System.Drawing.Point(27, 345);
            this.LabelDefaultUoM.Name = "LabelDefaultUoM";
            this.LabelDefaultUoM.Size = new System.Drawing.Size(85, 19);
            this.LabelDefaultUoM.TabIndex = 10;
            this.LabelDefaultUoM.Text = "Embalagem";
            // 
            // ComboBoxDefaultUoM
            // 
            this.ComboBoxDefaultUoM.FormattingEnabled = true;
            this.ComboBoxDefaultUoM.Location = new System.Drawing.Point(27, 367);
            this.ComboBoxDefaultUoM.Name = "ComboBoxDefaultUoM";
            this.ComboBoxDefaultUoM.Size = new System.Drawing.Size(356, 27);
            this.ComboBoxDefaultUoM.TabIndex = 6;
            this.ComboBoxDefaultUoM.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxDefaultUoM_SelectionChangeCommitted);
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
            // ComboBoxAlternativeUoM
            // 
            this.ComboBoxAlternativeUoM.FormattingEnabled = true;
            this.ComboBoxAlternativeUoM.ItemHeight = 19;
            this.ComboBoxAlternativeUoM.Location = new System.Drawing.Point(27, 503);
            this.ComboBoxAlternativeUoM.Name = "ComboBoxAlternativeUoM";
            this.ComboBoxAlternativeUoM.Size = new System.Drawing.Size(356, 27);
            this.ComboBoxAlternativeUoM.TabIndex = 20;
            this.ComboBoxAlternativeUoM.Visible = false;
            this.ComboBoxAlternativeUoM.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxAlternativeUoM_SelectionChangeCommitted);
            // 
            // LabelAlternativeUoM
            // 
            this.LabelAlternativeUoM.AutoSize = true;
            this.LabelAlternativeUoM.Location = new System.Drawing.Point(27, 481);
            this.LabelAlternativeUoM.Name = "LabelAlternativeUoM";
            this.LabelAlternativeUoM.Size = new System.Drawing.Size(159, 19);
            this.LabelAlternativeUoM.TabIndex = 21;
            this.LabelAlternativeUoM.Text = "Embalagem alternativa";
            this.LabelAlternativeUoM.Visible = false;
            // 
            // TextBoxAlternativePrice
            // 
            this.TextBoxAlternativePrice.Location = new System.Drawing.Point(27, 448);
            this.TextBoxAlternativePrice.Name = "TextBoxAlternativePrice";
            this.TextBoxAlternativePrice.Size = new System.Drawing.Size(356, 27);
            this.TextBoxAlternativePrice.TabIndex = 9;
            this.TextBoxAlternativePrice.Visible = false;
            // 
            // LabelAlternativePrice
            // 
            this.LabelAlternativePrice.AutoSize = true;
            this.LabelAlternativePrice.Location = new System.Drawing.Point(27, 426);
            this.LabelAlternativePrice.Name = "LabelAlternativePrice";
            this.LabelAlternativePrice.Size = new System.Drawing.Size(45, 19);
            this.LabelAlternativePrice.TabIndex = 19;
            this.LabelAlternativePrice.Text = "Preço";
            this.LabelAlternativePrice.Visible = false;
            // 
            // CheckBoxAlternative
            // 
            this.CheckBoxAlternative.AutoSize = true;
            this.CheckBoxAlternative.Location = new System.Drawing.Point(27, 400);
            this.CheckBoxAlternative.Name = "CheckBoxAlternative";
            this.CheckBoxAlternative.Size = new System.Drawing.Size(220, 23);
            this.CheckBoxAlternative.TabIndex = 22;
            this.CheckBoxAlternative.Text = "Preço/embalagem alternativo";
            this.CheckBoxAlternative.UseVisualStyleBackColor = true;
            this.CheckBoxAlternative.CheckedChanged += new System.EventHandler(this.CheckBoxAlternative_CheckedChanged);
            // 
            // OilDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 632);
            this.Controls.Add(this.CheckBoxAlternative);
            this.Controls.Add(this.ComboBoxAlternativeUoM);
            this.Controls.Add(this.LabelAlternativeUoM);
            this.Controls.Add(this.TextBoxAlternativePrice);
            this.Controls.Add(this.LabelAlternativePrice);
            this.Controls.Add(this.TextBoxApi);
            this.Controls.Add(this.LabelApi);
            this.Controls.Add(this.TextBoxViscosity);
            this.Controls.Add(this.LabelViscosity);
            this.Controls.Add(this.ButtonProcess);
            this.Controls.Add(this.ComboBoxDefaultUoM);
            this.Controls.Add(this.LabelDefaultUoM);
            this.Controls.Add(this.ComboBoxType);
            this.Controls.Add(this.LabelTipo);
            this.Controls.Add(this.TextBoxStockQuantity);
            this.Controls.Add(this.LabelStockQuantity);
            this.Controls.Add(this.TextBoxDefaultPrice);
            this.Controls.Add(this.LabelDefaultPrice);
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
        private Label LabelDefaultPrice;
        private TextBox TextBoxDefaultPrice;
        private Label LabelTipo;
        private TextBox TextBoxStockQuantity;
        private Label LabelStockQuantity;
        private ComboBox ComboBoxType;
        private Button ButtonProcess;
        private TextBox TextBoxViscosity;
        private Label LabelViscosity;
        private Label LabelDefaultUoM;
        private ComboBox ComboBoxDefaultUoM;
        private TextBox TextBoxApi;
        private Label LabelApi;
        private ComboBox ComboBoxAlternativeUoM;
        private Label LabelAlternativeUoM;
        private TextBox TextBoxAlternativePrice;
        private Label LabelAlternativePrice;
        private CheckBox CheckBoxAlternative;
    }
}