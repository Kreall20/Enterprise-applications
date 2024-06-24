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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Lab2ORM
{
    public partial class SignUp : Form
    {
        private MusicPlayerContext db;
        public SignUp(MusicPlayerContext db)
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
            this.db = db;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "") throw new Exception();
                var userExists = db.Пользователиs.Any(u => u.Login == textBox1.Text);

                if (userExists)
                {
                    MessageBox.Show("Пользователь уже существует");
                    return;
                }
                else
                {
                    db.Пользователиs.Add(new Пользователи
                    {
                        Login = textBox1.Text,
                        Password = textBox2.Text,
                        ТипПользователя = 2
                    });
                    db.SaveChanges();
                    textBox1.Text = "";
                    textBox2.Text = "";
                }   
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
    }
}
