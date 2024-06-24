using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using Lab2ORM.Classes;
using Lab2ORM.Forms;
using Lab2ORM.Presenters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.ApplicationServices;

namespace Lab2ORM.Forms
{
    public partial class AdminPanel : Form
    {
        private LogIn login;
        private MusicPlayerContext db;
        MediaPlayer player = new MediaPlayer();
        private int UserId;
        private int user = 0;
        private int playlist = 0;
        private int music = 0;
        bool flag = false;
        int mucisdel = 0;
        private AdminPresenter presenter;
        public AdminPanel(MusicPlayerContext db, int userId)
        {
            InitializeComponent();
            login = new LogIn(db);
            this.db = db;
            UserId = userId;
            presenter = new AdminPresenter(this, db);
            Refresh();
        }

        public AdminPanel(LogIn logIn, MusicPlayerContext db,int userId)
        {
            InitializeComponent();
            login = logIn;
            this.db = db;
            UserId = userId;
            presenter = new AdminPresenter(this, db);
            Refresh();
        }
        public void Refresh()
        {
            presenter.Allinf(dataGridView2, dataGridView3, comboBox1, comboBox2,comboBox3, comboBox4, comboBox5, UserId);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists("user.json"))
                File.Delete("user.json");
            this.Hide();
            login.Show();
            player.Close();
            mucisdel = 0;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Albums albums = new Albums(db,UserId);
            albums.ShowDialog();
            Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Genre genre = new Genre(db);
            genre.ShowDialog();
            Refresh();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Perfomers perfomers = new Perfomers(db);
            perfomers.ShowDialog();
            Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PlayLists playlists = new PlayLists(db,UserId);
            playlists.ShowDialog();
            Refresh();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            playlist = 0;
            music = 0;
            int userid = (int)dataGridView2.SelectedRows[0].Cells["КодПользователя"].Value;
            presenter.PlaylistsofUser(dataGridView1, ref user, userid);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                music = 0;
                int playlistid = (int)dataGridView1.SelectedRows[0].Cells["КодПлейлиста"].Value;
                presenter.SongsOfPlaylist(dataGridView3, ref playlist, playlistid);
            }
            catch
            {

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            AddMusic addmusic = new AddMusic(db,UserId, mucisdel);
            addmusic.ShowDialog();
            Refresh();
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            player.Pause();
            mucisdel = 0;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                player.Play();
                presenter.Musicstart(ref mucisdel, flag, dataGridView3);
            }
            catch
            {

            }
        }

        private void dataGridView3_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count == 1)
            {
                var song = presenter.MusicDoubleclick(flag, dataGridView3);
                string filePath = song.Путь;
                label2.Text = song.НазваниеПесни;
                if (File.Exists(filePath))
                {
                    player.Open(new Uri(filePath));
                    player.Play();
                    var songid = db.Песниs.Where(p => p.НазваниеПесни == song.НазваниеПесни).First();
                    mucisdel = songid.КодПесни;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                flag = true;
                string user = "%" + comboBox5.Text + "%";
                string genre = "%" + comboBox4.Text + "%";
                string playlist = "%" + comboBox3.Text + "%";
                string album = "%" + comboBox2.Text + "%";
                string performer = "%" + comboBox1.Text + "%";
                string songname = "%" + textBox1.Text+"%";

                presenter.Search(user, playlist, album, performer, genre, songname, dataGridView3);
            }

            catch
            {
                MessageBox.Show("Введите условие поиска", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            flag = false;
            Refresh();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Musics musics = new Musics(db, UserId);
            musics.ShowDialog();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                playlist = 0;
                int musicid;
                if (flag)
                {
                    var song = dataGridView3.SelectedRows[0].Cells["SongName"].Value.ToString();
                    var music1 = db.Песниs.Where(p => p.НазваниеПесни == song).First();
                    musicid = music1.КодПесни;
                }
                else
                {
                    musicid = (int)dataGridView3.SelectedRows[0].Cells["КодПесни"].Value;
                    music = musicid;
                }
            }
            catch
            {

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                presenter.Delete(user, playlist, music, dataGridView1, dataGridView2, dataGridView3);
                   
            }
            catch
            {
                MessageBox.Show("Выберите что удалить");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ResetLoginandPassword reset = new ResetLoginandPassword(UserId, db);
            reset.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                flag = true;
                string user = "%" + comboBox5.Text + "%";
                string genre = "%" + comboBox4.Text + "%";
                string playlist = "%" + comboBox3.Text + "%";
                string album = "%" + comboBox2.Text + "%";
                string performer = "%" + comboBox1.Text + "%";
                string songname = "%" + textBox1.Text + "%";

                presenter.Search(user, playlist, album, performer, genre, songname, dataGridView3);
               
            }
            catch
            {

            }
        }
    }
}
