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
    public partial class Form3 : Form
    {
        private string connectionString;
        private int id;
        private string dish;
        public Form3()
        {
            InitializeComponent();
        }
        public Form3(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.Show();
        }
        public void InsertDish(string Name,int type)
        {
            if (Name == null || Name.Trim() == "") throw new Exception();
            button5.Enabled = false;
            string sql = "Select Count(*) From Блюда Where НаимБлюда = @Name";
            int number = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Name",Name);
                number = (int)command.ExecuteScalar();
            }
            if (number != 0)
            {
                MessageBox.Show("Такое Блюдо уже есть");
            }
            else
            {
                sql = "Insert into Блюда (НаимБлюда,Цена,ТипБлюда) Values(@НаимБлюда,@Цена,@ТипБлюда)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@НаимБлюда", Name);
                    command.Parameters.AddWithValue("@Цена", 0);
                    command.Parameters.AddWithValue("@ТипБлюда", type);
                    number = command.ExecuteNonQuery();
                }
            } 
        }
        public void UpdateDish(string dishname,string Name,int type,int id)
        {
            if (Name == null || Name.Trim() == "") throw new Exception();
            button5.Enabled = true;
            bool flag = false;
            if(dishname.Trim() == Name.Trim()) flag = true;
            string sql = "Select Count(*) From Блюда Where НаимБлюда = @Name";
            int number = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Name", Name);
                number = (int)command.ExecuteScalar();
            }
            if (number != 0 && !flag)
            {
                MessageBox.Show("Такое Блюдо уже есть");
                return;
            }
            sql = "Update Блюда Set НаимБлюда = @НаимБлюда,ТипБлюда = @Type where КодБлюда = @Код";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@НаимБлюда", Name);
                command.Parameters.AddWithValue("@Type", type);
                command.Parameters.AddWithValue("@Код", id);
                command.ExecuteNonQuery();
            }
        }
        public void DeleteDish(int id)
        {
            button5.Enabled = false;
            string sql = $"Delete From Блюда Where КодБлюда = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id",id);
                command.ExecuteNonQuery();
            }
        }
        public void SelectTypes()
        {
            button5.Enabled = false;
            string sql = "Select * From ТипыБлюд";
            using(SqlConnection connection = new SqlConnection(connectionString))
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
        public void SelectDishes()
        {
            button5.Enabled = false;
            string sql = "Select Блюда.КодБлюда,Блюда.ТипБлюда,Блюда.НаимБлюда,Блюда.Цена,ТипыБлюд.НаимТипаБлюда From Блюда " +
                "Inner Join ТипыБлюд on ТипыБлюд.КодТипаБлюда = Блюда.ТипБлюда " +
                "Order by НаимБлюда";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns["КодБлюда"].Visible = false;
                dataGridView1.Columns["ТипБлюда"].Visible = false;
            }
        }
        public void RefreshDishes()
        {
            button5.Enabled = false;
            /*string sql = "Select Блюда.КодБлюда,НаимБлюда,Isnull(Sum(Ингредиенты.Цена * КалькуляцияБлюд.Кол_во),0) as Сумма,ТипБлюда From Блюда "+
                "Left Join КалькуляцияБлюд on КалькуляцияБлюд.КодБлюда = Блюда.КодБлюда "+
                "Left Join Ингредиенты on Ингредиенты.КодИнг = КалькуляцияБлюд.КодИнгр "+
                "Group by НаимБлюда,ТипБлюда,Блюда.КодБлюда "+
                "Order by Sum(Ингредиенты.Цена* КалькуляцияБлюд.Кол_во) desc";*/
            string sql = "UPDATE Блюда SET Цена = (SELECT ISNULL(SUM(Ингредиенты.Цена * КалькуляцияБлюд.Кол_во),0) " +
                "FROM КалькуляцияБлюд " +
                "LEFT JOIN Ингредиенты ON Ингредиенты.КодИнг = КалькуляцияБлюд.КодИнгр " +
                "WHERE КалькуляцияБлюд.КодБлюда = Блюда.КодБлюда)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            RefreshDishes();
            SelectDishes();
            SelectTypes();
            button5.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                InsertDish(textBox1.Text, int.Parse(textBox3.Text));
                SelectDishes();
                textBox1.Text = null;
                textBox3.Text = null;
            }
            catch
            {
                MessageBox.Show("Не все поля указаны или введены некорректные данные");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                button5.Enabled = true;
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["НаимБлюда"].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells["ТипБлюда"].Value.ToString();
                id = (int)dataGridView1.SelectedRows[0].Cells["КодБлюда"].Value;
                dish = dataGridView1.SelectedRows[0].Cells["НаимБлюда"].Value.ToString();
            }
            catch
            {
                MessageBox.Show("не выбрана строка");
            }
        }

        private void button3_Click(object sender, EventArgs e)
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
                    int id = (int)row.Cells["КодБлюда"].Value;
                    DeleteDish(id);
                }
                SelectDishes();
                textBox1.Text = null;
                textBox3.Text = null;
                while (dataGridView3.Rows.Count > 0)
                {
                    dataGridView3.Rows.RemoveAt(0);
                }
            }
            catch
            {
                MessageBox.Show("Не выбрано Блюдо(а)");
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 1 && dataGridView1.SelectedRows.Count == 0) MessageBox.Show("Выберете 1 строку");
                else
                {
                    Form8 form8 = new Form8(connectionString, dataGridView1.SelectedRows[0]);
                    form8.ShowDialog();
                    this.RefreshDishes();
                    this.AllIng(dataGridView1.SelectedRows[0]);
                    this.SelectDishes();
                }
            }
            catch
            {
                MessageBox.Show("Отсутствуют блюда");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateDish(dish,textBox1.Text,int.Parse(textBox3.Text),id);
                SelectDishes();
                textBox1.Text = null;
                textBox3.Text = null;
                button5.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Не все поля указаны или введены некорректные данные");
            }
        }
        public void AllIng(DataGridViewRow row)
        {
            string sql = "Select Ингредиенты.НаимИнг,Кол_во,Ед_Изм.НаимЕдИзм From КалькуляцияБлюд " +
                "Inner Join Ингредиенты on КалькуляцияБлюд.КодИнгр = Ингредиенты.КодИнг " +
                "Inner Join Ед_Изм on Ед_Изм.КодЕдИзм = Ингредиенты.Ед_Изм " +
                "Inner Join Блюда on Блюда.КодБлюда = КалькуляцияБлюд.КодБлюда "+
                "Where КалькуляцияБлюд.КодБлюда = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", row.Cells["КодБлюда"].Value);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView3.DataSource = dataTable;
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 1) MessageBox.Show("Нужно выбрать 1 блюдо");
                else
                {
                    AllIng(dataGridView1.SelectedRows[0]);
                }
            }
            catch
            {

            }
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 1) MessageBox.Show("Нужно выбрать 1 тип");
            else
            {
                textBox3.Text = dataGridView2.Rows[e.RowIndex].Cells["КодТипаБлюда"].Value.ToString();
            }
        }
    }
}
