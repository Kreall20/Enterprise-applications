using Lab2ORM.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Lab2ORM.Presenters
{
    public class SignUpPresenter
    {
        private SignUp view;
        private MusicPlayerContext db;
        public SignUpPresenter(SignUp view, MusicPlayerContext db)
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
        public void AddnewUser(string username, string password)
        {
            var userExists = db.Пользователиs.Any(u => u.Login == username);

            if (userExists)
            {
                MessageBox.Show("Пользователь уже существует");
                return;
            }
            else
            {
                if (password.Length < 4)
                {
                    MessageBox.Show("Длина пароля не меньше 4");
                    return;
                }
                db.Пользователиs.Add(new Пользователи
                {
                    Login = username,
                    Password = GetHash(password),
                    ТипПользователя = 2
                });
                db.SaveChanges();
                view.ClearTextboxes();
            }
        }
    }
}
