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
    public partial class Employers_offers : Form
    {
        private EmploymentAgencyContext db;
        private int userid;
        private ApplicantService applicantService;
        public Employers_offers(EmploymentAgencyContext db, int userid)
        {
            InitializeComponent();
            this.db = db;
            this.userid = userid;
            applicantService = new ApplicantService(this, db, userid);
            //applicantService.GetVacancistome(userid,vacs);
        }

        /*private void acceptbtn_Click(object sender, EventArgs e)
        {
            applicantService.Send(vacs, userid);
        }

        private void DeleteOffers_Click(object sender, EventArgs e)
        {
            applicantService.DeleteOffersofAppl(vacs);
            applicantService.GetVacancistome(userid, vacs);
        }*/

        private void Employers_offers_Load(object sender, EventArgs e)
        {

        }
    }
}
