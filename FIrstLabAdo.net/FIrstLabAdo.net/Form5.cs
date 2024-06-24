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

namespace FIrstLabAdo.net
{
    public partial class Form5 : Form
    {
        private string connectionString;
        private int id;
        private int dish;
        public Form5()
        {
            InitializeComponent();
        }
        public Form5(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
        }
        public void AddOrder(DataGridViewRow row,DateTime time)
        {
            string codeoforder = time.Year.ToString() + time.Month.ToString()+time.Hour.ToString()+time.Second;
            string sql = "Insert into Заказы (КодЗаказа,КодБлюда,ДатаЗаказа,Кол_во,Сумма) Values(@КодЗаказа,@КодБлюда,@ДатаЗаказа,@Кол_во,@Сумма)";
            string sql1 = "Select Цена From Блюда Where КодБлюда = @КодБлюда";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand(sql1, connection);
                command1.Parameters.AddWithValue("@КодБлюда", row.Cells["КодБлюда"].Value.ToString());
                decimal sum = (decimal)command1.ExecuteScalar();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@КодБлюда", row.Cells["КодБлюда"].Value.ToString());
                command.Parameters.AddWithValue("@ДатаЗаказа", time);
                command.Parameters.AddWithValue("@КодЗаказа", codeoforder.Trim());
                command.Parameters.AddWithValue("@Кол_во", (int)row.Cells["Количество"].Value);
                command.Parameters.AddWithValue("@Сумма", sum * (int)row.Cells["Количество"].Value);
                command.ExecuteNonQuery();
            }
        }
        public void Update(int id, int count,int dish)
        {
            button5.Enabled = true;
            decimal sum;
            string sql1 = "Select Цена From Блюда Where КодБлюда = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql1, connection);
                command.Parameters.AddWithValue("@id", dish);
                sum = (decimal)command.ExecuteScalar();
            }
            string sql = "Update Корзина Set Количество = @Количество,Сумма = @Сумма Where id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Количество", count);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@Сумма", sum * count);
                command.ExecuteNonQuery();
            }

        }
        public void DeleteItem(int id)
        {
            button5.Enabled = false;
            string sql = "Delete From Корзина Where id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                List<DataGridViewRow> selectedRows = new List<DataGridViewRow>();
                DateTime time = DateTime.Now;
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                { 
                        selectedRows.Add(row);
                }
                foreach (DataGridViewRow row in selectedRows)
                {        
                    AddOrder(row,time);
                    id = (int)row.Cells["id"].Value;
                    DeleteItem(id);
                }
                Refresh();
                MessageBox.Show("Заказ Выполнен");
            }
            catch
            {
                MessageBox.Show("Не выбрано Блюдо(а)");
            }
        }
        public void Refresh()
        {
            string sql = "Select Корзина.id,Корзина.КодБлюда,Блюда.НаимБлюда,Количество,Блюда.Цена,Сумма From Корзина " +
                "Inner Join Блюда on Блюда.КодБлюда = Корзина.КодБлюда";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql1 = "UPDATE Корзина SET Сумма = Блюда.Цена * Корзина.Количество "+
                    "FROM Корзина " +
                    "Inner JOIN Блюда ON Блюда.КодБлюда = Корзина.КодБлюда";
                SqlCommand command = new SqlCommand(sql1, connection);
                command.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns["КодБлюда"].Visible = false;
                dataGridView1.Columns["id"].Visible = false;
            }
            string sum = "Select Sum(Сумма) From Корзина";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sum, connection);
                if (command.ExecuteScalar() == null) label3.Text = "0";
                else label3.Text = command.ExecuteScalar().ToString();
            }
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            button5.Enabled = false;
            Refresh();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["Количество"].Value.ToString();
                button5.Enabled = true;
                id = (int)dataGridView1.SelectedRows[0].Cells["id"].Value;
                dish = (int)dataGridView1.SelectedRows[0].Cells["КодБлюда"].Value;
            }
            catch
            {
                MessageBox.Show("Не выбрано Блюдо(а)");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                List<DataGridViewRow> selectedRows = new List<DataGridViewRow>();
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    selectedRows.Add(row);
                }
                foreach (DataGridViewRow row in selectedRows)
                {
                    int id = (int)row.Cells["id"].Value;
                    DeleteItem(id);
                }
                Refresh();
            }
            catch
            {
                MessageBox.Show("Не выбрано Блюдо(а)");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form9 form9 = new Form9(connectionString);
            form9.ShowDialog();
            this.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int count;
                bool result = int.TryParse(textBox1.Text, out count);
                if (result && count != 0 && count > 0)
                {
                    Update(id, count,dish);
                    Refresh();
                    button5.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Некорректный ввод количества");
                }
                textBox1.Text = null;
            }
            catch
            {
                if (textBox1.Text == "") MessageBox.Show("Не выбрано количество");
                else MessageBox.Show("Не выбрано блюдо");
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(connectionString);
            form4.ShowDialog();
        }
    }
}
