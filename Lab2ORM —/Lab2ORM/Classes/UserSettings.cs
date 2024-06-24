using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2ORM.Classes
{
    public class UserSettings
    {
        public int ТипПользователя { get; set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;
        public int UserId { get; set; }

        public UserSettings(int типПользователя, string Login, string password, int userId)
        {
            ТипПользователя = типПользователя;
            this.Login = Login;
            Password = password;
            UserId = userId;
        }
        public UserSettings()
        {
        }
    }
}
