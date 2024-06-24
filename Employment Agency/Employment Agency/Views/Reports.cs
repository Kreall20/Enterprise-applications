using Employment_Agency.Controllers;
using Employment_Agency.Models;
using Employment_Agency.Servises;
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
    public partial class Reports : Form
    {
        private EmploymentAgencyContext db;
        private int userid;
        private ReportService reportService;
        public Reports(EmploymentAgencyContext db, int userid)
        {
            InitializeComponent();
            this.db = db;
            this.userid = userid;
            reportService = new ReportService(this, db, userid);
            reportService.GetDeals(deals);
            reportService.GetEmpouersforRequest(employers);
        }

        private void GetReportbtn_Click(object sender, EventArgs e)
        {
            reportService.GetDealApplicantsDate(deals, dateTimePicker1, dateTimePicker2);

            reportService.GetEmpouersforRequestDate(employers, dateTimePicker1, dateTimePicker2);
        }

        private void deals_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            reportService.GetDealApplicants(deals, applicants);
        }
    }
}
