using Lab2ORM.Classes;
using Lab2ORM.Forms;
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
        public LogIn(MusicPlayerContext dbcontext)
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
            db = dbcontext;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "") throw new Exception();
                var user = db.Пользователиs.FirstOrDefault(u => u.Login == textBox1.Text.Trim() && u.Password == textBox2.Text.Trim());
                if (user != null)
                {
                    this.Hide();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    if (checkBox1.Checked == true)
                    {
                        var settings = new UserSettings()
                        {
                            Login = user.Login,
                            Password = user.Password,
                            ТипПользователя = user.ТипПользователя,
                            UserId = user.КодПользователя
                        };
                        string json = JsonConvert.SerializeObject(settings);
                        File.WriteAllText("user.json", json);
                    }
                    if (user.ТипПользователя == 1)
                    {

                        AdminPanel adminPanel = new AdminPanel(this,db,user.КодПользователя);
                        adminPanel.Show();
                    }
                    else
                    {
                        MainForm mainform = new MainForm(this,db,user.КодПользователя);
                        mainform.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Неверный email или пароль.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
