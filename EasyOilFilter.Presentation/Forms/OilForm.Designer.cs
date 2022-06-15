namespace EasyOilFilter.Presentation.Forms
{
    partial class OilForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OilForm));
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.ButtonSearch = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.LabelViscosity = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.ComboType = new System.Windows.Forms.ComboBox();
            this.TextViscosity = new System.Windows.Forms.TextBox();
            this.TextName = new System.Windows.Forms.TextBox();
            this.GroupBoxSearch = new System.Windows.Forms.GroupBox();
            this.GroupBoxUtils = new System.Windows.Forms.GroupBox();
            this.TextBoxChangePriceValue = new System.Windows.Forms.TextBox();
            this.TextBoxChangePricePercentage = new System.Windows.Forms.TextBox();
            this.CheckBoxChangePriceValue = new System.Windows.Forms.CheckBox();
            this.ButtonChangePriceValue = new System.Windows.Forms.Button();
            this.CheckBoxChangePricePercentage = new System.Windows.Forms.CheckBox();
            this.ButtonChangePricePercentage = new System.Windows.Forms.Button();
            this.ButtonAddOil = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.GroupBoxSearch.SuspendLayout();
            this.GroupBoxUtils.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGridView
            // 
            this.DataGridView.AllowUserToAddRows = false;
            this.DataGridView.AllowUserToDeleteRows = false;
            this.DataGridView.AllowUserToOrderColumns = true;
            this.DataGridView.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView.Location = new System.Drawing.Point(14, 117);
            this.DataGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DataGridView.MultiSelect = false;
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.ReadOnly = true;
            this.DataGridView.RowTemplate.Height = 25;
            this.DataGridView.Size = new System.Drawing.Size(930, 280);
            this.DataGridView.TabIndex = 0;
            this.DataGridView.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_RowHeaderMouseDoubleClick);
            // 
            // ButtonSearch
            // 
            this.ButtonSearch.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonSearch.Image = ((System.Drawing.Image)(resources.GetObject("ButtonSearch.Image")));
            this.ButtonSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonSearch.Location = new System.Drawing.Point(834, 52);
            this.ButtonSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ButtonSearch.Name = "ButtonSearch";
            this.ButtonSearch.Size = new System.Drawing.Size(90, 30);
            this.ButtonSearch.TabIndex = 1;
            this.ButtonSearch.Text = "Filtrar";
            this.ButtonSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonSearch.UseVisualStyleBackColor = true;
            this.ButtonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelName.Location = new System.Drawing.Point(11, 34);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(91, 19);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "Lubrificante";
            // 
            // LabelViscosity
            // 
            this.LabelViscosity.AutoSize = true;
            this.LabelViscosity.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LabelViscosity.Location = new System.Drawing.Point(320, 34);
            this.LabelViscosity.Name = "LabelViscosity";
            this.LabelViscosity.Size = new System.Drawing.Size(88, 19);
            this.LabelViscosity.TabIndex = 3;
            this.LabelViscosity.Text = "Viscosidade";
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelType.Location = new System.Drawing.Point(489, 34);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(39, 19);
            this.labelType.TabIndex = 4;
            this.labelType.Text = "Tipo";
            // 
            // ComboType
            // 
            this.ComboType.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ComboType.FormattingEnabled = true;
            this.ComboType.Location = new System.Drawing.Point(489, 56);
            this.ComboType.Name = "ComboType";
            this.ComboType.Size = new System.Drawing.Size(120, 27);
            this.ComboType.TabIndex = 5;
            // 
            // TextViscosity
            // 
            this.TextViscosity.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextViscosity.Location = new System.Drawing.Point(320, 59);
            this.TextViscosity.Name = "TextViscosity";
            this.TextViscosity.Size = new System.Drawing.Size(120, 27);
            this.TextViscosity.TabIndex = 6;
            // 
            // TextName
            // 
            this.TextName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextName.Location = new System.Drawing.Point(11, 59);
            this.TextName.Name = "TextName";
            this.TextName.Size = new System.Drawing.Size(263, 27);
            this.TextName.TabIndex = 7;
            // 
            // GroupBoxSearch
            // 
            this.GroupBoxSearch.Controls.Add(this.ComboType);
            this.GroupBoxSearch.Controls.Add(this.ButtonSearch);
            this.GroupBoxSearch.Controls.Add(this.labelName);
            this.GroupBoxSearch.Controls.Add(this.TextName);
            this.GroupBoxSearch.Controls.Add(this.labelType);
            this.GroupBoxSearch.Controls.Add(this.LabelViscosity);
            this.GroupBoxSearch.Controls.Add(this.TextViscosity);
            this.GroupBoxSearch.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GroupBoxSearch.Location = new System.Drawing.Point(14, 12);
            this.GroupBoxSearch.Name = "GroupBoxSearch";
            this.GroupBoxSearch.Size = new System.Drawing.Size(930, 98);
            this.GroupBoxSearch.TabIndex = 8;
            this.GroupBoxSearch.TabStop = false;
            this.GroupBoxSearch.Text = "Pesquisar";
            // 
            // GroupBoxUtils
            // 
            this.GroupBoxUtils.Controls.Add(this.TextBoxChangePriceValue);
            this.GroupBoxUtils.Controls.Add(this.TextBoxChangePricePercentage);
            this.GroupBoxUtils.Controls.Add(this.CheckBoxChangePriceValue);
            this.GroupBoxUtils.Controls.Add(this.ButtonChangePriceValue);
            this.GroupBoxUtils.Controls.Add(this.CheckBoxChangePricePercentage);
            this.GroupBoxUtils.Controls.Add(this.ButtonChangePricePercentage);
            this.GroupBoxUtils.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GroupBoxUtils.Location = new System.Drawing.Point(14, 420);
            this.GroupBoxUtils.Name = "GroupBoxUtils";
            this.GroupBoxUtils.Size = new System.Drawing.Size(930, 130);
            this.GroupBoxUtils.TabIndex = 9;
            this.GroupBoxUtils.TabStop = false;
            this.GroupBoxUtils.Text = "Alterar preço padrão de todos lubrificantes";
            // 
            // TextBoxChangePriceValue
            // 
            this.TextBoxChangePriceValue.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextBoxChangePriceValue.Location = new System.Drawing.Point(229, 81);
            this.TextBoxChangePriceValue.Name = "TextBoxChangePriceValue";
            this.TextBoxChangePriceValue.PlaceholderText = "Preencher no formato: 5,75";
            this.TextBoxChangePriceValue.Size = new System.Drawing.Size(193, 27);
            this.TextBoxChangePriceValue.TabIndex = 10;
            // 
            // TextBoxChangePricePercentage
            // 
            this.TextBoxChangePricePercentage.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextBoxChangePricePercentage.Location = new System.Drawing.Point(229, 32);
            this.TextBoxChangePricePercentage.Name = "TextBoxChangePricePercentage";
            this.TextBoxChangePricePercentage.PlaceholderText = "Preencher no formato: 3,25";
            this.TextBoxChangePricePercentage.Size = new System.Drawing.Size(193, 27);
            this.TextBoxChangePricePercentage.TabIndex = 9;
            // 
            // CheckBoxChangePriceValue
            // 
            this.CheckBoxChangePriceValue.AutoSize = true;
            this.CheckBoxChangePriceValue.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CheckBoxChangePriceValue.Location = new System.Drawing.Point(12, 85);
            this.CheckBoxChangePriceValue.Name = "CheckBoxChangePriceValue";
            this.CheckBoxChangePriceValue.Size = new System.Drawing.Size(193, 23);
            this.CheckBoxChangePriceValue.TabIndex = 8;
            this.CheckBoxChangePriceValue.Text = "Alterar por valor absoluto";
            this.CheckBoxChangePriceValue.UseVisualStyleBackColor = true;
            this.CheckBoxChangePriceValue.CheckedChanged += new System.EventHandler(this.CheckBoxChangePriceValue_CheckedChanged);
            // 
            // ButtonChangePriceValue
            // 
            this.ButtonChangePriceValue.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonChangePriceValue.Image = ((System.Drawing.Image)(resources.GetObject("ButtonChangePriceValue.Image")));
            this.ButtonChangePriceValue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonChangePriceValue.Location = new System.Drawing.Point(466, 78);
            this.ButtonChangePriceValue.Name = "ButtonChangePriceValue";
            this.ButtonChangePriceValue.Size = new System.Drawing.Size(110, 30);
            this.ButtonChangePriceValue.TabIndex = 6;
            this.ButtonChangePriceValue.Text = "Executar";
            this.ButtonChangePriceValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonChangePriceValue.UseVisualStyleBackColor = true;
            this.ButtonChangePriceValue.Click += new System.EventHandler(this.ButtonChangePriceValue_Click);
            // 
            // CheckBoxChangePricePercentage
            // 
            this.CheckBoxChangePricePercentage.AutoSize = true;
            this.CheckBoxChangePricePercentage.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CheckBoxChangePricePercentage.Location = new System.Drawing.Point(12, 34);
            this.CheckBoxChangePricePercentage.Name = "CheckBoxChangePricePercentage";
            this.CheckBoxChangePricePercentage.Size = new System.Drawing.Size(186, 23);
            this.CheckBoxChangePricePercentage.TabIndex = 5;
            this.CheckBoxChangePricePercentage.Text = "Alterar por porcentagem";
            this.CheckBoxChangePricePercentage.UseVisualStyleBackColor = true;
            this.CheckBoxChangePricePercentage.CheckedChanged += new System.EventHandler(this.CheckBoxChangePricePercentage_CheckedChanged);
            // 
            // ButtonChangePricePercentage
            // 
            this.ButtonChangePricePercentage.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonChangePricePercentage.Image = ((System.Drawing.Image)(resources.GetObject("ButtonChangePricePercentage.Image")));
            this.ButtonChangePricePercentage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonChangePricePercentage.Location = new System.Drawing.Point(466, 29);
            this.ButtonChangePricePercentage.Name = "ButtonChangePricePercentage";
            this.ButtonChangePricePercentage.Size = new System.Drawing.Size(110, 30);
            this.ButtonChangePricePercentage.TabIndex = 3;
            this.ButtonChangePricePercentage.Text = "Executar";
            this.ButtonChangePricePercentage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonChangePricePercentage.UseVisualStyleBackColor = true;
            this.ButtonChangePricePercentage.Click += new System.EventHandler(this.ButtonChangePricePercentage_Click);
            // 
            // ButtonAddOil
            // 
            this.ButtonAddOil.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonAddOil.Image = ((System.Drawing.Image)(resources.GetObject("ButtonAddOil.Image")));
            this.ButtonAddOil.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonAddOil.Location = new System.Drawing.Point(854, 599);
            this.ButtonAddOil.Name = "ButtonAddOil";
            this.ButtonAddOil.Size = new System.Drawing.Size(90, 30);
            this.ButtonAddOil.TabIndex = 0;
            this.ButtonAddOil.Text = "Novo";
            this.ButtonAddOil.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonAddOil.UseVisualStyleBackColor = true;
            this.ButtonAddOil.Click += new System.EventHandler(this.ButtonAddOil_Click);
            // 
            // OilForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 641);
            this.Controls.Add(this.GroupBoxUtils);
            this.Controls.Add(this.GroupBoxSearch);
            this.Controls.Add(this.DataGridView);
            this.Controls.Add(this.ButtonAddOil);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "OilForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lubrificantes";
            this.Load += new System.EventHandler(this.OilForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.GroupBoxSearch.ResumeLayout(false);
            this.GroupBoxSearch.PerformLayout();
            this.GroupBoxUtils.ResumeLayout(false);
            this.GroupBoxUtils.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView DataGridView;
        private Button ButtonSearch;
        private Label labelName;
        private Label LabelViscosity;
        private Label labelType;
        private ComboBox ComboType;
        private TextBox TextViscosity;
        private TextBox TextName;
        private GroupBox GroupBoxSearch;
        private GroupBox GroupBoxUtils;
        private Button ButtonAddOil;
        private CheckBox CheckBoxChangePriceValue;
        private Button ButtonChangePriceValue;
        private CheckBox CheckBoxChangePricePercentage;
        private Button ButtonChangePricePercentage;
        private TextBox TextBoxChangePriceValue;
        private TextBox TextBoxChangePricePercentage;
    }
}