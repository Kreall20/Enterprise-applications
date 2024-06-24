using Lab2ORM.Classes;
using Lab2ORM.Forms;
using Lab2ORM.Presenters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Lab2ORM
{
    public partial class LogIn : Form
    {
        private MusicPlayerContext db;
        private LoginPresenter presenter;
        public LogIn(MusicPlayerContext dbcontext)
        {
            InitializeComponent();
            db = dbcontext;
            presenter = new LoginPresenter(this, db);
            textBox2.PasswordChar = '*';
        }
        public void ClearBoxes()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "") throw new Exception();
                presenter.Entry(textBox1.Text, textBox2.Text,checkBox1.Checked);
            }
            catch
            {
                MessageBox.Show("Не введены данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void label5_Click(object sender, EventArgs e)
        {
            SignUp sign = new SignUp(db);
            sign.Show();
        }
    }
}
