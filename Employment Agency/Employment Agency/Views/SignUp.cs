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
    public partial class SignUp : Form
    {
        private EmploymentAgencyContext db;
        private UserService userService;
        private int userid;
        public SignUp(EmploymentAgencyContext db)
        {
            InitializeComponent();
            this.db = db;
            Passwtb.PasswordChar = '*';
            userService = new UserService(db, this);
        }
        public void ClearBoxes()
        {
            Logintb.Text = "";
            Passwtb.Text = "";
        }
        private void Signbtn_Click(object sender, EventArgs e)
        {
            userService.AddnewUser(Logintb.Text, Passwtb.Text);
        }

        private void loginlabel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
