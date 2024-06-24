using Lab2ORM.Classes;
using Lab2ORM.Presenters;
using Microsoft.EntityFrameworkCore;
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
    public partial class PlayLists : Form
    {
        private MusicPlayerContext db;
        private int UserId;
        private int playlistid = -1;
        private string playlistname;
        public int music = -1;
        private List<ПесниПлейлистов> songstoRemove = new List<ПесниПлейлистов>();
        private PlaylistsPresenter presenter;
        public PlayLists(MusicPlayerContext db,int userId)
        {
            InitializeComponent();
            button2.Enabled = false;
            this.db = db;
            presenter = new PlaylistsPresenter(db, this);
            UserId = userId;
            Refresh();
        }
        public void Refresh()
        {
            button2.Enabled = false;
            textBox1.Text = "";
            presenter.PlaylistsofUser(UserId,dataGridView1);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == "") throw new Exception();
                presenter.AddPlaylist(textBox1.Text, UserId);
                Refresh();
            }
            catch
            {
                MessageBox.Show("Введите название плейлиста", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        public void Update()
        {
            if (dataGridView1.SelectedRows.Count > 1) MessageBox.Show("Нужно выбрать одну строку");
            else
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["Плейлист"].Value.ToString();
                playlistname = dataGridView1.SelectedRows[0].Cells["Плейлист"].Value.ToString();
                playlistid = (int)dataGridView1.SelectedRows[0].Cells["КодПлейлиста"].Value;
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0) throw new Exception();
                presenter.RemovePlaylist(dataGridView1);
            }
            catch
            {
                MessageBox.Show("Выберите плейлист", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if(playlistid == -1)
                {
                    MessageBox.Show("Выберите плейлист", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
                presenter.UpdatePlaylist(playlistname, UserId, textBox1.Text, playlistid);
            }
             catch
            {
                MessageBox.Show("Введите название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           Update();
           button2.Enabled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                presenter.SongsofPlaylist(dataGridView1,dataGridView2);
            }
            catch
            {

            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*try
            {
                music = (int)dataGridView2.SelectedRows[0].Cells["КодПесни"].Value;
                var pl = (int)dataGridView1.SelectedRows[0].Cells["КодПлейлиста"].Value;
                var selectedRows = dataGridView2.SelectedRows;
                songstoRemove = new List<ПесниПлейлистов>();
                foreach (DataGridViewRow row in selectedRows)
                {
                    var songid = (int)row.Cells["КодПесни"].Value;
                    var song = db.ПесниПлейлистовs.Where(p => p.КодПесни == songid && p.КодПлейлиста == pl).First();
                    songstoRemove.Add(song);
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
                if (dataGridView2.SelectedRows.Count == 0) throw new Exception();
                songstoRemove = new List<ПесниПлейлистов>();
                presenter.DeletesongsinPlaylist(dataGridView2,dataGridView1, songstoRemove);
                return;
            }
            catch
            {

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
