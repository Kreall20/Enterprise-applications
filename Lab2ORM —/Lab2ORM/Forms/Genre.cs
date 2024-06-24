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
    public partial class Genre : Form
    {
        private MusicPlayerContext db;
        private int genreid;
        private string genrename;
        public void Refresh()
        {
            button2.Enabled = false;
            textBox1.Text = "";
            var genres = db.Жанрыs.ToList();
            dataGridView1.DataSource = genres;
            dataGridView1.Columns["КодЖанра"].Visible = false;
            dataGridView1.Columns["Песниs"].Visible = false;
        }
        public Genre(MusicPlayerContext db)
        {
            InitializeComponent();
            textBox1.Text = "";
            button2.Enabled = false;
            this.db = db;
            Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var Genre = db.Жанрыs.Any(u => u.Жанр == textBox1.Text);
                if (Genre && textBox1.Text != genrename)
                {
                    MessageBox.Show("Такой Жанр уже есть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
                if (textBox1.Text != genrename)
                {
                    var genre = db.Жанрыs.Where(p => p.КодЖанра == genreid).First();
                    genre.Жанр = textBox1.Text;
                    db.SaveChanges();
                }
                Refresh();
            }
            catch
            {
                MessageBox.Show("Введите Жанр", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == "") throw new Exception();
                var Genre = db.Жанрыs.Any(u => u.Жанр == textBox1.Text);
                if (Genre)
                {
                    MessageBox.Show("Плейлист уже существует");
                    return;
                }
                db.Жанрыs.Add(new Classes.Жанры
                {
                    Жанр = textBox1.Text
                });
                db.SaveChanges();
                Refresh();
            }
            catch
            {
                MessageBox.Show("Введите Жанр", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0) throw new Exception();
                var selectedRows = dataGridView1.SelectedRows;
                var genresToRemove = new List<Жанры>();
                foreach (DataGridViewRow row in selectedRows)
                {
                    var genreid = (int)row.Cells["КодЖанра"].Value;
                    var genre = db.Жанрыs.Where(p => p.КодЖанра == genreid).First();
                    genresToRemove.Add(genre);
                }
                db.Жанрыs.RemoveRange(genresToRemove);
                db.SaveChanges();
                Refresh();
            }
            catch
            {
                MessageBox.Show("Выберите Жанр", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        public void Update()
        {
            if (dataGridView1.SelectedRows.Count > 1) MessageBox.Show("Нужно выбрать одну строку");
            else
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["Жанр"].Value.ToString();
                genrename = dataGridView1.SelectedRows[0].Cells["Жанр"].Value.ToString();
                genreid = (int)dataGridView1.SelectedRows[0].Cells["КодЖанра"].Value;
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            Update();
            button2.Enabled = true;
        }
    }
}
