namespace Employment_Agency.Views
{
    partial class AllVacancises
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
            this.vacs = new System.Windows.Forms.DataGridView();
            this.AddrequestToEmployer = new System.Windows.Forms.Button();
            this.Pccheckb = new System.Windows.Forms.CheckBox();
            this.Driveliccheck = new System.Windows.Forms.CheckBox();
            this.Poscb = new System.Windows.Forms.ComboBox();
            this.Schedulecb = new System.Windows.Forms.ComboBox();
            this.educationcb = new System.Windows.Forms.ComboBox();
            this.Gendercb = new System.Windows.Forms.ComboBox();
            this.Exptb = new System.Windows.Forms.TextBox();
            this.citytb = new System.Windows.Forms.TextBox();
            this.Salarytb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Search = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Employercb = new System.Windows.Forms.ComboBox();
            this.MyRequest = new System.Windows.Forms.Button();
            this.getvac = new System.Windows.Forms.Button();
            this.lang = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.vacs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lang)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 18.32727F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(665, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 38);
            this.label2.TabIndex = 4;
            this.label2.Text = "Вакансии";
            // 
            // vacs
            // 
            this.vacs.BackgroundColor = System.Drawing.Color.DeepSkyBlue;
            this.vacs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.vacs.Location = new System.Drawing.Point(26, 313);
            this.vacs.Name = "vacs";
            this.vacs.RowHeadersWidth = 47;
            this.vacs.RowTemplate.Height = 28;
            this.vacs.Size = new System.Drawing.Size(1415, 376);
            this.vacs.TabIndex = 5;
            // 
            // AddrequestToEmployer
            // 
            this.AddrequestToEmployer.BackColor = System.Drawing.Color.MediumBlue;
            this.AddrequestToEmployer.Font = new System.Drawing.Font("Segoe UI", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AddrequestToEmployer.ForeColor = System.Drawing.Color.White;
            this.AddrequestToEmployer.Location = new System.Drawing.Point(541, 726);
            this.AddrequestToEmployer.Name = "AddrequestToEmployer";
            this.AddrequestToEmployer.Size = new System.Drawing.Size(383, 54);
            this.AddrequestToEmployer.TabIndex = 38;
            this.AddrequestToEmployer.Text = "Подать заявку";
            this.AddrequestToEmployer.UseVisualStyleBackColor = false;
            this.AddrequestToEmployer.Click += new System.EventHandler(this.AddrequestToEmployer_Click);
            // 
            // Pccheckb
            // 
            this.Pccheckb.AutoSize = true;
            this.Pccheckb.BackColor = System.Drawing.Color.Transparent;
            this.Pccheckb.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Pccheckb.Location = new System.Drawing.Point(854, 80);
            this.Pccheckb.Name = "Pccheckb";
            this.Pccheckb.Size = new System.Drawing.Size(123, 29);
            this.Pccheckb.TabIndex = 81;
            this.Pccheckb.Text = "Знание Пк";
            this.Pccheckb.UseVisualStyleBackColor = false;
            // 
            // Driveliccheck
            // 
            this.Driveliccheck.AutoSize = true;
            this.Driveliccheck.BackColor = System.Drawing.Color.Transparent;
            this.Driveliccheck.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Driveliccheck.Location = new System.Drawing.Point(26, 143);
            this.Driveliccheck.Name = "Driveliccheck";
            this.Driveliccheck.Size = new System.Drawing.Size(198, 29);
            this.Driveliccheck.TabIndex = 80;
            this.Driveliccheck.Text = "Наличие вод.Прав";
            this.Driveliccheck.UseVisualStyleBackColor = false;
            // 
            // Poscb
            // 
            this.Poscb.FormattingEnabled = true;
            this.Poscb.Location = new System.Drawing.Point(226, 78);
            this.Poscb.Name = "Poscb";
            this.Poscb.Size = new System.Drawing.Size(181, 27);
            this.Poscb.TabIndex = 79;
            // 
            // Schedulecb
            // 
            this.Schedulecb.FormattingEnabled = true;
            this.Schedulecb.Location = new System.Drawing.Point(651, 183);
            this.Schedulecb.Name = "Schedulecb";
            this.Schedulecb.Size = new System.Drawing.Size(173, 27);
            this.Schedulecb.TabIndex = 78;
            // 
            // educationcb
            // 
            this.educationcb.FormattingEnabled = true;
            this.educationcb.Location = new System.Drawing.Point(226, 194);
            this.educationcb.Name = "educationcb";
            this.educationcb.Size = new System.Drawing.Size(181, 27);
            this.educationcb.TabIndex = 77;
            // 
            // Gendercb
            // 
            this.Gendercb.FormattingEnabled = true;
            this.Gendercb.Location = new System.Drawing.Point(854, 174);
            this.Gendercb.Name = "Gendercb";
            this.Gendercb.Size = new System.Drawing.Size(139, 27);
            this.Gendercb.TabIndex = 76;
            // 
            // Exptb
            // 
            this.Exptb.Location = new System.Drawing.Point(1056, 125);
            this.Exptb.Name = "Exptb";
            this.Exptb.Size = new System.Drawing.Size(133, 26);
            this.Exptb.TabIndex = 74;
            // 
            // citytb
            // 
            this.citytb.Location = new System.Drawing.Point(575, 79);
            this.citytb.Name = "citytb";
            this.citytb.Size = new System.Drawing.Size(249, 26);
            this.citytb.TabIndex = 73;
            // 
            // Salarytb
            // 
            this.Salarytb.Location = new System.Drawing.Point(637, 125);
            this.Salarytb.Name = "Salarytb";
            this.Salarytb.Size = new System.Drawing.Size(187, 26);
            this.Salarytb.TabIndex = 75;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(854, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 25);
            this.label6.TabIndex = 71;
            this.label6.Text = "Пол";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label14.Location = new System.Drawing.Point(451, 80);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 25);
            this.label14.TabIndex = 70;
            this.label14.Text = "Город";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(1266, 84);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 25);
            this.label9.TabIndex = 69;
            this.label9.Text = "Языки";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label13.Location = new System.Drawing.Point(451, 126);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 25);
            this.label13.TabIndex = 68;
            this.label13.Text = "Зарплата";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(26, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 25);
            this.label10.TabIndex = 67;
            this.label10.Text = "Должность";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(1056, 84);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(175, 25);
            this.label11.TabIndex = 66;
            this.label11.Text = "Опыт работы (лет)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label12.Location = new System.Drawing.Point(451, 181);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(146, 25);
            this.label12.TabIndex = 65;
            this.label12.Text = "График работы";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(26, 192);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(131, 25);
            this.label8.TabIndex = 72;
            this.label8.Text = "Образование";
            // 
            // Search
            // 
            this.Search.BackColor = System.Drawing.Color.MediumBlue;
            this.Search.Font = new System.Drawing.Font("Segoe UI", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Search.ForeColor = System.Drawing.Color.White;
            this.Search.Location = new System.Drawing.Point(1056, 167);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(175, 54);
            this.Search.TabIndex = 84;
            this.Search.Text = "Поиск";
            this.Search.UseVisualStyleBackColor = false;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(26, 249);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 25);
            this.label1.TabIndex = 67;
            this.label1.Text = "Работодатель";
            // 
            // Employercb
            // 
            this.Employercb.FormattingEnabled = true;
            this.Employercb.Location = new System.Drawing.Point(226, 247);
            this.Employercb.Name = "Employercb";
            this.Employercb.Size = new System.Drawing.Size(181, 27);
            this.Employercb.TabIndex = 79;
            // 
            // MyRequest
            // 
            this.MyRequest.BackColor = System.Drawing.Color.MediumBlue;
            this.MyRequest.Font = new System.Drawing.Font("Segoe UI", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MyRequest.ForeColor = System.Drawing.Color.White;
            this.MyRequest.Location = new System.Drawing.Point(1056, 227);
            this.MyRequest.Name = "MyRequest";
            this.MyRequest.Size = new System.Drawing.Size(175, 80);
            this.MyRequest.TabIndex = 84;
            this.MyRequest.Text = "Показать на оснвое моей заявки";
            this.MyRequest.UseVisualStyleBackColor = false;
            this.MyRequest.Click += new System.EventHandler(this.MyRequest_Click);
            // 
            // getvac
            // 
            this.getvac.BackColor = System.Drawing.Color.MediumBlue;
            this.getvac.Font = new System.Drawing.Font("Segoe UI", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.getvac.ForeColor = System.Drawing.Color.White;
            this.getvac.Location = new System.Drawing.Point(854, 235);
            this.getvac.Name = "getvac";
            this.getvac.Size = new System.Drawing.Size(175, 72);
            this.getvac.TabIndex = 84;
            this.getvac.Text = "Показать все";
            this.getvac.UseVisualStyleBackColor = false;
            this.getvac.Click += new System.EventHandler(this.AddOrUpdaterRqustbtn_Click);
            // 
            // lang
            // 
            this.lang.BackgroundColor = System.Drawing.Color.DeepSkyBlue;
            this.lang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lang.Location = new System.Drawing.Point(1249, 136);
            this.lang.Name = "lang";
            this.lang.RowHeadersWidth = 47;
            this.lang.RowTemplate.Height = 28;
            this.lang.Size = new System.Drawing.Size(178, 154);
            this.lang.TabIndex = 85;
            // 
            // AllVacancises
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Employment_Agency.Properties.Resources._1612170617_78_p_fon_gradient_sinii_fioletovii_93;
            this.ClientSize = new System.Drawing.Size(1439, 805);
            this.Controls.Add(this.lang);
            this.Controls.Add(this.MyRequest);
            this.Controls.Add(this.getvac);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.Pccheckb);
            this.Controls.Add(this.Driveliccheck);
            this.Controls.Add(this.Employercb);
            this.Controls.Add(this.Poscb);
            this.Controls.Add(this.Schedulecb);
            this.Controls.Add(this.educationcb);
            this.Controls.Add(this.Gendercb);
            this.Controls.Add(this.Exptb);
            this.Controls.Add(this.citytb);
            this.Controls.Add(this.Salarytb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.AddrequestToEmployer);
            this.Controls.Add(this.vacs);
            this.Controls.Add(this.label2);
            this.Name = "AllVacancises";
            this.Text = "AllVacancises";
            ((System.ComponentModel.ISupportInitialize)(this.vacs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label2;
        private DataGridView vacs;
        private Button AddrequestToEmployer;
        private CheckBox Pccheckb;
        private CheckBox Driveliccheck;
        private ComboBox Poscb;
        private ComboBox Schedulecb;
        private ComboBox educationcb;
        private ComboBox Gendercb;
        private TextBox Exptb;
        private TextBox citytb;
        private TextBox Salarytb;
        private Label label6;
        private Label label14;
        private Label label9;
        private Label label13;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label8;
        private Button Search;
        private Label label1;
        private ComboBox Employercb;
        private Button MyRequest;
        private Button getvac;
        private DataGridView lang;
    }
}