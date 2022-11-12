namespace EasyOilFilter.Presentation.Forms
{
    partial class PurchaseListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchaseListForm));
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.LabelDate = new System.Windows.Forms.Label();
            this.GroupBoxSearch = new System.Windows.Forms.GroupBox();
            this.LabelEndDate = new System.Windows.Forms.Label();
            this.DateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.LabelStartDate = new System.Windows.Forms.Label();
            this.LabelTotal = new System.Windows.Forms.Label();
            this.DateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.ButtonSearch = new System.Windows.Forms.Button();
            this.ButtonAddSale = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.GroupBoxSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGridView
            // 
            this.DataGridView.AllowUserToAddRows = false;
            this.DataGridView.AllowUserToDeleteRows = false;
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView.Location = new System.Drawing.Point(12, 41);
            this.DataGridView.MultiSelect = false;
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.ReadOnly = true;
            this.DataGridView.RowTemplate.Height = 25;
            this.DataGridView.Size = new System.Drawing.Size(650, 440);
            this.DataGridView.TabIndex = 0;
            this.DataGridView.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_RowHeaderMouseDoubleClick);
            // 
            // LabelDate
            // 
            this.LabelDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelDate.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelDate.Location = new System.Drawing.Point(0, 0);
            this.LabelDate.Name = "LabelDate";
            this.LabelDate.Size = new System.Drawing.Size(675, 29);
            this.LabelDate.TabIndex = 1;
            this.LabelDate.Text = "dd/MM/yy";
            this.LabelDate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // GroupBoxSearch
            // 
            this.GroupBoxSearch.Controls.Add(this.LabelEndDate);
            this.GroupBoxSearch.Controls.Add(this.DateTimePickerEnd);
            this.GroupBoxSearch.Controls.Add(this.LabelStartDate);
            this.GroupBoxSearch.Controls.Add(this.LabelTotal);
            this.GroupBoxSearch.Controls.Add(this.DateTimePickerStart);
            this.GroupBoxSearch.Controls.Add(this.ButtonSearch);
            this.GroupBoxSearch.Location = new System.Drawing.Point(12, 495);
            this.GroupBoxSearch.Name = "GroupBoxSearch";
            this.GroupBoxSearch.Size = new System.Drawing.Size(650, 92);
            this.GroupBoxSearch.TabIndex = 2;
            this.GroupBoxSearch.TabStop = false;
            this.GroupBoxSearch.Text = "Consultar compras";
            // 
            // LabelEndDate
            // 
            this.LabelEndDate.AutoSize = true;
            this.LabelEndDate.Location = new System.Drawing.Point(189, 27);
            this.LabelEndDate.Name = "LabelEndDate";
            this.LabelEndDate.Size = new System.Drawing.Size(25, 15);
            this.LabelEndDate.TabIndex = 7;
            this.LabelEndDate.Text = "Até";
            // 
            // DateTimePickerEnd
            // 
            this.DateTimePickerEnd.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTimePickerEnd.Location = new System.Drawing.Point(189, 45);
            this.DateTimePickerEnd.Name = "DateTimePickerEnd";
            this.DateTimePickerEnd.Size = new System.Drawing.Size(124, 27);
            this.DateTimePickerEnd.TabIndex = 6;
            this.DateTimePickerEnd.Value = new System.DateTime(2022, 5, 30, 19, 12, 59, 0);
            // 
            // LabelStartDate
            // 
            this.LabelStartDate.AutoSize = true;
            this.LabelStartDate.Location = new System.Drawing.Point(26, 27);
            this.LabelStartDate.Name = "LabelStartDate";
            this.LabelStartDate.Size = new System.Drawing.Size(21, 15);
            this.LabelStartDate.TabIndex = 5;
            this.LabelStartDate.Text = "De";
            // 
            // LabelTotal
            // 
            this.LabelTotal.AutoSize = true;
            this.LabelTotal.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelTotal.ForeColor = System.Drawing.Color.Black;
            this.LabelTotal.Location = new System.Drawing.Point(514, 29);
            this.LabelTotal.Name = "LabelTotal";
            this.LabelTotal.Size = new System.Drawing.Size(0, 29);
            this.LabelTotal.TabIndex = 4;
            // 
            // DateTimePickerStart
            // 
            this.DateTimePickerStart.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTimePickerStart.Location = new System.Drawing.Point(26, 45);
            this.DateTimePickerStart.Name = "DateTimePickerStart";
            this.DateTimePickerStart.Size = new System.Drawing.Size(124, 27);
            this.DateTimePickerStart.TabIndex = 1;
            this.DateTimePickerStart.Value = new System.DateTime(2022, 5, 30, 19, 12, 59, 0);
            // 
            // ButtonSearch
            // 
            this.ButtonSearch.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonSearch.Image = ((System.Drawing.Image)(resources.GetObject("ButtonSearch.Image")));
            this.ButtonSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonSearch.Location = new System.Drawing.Point(352, 45);
            this.ButtonSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ButtonSearch.Name = "ButtonSearch";
            this.ButtonSearch.Size = new System.Drawing.Size(90, 30);
            this.ButtonSearch.TabIndex = 2;
            this.ButtonSearch.Text = "Exibir";
            this.ButtonSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonSearch.UseVisualStyleBackColor = true;
            this.ButtonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
            // 
            // ButtonAddSale
            // 
            this.ButtonAddSale.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonAddSale.Image = ((System.Drawing.Image)(resources.GetObject("ButtonAddSale.Image")));
            this.ButtonAddSale.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonAddSale.Location = new System.Drawing.Point(541, 593);
            this.ButtonAddSale.Name = "ButtonAddSale";
            this.ButtonAddSale.Size = new System.Drawing.Size(121, 31);
            this.ButtonAddSale.TabIndex = 3;
            this.ButtonAddSale.Text = "Cadastrar";
            this.ButtonAddSale.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonAddSale.UseVisualStyleBackColor = true;
            this.ButtonAddSale.Click += new System.EventHandler(this.ButtonAddPurchase_Click);
            // 
            // PurchaseListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 646);
            this.Controls.Add(this.ButtonAddSale);
            this.Controls.Add(this.GroupBoxSearch);
            this.Controls.Add(this.LabelDate);
            this.Controls.Add(this.DataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PurchaseListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compras";
            this.Load += new System.EventHandler(this.PurchaseForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.GroupBoxSearch.ResumeLayout(false);
            this.GroupBoxSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView DataGridView;
        private Label LabelDate;
        private GroupBox GroupBoxSearch;
        private Button ButtonSearch;
        private Button ButtonAddSale;
        private DateTimePicker DateTimePickerStart;
        private Label LabelTotal;
        private Label LabelEndDate;
        private DateTimePicker DateTimePickerEnd;
        private Label LabelStartDate;
    }
}