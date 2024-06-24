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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace FIrstLabAdo.net
{
    public partial class Form9 : Form
    {
        private string connectionString;
        public Form9()
        {
            InitializeComponent();
        }
        public Form9(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
        }
        public void Refresh()
        {
            string sql = "Select * From Блюда";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns["КодБлюда"].Visible = false;
            }
        }
        public void Insert(int id, int count)
        {
            string sql1 = "Insert into Корзина (КодБлюда,Количество,Сумма) Values(@id,@Количество,@Сумма)";
            string sql = "Select count(*) From Корзина Where КодБлюда = @id";
            string sql2 = "Select Количество From Корзина Where КодБлюда = @id";
            string sql3 = "Update Корзина Set Количество = @Количество,Сумма = @Сумма Where КодБлюда = @id";
            string Price = "Select Цена From Блюда Where КодБлюда = @КодБлюда";
            int number = 0;
            decimal priceofDish;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                number = (int)command.ExecuteScalar();
                SqlCommand command1 = new SqlCommand(Price, connection);
                command1.Parameters.AddWithValue("@КодБлюда", id);
                priceofDish = (decimal)command1.ExecuteScalar();

            }
            if(number != 0)
            {
                int amount;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql2, connection);
                    command.Parameters.AddWithValue("@id", id);
                    amount = (int)command.ExecuteScalar();
                    SqlCommand command2 = new SqlCommand(sql3, connection);
                    command2.Parameters.AddWithValue("@id", id);
                    command2.Parameters.AddWithValue("@Количество", amount+count);
                    command2.Parameters.AddWithValue("@Сумма", (amount + count)*priceofDish);
                    command2.ExecuteNonQuery();
                } 
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql1, connection);
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@Количество", count);
                    command.Parameters.AddWithValue("@Сумма", (decimal)count * priceofDish);
                    command.ExecuteNonQuery();
                }
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int id = (int)dataGridView1.SelectedRows[0].Cells["КодБлюда"].Value;
                int count;
                bool result = int.TryParse(textBox1.Text, out count);
                if ( result && count != 0 && count > 0) { 
                    Insert(id,count);
                    MessageBox.Show("Блюдо добавлено в корзину");
                    }
                else
                {
                    MessageBox.Show("Некорректный ввод количества");
                }
            }
            catch
            {
                if(textBox1.Text == "") MessageBox.Show("Укажите количество");
                else MessageBox.Show("Не выбрано(ы) Блюдо(а)");
            }
        }
        public void SelectTypes()
        {
            string sql = "Select * From ТипыБлюд";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView2.DataSource = ds.Tables[0];
                dataGridView2.Columns["КодТипаБлюда"].Visible = false;
            }
        }
        private void Form9_Load(object sender, EventArgs e)
        {
            Refresh();
            SelectTypes();
        }
        public void SelectDishesOfType(int id)
        {
            string sql = "Select НаимБлюда,Цена,НаимТипаБлюда From Блюда " +
                "Inner Join ТипыБлюд on ТипыБлюд.КодТипаБлюда = Блюда.ТипБлюда " +
                "Where ТипБлюда = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id",id);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
        }
        private void dataGridView2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 1) MessageBox.Show("Нужно выбрать 1 тип");
            else
            {
                int id = (int)dataGridView2.SelectedRows[0].Cells["КодТипаБлюда"].Value;
                SelectDishesOfType(id);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
