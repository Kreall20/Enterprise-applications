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
    public partial class Login : Form
    {
        private EmploymentAgencyContext db;
        private UserService userService;
        private int userid;
        public Login(EmploymentAgencyContext context)
        {
            InitializeComponent();
            Passtb.PasswordChar = '*';
            db = context;
            userService = new UserService(db,this);
        }
        public Login(EmploymentAgencyContext context,int userid)
        {
            InitializeComponent();
            Passtb.PasswordChar = '*';
            db = context;
            userService = new UserService(db,this);
            this.userid = userid;
        }
        private void loginbtn_Click(object sender, EventArgs e)
        {
            userService.Entry(Logintb.Text, Passtb.Text,checkBox1.Checked);
            /*AdminPanel adminPanel = new AdminPanel();
            adminPanel.Show();
            MainPanel mainPanel = new MainPanel();
            mainPanel.Show();
            Responded responded = new Responded();
            responded.Show();*/
        }

        private void Signlabel_Click(object sender, EventArgs e)
        {
            SignUp signUp = new SignUp(db);
            signUp.Show();
        }
        public void ClearBoxes()
        {
            Logintb.Text = "";
            Passtb.Text = "";
        }
    }
}
