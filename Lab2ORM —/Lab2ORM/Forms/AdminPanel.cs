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
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        public AdminPanel(MusicPlayerContext db, int userId)
        {
            InitializeComponent();
            login = new LogIn(db);
            this.db = db;
            UserId = userId;
            Refresh();
        }

        public AdminPanel(LogIn logIn, MusicPlayerContext db,int userId)
        {
            InitializeComponent();
            login = logIn;
            this.db = db;
            UserId = userId;
            Refresh();
        }
        public void Refresh()
        {
            var users = (from user in db.Пользователиs
                         select new
                         {
                             Login = user.Login,
                             КодПользователя = user.КодПользователя
                         }).ToList();
            dataGridView2.DataSource = users;
            dataGridView2.Columns["КодПользователя"].Visible = false;
            var music = (from song in db.Песниs
                          join perfomer in db.Исполнителиs on song.Автор equals perfomer.КодИсполнителя
                          join genre in db.Жанрыs on song.Жанр equals genre.КодЖанра
                          select new
                          {
                              Песня = song.НазваниеПесни,
                              Жанр = genre.Жанр,
                              Автор = perfomer.Исполнитель
                          }).ToList();
            dataGridView3.DataSource = music;
            var performers = db.Исполнителиs.ToList();
            performers.Insert(0, new Исполнители { Исполнитель = "" });
            comboBox1.DataSource = performers;
            var albums = db.Альбомыs.ToList();
            comboBox2.DataSource = albums;
            albums.Insert(0, new Альбомы { НазваниеАльбома=""});
            var playlists = db.Плейлистыs.Where(p => p.UserId == UserId).ToList();
            comboBox3.DataSource = playlists;
            playlists.Insert(0, new Плейлисты { Плейлист = "" });
            var genres = db.Жанрыs.ToList();
            genres.Insert(0, new Жанры { Жанр = "" });
            var allusers = db.Пользователиs.ToList();
            allusers.Insert(0, new Пользователи { Login = "" });
            comboBox4.DataSource = genres;
            comboBox5.DataSource = allusers;
            comboBox4.DataSource = genres;
            comboBox1.DisplayMember = "Исполнитель";
            comboBox2.DisplayMember = "НазваниеАльбома";
            comboBox3.DisplayMember = "Плейлист";
            comboBox4.DisplayMember = "Жанр";
            comboBox5.DisplayMember = "Login";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists("user.json"))
                File.Delete("user.json");
            this.Hide();
            login.Show();
            player.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Albums albums = new Albums(db,UserId);
            albums.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Genre genre = new Genre(db);
            genre.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Perfomers perfomers = new Perfomers(db);
            perfomers.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PlayLists playlists = new PlayLists(db,UserId);
            playlists.ShowDialog();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int userid = (int)dataGridView2.SelectedRows[0].Cells["КодПользователя"].Value;
            var playlistsofUser = (from playlist in db.Плейлистыs
                                   where playlist.UserId == userid
                                   select new
                                   {
                                       КодПлейлиста = playlist.КодПлейлиста,
                                       Плейлист = playlist.Плейлист
                                   }).ToList();
            dataGridView1.DataSource = playlistsofUser;
            dataGridView1.Columns["КодПлейлиста"].Visible = false;
            user = userid;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int playlistid = (int)dataGridView1.SelectedRows[0].Cells["КодПлейлиста"].Value;
            var musicofplaylist = (from playlist in db.ПесниПлейлистовs
                                   join song in db.Песниs on playlist.КодПесни equals song.КодПесни
                                   join perfomer in db.Исполнителиs on song.Автор equals perfomer.КодИсполнителя
                                   join genre in db.Жанрыs on song.Жанр equals genre.КодЖанра
                                   where playlist.КодПлейлиста == playlistid
                                   select new
                                   {
                                       НазваниеПесни = song.НазваниеПесни,
                                       Автор = genre.Жанр,
                                       Исполнитель = perfomer.Исполнитель,
                                       КодПесни = song.КодПесни
                                   }).ToList();
            dataGridView3.DataSource = musicofplaylist;
            dataGridView3.Columns["КодПесни"].Visible = false;
            playlist = playlistid;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            AddMusic addmusic = new AddMusic(db);
            addmusic.ShowDialog();
            Refresh();
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            player.Pause();   
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            player.Play();
        }

        private void dataGridView3_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count == 1)
            {
                int musicid = (int)dataGridView3.SelectedRows[0].Cells["КодПесни"].Value;
                var musicstart = db.Песниs.Where(p => p.КодПесни == musicid).First();
                string filePath = musicstart.Путь;
                label2.Text = musicstart.НазваниеПесни;
                if (File.Exists(filePath))
                {
                    player.Open(new Uri(filePath));
                    player.Play();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                /*string userp = comboBox5.Text == "" ? "%" : '%'+comboBox5.Text+"%";
                string genrep= comboBox4.Text == "" ? "%" : '%' + comboBox4.Text + "%";
                string playlistp = comboBox3.Text == "%" ? "" : '%' + comboBox3.Text + "%";
                string albump = comboBox2.Text == "" ? "%" : '%' + comboBox2.Text + "%";
                string performerp = comboBox1.Text == "%" ? "" : '%' + comboBox1.Text + "%";*/

                /*string user = comboBox5.Text == "" ? "%" : "%" + comboBox5.Text + "%";
                string genre = comboBox4.Text == "" ? "%" : "%" + comboBox4.Text + "%";
                string playlist = comboBox3.Text == "" ? "%" : "%" + comboBox3.Text + "%";
                string album = comboBox2.Text == "" ? "%" : "%" + comboBox2.Text + "%";
                string performer = comboBox1.Text == "" ? "%" : "%" + comboBox1.Text + "%";*/
                string user = comboBox5.Text;
                string genre = comboBox4.Text;
                string playlist = comboBox3.Text;
                string album = comboBox2.Text;
                string performer = comboBox1.Text;
                /*var songs = (from u in db.Пользователиs
                             join pl in db.Плейлистыs on u.КодПользователя equals pl.UserId
                             join songsofpl in db.ПесниПлейлистовs on pl.КодПлейлиста equals songsofpl.КодПлейлиста
                             join music in db.Песниs on songsofpl.КодПесни equals music.КодПесни
                             join perf in db.Исполнителиs on music.Автор equals perf.КодИсполнителя
                             join g in db.Жанрыs on music.Жанр equals g.КодЖанра
                             join songsofalbum in db.ПесниАльбомаs on music.КодПесни equals songsofalbum.КодПесни
                             join a in db.Альбомыs on songsofalbum.КодАльбома equals a.КодАльбома
                             select new
                             {
                                 НазваниеПесни = music.НазваниеПесни,
                                 Автор = perf.Исполнитель
                             }).ToList();
            }*/
                /*"Full JOIN Песни music ON songsofpl.КодПесни = music.КодПесни " +
                    music.НазваниеПесни, music.Автор,music.Путь,music.КодПесни, music.Жанр FROM Пользователи u
                "Full JOIN Плейлисты pl ON u.КодПользователя = pl.UserId " +*/

                /*var songs = db.Песниs.FromSqlRaw("SELECT * From Песни music " +
                "Full JOIN ПесниПлейлистов songsofpl ON pl.КодПлейлиста = songsofpl.КодПлейлиста " +
                "Full JOIN Пользователи u ON Плейлисты.UserId = u.КодПользователя " +
                "Full JOIN Исполнители perf ON music.Автор = perf.КодИсполнителя " +
                "Full JOIN Жанры g ON music.Жанр = g.КодЖанра " +
                "Full JOIN ПесниАльбома songsofalbum ON music.КодПесни = songsofalbum.КодПесни " +
                "Full JOIN Альбомы a ON songsofalbum.КодАльбома = a.КодАльбома " +
                "WHERE u.Login like '%{0}%' and pl.Плейлист like '%{1}%' and perf.Исполнитель like '%{2}%' and g.Жанр like '%{3}%' " +
                "and a.НазваниеАльбома like '%{4}%'",user,playlist,performer,genre,album).ToList();
                dataGridView3.DataSource = songs;*/
/*                string connectionString = "Data Source=HOME-PC\\SQLEXPRESS01;Initial Catalog=MusicPlayer;Integrated Security=True;TrustServerCertificate=true";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * From Песни music " +
                        "Full JOIN ПесниПлейлистов songsofpl ON pl.КодПлейлиста = songsofpl.КодПлейлиста " +
                        "Full JOIN Пользователи u ON Плейлисты.UserId = u.КодПользователя " +
                        "Full JOIN Исполнители perf ON music.Автор = perf.КодИсполнителя " +
                        "Full JOIN Жанры g ON music.Жанр = g.КодЖанра " +
                        "Full JOIN ПесниАльбома songsofalbum ON music.КодПесни = songsofalbum.КодПесни " +
                        "Full JOIN Альбомы a ON songsofalbum.КодАльбома = a.КодАльбома " +
                        "WHERE u.Login like '%@user%'  and perf.Исполнитель like '%@performer%' and g.Жанр like '%@genre%' " +
                        "and a.НазваниеАльбома like '%@album%'";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@user",user);
                    command.Parameters.AddWithValue("@performer", performer);
                    command.Parameters.AddWithValue("@genre", genre);
                    command.Parameters.AddWithValue("@album", album);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView3.DataSource = ds.Tables[0];
                }*/
            }

            catch
            {
                MessageBox.Show("Введите условие поиска", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Musics musics = new Musics(db, UserId);
            musics.ShowDialog();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int musicid = (int)dataGridView3.SelectedRows[0].Cells["КодПесни"].Value;
            music = musicid;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (user == 0) throw new Exception();
                if(playlist == 0 && music == 0)
                {
                    var selectedRows = dataGridView2.SelectedRows;
                    var Userstodelete = new List<Пользователи>();
                    foreach (DataGridViewRow row in selectedRows)
                    {
                        var user = (Пользователи)row.DataBoundItem;
                        Userstodelete.Add(user);
                    }
                    db.Пользователиs.RemoveRange(Userstodelete);
                    db.SaveChanges();
                    Refresh();
                }
                else if(playlist !=0 && music == 0)
                {
                    var selectedRows = dataGridView1.SelectedRows;
                    var playlists = new List<Плейлисты>();
                    foreach (DataGridViewRow row in selectedRows)
                    {
                        var playlist = (Плейлисты)row.DataBoundItem;
                        playlists.Add(playlist);
                    }
                    var playlistsofuser = playlists.Where(p => p.UserId == user);
                    db.Плейлистыs.RemoveRange(playlistsofuser);
                    db.SaveChanges();
                    Refresh();
                }
                else
                {
                    var selectedRows = dataGridView3.SelectedRows;
                    var musics = new List<ПесниПлейлистов>();
                    foreach (DataGridViewRow row in selectedRows)
                    {
                        var music = (ПесниПлейлистов)row.DataBoundItem;
                        musics.Add(music);
                    }
                    var musicofplaylist = musics.Where(p => p.КодПлейлиста == playlist);
                    db.ПесниПлейлистовs.RemoveRange(musicofplaylist);
                    db.SaveChanges();
                    Refresh();
                }
                   
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
    }
}
