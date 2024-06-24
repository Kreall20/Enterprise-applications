using Lab2ORM.Classes;
using Lab2ORM.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace Lab2ORM
{
    public partial class MainForm : Form
    {
        private bool flagofinsertmusic = false;
        private LogIn login;
        private MusicPlayerContext db;
        private int UserId;
        MediaPlayer player = new MediaPlayer();
        public MainForm(LogIn login, MusicPlayerContext db,int userId)
        {
            InitializeComponent();
            this.login = login;
            this.db = db;
            UserId = userId;
            Refresh();
            var perfomers = db.Исполнителиs.ToList();
            comboBox1.DataSource = perfomers;
            var albums = db.Альбомыs.ToList();
            comboBox2.DataSource = albums;
            var playlists = db.Плейлистыs.Where(p => p.UserId == UserId).ToList();
            comboBox3.DataSource = playlists;
            var genres = db.Жанрыs.ToList();
            comboBox4.DataSource = genres;
        }

        public MainForm(MusicPlayerContext db,int userId)
        {
            InitializeComponent();
            login = new LogIn(db);
            this.db = db;
            UserId = userId;
            Refresh();
        }
        public void Refresh()
        {
            var playlistsofUser = (from playlist in db.Плейлистыs
                                   where playlist.UserId == UserId
                                   select new
                                   {
                                       КодПлейлиста = playlist.КодПлейлиста,
                                       Плейлист = playlist.Плейлист
                                   }).ToList();
            dataGridView2.DataSource = playlistsofUser;
            var performers = db.Исполнителиs.ToList();
            var albums = db.Альбомыs.ToList();
            performers.Insert(0, new Исполнители { Исполнитель = "" });
            albums.Insert(0, new Альбомы { НазваниеАльбома = "" });
            playlistsofUser.Insert(0, new  { КодПлейлиста = 10,Плейлист = "" });
            comboBox1.DataSource = performers;
            comboBox2.DataSource = albums;
            comboBox3.DataSource = playlistsofUser;
            var genres = db.Жанрыs.ToList();
            genres.Insert(0, new Жанры { Жанр = "" });
            comboBox4.DataSource = genres;
            comboBox1.DisplayMember = "Исполнитель";
            comboBox2.DisplayMember = "НазваниеАльбома";
            comboBox3.DisplayMember = "Плейлист";
            comboBox4.DisplayMember = "Жанр";
        }
        private void MainForm_Load(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e){}
        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists("user.json"))
                File.Delete("user.json");
            this.Hide();
            login.Show();
            player.Pause();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == ""  && comboBox2.Text == "" && comboBox3.Text == "" && comboBox4.Text == "") throw new Exception();
                
            }
            catch
            {
                MessageBox.Show("Введите условие поиска", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PlayLists playLists = new PlayLists(db,UserId);
            playLists.ShowDialog();
            Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Musics musics = new Musics(db, UserId);
            musics.ShowDialog();
            Refresh();
        }

        private void button7_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            player.Pause();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int musicid = (int)dataGridView1.SelectedRows[0].Cells["КодПесни"].Value;
                var music = db.Песниs.Where(p => p.КодПесни == musicid).First();
                /*string filePath = dataGridView3.SelectedRows[0].Cells["Путь"].Value.ToString();
                label1.Text = dataGridView3.SelectedRows[0].Cells["НазваниеПесни"].Value.ToString();*/
                label1.Text = music.НазваниеПесни;
                if (File.Exists(music.Путь))
                {
                    player.Open(new Uri(music.Путь));
                    player.Play();
                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            player.Play();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 1)
            {
                int playlistid = (int)dataGridView2.SelectedRows[0].Cells["КодПлейлиста"].Value;
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
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            ResetLoginandPassword reset = new ResetLoginandPassword(UserId, db);
            reset.ShowDialog();
        }
    }
}
