using Lab2ORM.Classes;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2ORM.Forms
{
    public partial class Perfomers : Form
    {
        private MusicPlayerContext db;
        private int perfomerid;
        private string perfomername;
        public void Refresh()
        {
            button2.Enabled = false;
            textBox1.Text = "";
            var users = db.Исполнителиs.ToList();
            dataGridView1.DataSource = users;
            dataGridView1.Columns["КодИсполнителя"].Visible = false;
            dataGridView1.Columns["Песниs"].Visible = false;
        }
        public Perfomers(MusicPlayerContext db)
        {
            InitializeComponent();
            button2.Enabled = false;
            this.db = db;
            Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var Perfomer = db.Исполнителиs.Any(u => u.Исполнитель == textBox1.Text);
                if (Perfomer && textBox1.Text != perfomername)
                {
                    MessageBox.Show("Такой исполнитель уже есть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
                if (textBox1.Text != perfomername)
                {
                    var perfomer = db.Исполнителиs.Where(p => p.КодИсполнителя == perfomerid).First();
                    perfomer.Исполнитель = textBox1.Text;
                    db.SaveChanges();
                }
                Refresh();
            }
            catch
            {
                MessageBox.Show("Введите Имя исполнителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == "") throw new Exception();
                var Perfomer = db.Исполнителиs.Any(u => u.Исполнитель == textBox1.Text);
                if (Perfomer)
                {
                    MessageBox.Show("Плейлист уже существует");
                    return;
                }
                db.Исполнителиs.Add(new Classes.Исполнители
                {
                    Исполнитель = textBox1.Text
                });
                db.SaveChanges();
                Refresh();
            }
            catch
            {
                MessageBox.Show("Введите имя исполнителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        public void Update()
        {
            if (dataGridView1.SelectedRows.Count > 1) MessageBox.Show("Нужно выбрать одну строку");
            else
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["Исполнитель"].Value.ToString();
                perfomername = dataGridView1.SelectedRows[0].Cells["Исполнитель"].Value.ToString();
                perfomerid = (int)dataGridView1.SelectedRows[0].Cells["КодИсполнителя"].Value;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0) throw new Exception();
                var selectedRows = dataGridView1.SelectedRows;
                var perfomersToRemove = new List<Исполнители>();
                foreach (DataGridViewRow row in selectedRows)
                {
                    var performerid = (int)row.Cells["КодИсполнителя"].Value;
                    var perfomer = db.Исполнителиs.Where(p => p.КодИсполнителя == performerid).First();
                    perfomersToRemove.Add(perfomer);
                }
                db.Исполнителиs.RemoveRange(perfomersToRemove);
                db.SaveChanges();
                Refresh();
            }
            catch
            {
                MessageBox.Show("Выберите имя Исполнителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Update();

        }
    }
}
