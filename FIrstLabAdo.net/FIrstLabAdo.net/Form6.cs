using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIrstLabAdo.net
{
    public partial class Form6 : Form
    {
        private string connectionString;
        public Form6()
        {
            InitializeComponent();
        }
        public Form6(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
        }
        public void Ordersovertime(DateTime f, DateTime s)
        {
            string sql = "Select Ингредиенты.НаимИнг,Sum(КалькуляцияБлюд.Кол_во * Заказы.Кол_во) as Количество,Sum(КалькуляцияБлюд.Кол_во * Заказы.Кол_во) * Ингредиенты.Цена as Сумма From Заказы " +
                "Inner Join КалькуляцияБлюд on КалькуляцияБлюд.КодБлюда = Заказы.КодБлюда " +
                "Inner Join Ингредиенты On Ингредиенты.КодИнг = КалькуляцияБлюд.КодИнгр " +
                "Where Заказы.ДатаЗаказа >= @fd and Заказы.ДатаЗаказа <= @sd " +
                "Group by Ингредиенты.НаимИнг,Ингредиенты.Цена " +
                "Order by Sum(КалькуляцияБлюд.Кол_во * Заказы.Кол_во) desc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@fd", f);
                command.Parameters.AddWithValue("@sd", s);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }
        public void RefreshAll()
        {
            string sql = "Select Ингредиенты.НаимИнг,Ед_Изм.НаимЕдИзм,Sum(КалькуляцияБлюд.Кол_во * Заказы.Кол_во) as Количество,Sum(КалькуляцияБлюд.Кол_во * Заказы.Кол_во) * Ингредиенты.Цена as Сумма From Заказы " +
                "Inner Join КалькуляцияБлюд on КалькуляцияБлюд.КодБлюда = Заказы.КодБлюда " +
                "Inner Join Ингредиенты On Ингредиенты.КодИнг = КалькуляцияБлюд.КодИнгр " +
                "Inner Join Ед_Изм on Ед_Изм.КодЕдИзм = Ингредиенты.Ед_Изм " +
                "Group by Ингредиенты.НаимИнг,Ингредиенты.Цена,Ед_Изм.НаимЕдИзм " +
                "Order by Sum(КалькуляцияБлюд.Кол_во * Заказы.Кол_во) desc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
        }
        private void Form6_Load(object sender, EventArgs e)
        {
            RefreshAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ordersovertime(dateTimePicker1.Value, dateTimePicker2.Value);
        }
    }
}
