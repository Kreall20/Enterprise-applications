using Lab2ORM.Classes;
using Lab2ORM.Forms;
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

namespace Lab2ORM
{
    public partial class AddMusic : Form
    {
        private MusicPlayerContext db;
        private int musicid;
        private string musicname;
        private int Userid;
        private AddMusicPresenter presenter;
        int flag;
        public AddMusic(MusicPlayerContext db,int user,int flag)
        {
            InitializeComponent();
            button2.Enabled = false;
            this.db = db;
            Userid = user;
            this.flag = flag;
            presenter = new AddMusicPresenter(db, this);
            Refresh();
        }
        public void Refresh()
        {
            textBox1.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            button2.Enabled = false;
            presenter.Allinf(dataGridView1, comboBox1, comboBox2);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                presenter.AddMusic(textBox1.Text, comboBox1.Text, comboBox2.Text,Userid);
            }
            catch
            {
                MessageBox.Show("Введите все данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        public void Update()
        {
            if (dataGridView1.SelectedRows.Count > 1) MessageBox.Show("Нужно выбрать одну строку");
            else
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["Песня"].Value.ToString();
                musicname = dataGridView1.SelectedRows[0].Cells["Песня"].Value.ToString();
                musicid = (int)dataGridView1.SelectedRows[0].Cells["КодПесни"].Value;
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            Update();
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                presenter.UpdateSong(musicname, textBox1.Text, musicid, comboBox1.Text, comboBox2.Text);
            }
            catch
            {
                MessageBox.Show("Введите Все данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0) throw new Exception();
                presenter.DeleteSongs(dataGridView1);
            }
            catch
            {
                MessageBox.Show("Выберите музыку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}
