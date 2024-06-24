using Lab2ORM.Classes;
using Lab2ORM.Presenters;
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
        private GenrePresenter presenter;
        public void Refresh()
        {
            button2.Enabled = false;
            textBox1.Text = "";
            presenter.AllGenres(dataGridView1);
        }
        public Genre(MusicPlayerContext db)
        {
            InitializeComponent();
            textBox1.Text = "";
            button2.Enabled = false;
            this.db = db;
            presenter = new GenrePresenter(this, db);
            Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == "") throw new Exception();
                presenter.UpdateGenre(genrename, textBox1.Text, genreid);
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
                presenter.AddGenre(textBox1.Text);
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
                presenter.DeleteGenres(dataGridView1);
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
