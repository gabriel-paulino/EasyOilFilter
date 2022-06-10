namespace EasyOilFilter.Presentation.Forms
{
    partial class FilterForm
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterForm));
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.ButtonSearch = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.ComboType = new System.Windows.Forms.ComboBox();
            this.TextName = new System.Windows.Forms.TextBox();
            this.GroupBoxSearch = new System.Windows.Forms.GroupBox();
            this.labelManufacturer = new System.Windows.Forms.Label();
            this.TextManufacturer = new System.Windows.Forms.TextBox();
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
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView.Location = new System.Drawing.Point(14, 117);
            this.DataGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DataGridView.MultiSelect = false;
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.ReadOnly = true;
            this.DataGridView.RowTemplate.Height = 25;
            this.DataGridView.Size = new System.Drawing.Size(758, 280);
            this.DataGridView.TabIndex = 0;
            this.DataGridView.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_RowHeaderMouseDoubleClick);
            // 
            // ButtonSearch
            // 
            this.ButtonSearch.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonSearch.Image = ((System.Drawing.Image)(resources.GetObject("ButtonSearch.Image")));
            this.ButtonSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonSearch.Location = new System.Drawing.Point(662, 53);
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
            this.labelName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelName.Location = new System.Drawing.Point(11, 34);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(47, 19);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "Nome";
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelType.Location = new System.Drawing.Point(489, 34);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(37, 19);
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
            // TextName
            // 
            this.TextName.Location = new System.Drawing.Point(11, 59);
            this.TextName.Name = "TextName";
            this.TextName.Size = new System.Drawing.Size(239, 23);
            this.TextName.TabIndex = 7;
            // 
            // GroupBoxSearch
            // 
            this.GroupBoxSearch.Controls.Add(this.ComboType);
            this.GroupBoxSearch.Controls.Add(this.ButtonSearch);
            this.GroupBoxSearch.Controls.Add(this.labelName);
            this.GroupBoxSearch.Controls.Add(this.TextName);
            this.GroupBoxSearch.Controls.Add(this.labelType);
            this.GroupBoxSearch.Controls.Add(this.labelManufacturer);
            this.GroupBoxSearch.Controls.Add(this.TextManufacturer);
            this.GroupBoxSearch.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GroupBoxSearch.Location = new System.Drawing.Point(14, 12);
            this.GroupBoxSearch.Name = "GroupBoxSearch";
            this.GroupBoxSearch.Size = new System.Drawing.Size(758, 98);
            this.GroupBoxSearch.TabIndex = 8;
            this.GroupBoxSearch.TabStop = false;
            this.GroupBoxSearch.Text = "Pesquisar";
            // 
            // labelManufacturer
            // 
            this.labelManufacturer.AutoSize = true;
            this.labelManufacturer.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelManufacturer.Location = new System.Drawing.Point(306, 34);
            this.labelManufacturer.Name = "labelManufacturer";
            this.labelManufacturer.Size = new System.Drawing.Size(77, 19);
            this.labelManufacturer.TabIndex = 3;
            this.labelManufacturer.Text = "Fabricante";
            // 
            // TextManufacturer
            // 
            this.TextManufacturer.Location = new System.Drawing.Point(306, 59);
            this.TextManufacturer.Name = "TextManufacturer";
            this.TextManufacturer.Size = new System.Drawing.Size(138, 23);
            this.TextManufacturer.TabIndex = 6;
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
            this.GroupBoxUtils.Size = new System.Drawing.Size(758, 130);
            this.GroupBoxUtils.TabIndex = 9;
            this.GroupBoxUtils.TabStop = false;
            this.GroupBoxUtils.Text = "Alterar preço de todos filtros";
            // 
            // TextBoxChangePriceValue
            // 
            this.TextBoxChangePriceValue.Location = new System.Drawing.Point(187, 83);
            this.TextBoxChangePriceValue.Name = "TextBoxChangePriceValue";
            this.TextBoxChangePriceValue.PlaceholderText = "Preencher no formato: 5,75";
            this.TextBoxChangePriceValue.Size = new System.Drawing.Size(165, 23);
            this.TextBoxChangePriceValue.TabIndex = 10;
            // 
            // TextBoxChangePricePercentage
            // 
            this.TextBoxChangePricePercentage.Location = new System.Drawing.Point(187, 30);
            this.TextBoxChangePricePercentage.Name = "TextBoxChangePricePercentage";
            this.TextBoxChangePricePercentage.PlaceholderText = "Preencher no formato: 3,25";
            this.TextBoxChangePricePercentage.Size = new System.Drawing.Size(165, 23);
            this.TextBoxChangePricePercentage.TabIndex = 9;
            // 
            // CheckBoxChangePriceValue
            // 
            this.CheckBoxChangePriceValue.AutoSize = true;
            this.CheckBoxChangePriceValue.Location = new System.Drawing.Point(12, 85);
            this.CheckBoxChangePriceValue.Name = "CheckBoxChangePriceValue";
            this.CheckBoxChangePriceValue.Size = new System.Drawing.Size(170, 19);
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
            this.ButtonChangePriceValue.Location = new System.Drawing.Point(383, 77);
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
            this.CheckBoxChangePricePercentage.Location = new System.Drawing.Point(12, 34);
            this.CheckBoxChangePricePercentage.Name = "CheckBoxChangePricePercentage";
            this.CheckBoxChangePricePercentage.Size = new System.Drawing.Size(160, 19);
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
            this.ButtonChangePricePercentage.Location = new System.Drawing.Point(383, 26);
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
            this.ButtonAddOil.Location = new System.Drawing.Point(676, 589);
            this.ButtonAddOil.Name = "ButtonAddOil";
            this.ButtonAddOil.Size = new System.Drawing.Size(90, 30);
            this.ButtonAddOil.TabIndex = 0;
            this.ButtonAddOil.Text = "Novo";
            this.ButtonAddOil.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonAddOil.UseVisualStyleBackColor = true;
            this.ButtonAddOil.Click += new System.EventHandler(this.ButtonAddFilter_Click);
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 641);
            this.Controls.Add(this.GroupBoxUtils);
            this.Controls.Add(this.GroupBoxSearch);
            this.Controls.Add(this.DataGridView);
            this.Controls.Add(this.ButtonAddOil);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "FilterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtros";
            this.Load += new System.EventHandler(this.FilterForm_Load);
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
        private Label labelType;
        private ComboBox ComboType;
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
        private Label labelManufacturer;
        private TextBox TextManufacturer;
    }
}