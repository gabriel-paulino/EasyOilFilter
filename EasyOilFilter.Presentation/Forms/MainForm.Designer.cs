namespace EasyOilFilter.Presentation.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ButtonOil = new System.Windows.Forms.Button();
            this.ButtonFilter = new System.Windows.Forms.Button();
            this.ButtonSale = new System.Windows.Forms.Button();
            this.PictureBoxMain = new System.Windows.Forms.PictureBox();
            this.ButtonPurchase = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxMain)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonOil
            // 
            this.ButtonOil.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonOil.Image = ((System.Drawing.Image)(resources.GetObject("ButtonOil.Image")));
            this.ButtonOil.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonOil.Location = new System.Drawing.Point(12, 12);
            this.ButtonOil.Name = "ButtonOil";
            this.ButtonOil.Size = new System.Drawing.Size(220, 70);
            this.ButtonOil.TabIndex = 0;
            this.ButtonOil.Text = "Lubrificantes";
            this.ButtonOil.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonOil.UseVisualStyleBackColor = true;
            this.ButtonOil.Click += new System.EventHandler(this.ButtonLubs_Click);
            // 
            // ButtonFilter
            // 
            this.ButtonFilter.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonFilter.Image = ((System.Drawing.Image)(resources.GetObject("ButtonFilter.Image")));
            this.ButtonFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonFilter.Location = new System.Drawing.Point(12, 106);
            this.ButtonFilter.Name = "ButtonFilter";
            this.ButtonFilter.Size = new System.Drawing.Size(220, 70);
            this.ButtonFilter.TabIndex = 1;
            this.ButtonFilter.Text = "Filtros";
            this.ButtonFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonFilter.UseVisualStyleBackColor = true;
            this.ButtonFilter.Click += new System.EventHandler(this.ButtonFilters_Click);
            // 
            // ButtonSale
            // 
            this.ButtonSale.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonSale.Image = ((System.Drawing.Image)(resources.GetObject("ButtonSale.Image")));
            this.ButtonSale.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonSale.Location = new System.Drawing.Point(12, 202);
            this.ButtonSale.Name = "ButtonSale";
            this.ButtonSale.Size = new System.Drawing.Size(220, 70);
            this.ButtonSale.TabIndex = 2;
            this.ButtonSale.Text = "Vendas";
            this.ButtonSale.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonSale.UseVisualStyleBackColor = true;
            this.ButtonSale.Click += new System.EventHandler(this.ButtonSale_Click);
            // 
            // PictureBoxMain
            // 
            this.PictureBoxMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBoxMain.Image = ((System.Drawing.Image)(resources.GetObject("PictureBoxMain.Image")));
            this.PictureBoxMain.InitialImage = null;
            this.PictureBoxMain.Location = new System.Drawing.Point(315, 12);
            this.PictureBoxMain.Name = "PictureBoxMain";
            this.PictureBoxMain.Size = new System.Drawing.Size(339, 355);
            this.PictureBoxMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBoxMain.TabIndex = 3;
            this.PictureBoxMain.TabStop = false;
            // 
            // ButtonPurchase
            // 
            this.ButtonPurchase.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonPurchase.Image = ((System.Drawing.Image)(resources.GetObject("ButtonPurchase.Image")));
            this.ButtonPurchase.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonPurchase.Location = new System.Drawing.Point(12, 297);
            this.ButtonPurchase.Name = "ButtonPurchase";
            this.ButtonPurchase.Size = new System.Drawing.Size(220, 70);
            this.ButtonPurchase.TabIndex = 4;
            this.ButtonPurchase.Text = "Compras";
            this.ButtonPurchase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonPurchase.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 392);
            this.Controls.Add(this.ButtonPurchase);
            this.Controls.Add(this.PictureBoxMain);
            this.Controls.Add(this.ButtonSale);
            this.Controls.Add(this.ButtonFilter);
            this.Controls.Add(this.ButtonOil);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Easy Oil Filter";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button ButtonOil;
        private Button ButtonFilter;
        private Button ButtonSale;
        private PictureBox PictureBoxMain;
        private Button ButtonPurchase;
    }
}