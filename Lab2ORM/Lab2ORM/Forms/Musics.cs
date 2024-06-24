using Guna.UI2.AnimatorNS;
using Lab2ORM.Classes;
using Lab2ORM.Forms;
using Lab2ORM.Presenters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Resources.Extensions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Lab2ORM
{
    public partial class Musics : Form
    {
        private MusicPlayerContext db;
        private int userid;
        private int playlistid = 0;
        private int musicid;
        bool flag = false;
        private MusicPresenter presenter;
        public Musics(MusicPlayerContext db, int userid)
        {
            InitializeComponent();
            this.db = db;
            this.userid = userid;
            presenter = new MusicPresenter(this, db);
            Refrefsh();
        }
        public void Refrefsh()
        {
            presenter.AllInform(comboBox4, comboBox2, comboBox1, comboBox2, dataGridView1, dataGridView3, userid);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                playlistid = (int)dataGridView1.SelectedRows[0].Cells["КодПлейлиста"].Value;
                presenter.SongsofPlaylist(dataGridView2, playlistid);
            }
            else
            {
                MessageBox.Show("Выберите 1 плейлист");
            }
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            presenter.AddSongs(playlistid, ref musicid, flag, dataGridView3, dataGridView2, userid);
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*try
            {
                if (dataGridView3.SelectedRows.Count != 0)
                {
                    var selectedRows = dataGridView2.SelectedRows;
                    foreach (DataGridViewRow row in selectedRows)
                    {
                        if (!flag)
                        {
                            musicid.Add((int)dataGridView3.SelectedRows[0].Cells["КодПесни"].Value);
                        }
                        else
                        {
                            var song = dataGridView3.SelectedRows[0].Cells["SongName"].Value.ToString();
                            var music = db.Песниs.Where(p => p.НазваниеПесни == song).First();
                            musicid.Add(music.КодПесни);
                        }
                    }
                }
            }
            catch
            {

            }*/
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch
            {
                MessageBox.Show("Введите условие поиска", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            flag = false;
            presenter.Allsongs(dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                flag = true;
                string genre = "%" + comboBox4.Text + "%";
                string playlist = "%" + comboBox3.Text + "%";
                string album = "%" + comboBox2.Text + "%";
                string performer = "%" + comboBox1.Text + "%";
                var user = db.Пользователиs.Where(p => p.ТипПользователя == 1).First().Login;
                string songname = "%" + textBox1.Text + "%";

                presenter.Search(user, playlist, album, performer, genre, songname, dataGridView3);
            }
            catch
            {

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                flag = true;
                string genre = "%" + comboBox4.Text + "%";
                string playlist = "%" + comboBox3.Text + "%";
                string album = "%" + comboBox2.Text + "%";
                string performer = "%" + comboBox1.Text + "%";
                var user = db.Пользователиs.Where(p => p.ТипПользователя == 1).First().Login;
                string songname = "%" + textBox1.Text + "%";
                presenter.Search(user, playlist, album, performer, genre, songname, dataGridView3);
            }
            catch
            {

            }
        }
    }
}
