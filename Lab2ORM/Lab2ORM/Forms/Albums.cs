using Guna.UI2.WinForms.Enums;
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
    public partial class Albums : Form
    {
        private MusicPlayerContext db;
        private int albumid;
        private string albumname;
        private int Userid;
        private AlbumPresenter presenter;
        public void Refresh()
        {
            textBox1.Text = "";
            comboBox1.Text = "";
            presenter.Allinf(comboBox1, dataGridView1, dataGridView3);
        }
        public Albums(MusicPlayerContext db,int User)
        {
            InitializeComponent();
            button2.Enabled = false;
            this.db = db;
            Userid = User;
            presenter = new AlbumPresenter(this, db);
            Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                presenter.UpdateAlbum(albumname, textBox1.Text,albumid, comboBox1.Text);
            }
            catch
            {
                MessageBox.Show("Введите название Альбома", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                presenter.AddAlbum(textBox1.Text, comboBox1.Text);
            }
            catch
            {
                MessageBox.Show("Введите Альбом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        public void Update()
        {
            if (dataGridView1.SelectedRows.Count > 1) MessageBox.Show("Нужно выбрать одну строку");
            else
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["НазваниеАльбома"].Value.ToString();
                int perfomer = (int)dataGridView1.SelectedRows[0].Cells["Автор"].Value;
                var autor = db.Исполнителиs.Where(p => p.КодИсполнителя == perfomer).First();
                comboBox1.Text = autor.Исполнитель;
                albumname = dataGridView1.SelectedRows[0].Cells["НазваниеАльбома"].Value.ToString();
                albumid = (int)dataGridView1.SelectedRows[0].Cells["КодАльбома"].Value;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0) throw new Exception();
                presenter.DeleteAlbum(dataGridView1);
            }
            catch
            {
                MessageBox.Show("Выберите Альбом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Update();
            button2.Enabled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 1) throw new Exception();
                presenter.AlbumSongs(dataGridView1, dataGridView2);
            }
            catch
            {
                MessageBox.Show("Выберите Альбом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 1 || dataGridView1.SelectedRows.Count == 0 || dataGridView3.SelectedRows.Count == 0) throw new Exception();
                presenter.AddSongsinAlbum(dataGridView1, dataGridView3, Userid);
            }
            catch
            {
                MessageBox.Show("Не выбраны нужные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                presenter.DeletesongsinAlbum(dataGridView1, dataGridView2);
            }
            catch
            {
                MessageBox.Show("Не выбраны нужные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}
