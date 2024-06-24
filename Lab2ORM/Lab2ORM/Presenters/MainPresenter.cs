using Guna.UI2.AnimatorNS;
using Lab2ORM.Classes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2ORM.Presenters
{
    public class MainPresenter
    {
        private MusicPlayerContext db;
        private MainForm view;
        public MainPresenter(MainForm view, MusicPlayerContext db)
        {
            this.view = view;
            this.db = db;
        }
        public void SongofPlaylist(DataGridView dataGridView1,int playlistid)
        {
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
                                       КодПесни = song.КодПесни,
                                       Жанр = genre.Жанр
                                   }).ToList();
            dataGridView1.DataSource = musicofplaylist;
            dataGridView1.Columns["КодПесни"].Visible = false;
        }
        public void Allinf(DataGridView dataGridView2,
            ComboBox comboBox1, ComboBox comboBox2, ComboBox comboBox3, ComboBox comboBox4, int UserId)
        {
            var playlistsofUser = (from playlist in db.Плейлистыs
                                   where playlist.UserId == UserId
                                   select new
                                   {
                                       КодПлейлиста = playlist.КодПлейлиста,
                                       Плейлист = playlist.Плейлист
                                   }).ToList();
            dataGridView2.DataSource = playlistsofUser;
            dataGridView2.Columns["КодПлейлиста"].Visible = false;
            var performers = db.Исполнителиs.ToList();
            var albums = db.Альбомыs.ToList();
            performers.Insert(0, new Исполнители { Исполнитель = "" });
            albums.Insert(0, new Альбомы { НазваниеАльбома = "" });
            var playlists = (from playlist in db.Плейлистыs
                             where playlist.UserId == UserId
                             select new
                             {
                                 КодПлейлиста = playlist.КодПлейлиста,
                                 Плейлист = playlist.Плейлист
                             }).ToList();
            playlists.Insert(0, new { КодПлейлиста = 10, Плейлист = "" });
            comboBox1.DataSource = performers;
            comboBox2.DataSource = albums;
            comboBox3.DataSource = playlists;
            var genres = db.Жанрыs.ToList();
            genres.Insert(0, new Жанры { Жанр = "" });
            comboBox4.DataSource = genres;
            comboBox1.DisplayMember = "Исполнитель";
            comboBox2.DisplayMember = "НазваниеАльбома";
            comboBox3.DisplayMember = "Плейлист";
            comboBox4.DisplayMember = "Жанр";
        }
        public void Search(string playlist, string album, string performer, string genre, string songname, int UserId,DataGridView dataGridView1)
        {
            var user = db.Пользователиs.Where(p => p.КодПользователя == UserId).First().Login;
            var songs = db.ПесниПользователяs.FromSqlRaw("SELECT id,UserId,КодМузыки,ПутьПесни,Альбом,Плейлист,UserName,ПесниПользователя.Жанр,ПесниПользователя.Исполнитель,Песни.НазваниеПесни From ПесниПользователя " +
               "Inner Join Песни on Песни.КодПесни = ПесниПользователя.КодМузыки " +
            "WHERE ПесниПользователя.UserName = @user and ПесниПользователя.Плейлист like @playlist and ПесниПользователя.Исполнитель like @performer and ПесниПользователя.Жанр like @genre " +
            "and ПесниПользователя.Альбом like @album and Песни.НазваниеПесни like @song", new SqlParameter("@user", user), new SqlParameter("@playlist", playlist), new SqlParameter("@performer", performer), new SqlParameter("@genre", genre), new SqlParameter("@album", album), new SqlParameter("@song", songname)).ToList();
            var list = new List<Song>();
            bool plisem = false;
            bool alisem = false;
            songs = songs.GroupBy(s => s.КодМузыки)
                   .Select(g => g.First())
                   .ToList();
            foreach (var item in songs)
            {
                var song = db.Песниs.Where(p => p.КодПесни == item.КодМузыки).First();
                if (item.Альбом == "") alisem = true;
                if (item.Плейлист == "") plisem = true;
                list.Add(new Song
                {
                    SongName = song.НазваниеПесни,
                    Исполнитель = item.Исполнитель,
                    Жанр = item.Жанр,
                    Альбом = item.Альбом,
                    UserName = item.UserName,
                    ПутьПесни = item.ПутьПесни,
                    Плейлист = item.Плейлист
                });
            }

            dataGridView1.DataSource = list;
            dataGridView1.Columns["UserName"].Visible = false;
            if (plisem)
                dataGridView1.Columns["Плейлист"].Visible = false;
            if (alisem)
                dataGridView1.Columns["Альбом"].Visible = false;
        }
        public Песни SongDobleclick(DataGridView dataGridView1, DataGridView dataGridView2,bool flag)
        {
            int musicid;
            if (flag)
            {
                var song = dataGridView2.SelectedRows[0].Cells["SongName"].Value.ToString();
                var music1 = db.Песниs.Where(p => p.НазваниеПесни == song).First();
                musicid = music1.КодПесни;
            }
            else
                musicid = (int)dataGridView1.SelectedRows[0].Cells["КодПесни"].Value;
            var music = db.Песниs.Where(p => p.КодПесни == musicid).First();
            return music;
        }
    }
}
