using Employment_Agency.Controllers;
using Employment_Agency.Models;
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
    public partial class MyReq : Form
    {
        private EmploymentAgencyContext db;
        private int userid;
        private EmployerService employerService;
        int vacancyid;

        public MyReq(EmploymentAgencyContext db, int userid)
        {
            InitializeComponent();
            this.db = db;
            this.userid = userid;
            employerService = new EmployerService(this, db, userid);
            employerService.GetVacancis(vacs, userid);
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
            var uniqueLanguages = db.ApplicantLanguages
            .GroupBy(vl => vl.Language)
            .Select(g => g.First())
            .ToList();

            lang.DataSource = uniqueLanguages;
            lang.Columns["id"].Visible = false;
            lang.Columns["Applicant"].Visible = false;
            lang.Columns["ApplicantNavigation"].Visible = false;
        }
        private void UpdateRequest_Click(object sender, EventArgs e)
        {
            try
            {
                int vacancyid = (int)vacs.SelectedRows[0].Cells["Vacancyid"].Value;
                Employer employer = new Employer(db, userid, vacancyid);
                employer.Show();
            }
            catch
            {
                MessageBox.Show("Необходимо выбрать заявку");
            }
        }

        private void acceptbtn_Click(object sender, EventArgs e)
        {
            employerService.Accept(applicantstovacancy, vacancyid);
        }

        private void Search_Click(object sender, EventArgs e)
        {
            //Поиск
            try
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
                string educ = "%" + educationcb.Text + "%";
                string pos = "%" + Poscb.Text + "%";
                string sched = "%" + Schedulecb.Text + "%";

                employerService.Serch(gender,
                    educ, pos, Driveliccheck.Checked, s,
                    citytb.Text, Pccheckb.Checked, selectedLanguages, w, sched, applicantstovacancy, userid);
            }
            catch
            {

            }
        }

        private void AllAppl_Click(object sender, EventArgs e)
        {
            employerService.GetAll(applicantstovacancy,userid);
        }

        private void vacs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var selectedLanguages = lang.SelectedRows;
                vacancyid = (int)vacs.SelectedRows[0].Cells["Vacancyid"].Value;
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

                var vac = db.Vacancies.Where(p => p.Vacancyid == vacancyid).First();
                var educ = db.Educations.Where(p => p.Educationid == vac.Education).First();
                var pos = db.Positions.Where(p => p.Positionid == vac.Position).First();

                string gender = "%" + vac.Gender + "%";
                string educs = "%" + educ.Education1 + "%";
                string poss = "%" + pos.Position1 + "%";
                string sched = "%" + vac.Schedule + "%";

                employerService.Serchmy(gender,
                    educs, poss, Driveliccheck.Checked, s,
                    citytb.Text, Pccheckb.Checked, vac.Languages, w, sched, applicantstovacancy, userid,vacancyid);
            }
            catch
            {

            }
        }

        private void DeleteRequest_Click(object sender, EventArgs e)
        {
            employerService.DeleteRequest(vacs.SelectedRows);
        }

        private void MyReq_Load(object sender, EventArgs e)
        {

        }

        private void Search_Click_1(object sender, EventArgs e)
        {

        }
    }
}
