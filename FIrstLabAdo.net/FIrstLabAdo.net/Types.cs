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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FIrstLabAdo.net
{
    public partial class Types : Form
    {
        private string connectionString;
        private int id;
        private string name;
        public Types()
        {
            InitializeComponent();
        }
        public Types(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
        }
        public void Refresh()
        {
            string sql = "Select * From ТипыБлюд";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns["КодТипаБлюда"].Visible = false;
            }
        }
        public void Insert(string Name)
        {
            if (Name == null || Name.Trim() == "") throw new Exception();
            button5.Enabled = false;
            string sql = "Select Count(*) From ТипыБлюд Where НаимТипаБлюда = @НаимТипаБлюда";
            int number = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@НаимТипаБлюда", Name);
                number = (int)command.ExecuteScalar();
            }
            if (number != 0)
            {
                MessageBox.Show("Такой Тип уже есть");
            }
            else
            {
                sql = "Insert into ТипыБлюд (НаимТипаБлюда) Values(@НаимТипаБлюда)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@НаимТипаБлюда", Name);
                    number = command.ExecuteNonQuery();
                }
            }

        }
        public void Update(string oldname,string Name,int id)
        {
            if (Name == null || Name.Trim() == "") throw new Exception();
            string sql = "Select Count(*) From ТипыБлюд  Where НаимТипаБлюда = @Name";
            int number = 0;
            bool flag = false;
            if (oldname.Trim() == Name.Trim()) flag = true;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Name", Name);
                number = (int)command.ExecuteScalar();
            }
            if (number != 0 && !flag)
            {
                MessageBox.Show("Такой Тип уже есть");
                return;
            }
            button5.Enabled = true;
            sql = $"Update ТипыБлюд Set НаимТипаБлюда = @НаимТипаБлюда where КодТипаБлюда = @КодТипаБлюда";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@НаимТипаБлюда", Name);
                command.Parameters.AddWithValue("@КодТипаБлюда", id);
                command.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            button5.Enabled = false;
            string sql = $"Delete From ТипыБлюд Where КодТипаБлюда = @КодТипаБлюда";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@КодТипаБлюда", id);
                command.ExecuteNonQuery();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Insert(textBox1.Text);
                Refresh();
                textBox1.Text = null;
            }
            catch
            {
                MessageBox.Show("Не указано название");
                textBox1.Text = null;
            }
        }

        private void Types_Load(object sender, EventArgs e)
        {
            Refresh();
            button5.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                button5.Enabled = true;
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["НаимТипаБлюда"].Value.ToString();
                id = (int)dataGridView1.SelectedRows[0].Cells["КодТипаБлюда"].Value;
                name = dataGridView1.SelectedRows[0].Cells["НаимТипаБлюда"].Value.ToString();
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
                    int id = (int)row.Cells["КодТипаБлюда"].Value;
                    Delete(id);
                }
                Refresh();
                textBox1.Text = null;
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
                Update(name,textBox1.Text,id);
                Refresh();
                textBox1.Text = null;
                button5.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Не все поля указаны или введены некорректные данные");
                textBox1.Text = null;
            }
        }
    }
}
