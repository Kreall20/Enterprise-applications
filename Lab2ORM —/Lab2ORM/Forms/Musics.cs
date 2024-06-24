using Lab2ORM.Forms;
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
    public partial class Musics : Form
    {
        private MusicPlayerContext db;
        private int userid;
        private int playlistid = 0;
        private int musicid = 0;
        public Musics(MusicPlayerContext db, int userid)
        {
            InitializeComponent();
            this.db = db;
            this.userid = userid;
            Refrefsh();
        }
        public void Refrefsh()
        {
            var genres = db.Жанрыs.ToList();
            comboBox1.DataSource = genres;
            comboBox1.DisplayMember = "Жанр";
            var perfomers = db.Исполнителиs.ToList();
            comboBox2.DataSource = perfomers;
            comboBox2.DisplayMember = "Исполнитель";
            var playlistsofUser = (from playlist in db.Плейлистыs
                                   where playlist.UserId == userid
                                   select new
                                   {
                                       КодПлейлиста = playlist.КодПлейлиста,
                                       Плейлист = playlist.Плейлист
                                   }).ToList();
            dataGridView1.DataSource = playlistsofUser;
            dataGridView1.Columns["КодПлейлиста"].Visible = false;
            var songs = (from song in db.Песниs
                         join perfomer in db.Исполнителиs on song.Автор equals perfomer.КодИсполнителя
                         join genre in db.Жанрыs on song.Жанр equals genre.КодЖанра
                         select new
                         {
                             НазваниеПесни = song.НазваниеПесни,
                             Автор = genre.Жанр,
                             Исполнитель = perfomer.Исполнитель,
                             КодПесни = song.КодПесни
                         }).ToList();
            dataGridView3.DataSource = songs;
            dataGridView3.Columns["КодПесни"].Visible = false;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                playlistid = (int)dataGridView1.SelectedRows[0].Cells["КодПлейлиста"].Value;
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
                dataGridView2.DataSource = musicofplaylist;
            }
            else
            {
                MessageBox.Show("Выберите 1 плейлист");
            }
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            if(playlistid == 0)
            {
                MessageBox.Show("Выберите плейлист");
            }
            if (musicid == 0)
            {
                MessageBox.Show("Выберите музыку");
            }
            else {
                db.ПесниПлейлистовs.Add(new Classes.ПесниПлейлистов
                {
                    КодПесни = musicid,
                    КодПлейлиста = playlistid
                });
                db.SaveChanges();
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
                dataGridView2.DataSource = musicofplaylist;
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView3.SelectedRows.Count == 1)
            {
                musicid = (int)dataGridView3.SelectedRows[0].Cells["КодПесни"].Value;
            }
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
            var songs = (from song in db.Песниs
                         join perfomer in db.Исполнителиs on song.Автор equals perfomer.КодИсполнителя
                         join genre in db.Жанрыs on song.Жанр equals genre.КодЖанра
                         select new
                         {
                             НазваниеПесни = song.НазваниеПесни,
                             Автор = genre.Жанр,
                             Исполнитель = perfomer.Исполнитель,
                             КодПесни = song.КодПесни
                         }).ToList();
            dataGridView3.DataSource = songs;
            dataGridView3.Columns["КодПесни"].Visible = false;
        }
    }
}
