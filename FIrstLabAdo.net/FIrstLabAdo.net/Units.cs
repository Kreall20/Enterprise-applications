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
    public partial class Units : Form
    {
        private string connectionString;
        private int id;
        private string oldname;
        public Units()
        {
            InitializeComponent();
        }
        public Units(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
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
        public void Insert(string Name)
        {
            if (Name == null || Name.Trim() == "") throw new Exception();
            button5.Enabled = false;
            string sql = "Select Count(*) From Ед_Изм Where НаимЕдИзм = @НаимЕдИзм";
            int number = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@НаимЕдИзм", Name);
                number = (int)command.ExecuteScalar();
            }
            if (number != 0)
            {
                MessageBox.Show("Такая единица уже есть");
            }
            else
            {
                sql = "Insert into Ед_Изм (НаимЕдИзм) Values(@НаимЕдИзм)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@НаимЕдИзм", Name);
                    number = command.ExecuteNonQuery();
                }
            }

        }
        public void Update(string oldname,string Name, int id)
        {
            if (Name == null || Name.Trim() == "") throw new Exception();
            string sql = "Select Count(*) From Ед_Изм  Where НаимЕдИзм = @НаимЕдИзм";
            int number = 0;
            bool flag = false;
            if (oldname.Trim() == Name.Trim()) flag = true;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@НаимЕдИзм", Name);
                number = (int)command.ExecuteScalar();
            }
            if (number != 0 && !flag)
            {
                MessageBox.Show("Такая единица уже есть");
                return;
            }
            button5.Enabled = true;
            sql = $"Update Ед_Изм Set НаимЕдИзм = @НаимЕдИзм where КодЕдИзм = @КодЕдИзм";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@НаимЕдИзм", Name);
                command.Parameters.AddWithValue("@КодЕдИзм", id);
                command.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            button5.Enabled = false;
            string sql = $"Delete From Ед_Изм Where КодЕдИзм = @КодЕдИзм";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@КодЕдИзм", id);
                command.ExecuteNonQuery();
            }
        }
        public void Refresh()
        {
            string sql = "Select * From Ед_изм";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns["КодЕдИзм"].Visible = false;
            }
        }
        private void Units_Load(object sender, EventArgs e)
        {
            Refresh();
            button5.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                button5.Enabled = true;
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["НаимЕдИзм"].Value.ToString();
                id = (int)dataGridView1.SelectedRows[0].Cells["КодЕдИзм"].Value;
                oldname = dataGridView1.SelectedRows[0].Cells["НаимЕдИзм"].Value.ToString();
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
                    int id = (int)row.Cells["КодЕдИзм"].Value;
                    Delete(id);
                }
                Refresh();
                textBox1.Text = null;
            }
            catch
            {
                MessageBox.Show("Не выбрана(ы) Ед_Изм(ы)");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Update(oldname,textBox1.Text, id);
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
