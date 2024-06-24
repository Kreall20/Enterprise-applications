using Employment_Agency.Controllers;
using Employment_Agency.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Employment_Agency
{
    public partial class AddApplicant : Form
    {
        private EmploymentAgencyContext db;
        private int userid;
        private ApplicantService applicantService;
        public AddApplicant(EmploymentAgencyContext db, int userid)
        {
            InitializeComponent();
            this.db = db;
            this.userid = userid;
            applicantService = new ApplicantService(this, db, userid);
            applicantService.ViewApplicant();   
            Refresh();
        }
        private void Refresh()
        {
            string[] Genders = {"Мужской","Женский" };
            Gendercb.DataSource= Genders;
            educationcb.DataSource = db.Educations.ToList();
            educationcb.DisplayMember = "Education1";
            Poscb.DataSource = db.Positions.ToList();
            Poscb.DisplayMember = "Position1";
            string[] Schedules = new string[]
            {
                "Скользящий",
                "Ненормированный",
                "Гибкий",
                "Сменный",
                "Разделенный"
            };
            Schedulecb.DataSource = Schedules;
        }
        private void AddApplicant_Load(object sender, EventArgs e)
        {
            //applicantService.ViewApplicant();
        }
        private void AddOrUpdaterRqustbtn_Click(object sender, EventArgs e)
        {
            applicantService.AddApplicant(FIOtb.Text, Gendercb.Text, dateTimePicker1.Value,
                Phtb.Text, educationcb.Text, Poscb.Text, Driveliccheck.Checked, Salarytb.Text,
                citytb.Text, Pccheckb.Checked, Langtb.Text, Exptb.Text, Schedulecb.Text);
        }
        public void ViewthisApplicant(string Fio, string Gender, DateTime dateofbirth, string Phonenumber, string education, string Pos, bool drivelic, string salary, string city, bool Pc_kn, string Languages, string WorkExp, string Schedule)
        {
            FIOtb.Text = Fio;
            Gendercb.SelectedItem = Gender;
            dateTimePicker1.Value = dateofbirth;
            Phtb.Text = Phonenumber;
            educationcb.SelectedItem = education;
            Poscb.Text = Pos;
            Driveliccheck.Checked = drivelic;
            Salarytb.Text = salary;
            citytb.Text = city;
            Pccheckb.Checked = Pc_kn;
            Langtb.Text = Languages;
            Exptb.Text = WorkExp;
            Schedulecb.SelectedItem = Schedule;
        }

        private void DeleteRequest_Click(object sender, EventArgs e)
        {
            applicantService.Deleteappl(userid);
        }
    }
}
