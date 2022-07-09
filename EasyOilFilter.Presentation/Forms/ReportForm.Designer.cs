namespace EasyOilFilter.Presentation.Forms
{
    partial class ReportForm
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
            this.StartDatePicker = new System.Windows.Forms.DateTimePicker();
            this.LabelStartDate = new System.Windows.Forms.Label();
            this.LabelFinalDate = new System.Windows.Forms.Label();
            this.FinalDatePicker = new System.Windows.Forms.DateTimePicker();
            this.ButtonSaleReport = new System.Windows.Forms.Button();
            this.ButtonSoldItemReport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartDatePicker
            // 
            this.StartDatePicker.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.StartDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.StartDatePicker.Location = new System.Drawing.Point(12, 31);
            this.StartDatePicker.Name = "StartDatePicker";
            this.StartDatePicker.Size = new System.Drawing.Size(124, 27);
            this.StartDatePicker.TabIndex = 0;
            // 
            // LabelStartDate
            // 
            this.LabelStartDate.AutoSize = true;
            this.LabelStartDate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LabelStartDate.Location = new System.Drawing.Point(12, 9);
            this.LabelStartDate.Name = "LabelStartDate";
            this.LabelStartDate.Size = new System.Drawing.Size(85, 19);
            this.LabelStartDate.TabIndex = 1;
            this.LabelStartDate.Text = "Data inicial";
            // 
            // LabelFinalDate
            // 
            this.LabelFinalDate.AutoSize = true;
            this.LabelFinalDate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LabelFinalDate.Location = new System.Drawing.Point(12, 72);
            this.LabelFinalDate.Name = "LabelFinalDate";
            this.LabelFinalDate.Size = new System.Drawing.Size(75, 19);
            this.LabelFinalDate.TabIndex = 2;
            this.LabelFinalDate.Text = "Data final";
            // 
            // FinalDatePicker
            // 
            this.FinalDatePicker.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FinalDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.FinalDatePicker.Location = new System.Drawing.Point(12, 94);
            this.FinalDatePicker.Name = "FinalDatePicker";
            this.FinalDatePicker.Size = new System.Drawing.Size(124, 27);
            this.FinalDatePicker.TabIndex = 3;
            // 
            // ButtonSaleReport
            // 
            this.ButtonSaleReport.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonSaleReport.Location = new System.Drawing.Point(12, 190);
            this.ButtonSaleReport.Name = "ButtonSaleReport";
            this.ButtonSaleReport.Size = new System.Drawing.Size(245, 28);
            this.ButtonSaleReport.TabIndex = 16;
            this.ButtonSaleReport.Text = "Vendas";
            this.ButtonSaleReport.UseVisualStyleBackColor = true;
            this.ButtonSaleReport.Click += new System.EventHandler(this.ButtonSaleReport_Click);
            // 
            // ButtonSoldItemReport
            // 
            this.ButtonSoldItemReport.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonSoldItemReport.Location = new System.Drawing.Point(12, 233);
            this.ButtonSoldItemReport.Name = "ButtonSoldItemReport";
            this.ButtonSoldItemReport.Size = new System.Drawing.Size(245, 28);
            this.ButtonSoldItemReport.TabIndex = 17;
            this.ButtonSoldItemReport.Text = "Itens vendidos";
            this.ButtonSoldItemReport.UseVisualStyleBackColor = true;
            this.ButtonSoldItemReport.Click += new System.EventHandler(this.ButtonSoldItemReport_Click);
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 283);
            this.Controls.Add(this.ButtonSoldItemReport);
            this.Controls.Add(this.ButtonSaleReport);
            this.Controls.Add(this.FinalDatePicker);
            this.Controls.Add(this.LabelFinalDate);
            this.Controls.Add(this.LabelStartDate);
            this.Controls.Add(this.StartDatePicker);
            this.MaximizeBox = false;
            this.Name = "ReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatórios";
            this.Load += new System.EventHandler(this.SaleReportForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DateTimePicker StartDatePicker;
        private Label LabelStartDate;
        private Label LabelFinalDate;
        private DateTimePicker FinalDatePicker;
        private Button ButtonSaleReport;
        private Button ButtonSoldItemReport;
    }
}