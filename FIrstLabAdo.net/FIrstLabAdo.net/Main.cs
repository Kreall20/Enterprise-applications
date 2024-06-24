using System.Data.SqlTypes;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace FIrstLabAdo.net
{
    public partial class Main : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public Main()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(connectionString);
            form2.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(connectionString);
            form3.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(connectionString);
            form4.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(connectionString);
            form2.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(connectionString);
            form3.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(connectionString);
            form5.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(connectionString);
            form4.ShowDialog();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Form6 form6 = new Form6(connectionString);
            form6.ShowDialog();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Form7 form7 = new Form7(connectionString);
            form7.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Units units = new Units(connectionString);
            units.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Types typesofdishes = new Types(connectionString);
            typesofdishes.ShowDialog();
        }
    }
}