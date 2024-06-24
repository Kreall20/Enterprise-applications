using Employment_Agency.Controllers;
using Employment_Agency.Models;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employment_Agency.Views
{
    public partial class AdminPanel : Form
    {
        private Login loginview;
        private EmploymentAgencyContext db;
        private int userid;
        private AdminService adminService;
        private int flag = 0;

        public AdminPanel(EmploymentAgencyContext db, int userid)
        {
            InitializeComponent();
            this.db = db;
            this.userid = userid;
            adminService = new AdminService(this,db,userid);
            adminService.GetApplicantsEmployers(applicants, vacs);
            Refresh();
        }
        public AdminPanel(Login loginview, EmploymentAgencyContext db, int userid)
        {
            InitializeComponent();
            this.loginview = loginview;
            this.db = db;
            this.userid = userid;
            adminService = new AdminService(this, db, userid);
            adminService.GetApplicantsEmployers(applicants, vacs);
            Refresh();
        }
        private void Refresh()
        {
            string[] Genders = { "", "Мужской", "Женский" };
            Gendercb.DataSource = Genders;

            var educationList = new List<Education>();
            educationList.Add(new Education { Education1 = "" });
            educationList.AddRange(db.Educations.ToList());
            educationcb.DataSource = educationList;
            educationcb.DisplayMember = "Education1";

            var positionList = new List<Position>();
            positionList.Add(new Position { Position1 = "" });
            positionList.AddRange(db.Positions.ToList());
            Poscb.DataSource = positionList;
            Poscb.DisplayMember = "Position1";

            string[] Schedules = new string[]
            {
                "",
                "Скользящий",
                "Ненормированный",
                "Гибкий",
                "Сменный",
                "Разделенный"
            };
            Schedulecb.DataSource = Schedules;

            var employerList = new List<Models.Employer>();
            employerList.Add(new Models.Employer { Company = "", Fio = "", Userid = 1, PhoneNumber = "", Address = "", Empoyerid = 2 });
            employerList.AddRange(db.Employers.Where(p => p.Userid != userid).ToList());
            Employercb.DataSource = employerList;
            Employercb.DisplayMember = "Company";

            var uniqueLanguages = db.VacancyLanguages
            .GroupBy(vl => vl.Language)
            .Select(g => g.First())
            .ToList();

            lang.DataSource = uniqueLanguages;
            lang.Columns["id"].Visible = false;
            lang.Columns["Vacancy"].Visible = false;
            lang.Columns["VacancyNavigation"].Visible = false;
        }
        private void Reportsbtn_Click(object sender, EventArgs e)
        {
            Reports report = new Reports(db,userid);
            report.Show();
        }

        private void AddOrUpdaterRqustbtn_Click(object sender, EventArgs e)
        {
            var selectedLanguages = lang.SelectedRows;
            int s = 1;
            int w = 100;
            if (int.TryParse(Salarytb.Text, out int parsedSalary))
            {
                s = parsedSalary;
            }

            if (int.TryParse(Exptb.Text, out int parsedWorkExp))
            {
                w = parsedWorkExp;
            }
            string gender = "%" + Gendercb.Text + "%";
            string emp = "%" + Employercb.Text + "%";
            string educ = "%" + educationcb.Text + "%";
            string pos = "%" + Poscb.Text + "%";
            string sched = "%" + Schedulecb.Text + "%";

            adminService.Serchvac(gender, emp,
                educ, pos, Driveliccheck.Checked, s,
                citytb.Text, Pccheckb.Checked, selectedLanguages, w, sched, vacs, userid);

            adminService.Serchappl(gender,
                    educ, pos, Driveliccheck.Checked, s,
                    citytb.Text, Pccheckb.Checked, selectedLanguages, w, sched, applicants, userid);
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            if (File.Exists("user.json"))
                File.Delete("user.json");
            this.Hide();
            loginview.Show();
        }

        private void EducPositions_Click(object sender, EventArgs e)
        {
            EducationPosition educationPosition = new EducationPosition(db,userid);
            educationPosition.Show();
        }

        private void Users_Click(object sender, EventArgs e)
        {
            Users users = new Users(db, userid);
            users.Show();
        }

        private void Deleteemorapplbtn_Click(object sender, EventArgs e)
        {
            if(flag == 1) 
            {
                adminService.DelteApplicants(applicants);
                adminService.GetApplicantsEmployers(applicants, vacs);
            }
            if (flag == 2)
            {
                adminService.DelteEmployers(applicants);
                adminService.GetApplicantsEmployers(applicants, vacs);
            }
        }

        private void DeleteVacbtn_Click(object sender, EventArgs e)
        {
            adminService.DeleteVacancy(vacs);
            adminService.GetApplicantsEmployers(applicants, vacs);
        }

        private void Applicantsbtn_Click(object sender, EventArgs e)
        {
            adminService.GetApplicantsEmployers(applicants, vacs);
            adminService.GetApplicants(applicants);
            flag = 1;
        }

        private void Employersbtn_Click(object sender, EventArgs e)
        {
            adminService.GetApplicantsEmployers(applicants, vacs);
            adminService.GetEmployers(applicants);
            flag = 2;
        }
    }
}
