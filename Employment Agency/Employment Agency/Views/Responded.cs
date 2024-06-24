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
    public partial class Responded : Form
    {
        private EmploymentAgencyContext db;
        private int userid;
        private EmployerService employerService;
        private int vacancyid;

        public Responded(EmploymentAgencyContext db, int userid)
        {
            InitializeComponent();
            this.db = db;
            this.userid = userid;
            employerService = new EmployerService(this, db, userid);
            employerService.GetVacancis(Vacs, userid);
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
                int vacancyid = (int)Vacs.SelectedRows[0].Cells["Vacancyid"].Value;
                Employer employer = new Employer(db, userid, vacancyid);
                employer.Show();
            }
            catch
            {
                MessageBox.Show("Необходимо выбрать заявку");
            }
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
                    citytb.Text, Pccheckb.Checked, selectedLanguages, w, sched, VacAppl, userid);
            }
            catch
            {

            }
        }

        private void acceptbtn_Click(object sender, EventArgs e)
        {
            employerService.Accept(VacAppl, vacancyid);
        }

        private void Vacs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                vacancyid = (int)Vacs.SelectedRows[0].Cells["Vacancyid"].Value;
                employerService.GetByVacancy(Vacs.SelectedRows[0],VacAppl);
            }
            catch
            {

            }
        }

        private void DeleteReq_Click(object sender, EventArgs e)
        {
            employerService.DeleteRequest(Vacs.SelectedRows);
            employerService.GetVacancis(Vacs, userid);
        }

        private void DeleteOffers_Click(object sender, EventArgs e)
        {
            employerService.DeleteOffersofAppl(VacAppl);
        }
    }
}
