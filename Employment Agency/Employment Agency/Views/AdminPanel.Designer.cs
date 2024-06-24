namespace Employment_Agency.Views
{
    partial class AdminPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminPanel));
            this.label1 = new System.Windows.Forms.Label();
            this.Applicantsbtn = new System.Windows.Forms.Button();
            this.Employersbtn = new System.Windows.Forms.Button();
            this.Reportsbtn = new System.Windows.Forms.Button();
            this.applicants = new System.Windows.Forms.DataGridView();
            this.vacs = new System.Windows.Forms.DataGridView();
            this.DeleteVacbtn = new System.Windows.Forms.Button();
            this.Deleteemorapplbtn = new System.Windows.Forms.Button();
            this.Search = new System.Windows.Forms.Button();
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
            this.Exitbtn = new System.Windows.Forms.Button();
            this.EducPositions = new System.Windows.Forms.Button();
            this.Users = new System.Windows.Forms.Button();
            this.lang = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.Employercb = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.applicants)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vacs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lang)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 18.32727F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(481, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(523, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Добро Пожаловать В Панель Админа";
            // 
            // Applicantsbtn
            // 
            this.Applicantsbtn.BackColor = System.Drawing.Color.Aquamarine;
            this.Applicantsbtn.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Applicantsbtn.Location = new System.Drawing.Point(27, 76);
            this.Applicantsbtn.Name = "Applicantsbtn";
            this.Applicantsbtn.Size = new System.Drawing.Size(149, 54);
            this.Applicantsbtn.TabIndex = 1;
            this.Applicantsbtn.Text = "Соискатели";
            this.Applicantsbtn.UseVisualStyleBackColor = false;
            this.Applicantsbtn.Click += new System.EventHandler(this.Applicantsbtn_Click);
            // 
            // Employersbtn
            // 
            this.Employersbtn.BackColor = System.Drawing.Color.Aquamarine;
            this.Employersbtn.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Employersbtn.Location = new System.Drawing.Point(619, 76);
            this.Employersbtn.Name = "Employersbtn";
            this.Employersbtn.Size = new System.Drawing.Size(149, 54);
            this.Employersbtn.TabIndex = 1;
            this.Employersbtn.Text = "Работодатели";
            this.Employersbtn.UseVisualStyleBackColor = false;
            this.Employersbtn.Click += new System.EventHandler(this.Employersbtn_Click);
            // 
            // Reportsbtn
            // 
            this.Reportsbtn.BackColor = System.Drawing.Color.Aquamarine;
            this.Reportsbtn.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Reportsbtn.Location = new System.Drawing.Point(1293, 76);
            this.Reportsbtn.Name = "Reportsbtn";
            this.Reportsbtn.Size = new System.Drawing.Size(149, 54);
            this.Reportsbtn.TabIndex = 1;
            this.Reportsbtn.Text = "Отчеты";
            this.Reportsbtn.UseVisualStyleBackColor = false;
            this.Reportsbtn.Click += new System.EventHandler(this.Reportsbtn_Click);
            // 
            // applicants
            // 
            this.applicants.BackgroundColor = System.Drawing.Color.Cyan;
            this.applicants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.applicants.Location = new System.Drawing.Point(12, 391);
            this.applicants.Name = "applicants";
            this.applicants.RowHeadersWidth = 47;
            this.applicants.RowTemplate.Height = 28;
            this.applicants.Size = new System.Drawing.Size(1415, 281);
            this.applicants.TabIndex = 2;
            // 
            // vacs
            // 
            this.vacs.BackgroundColor = System.Drawing.Color.Cyan;
            this.vacs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.vacs.Location = new System.Drawing.Point(12, 747);
            this.vacs.Name = "vacs";
            this.vacs.RowHeadersWidth = 47;
            this.vacs.RowTemplate.Height = 28;
            this.vacs.Size = new System.Drawing.Size(1415, 281);
            this.vacs.TabIndex = 2;
            // 
            // DeleteVacbtn
            // 
            this.DeleteVacbtn.BackColor = System.Drawing.Color.MediumBlue;
            this.DeleteVacbtn.Font = new System.Drawing.Font("Segoe UI", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DeleteVacbtn.ForeColor = System.Drawing.Color.White;
            this.DeleteVacbtn.Location = new System.Drawing.Point(828, 77);
            this.DeleteVacbtn.Name = "DeleteVacbtn";
            this.DeleteVacbtn.Size = new System.Drawing.Size(426, 54);
            this.DeleteVacbtn.TabIndex = 36;
            this.DeleteVacbtn.Text = "Delete Vacancises";
            this.DeleteVacbtn.UseVisualStyleBackColor = false;
            this.DeleteVacbtn.Click += new System.EventHandler(this.DeleteVacbtn_Click);
            // 
            // Deleteemorapplbtn
            // 
            this.Deleteemorapplbtn.BackColor = System.Drawing.Color.MediumBlue;
            this.Deleteemorapplbtn.Font = new System.Drawing.Font("Segoe UI", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Deleteemorapplbtn.ForeColor = System.Drawing.Color.White;
            this.Deleteemorapplbtn.Location = new System.Drawing.Point(209, 76);
            this.Deleteemorapplbtn.Name = "Deleteemorapplbtn";
            this.Deleteemorapplbtn.Size = new System.Drawing.Size(383, 54);
            this.Deleteemorapplbtn.TabIndex = 36;
            this.Deleteemorapplbtn.Text = "Delete Employer/Applicant";
            this.Deleteemorapplbtn.UseVisualStyleBackColor = false;
            this.Deleteemorapplbtn.Click += new System.EventHandler(this.Deleteemorapplbtn_Click);
            // 
            // Search
            // 
            this.Search.BackColor = System.Drawing.Color.MediumBlue;
            this.Search.Font = new System.Drawing.Font("Segoe UI", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Search.ForeColor = System.Drawing.Color.White;
            this.Search.Location = new System.Drawing.Point(1061, 259);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(175, 54);
            this.Search.TabIndex = 105;
            this.Search.Text = "Поиск";
            this.Search.UseVisualStyleBackColor = false;
            this.Search.Click += new System.EventHandler(this.AddOrUpdaterRqustbtn_Click);
            // 
            // Pccheckb
            // 
            this.Pccheckb.AutoSize = true;
            this.Pccheckb.BackColor = System.Drawing.Color.Transparent;
            this.Pccheckb.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Pccheckb.Location = new System.Drawing.Point(859, 172);
            this.Pccheckb.Name = "Pccheckb";
            this.Pccheckb.Size = new System.Drawing.Size(123, 29);
            this.Pccheckb.TabIndex = 103;
            this.Pccheckb.Text = "Знание Пк";
            this.Pccheckb.UseVisualStyleBackColor = false;
            // 
            // Driveliccheck
            // 
            this.Driveliccheck.AutoSize = true;
            this.Driveliccheck.BackColor = System.Drawing.Color.Transparent;
            this.Driveliccheck.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Driveliccheck.Location = new System.Drawing.Point(31, 235);
            this.Driveliccheck.Name = "Driveliccheck";
            this.Driveliccheck.Size = new System.Drawing.Size(198, 29);
            this.Driveliccheck.TabIndex = 102;
            this.Driveliccheck.Text = "Наличие вод.Прав";
            this.Driveliccheck.UseVisualStyleBackColor = false;
            // 
            // Poscb
            // 
            this.Poscb.FormattingEnabled = true;
            this.Poscb.Location = new System.Drawing.Point(231, 170);
            this.Poscb.Name = "Poscb";
            this.Poscb.Size = new System.Drawing.Size(181, 27);
            this.Poscb.TabIndex = 100;
            // 
            // Schedulecb
            // 
            this.Schedulecb.FormattingEnabled = true;
            this.Schedulecb.Location = new System.Drawing.Point(656, 275);
            this.Schedulecb.Name = "Schedulecb";
            this.Schedulecb.Size = new System.Drawing.Size(173, 27);
            this.Schedulecb.TabIndex = 99;
            // 
            // educationcb
            // 
            this.educationcb.FormattingEnabled = true;
            this.educationcb.Location = new System.Drawing.Point(231, 286);
            this.educationcb.Name = "educationcb";
            this.educationcb.Size = new System.Drawing.Size(181, 27);
            this.educationcb.TabIndex = 98;
            // 
            // Gendercb
            // 
            this.Gendercb.FormattingEnabled = true;
            this.Gendercb.Location = new System.Drawing.Point(859, 266);
            this.Gendercb.Name = "Gendercb";
            this.Gendercb.Size = new System.Drawing.Size(139, 27);
            this.Gendercb.TabIndex = 97;
            // 
            // Exptb
            // 
            this.Exptb.Location = new System.Drawing.Point(1061, 217);
            this.Exptb.Name = "Exptb";
            this.Exptb.Size = new System.Drawing.Size(133, 26);
            this.Exptb.TabIndex = 95;
            // 
            // citytb
            // 
            this.citytb.Location = new System.Drawing.Point(580, 171);
            this.citytb.Name = "citytb";
            this.citytb.Size = new System.Drawing.Size(249, 26);
            this.citytb.TabIndex = 94;
            // 
            // Salarytb
            // 
            this.Salarytb.Location = new System.Drawing.Point(642, 217);
            this.Salarytb.Name = "Salarytb";
            this.Salarytb.Size = new System.Drawing.Size(187, 26);
            this.Salarytb.TabIndex = 96;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(859, 228);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 25);
            this.label6.TabIndex = 92;
            this.label6.Text = "Пол";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label14.Location = new System.Drawing.Point(456, 172);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 25);
            this.label14.TabIndex = 91;
            this.label14.Text = "Город";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(1271, 176);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 25);
            this.label9.TabIndex = 90;
            this.label9.Text = "Языки";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label13.Location = new System.Drawing.Point(456, 218);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 25);
            this.label13.TabIndex = 89;
            this.label13.Text = "Зарплата";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(31, 172);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 25);
            this.label10.TabIndex = 87;
            this.label10.Text = "Должность";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(1061, 176);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(175, 25);
            this.label11.TabIndex = 86;
            this.label11.Text = "Опыт работы (лет)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label12.Location = new System.Drawing.Point(456, 273);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(146, 25);
            this.label12.TabIndex = 85;
            this.label12.Text = "График работы";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(31, 284);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(131, 25);
            this.label8.TabIndex = 93;
            this.label8.Text = "Образование";
            // 
            // Exitbtn
            // 
            this.Exitbtn.BackColor = System.Drawing.Color.MediumBlue;
            this.Exitbtn.Font = new System.Drawing.Font("Segoe UI", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Exitbtn.ForeColor = System.Drawing.Color.White;
            this.Exitbtn.Location = new System.Drawing.Point(1232, 5);
            this.Exitbtn.Name = "Exitbtn";
            this.Exitbtn.Size = new System.Drawing.Size(210, 54);
            this.Exitbtn.TabIndex = 106;
            this.Exitbtn.Text = "Выйти";
            this.Exitbtn.UseVisualStyleBackColor = false;
            this.Exitbtn.Click += new System.EventHandler(this.Exitbtn_Click);
            // 
            // EducPositions
            // 
            this.EducPositions.BackColor = System.Drawing.Color.MediumBlue;
            this.EducPositions.Font = new System.Drawing.Font("Segoe UI", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EducPositions.ForeColor = System.Drawing.Color.White;
            this.EducPositions.Location = new System.Drawing.Point(31, 5);
            this.EducPositions.Name = "EducPositions";
            this.EducPositions.Size = new System.Drawing.Size(248, 54);
            this.EducPositions.TabIndex = 106;
            this.EducPositions.Text = "Образование/Должности";
            this.EducPositions.UseVisualStyleBackColor = false;
            this.EducPositions.Click += new System.EventHandler(this.EducPositions_Click);
            // 
            // Users
            // 
            this.Users.BackColor = System.Drawing.Color.MediumBlue;
            this.Users.Font = new System.Drawing.Font("Segoe UI", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Users.ForeColor = System.Drawing.Color.White;
            this.Users.Location = new System.Drawing.Point(1040, 5);
            this.Users.Name = "Users";
            this.Users.Size = new System.Drawing.Size(175, 54);
            this.Users.TabIndex = 107;
            this.Users.Text = "Users";
            this.Users.UseVisualStyleBackColor = false;
            this.Users.Click += new System.EventHandler(this.Users_Click);
            // 
            // lang
            // 
            this.lang.BackgroundColor = System.Drawing.Color.DeepSkyBlue;
            this.lang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lang.Location = new System.Drawing.Point(1262, 217);
            this.lang.Name = "lang";
            this.lang.RowHeadersWidth = 47;
            this.lang.RowTemplate.Height = 28;
            this.lang.Size = new System.Drawing.Size(180, 109);
            this.lang.TabIndex = 134;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.78182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(31, 341);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 25);
            this.label2.TabIndex = 88;
            this.label2.Text = "Работодатель";
            // 
            // Employercb
            // 
            this.Employercb.FormattingEnabled = true;
            this.Employercb.Location = new System.Drawing.Point(231, 339);
            this.Employercb.Name = "Employercb";
            this.Employercb.Size = new System.Drawing.Size(181, 27);
            this.Employercb.TabIndex = 101;
            // 
            // AdminPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1475, 1058);
            this.Controls.Add(this.lang);
            this.Controls.Add(this.Users);
            this.Controls.Add(this.EducPositions);
            this.Controls.Add(this.Exitbtn);
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
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Deleteemorapplbtn);
            this.Controls.Add(this.DeleteVacbtn);
            this.Controls.Add(this.vacs);
            this.Controls.Add(this.applicants);
            this.Controls.Add(this.Reportsbtn);
            this.Controls.Add(this.Employersbtn);
            this.Controls.Add(this.Applicantsbtn);
            this.Controls.Add(this.label1);
            this.Name = "AdminPanel";
            this.Text = "AdminPanel";
            ((System.ComponentModel.ISupportInitialize)(this.applicants)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vacs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Button Applicantsbtn;
        private Button Employersbtn;
        private Button Reportsbtn;
        private DataGridView applicants;
        private DataGridView vacs;
        private Button DeleteVacbtn;
        private Button Deleteemorapplbtn;
        private Button Search;
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
        private Button Exitbtn;
        private Button EducPositions;
        private Button Users;
        private DataGridView lang;
        private Label label2;
        private ComboBox Employercb;
    }
}