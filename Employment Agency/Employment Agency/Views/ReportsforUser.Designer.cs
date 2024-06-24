namespace Employment_Agency.Views
{
    partial class ReportsforUser
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
            this.label2 = new System.Windows.Forms.Label();
            this.deals = new System.Windows.Forms.DataGridView();
            this.applicants = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.GetReportbtn = new System.Windows.Forms.Button();
            this.employers = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.deals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicants)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.employers)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 18.32727F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(523, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(418, 38);
            this.label2.TabIndex = 109;
            this.label2.Text = "Отчеты по принятым заявкам";
            // 
            // deals
            // 
            this.deals.BackgroundColor = System.Drawing.Color.DeepSkyBlue;
            this.deals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.deals.Location = new System.Drawing.Point(34, 101);
            this.deals.Name = "deals";
            this.deals.RowHeadersWidth = 47;
            this.deals.RowTemplate.Height = 28;
            this.deals.Size = new System.Drawing.Size(617, 307);
            this.deals.TabIndex = 110;
            this.deals.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.deals_CellClick);
            // 
            // applicants
            // 
            this.applicants.BackgroundColor = System.Drawing.Color.DeepSkyBlue;
            this.applicants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.applicants.Location = new System.Drawing.Point(769, 101);
            this.applicants.Name = "applicants";
            this.applicants.RowHeadersWidth = 47;
            this.applicants.RowTemplate.Height = 28;
            this.applicants.Size = new System.Drawing.Size(639, 307);
            this.applicants.TabIndex = 110;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(822, 435);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 25);
            this.label3.TabIndex = 116;
            this.label3.Text = "До";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(547, 435);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 25);
            this.label1.TabIndex = 117;
            this.label1.Text = "От";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(822, 472);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(229, 26);
            this.dateTimePicker2.TabIndex = 114;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(547, 472);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(229, 26);
            this.dateTimePicker1.TabIndex = 115;
            // 
            // GetReportbtn
            // 
            this.GetReportbtn.BackColor = System.Drawing.Color.Aquamarine;
            this.GetReportbtn.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.GetReportbtn.Location = new System.Drawing.Point(1177, 458);
            this.GetReportbtn.Name = "GetReportbtn";
            this.GetReportbtn.Size = new System.Drawing.Size(151, 54);
            this.GetReportbtn.TabIndex = 112;
            this.GetReportbtn.Text = "Представить";
            this.GetReportbtn.UseVisualStyleBackColor = false;
            this.GetReportbtn.Click += new System.EventHandler(this.GetReportbtn_Click);
            // 
            // employers
            // 
            this.employers.BackgroundColor = System.Drawing.Color.DeepSkyBlue;
            this.employers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.employers.Location = new System.Drawing.Point(34, 522);
            this.employers.Name = "employers";
            this.employers.RowHeadersWidth = 47;
            this.employers.RowTemplate.Height = 28;
            this.employers.Size = new System.Drawing.Size(793, 307);
            this.employers.TabIndex = 118;
            // 
            // ReportsforUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Employment_Agency.Properties.Resources._1612170617_78_p_fon_gradient_sinii_fioletovii_93;
            this.ClientSize = new System.Drawing.Size(1471, 869);
            this.Controls.Add(this.employers);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.GetReportbtn);
            this.Controls.Add(this.applicants);
            this.Controls.Add(this.deals);
            this.Controls.Add(this.label2);
            this.Name = "ReportsforUser";
            this.Text = "ReportsforUser";
            ((System.ComponentModel.ISupportInitialize)(this.deals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicants)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.employers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label2;
        private DataGridView deals;
        private DataGridView applicants;
        private Label label3;
        private Label label1;
        private DateTimePicker dateTimePicker2;
        private DateTimePicker dateTimePicker1;
        private Button GetReportbtn;
        private DataGridView employers;
    }
}