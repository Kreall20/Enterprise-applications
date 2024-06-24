using Employment_Agency.Models;
using Employment_Agency.Views;
using Lab2ORM.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment_Agency.Controllers
{
    public class UserService
    {
        private EmploymentAgencyContext db;
        private Login Loginview;
        private SignUp SignUpview;
        private Users users;
        private int userid;

        public UserService(EmploymentAgencyContext db, Login Loginview)
        {
            this.db = db;
            this.Loginview = Loginview;
        }
        public UserService(EmploymentAgencyContext db, SignUp SignUpview)
        {
            this.db = db;
            this.SignUpview = SignUpview;
        }

        public UserService(Users users, EmploymentAgencyContext db, int userid)
        {
            this.users = users;
            this.db = db;
            this.userid = userid;
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
            var userExists = db.Users.Any(u => u.Login == username);

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
                db.Users.Add(new User
                {
                    Login = username,
                    Password = GetHash(password),
                    TypeofUser = 2
                });
                db.SaveChanges();
                SignUpview.ClearBoxes();
            }
        }
        public void Entry(string Login, string password, bool checkedbox)
        {
            try
            {
                var hashcode = GetHash(password);
                var user = db.Users.Where(u => u.Login == Login.Trim() && u.Password == hashcode).First();
                if (user != null)
                {
                    Loginview.Hide();
                    Loginview.ClearBoxes();
                    if (checkedbox == true)
                    {
                        var hashcode1 = GetHash(user.Password);
                        var settings = new UserSettings()
                        {
                            Login = user.Login,
                            Password = hashcode1.ToString(),
                            ТипПользователя = user.TypeofUser,
                            UserId = user.Userid
                        };
                        string json = JsonConvert.SerializeObject(settings);
                        File.WriteAllText("user.json", json);
                    }
                    if (user.TypeofUser == 1)
                    {

                        AdminPanel adminPanel = new AdminPanel(Loginview, db, user.Userid);
                        adminPanel.Show();
                    }
                    else
                    {
                        MainPanel mainPanel = new MainPanel(Loginview, db, user.Userid);
                        mainPanel.Show();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Неверный email или пароль.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        public void Delete(DataGridView dataGridView1)
        {
            try
            {
                var selectedRows = dataGridView1.SelectedRows;
                foreach (DataGridViewRow row in selectedRows)
                {
                    int userid = (int)row.Cells["Userid"].Value;
                    var user = db.Users.Where(p => p.Userid == userid).First();
                    var applicants = db.DealApplicants.Where(p => p.Applicantid == userid).ToList();
                    db.DealApplicants.RemoveRange(applicants);
                    db.SaveChanges();
                    var applicantstovac = db.ApplicantToVacancies.Where(p => p.Userid == userid).ToList();
                    db.ApplicantToVacancies.RemoveRange(applicantstovac);
                    db.SaveChanges();
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
                users.AllUsers();
            }
            catch
            {

            }
        }
    }
}
