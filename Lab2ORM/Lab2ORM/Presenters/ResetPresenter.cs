using Lab2ORM.Classes;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2ORM.Presenters
{
    public class ResetPresenter
    {
        private ResetLoginandPassword view;
        private MusicPlayerContext db;
        public ResetPresenter(ResetLoginandPassword view, MusicPlayerContext db)
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
        public string CheckUser(int userId)
        {
            string Login = db.Пользователиs.Where(p => p.КодПользователя == userId).First().Login;
            return Login;
        }
        public void CheckData(string oldpass,string Loginnew,string Loginold,string newpass)
        {
            var hashcode = GetHash(oldpass);
            var user = db.Пользователиs.FirstOrDefault(u => u.Login == Loginold && u.Password == hashcode);
            if (user == null) throw new Exception();
            if (Loginold == Loginold)
            {
                var hashcoden = GetHash(newpass);
                user.Password = hashcoden;
                db.SaveChanges();
            }
            else
            {
                var usernew = db.Пользователиs.FirstOrDefault(u => u.Login == Loginnew.Trim());
                if (usernew == null)
                {
                    var songs = db.ПесниПользователяs.Where(p => p.UserName == usernew.Login).ToList();
                    foreach (var item in songs)
                    {
                        item.UserName = Loginnew.Trim();
                        db.SaveChanges();
                    }
                    user.Login = Loginnew.Trim();
                    if (newpass.Length < 4)
                    {
                        MessageBox.Show("Длина пароля не меньше 4");
                        return;
                    }
                    var hashcodenew = GetHash(newpass);
                    user.Password = hashcodenew;
                    db.SaveChanges();
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
            }
            MessageBox.Show("Данные успешно изменены");
            view.Close();
        }
    }
}
