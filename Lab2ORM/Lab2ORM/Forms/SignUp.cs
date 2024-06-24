using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab2ORM.Classes;
using Lab2ORM.Presenters;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Lab2ORM
{
    public partial class SignUp : Form
    {
        private SignUpPresenter presenter;
        
        private MusicPlayerContext db;
        public SignUp(MusicPlayerContext db)
        {
            InitializeComponent();
            this.db = db;
            presenter = new SignUpPresenter(this,db);
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "") throw new Exception();
                presenter.AddnewUser(textBox1.Text, textBox2.Text);
            }
            catch
            {
                MessageBox.Show("Не введены данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
        public void ClearTextboxes()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}
