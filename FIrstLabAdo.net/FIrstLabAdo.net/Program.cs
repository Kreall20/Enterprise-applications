using System.Configuration;

namespace FIrstLabAdo.net
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        public static string connectionString;
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            Application.Run(new Main());
        }
    }
}