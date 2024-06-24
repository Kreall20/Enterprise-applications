using Employment_Agency.Controllers;
using Employment_Agency.Models;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employment_Agency.Views
{
    public partial class AllVacancises : Form
    {
        private EmploymentAgencyContext db;
        private int userid;
        private ApplicantService applicantService;
        public AllVacancises(EmploymentAgencyContext db, int userid)
        {
            InitializeComponent();
            this.db = db;
            this.userid = userid;
            applicantService = new ApplicantService(this,db, userid);
            applicantService.GetVacancis(vacs, userid);
            Refresh();
        }
        private void Refresh()
        {
            string[] Genders = { "","Мужской", "Женский" };
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
            employerList.Add(new Models.Employer { Company = "",Fio ="",Userid = 1,PhoneNumber = "",Address = "",Empoyerid = 2});
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
        private void AddOrUpdaterRqustbtn_Click(object sender, EventArgs e)
        {
            applicantService.GetVacancis(vacs, userid);
        }

        private void MyRequest_Click(object sender, EventArgs e)
        {
            //Поиск на основе моей заявки

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
            var user = db.Applicants.Where(p => p.Applicantid == userid).First();
            var educ = db.Educations.Where(p => p.Educationid == user.Education).First();
            var pos = db.Positions.Where(p => p.Positionid == user.Position).First();
            string gender = "%" + user.Gender + "%";
            string educs = "%" + educ.Education1 + "%";
            string poss = "%" + pos.Position1 + "%";
            string sched = "%" + user.Schedule + "%";

            applicantService.Serchmy(gender,
                educs, poss, Driveliccheck.Checked, s,
                citytb.Text, Pccheckb.Checked, user.Languages, w, sched, vacs, userid);
        }

        private void AddrequestToEmployer_Click(object sender, EventArgs e)
        {
            applicantService.Send(vacs,userid);
        }

        private void Search_Click(object sender, EventArgs e)
        {
            //Поиск
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

            applicantService.Serch(gender, emp,
                educ, pos, Driveliccheck.Checked, s,
                citytb.Text, Pccheckb.Checked, selectedLanguages, w, sched, vacs,userid);
        }
    }
}
