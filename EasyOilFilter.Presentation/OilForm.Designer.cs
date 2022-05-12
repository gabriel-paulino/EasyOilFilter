namespace EasyOilFilter.Presentation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OilForm));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.ButtonSearch = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.labelViscosity = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.ComboType = new System.Windows.Forms.ComboBox();
            this.TextViscosity = new System.Windows.Forms.TextBox();
            this.TextName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(14, 162);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 25;
            this.dataGridView.Size = new System.Drawing.Size(758, 280);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_RowHeaderMouseDoubleClick);
            // 
            // ButtonSearch
            // 
            this.ButtonSearch.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonSearch.Image = ((System.Drawing.Image)(resources.GetObject("ButtonSearch.Image")));
            this.ButtonSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonSearch.Location = new System.Drawing.Point(14, 124);
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
            this.labelName.Location = new System.Drawing.Point(14, 14);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(47, 19);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "Nome";
            // 
            // labelViscosity
            // 
            this.labelViscosity.AutoSize = true;
            this.labelViscosity.Location = new System.Drawing.Point(14, 47);
            this.labelViscosity.Name = "labelViscosity";
            this.labelViscosity.Size = new System.Drawing.Size(87, 19);
            this.labelViscosity.TabIndex = 3;
            this.labelViscosity.Text = "Viscosidade";
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(14, 80);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(37, 19);
            this.labelType.TabIndex = 4;
            this.labelType.Text = "Tipo";
            // 
            // ComboType
            // 
            this.ComboType.FormattingEnabled = true;
            this.ComboType.Location = new System.Drawing.Point(133, 72);
            this.ComboType.Name = "ComboType";
            this.ComboType.Size = new System.Drawing.Size(121, 27);
            this.ComboType.TabIndex = 5;
            // 
            // TextViscosity
            // 
            this.TextViscosity.Location = new System.Drawing.Point(133, 39);
            this.TextViscosity.Name = "TextViscosity";
            this.TextViscosity.Size = new System.Drawing.Size(121, 27);
            this.TextViscosity.TabIndex = 6;
            // 
            // TextName
            // 
            this.TextName.Location = new System.Drawing.Point(133, 6);
            this.TextName.Name = "TextName";
            this.TextName.Size = new System.Drawing.Size(263, 27);
            this.TextName.TabIndex = 7;
            // 
            // OilForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 534);
            this.Controls.Add(this.TextName);
            this.Controls.Add(this.TextViscosity);
            this.Controls.Add(this.ComboType);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.labelViscosity);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.ButtonSearch);
            this.Controls.Add(this.dataGridView);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "OilForm";
            this.Text = "Lubrificantes";
            this.Load += new System.EventHandler(this.OilForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView dataGridView;
        private Button ButtonSearch;
        private Label labelName;
        private Label labelViscosity;
        private Label labelType;
        private ComboBox ComboType;
        private TextBox TextViscosity;
        private TextBox TextName;
    }
}