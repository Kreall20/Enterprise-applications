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

namespace Employment_Agency.Views
{
    public partial class Employer : Form
    {
        private EmploymentAgencyContext db;
        private int userid;
        private EmployerService employerService;
        private int vacancyid;

        public Employer(EmploymentAgencyContext db, int userid)
        {
            InitializeComponent();
            this.db = db;
            this.userid = userid;
            employerService = new EmployerService(this,db, userid);
            Refresh();
        }

        public Employer(EmploymentAgencyContext db, int userid, int vacancyid)
        {
            InitializeComponent();
            this.db = db;
            this.userid = userid;
            this.vacancyid = vacancyid;
            employerService = new EmployerService(this, db, userid);
            Refresh();
            employerService.ViewEmployer(vacancyid);
        }

        private void Refresh()
        {
            string[] Genders = { "Мужской", "Женский" };
            Gendercb.DataSource = Genders;
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
        private void AddOrUpdaterRqustbtn_Click(object sender, EventArgs e)
        {
            employerService.AddEmployer(Fiotb.Text, Gendercb.Text, Comptb.Text, Addresstb.Text,
                Phtb.Text, educationcb.Text, Poscb.Text, Driveliccheck.Checked, Salarytb.Text,
                citytb.Text, Pccheckb.Checked, Langtb.Text, Exptb.Text, Schedulecb.Text,userid);
        }
        public void ViewthisEmployer(string Fio, string Gender, string NameofCompany, string Address, string Phonenumber, string education, string Pos, bool drivelic, string salary, string city, bool Pc_kn, string Languages, string WorkExp, string Schedule)
        {
            Fiotb.Text = Fio;
            Comptb.Text = NameofCompany;
            Addresstb.Text = Address;
            Gendercb.SelectedItem = Gender;
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

        private void Update_Click(object sender, EventArgs e)
        {
            employerService.Update(Fiotb.Text, Gendercb.Text, Comptb.Text, Addresstb.Text,
                Phtb.Text, educationcb.Text, Poscb.Text, Driveliccheck.Checked, Salarytb.Text,
                citytb.Text, Pccheckb.Checked, Langtb.Text, Exptb.Text, Schedulecb.Text,vacancyid);
        }
    }
}
