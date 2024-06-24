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
    public partial class Form2 : Form
    {
        private string connectionString;
        private int id;
        private string Ing;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
        }
        public void Insert(string Name, int unit, double price)
        {
            if (Name == null || Name.Trim() == "") throw new Exception();
            button5.Enabled = false;
            //string sql = "Select Count(*) From Ингредиенты Where НаимИнг = @Name";
            string sql = "Select Count(*) From Ингредиенты Where НаимИнг = @Name and Ед_Изм = @Ед_Изм";
            int number = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@Ед_Изм", unit);
                number = (int)command.ExecuteScalar();
            }
            if (number != 0)
            {
                MessageBox.Show("Такой Ингредиент уже есть");
            }
            else
            {
                sql = "Insert into Ингредиенты (НаимИнг,Ед_Изм,Цена) Values(@НаимИнг,@Ед_Изм,@Цена)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@НаимИнг", Name);
                    command.Parameters.AddWithValue("@Ед_Изм", unit);
                    command.Parameters.AddWithValue("@Цена", price);
                    number = command.ExecuteNonQuery();
                }
            }

        }
        public void Update(string Ing,string Name, int unit, double price, int id)
        {
            if (Name == null || Name.Trim() == "") throw new Exception();
            bool flag = false;
            if (Ing.Trim() == Name.Trim()) flag = true;
            string sql = "Select Count(*) From Ингредиенты Where НаимИнг = @Name";
            int number = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Name", Name);
                number = (int)command.ExecuteScalar();
            }
            if (number != 0 && flag == false)
            {
                MessageBox.Show("Такой Ингредиент уже есть");
                return;
            }
            button5.Enabled = true;
            sql = $"Update Ингредиенты Set НаимИнг = @НаимИнг,Ед_Изм = @Ед_Изм,Цена = @Цена where КодИнг = @Код";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@НаимИнг", Name);
                command.Parameters.AddWithValue("@Ед_Изм", unit);
                command.Parameters.AddWithValue("@Цена", price);
                command.Parameters.AddWithValue("@Код", id);
                command.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            button5.Enabled = false;
            string sql = $"Delete From Ингредиенты Where КодИнг = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double sum;
                bool result = double.TryParse(textBox3.Text, out sum);
                if (result && sum != 0)
                {
                    Insert(textBox1.Text, int.Parse(textBox2.Text),sum);
                    Refresh();
                }
                else
                {
                    MessageBox.Show("Введены некорректные данные");
                }
                textBox1.Text = null;
                textBox2.Text = null;
                textBox3.Text = null;
            }
            catch
            {
                MessageBox.Show("Не все поля указаны или введены некорректные данные");
            }
        }
        public void Refresh()
        {
            string sql = "Select КодИнг,НаимИнг,Ед_Изм.НаимЕдИзм,Ед_Изм.КодЕдИзм,Цена From Ингредиенты " +
                "Inner Join Ед_Изм on Ед_Изм.КодЕдИзм = Ингредиенты.Ед_Изм " +
                "Order by НаимИнг";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns["КодИнг"].Visible = false;
                dataGridView1.Columns["КодЕдИзм"].Visible = false;
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            Refresh();
            button5.Enabled = false;
            string sql = "Select * From Ед_Изм";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView2.DataSource = ds.Tables[0];
                dataGridView2.Columns["КодЕдИзм"].Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                button5.Enabled = true;
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["НаимИнг"].Value.ToString();
                Ing = dataGridView1.SelectedRows[0].Cells["НаимИнг"].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells["КодЕдИзм"].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells["Цена"].Value.ToString();
                id = (int)dataGridView1.SelectedRows[0].Cells["КодИнг"].Value;
            }
            catch
            {
                MessageBox.Show("не выбрана строка");
            }
        }

        private void button2_Click(object sender, EventArgs e)
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
                    int id = (int)row.Cells["КодИнг"].Value;
                    Delete(id);
                }
                Refresh();
                textBox1.Text = null;
                textBox2.Text = null;
                textBox3.Text = null;
            }
            catch
            {
                MessageBox.Show("Не выбран(ы) Ингредиент(ы)");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                double sum;
                bool result = double.TryParse(textBox3.Text, out sum);
                if (result && sum != 0)
                {
                    Update(Ing,textBox1.Text, int.Parse(textBox2.Text), sum, id);
                    Refresh();
                }
                else
                {
                    MessageBox.Show("Введены некорректные данные");
                }
                    textBox1.Text = null;
                    textBox2.Text = null;
                    textBox3.Text = null;
                    button5.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Не все поля указаны или введены некорректные данные");
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 1) MessageBox.Show("Нужно выбрать 1 ед.изм");
            else
            {
                textBox2.Text = dataGridView2.Rows[e.RowIndex].Cells["КодЕдИзм"].Value.ToString();
            }
        }
    }
}
