using Lab2ORM.Classes;
using Lab2ORM.Forms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Lab2ORM.Presenters
{
    public class LoginPresenter
    {
        private LogIn view;
        private MusicPlayerContext db;
        public LoginPresenter(LogIn view, MusicPlayerContext db)
        {
            this.view = view;
            this.db = db;
        }
        public string GetHash(string Text)
        {
            string password = "";
            foreach (var item in Text)
            {
                password += ((byte)item).ToString();
            }
            return password;
        }
        public void Entry(string Login,string password,bool checkedbox)
        {
            var hashcode = GetHash(password);
            var user = db.Пользователиs.FirstOrDefault(u => u.Login == Login.Trim() && u.Password == hashcode);
            if (user != null)
            {
                view.Hide();
                view.ClearBoxes();
                if (checkedbox == true)
                {
                    var hashcode1 = GetHash(user.Password);
                    var settings = new UserSettings()
                    {
                        Login = user.Login,
                        Password = hashcode1.ToString(),
                        ТипПользователя = user.ТипПользователя,
                        UserId = user.КодПользователя
                    };
                    string json = JsonConvert.SerializeObject(settings);
                    File.WriteAllText("user.json", json);
                }
                if (user.ТипПользователя == 1)
                {

                    AdminPanel adminPanel = new AdminPanel(view, db, user.КодПользователя);
                    adminPanel.Show();
                }
                else
                {
                    MainForm mainform = new MainForm(view, db, user.КодПользователя);
                    mainform.Show();
                }
            }
            else
            {
                MessageBox.Show("Неверный email или пароль.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
