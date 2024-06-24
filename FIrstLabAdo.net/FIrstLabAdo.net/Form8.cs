using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace FIrstLabAdo.net
{
    public partial class Form8 : Form
    {
        private DataGridViewRow rowDish;
        private string connectionString;
        private int id, dish,unit;
        public Form8()
        {
            InitializeComponent();
        }
        public Form8(string connectionString, DataGridViewRow row)
        {
            InitializeComponent();
            this.rowDish = row;
            this.connectionString = connectionString;

        }
        private void Form8_Load(object sender, EventArgs e)
        {
            Refresh();
            RefreshIng();
            button4.Enabled = false;
            label3.Text = "Ингредиенты для Блюда " + rowDish.Cells["НаимБлюда"].Value.ToString(); 
        }
        public void RefreshIng()
        {
            string sql = "Select КодИнг,НаимИнг,Ед_Изм.НаимЕдИзм,Ингредиенты.Ед_Изм,Цена From Ингредиенты " +
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
                dataGridView1.Columns["Ед_Изм"].Visible = false;
            }
        }
        public void Refresh()
        {
            string sql = "Select КалькуляцияБлюд.Id,КалькуляцияБлюд.КодБлюда,КалькуляцияБлюд.КодИнгр,Ингредиенты.НаимИнг,Ед_Изм.НаимЕдИзм,Кол_во,(Кол_во * Ингредиенты.Цена) as Цена From КалькуляцияБлюд " +
                "Inner Join Ингредиенты on КалькуляцияБлюд.КодИнгр = Ингредиенты.КодИнг " +
                "Inner Join Блюда on Блюда.КодБлюда = КалькуляцияБлюд.КодБлюда " +
                "Inner Join Ед_Изм on Ед_Изм.КодЕдИзм = Ингредиенты.Ед_Изм " +
                "Where КалькуляцияБлюд.КодБлюда = @id " +
                "Order by НаимИнг";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", (int)rowDish.Cells["КодБлюда"].Value);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;
                dataGridView2.Columns["КодИнгр"].Visible = false;
                dataGridView2.Columns["КодБлюда"].Visible = false;
                dataGridView2.Columns["Id"].Visible = false;
            }
        }
        public void UpdateDish(DataGridViewRow row, double count)
        {
            string sql = "Update Блюда Set Цена = @Сумма where КодБлюда = @Код";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string Price = "Select Цена From Блюда Where КодБлюда = @Код";
                SqlCommand command1 = new SqlCommand(Price, connection);
                command1.Parameters.AddWithValue("@Код", (int)rowDish.Cells["КодБлюда"].Value);
                decimal sum = (decimal)command1.ExecuteScalar();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Код", (int)rowDish.Cells["КодБлюда"].Value);
                command.Parameters.AddWithValue("@Сумма", sum + (decimal)row.Cells["Цена"].Value * (decimal)count);
                command.ExecuteNonQuery();
            }
        }
        public void Add(DataGridViewRow row, double count)
        {
            string sql1 = "Select Кол_во From КалькуляцияБлюд " +
                "Inner Join Ингредиенты on Ингредиенты.КодИнг = КалькуляцияБлюд.КодИнгр " +
                "Inner Join Ед_Изм on Ед_Изм.КодЕдИзм = Ингредиенты.Ед_Изм " +
                "Where КодБлюда = @КодБлюда and КалькуляцияБлюд.КодИнгр = @КодИнгр and Ед_Изм.КодЕдИзм = @ЕдИзм";
            object amount;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql1, connection);
                command.Parameters.AddWithValue("@КодБлюда", (int)rowDish.Cells["КодБлюда"].Value);
                command.Parameters.AddWithValue("@КодИнгр", (int)row.Cells["КодИнг"].Value);
                command.Parameters.AddWithValue("@ЕдИзм", (int)row.Cells["Ед_Изм"].Value);
                amount = command.ExecuteScalar();
            }
            if (amount != null)
            {
                sql1 = "Select Id From КалькуляцияБлюд " +
                "Inner Join Ингредиенты on Ингредиенты.КодИнг = КалькуляцияБлюд.КодИнгр " +
                "Inner Join Ед_Изм on Ед_Изм.КодЕдИзм = Ингредиенты.Ед_Изм " +
                "Where КодБлюда = @КодБлюда and КалькуляцияБлюд.КодИнгр = @КодИнгр and Ед_Изм.КодЕдИзм = @ЕдИзм";
                int id;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql1, connection);
                    command.Parameters.AddWithValue("@КодБлюда", (int)rowDish.Cells["КодБлюда"].Value);
                    command.Parameters.AddWithValue("@КодИнгр", (int)row.Cells["КодИнг"].Value);
                    command.Parameters.AddWithValue("@ЕдИзм", (int)row.Cells["Ед_Изм"].Value);
                    id = (int)command.ExecuteScalar();
                }

                string update = "Update КалькуляцияБлюд Set Кол_во = @Кол_во " +
                "Where Id = @Id";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(update, connection);
                    command.Parameters.AddWithValue("@Кол_во", (double)amount + count);
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                string sql = "Insert into КальКуляцияБлюд(КодБлюда,КодИнгр,Кол_во) Values(@КодБлюда,@КодИнгр,@Кол_во)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@КодБлюда", (int)rowDish.Cells["КодБлюда"].Value);
                    command.Parameters.AddWithValue("@КодИнгр", (int)row.Cells["КодИнг"].Value);
                    command.Parameters.AddWithValue("@Ед_Изм", (int)row.Cells["Ед_Изм"].Value);
                    command.Parameters.AddWithValue("@Кол_во", count);
                    command.ExecuteNonQuery();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                double count;
                bool result = double.TryParse(textBox1.Text, out count);
                if (result && count != 0 && count > 0)
                {
                    Add(selectedRow, count);
                    UpdateDish(selectedRow, count);
                    textBox1.Text = null;
                    Refresh();
                }
                else
                {
                    MessageBox.Show("Некорректный ввод количества");
                }
                textBox1.Text = null;
            }
            catch
            {
                if(textBox1.Text == "") MessageBox.Show("Укажите Количество");
                MessageBox.Show("Не выбран ни 1 ингредиент");
            }
        }
        public void Update(int id,double count)
        {
            string sql = "Update КалькуляцияБлюд Set Кол_во = @Кол_во "+
                "Where Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Кол_во", count);
                command.ExecuteNonQuery();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                button4.Enabled = true;
                textBox1.Text = dataGridView2.SelectedRows[0].Cells["Кол_во"].Value.ToString();
                id = (int)dataGridView2.SelectedRows[0].Cells["Id"].Value;
                dish = (int)dataGridView2.SelectedRows[0].Cells["КодБлюда"].Value;
            }
            catch
            {
                MessageBox.Show("не выбрана строка");
            }
        }
        public void Delete(int id)
        {
            string sql = "Delete From КалькуляцияБлюд " +
                "Where Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql,connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];
                string sql = "Select Ед_Изм.КодЕдИзм From КалькуляцияБлюд " +
                    "Inner Join Ингредиенты on Ингредиенты.КодИнг = КалькуляцияБлюд.КодИнгр " +
                    "Inner Join Ед_Изм on Ед_Изм.КодЕдИзм = Ингредиенты.Ед_Изм " +
                    "Where Ед_Изм.НаимЕдИзм = @ЕдИзм";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@ЕдИзм", selectedRow.Cells["НаимЕдИзм"].Value);
                    unit = (int)command.ExecuteScalar();
                }
            }
            catch
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                List<DataGridViewRow> selectedRows = new List<DataGridViewRow>();
                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    selectedRows.Add(row);
                }
                foreach (DataGridViewRow row in selectedRows)
                {
                    int id = (int)row.Cells["Id"].Value;
                    Delete(id);
                }
                Refresh();
                textBox1.Text = null;
            }
            catch
            {
                MessageBox.Show("Не выбран ни 1 ингредиент");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                double count,d;
                bool result = double.TryParse(textBox1.Text, out count);
                if (result && count != 0 && count > 0)
                {
                    Update(id,count);
                    Refresh();
                    textBox1.Text = null;
                    button4.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Некорректный ввод количества");
                }
                textBox1.Text = null;
            }
            catch
            {
                MessageBox.Show("Не все поля указаны или введены некорректные данные");
            }
        }
    }
}
