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
    public partial class Users : Form
    {
        private EmploymentAgencyContext db;
        private int userid;
        private UserService userService;
        private int vacancyid;
        public Users(EmploymentAgencyContext db, int userid)
        {
            InitializeComponent();
            this.db = db;
            this.userid = userid;
            userService = new UserService(this, db, userid);
            AllUsers();
        }
        public void AllUsers()
        {
            dataGridView1.DataSource = db.Users.Where(p => p.Userid != userid).ToList();
            dataGridView1.Columns["Userid"].Visible = false;
            dataGridView1.Columns["Password"].Visible = false;
            dataGridView1.Columns["TypeofUser"].Visible = false;
        }
        private void DeleteUser_Click(object sender, EventArgs e)
        {
            userService.Delete(dataGridView1);
        }
    }
}
