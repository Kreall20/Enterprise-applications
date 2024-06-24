using Employment_Agency.Models;
using Employment_Agency.Views;
using Lab2ORM.Classes;
using Microsoft.VisualBasic.Logging;
using Newtonsoft.Json;

namespace Employment_Agency
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            EmploymentAgencyContext db = new EmploymentAgencyContext();
            db.Applicants.ToList();
            if (File.Exists("user.json"))
            {
                string json = File.ReadAllText("user.json");
                UserSettings user = JsonConvert.DeserializeObject<UserSettings>(json);
                if (user.ТипПользователя == 1)
                {
                    Application.Run(new AdminPanel(db, user.UserId));
                }
                else
                {
                    Application.Run(new MainPanel(db, user.UserId));
                }
            }
            else Application.Run(new Login(db));
            db.Dispose();
            Application.Exit();
        }
    }
}