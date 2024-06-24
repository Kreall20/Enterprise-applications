using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employment_Agency.Views
{
    public partial class MainPanel : Form
    {
        private Login loginview;
        private Models.EmploymentAgencyContext db;
        private int userid;

        public MainPanel(Models.EmploymentAgencyContext db, int userid)
        {
            InitializeComponent();
            this.db = db;
            this.userid = userid;
            this.loginview = new Login(db);
        }

        public MainPanel(Login loginview, Models.EmploymentAgencyContext db, int userid)
        {
            InitializeComponent();
            this.loginview = loginview;
            this.db = db;
            this.userid = userid;
        }

        private void AddrequestApplbtn_Click(object sender, EventArgs e)
        {
            AddApplicant applicant = new AddApplicant(db,userid);
            applicant.Show();
        }

        private void GetReportsbtn_Click(object sender, EventArgs e)
        {
            ReportsforUser reportsuser = new ReportsforUser(db, userid);
            reportsuser.Show();
        }

        private void MyRequests_Click(object sender, EventArgs e)
        {
            MyReq my = new MyReq(db, userid);
            my.Show();
        }

        private void RespondedAppl_Click(object sender, EventArgs e)
        {
            Responded responded = new Responded(db, userid);
            responded.Show();
        }

        private void respondedEmployements_Click(object sender, EventArgs e)
        {
            Employers_offers _Offers = new Employers_offers(db, userid);
            _Offers.Show();
        }

        private void ViewVacanciesbtn_Click(object sender, EventArgs e)
        {
            AllVacancises allVacancises = new AllVacancises(db, userid);
            allVacancises.Show();
        }

        private void AddRequestEmpbtn_Click(object sender, EventArgs e)
        {
            Employer employer = new Employer(db, userid);
            employer.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists("user.json"))
                File.Delete("user.json");
            this.Hide();
            loginview.Show();
        }
    }
}
