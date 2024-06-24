using Lab2ORM.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;

namespace Lab2ORM.Classes
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
            MusicPlayerContext db = new MusicPlayerContext();
            db.Пользователиs.ToList();
            if (File.Exists("user.json"))
            {
                string json = File.ReadAllText("user.json");
                UserSettings user = JsonConvert.DeserializeObject<UserSettings>(json);
                if (user.ТипПользователя == 1)
                {
                    Application.Run(new AdminPanel(db,user.UserId));
                }
                else
                {
                    Application.Run(new MainForm(db,user.UserId));
                }
            }
            else Application.Run(new LogIn(db));
            db.Dispose();
            Application.Exit();
        }
    }
}