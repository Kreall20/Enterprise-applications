using Lab2ORM.Classes;
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
        private int playlistid;
        private string playlistname;
        private int music = -1;
        private List<ПесниПлейлистов> songstoRemove = new List<ПесниПлейлистов>();
        public PlayLists(MusicPlayerContext db,int userId)
        {
            InitializeComponent();
            button2.Enabled = false;
            this.db = db;
            UserId = userId;
            Refresh();
        }
        public void Refresh()
        {
            button2.Enabled = false;
            textBox1.Text = "";
            var playlistofuser = (from playlist in db.Плейлистыs
                                  where playlist.UserId == UserId
                                  select new
                                  {
                                      Плейлист = playlist.Плейлист,
                                      КодПлейлиста = playlist.КодПлейлиста,
                                      UserID = playlist.UserId
                                  }).ToList();
            dataGridView1.DataSource = playlistofuser;
            dataGridView1.Columns["КодПлейлиста"].Visible = false;
            dataGridView1.Columns["UserID"].Visible = false;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == "") throw new Exception();
                var Playlist = db.Плейлистыs.Any(u => u.Плейлист == textBox1.Text && u.UserId == UserId);
                if (Playlist)
                {
                    MessageBox.Show("Плейлист уже существует");
                    return;
                }
                db.Плейлистыs.Add(new Classes.Плейлисты
                {
                    UserId = UserId,
                    Плейлист = textBox1.Text.Trim()
                }); ;
                db.SaveChanges();
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
                if(music != -1)
                {
                    db.ПесниПлейлистовs.RemoveRange(songstoRemove);
                    db.SaveChanges();
                    Refresh();
                    return;
                }
                if (dataGridView1.SelectedRows.Count == 0) throw new Exception();
                var selectedRows = dataGridView1.SelectedRows;
                var playlistsToRemove = new List<Плейлисты>();
                foreach (DataGridViewRow row in selectedRows)
                {
                    var playlistid = (int)row.Cells["КодПлейлиста"].Value;
                    var playlist = db.Плейлистыs.Where(p => p.КодПлейлиста == playlistid).First();
                    playlistsToRemove.Add(playlist);
                }
                db.Плейлистыs.RemoveRange(playlistsToRemove);
                db.SaveChanges();
                Refresh();
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
                var Playlist = db.Плейлистыs.Any(u => u.Плейлист == textBox1.Text && u.UserId == UserId);
                if (Playlist && textBox1.Text != playlistname)
                {
                    MessageBox.Show("Такой плейлист уже есть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
                if (textBox1.Text != playlistname)
                {
                    var playlist = db.Плейлистыs.Where(p => p.UserId == UserId && p.КодПлейлиста == playlistid).First();
                    playlist.Плейлист = textBox1.Text;
                    db.SaveChanges();
                }
                Refresh();
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
            if (dataGridView1.SelectedRows.Count > 1) MessageBox.Show("Нужно выбрать одну строку");
            else
            {
                playlistid = (int)dataGridView1.SelectedRows[0].Cells["КодПлейлиста"].Value;
                var musicofplaylist = (from playlist in db.ПесниПлейлистовs
                                       join music in db.Песниs on playlist.КодПесни equals music.КодПесни
                                       where playlist.КодПлейлиста == playlistid
                                       select new
                                       {
                                           НазваниеПесни = music.НазваниеПесни,
                                           Путь = music.Путь,
                                           КодПесни = music.КодПесни
                                       }).ToList();
                dataGridView2.DataSource = musicofplaylist;
                dataGridView2.Columns["КодПесни"].Visible = false;
                dataGridView2.Columns["Путь"].Visible = false;
                music = -1;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            music = (int)dataGridView2.SelectedRows[0].Cells["КодПесни"].Value;
            var selectedRows = dataGridView2.SelectedRows;
            songstoRemove = new List<ПесниПлейлистов>();
            foreach (DataGridViewRow row in selectedRows)
            {
                var songid = (int)row.Cells["КодПесни"].Value;
                var song = db.ПесниПлейлистовs.Where(p => p.КодПесни == songid).First();
                songstoRemove.Add(song);
            }
        }
    }
}
