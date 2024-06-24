using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIrstLabAdo.net
{
    public partial class Form4 : Form
    {
        private string connectionString;
        public Form4()
        {
            InitializeComponent();
        }
        public Form4(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;

        }
        private void Form4_Load(object sender, EventArgs e)
        {
            string sql = "Select КодЗаказа,Блюда.НаимБлюда,ДатаЗаказа,Кол_во,Блюда.Цена,Сумма From Заказы " +
                "Inner Join Блюда on Блюда.КодБлюда = Заказы.КодБлюда";
            string sum = "Select Sum(Сумма) From Заказы";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sum, connection);
                label3.Text = command.ExecuteScalar().ToString();
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
        }
    }
}
