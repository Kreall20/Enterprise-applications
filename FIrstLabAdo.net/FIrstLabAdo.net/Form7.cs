using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace FIrstLabAdo.net
{
    public partial class Form7 : Form
    {
        private string connectionString;
        public Form7()
        {
            InitializeComponent();
        }
        public Form7(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
        }
        public void RefreshOrders()
        {
            string sql = "Select КодЗаказа,ДатаЗаказа,Sum(Сумма)as Сумма From Заказы " +
                "Group by КодЗаказа,ДатаЗаказа " +
                "Order by ДатаЗаказа";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            sql = "Select Sum(Блюда.Цена * Заказы.Кол_во) From Заказы Inner Join Блюда on Заказы.КодБлюда = Блюда.КодБлюда";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                label7.Text = command.ExecuteScalar().ToString();
            }
            while (dataGridView2.Rows.Count > 0)
            {
                dataGridView2.Rows.RemoveAt(0);
            }
            while (dataGridView3.Rows.Count > 0)
            {
                dataGridView3.Rows.RemoveAt(0);
            }
        }
        public void DishesofOrder(DataGridViewRow row)
        {
            string sql = "Select Заказы.КодЗаказа,Заказы.Кол_во,Заказы.КодБлюда,Блюда.НаимБлюда,Блюда.Цена,(Блюда.Цена * Заказы.Кол_во) as Итог " +
                "From Заказы Inner Join Блюда on Заказы.КодБлюда = Блюда.КодБлюда " +
                "Where Заказы.КодЗаказа = @КодЗаказа";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql,connection);
                command.Parameters.AddWithValue("@КодЗаказа", row.Cells["КодЗаказа"].Value.ToString());
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView3.DataSource = ds.Tables[0];
                dataGridView3.Columns["КодЗаказа"].Visible = false;
                dataGridView3.Columns["КодБлюда"].Visible = false;
            }
        }
        public void IngofDish(DataGridViewRow selectedRow)
        {
            string sql = "Select Ингредиенты.НаимИнг,КалькуляцияБлюд.Кол_во as КоличествонаПорицию,Ед_Изм.НаимЕдИзм,Sum(КалькуляцияБлюд.Кол_во * Заказы.Кол_во) as Общееколичество,Sum(КалькуляцияБлюд.Кол_во * Заказы.Кол_во) * Ингредиенты.Цена as Сумма From Заказы " +
                     "Inner Join КалькуляцияБлюд on КалькуляцияБлюд.КодБлюда = Заказы.КодБлюда " +
                     "Inner Join Ингредиенты On Ингредиенты.КодИнг = КалькуляцияБлюд.КодИнгр " +
                     "Inner Join Ед_Изм on Ед_Изм.КодЕдИзм = Ингредиенты.Ед_Изм " +
                     "Where Заказы.КодЗаказа = @КодЗаказа and Заказы.КодБлюда = @КодБлюда " +
                     "Group by Ингредиенты.НаимИнг,Ингредиенты.Цена,КалькуляцияБлюд.Кол_во,Ед_Изм.НаимЕдИзм " +
                     "Order by Sum(КалькуляцияБлюд.Кол_во * Заказы.Кол_во) desc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@КодЗаказа", selectedRow.Cells["КодЗаказа"].Value.ToString());
                command.Parameters.AddWithValue("@КодБлюда", (int)selectedRow.Cells["КодБлюда"].Value);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView2.DataSource = ds.Tables[0];
            }
        }
        public void Ordersovertime(DateTime f, DateTime s)
        {
            string sql = "Select КодЗаказа,ДатаЗаказа,Sum(Сумма)as Сумма From Заказы " +
                "Where ДатаЗаказа >= @fd and ДатаЗаказа <= @sd " +
                "Group by КодЗаказа,ДатаЗаказа " +
                "Order by ДатаЗаказа ";
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
            sql = "Select Sum(Блюда.Цена * Заказы.Кол_во) From Заказы Inner Join Блюда on Заказы.КодБлюда = Блюда.КодБлюда " +
                "Where Заказы.ДатаЗаказа >= @fd and Заказы.ДатаЗаказа <= @sd";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@fd", f);
                command.Parameters.AddWithValue("@sd", s);
                label7.Text = command.ExecuteScalar().ToString();
            }
        }
        private void Form7_Load(object sender, EventArgs e)
        {
            RefreshOrders();
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            RefreshOrders();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            while (dataGridView2.Rows.Count > 0)
            {
                dataGridView2.Rows.RemoveAt(0);
            }
            while (dataGridView3.Rows.Count > 0)
            {
                dataGridView3.Rows.RemoveAt(0);
            }
            Ordersovertime(dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                while (dataGridView2.Rows.Count > 0)
                {
                    dataGridView2.Rows.RemoveAt(0);
                }
                DishesofOrder(selectedRow);
            }
            catch
            {
                MessageBox.Show("Выберите 1 строку");
            }
                        /* if (e.RowIndex >= 0)
             {
                 DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                 string sql = "Select Ингредиенты.НаимИнг,Sum(КалькуляцияБлюд.Кол_во * Заказы.Кол_во) as Количество,Sum(КалькуляцияБлюд.Кол_во * Заказы.Кол_во) * Ингредиенты.Цена as Сумма From Заказы " +
                     "Inner Join КалькуляцияБлюд on КалькуляцияБлюд.КодБлюда = Заказы.КодБлюда " +
                     "Inner Join Ингредиенты On Ингредиенты.КодИнг = КалькуляцияБлюд.КодИнгр " +
                     "Where Заказы.КодЗаказа = @КодЗаказа and Заказы.КодБлюда = @КодБлюда " +
                     "Group by Ингредиенты.НаимИнг,Ингредиенты.Цена " +
                     "Order by Sum(КалькуляцияБлюд.Кол_во * Заказы.Кол_во) desc";
                 using (SqlConnection connection = new SqlConnection(connectionString))
                 {
                     connection.Open();
                     SqlCommand command = new SqlCommand(sql, connection);
                     command.Parameters.AddWithValue("@КодЗаказа", selectedRow.Cells["КодЗаказа"].Value.ToString());
                     command.Parameters.AddWithValue("@КодБлюда", (int)selectedRow.Cells["КодБлюда"].Value);
                     SqlDataAdapter adapter = new SqlDataAdapter(command);
                     DataSet ds = new DataSet();
                     adapter.Fill(ds);
                     dataGridView2.DataSource = ds.Tables[0];
                 }
                 sql = "Select Sum(Блюда.Цена * Заказы.Кол_во) From Заказы Inner Join Блюда on Заказы.КодБлюда = Блюда.КодБлюда " +
                 "Where Заказы.КодЗаказа = @КодЗаказа";
                 using (SqlConnection connection = new SqlConnection(connectionString))
                 {
                     connection.Open();
                     SqlCommand command = new SqlCommand(sql, connection);
                     command.Parameters.AddWithValue("@КодЗаказа", selectedRow.Cells["КодЗаказа"].Value.ToString());
                     label7.Text = command.ExecuteScalar().ToString();
                 }
             }*/
        }
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dataGridView3.Rows[e.RowIndex];
                IngofDish(selectedRow);
            }
            catch
            {
                MessageBox.Show("Выберите 1 строку");
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
