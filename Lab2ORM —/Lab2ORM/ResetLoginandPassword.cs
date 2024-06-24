using Lab2ORM.Classes;
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

namespace Lab2ORM
{
    public partial class ResetLoginandPassword : Form
    {
        private int UserId;
        private MusicPlayerContext db;
        private string Login;
        public ResetLoginandPassword(int userId, MusicPlayerContext db)
        {
            InitializeComponent();
            UserId = userId;
            this.db = db;
            textBox2.PasswordChar = '*';
            textBox3.PasswordChar = '*';
            Refresh();
        }
        public void Refresh()
        {
            textBox1.Text = db.Пользователиs.Where(p => p.КодПользователя == UserId).First().Login;
            Login = textBox1.Text;
            textBox2.Text = "";
            textBox3.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "") throw new ArgumentNullException();
                var user = db.Пользователиs.FirstOrDefault(u => u.Login == Login && u.Password == textBox2.Text.Trim());
                if (user == null) throw new Exception();
                if(Login == textBox1.Text)
                {
                    user.Password = textBox3.Text.Trim();
                }
                else
                {
                    var usernew = db.Пользователиs.FirstOrDefault(u => u.Login == textBox1.Text.Trim());
                    if (usernew == null)
                    {
                        user.Login = textBox1.Text.Trim();
                        user.Password = textBox3.Text.Trim();
                    }
                    else throw new NullReferenceException();
                    if (File.Exists("user.json"))
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
                    db.SaveChanges();
                    Refresh();
                    this.Close();
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Пожалуйста введите данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Такой пользователь уже есть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            catch
            {
                MessageBox.Show("Введен неверный пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}
