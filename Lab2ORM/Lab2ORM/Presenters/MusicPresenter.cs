using Guna.UI2.AnimatorNS;
using Lab2ORM.Classes;
using Lab2ORM.Forms;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Lab2ORM.Presenters
{
    public class MusicPresenter
    {
        private Musics view;
        private MusicPlayerContext db;
        public MusicPresenter(Musics view, MusicPlayerContext db)
        {
            this.view = view;
            this.db = db;
        }
        public void AllInform(ComboBox comboBox4, ComboBox comboBox2, ComboBox comboBox1, ComboBox comboBox3,DataGridView dataGridView1, DataGridView dataGridView3,int userid)
        {
            var genres = db.Жанрыs.ToList();
            genres.Insert(0, new Жанры { Жанр = "" });
            comboBox4.DataSource = genres;
            comboBox4.DisplayMember = "Жанр";
            var albums = db.Альбомыs.ToList();
            albums.Insert(0, new Альбомы { НазваниеАльбома = "" }); ;
            comboBox2.DataSource = albums;
            comboBox2.DisplayMember = "НазваниеАльбома";
            var perfomers = db.Исполнителиs.ToList();
            perfomers.Insert(0, new Исполнители { Исполнитель = "" });
            comboBox1.DataSource = perfomers;
            comboBox1.DisplayMember = "Исполнитель";
            var playlists = db.Плейлистыs.Where(p => p.UserId == userid).ToList();
            playlists.Insert(0, new Плейлисты { Плейлист = "" });
            comboBox3.DataSource = playlists;
            comboBox3.DisplayMember = "Плейлист";
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
        public void SongsofPlaylist(DataGridView gridView,int playlistid)
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
                                       КодПесни = song.КодПесни
                                   }).ToList();
            gridView.DataSource = musicofplaylist;
            gridView.Columns["КодПесни"].Visible = false;
        }
        public void Allsongs(DataGridView gridView)
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
            gridView.DataSource = songs;
            gridView.Columns["КодПесни"].Visible = false;
        }
        public void Search(string user,string playlist,string album,string performer,string genre,string songname,DataGridView dataGridView3)
        {
            var songs = db.ПесниПользователяs.FromSqlRaw("SELECT НазваниеПесни,id,КодМузыки,UserId,ПутьПесни,ПесниПользователя.Жанр,Альбом,Плейлист,ПесниПользователя.Исполнитель,UserName from ПесниПользователя " +
                    "Inner Join Песни on Песни.КодПесни = ПесниПользователя.КодМузыки " +
            "WHERE ПесниПользователя.UserName like @user and ПесниПользователя.Плейлист like @playlist and ПесниПользователя.Исполнитель like @performer and ПесниПользователя.Жанр like @genre " +
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

            dataGridView3.DataSource = list;
            dataGridView3.Columns["UserName"].Visible = false;
            if (plisem)
                dataGridView3.Columns["Плейлист"].Visible = false;
            if (alisem)
                dataGridView3.Columns["Альбом"].Visible = false;
        }
        public void AddSongs(int playlistid,ref int musicid,bool flag,DataGridView dataGridView3,DataGridView dataGridView2,int userid)
        {
            int music = 0;
            if (playlistid == 0)
            {
                MessageBox.Show("Выберите плейлист");
            }
            else
            {
                foreach (DataGridViewRow item in dataGridView3.SelectedRows)
                {
                    if (!flag)
                    {
                        music = (int)item.Cells["КодПесни"].Value;
                    }
                    else
                    {
                        var song = item.Cells["SongName"].Value.ToString();
                        var music1 = db.Песниs.Where(p => p.НазваниеПесни == song).First();
                        music = music1.КодПесни;
                    }
                    var issonginpl = db.ПесниПлейлистовs.Any(p => p.КодПесни == music && p.КодПлейлиста == playlistid);
                    if (!issonginpl)
                    {
                        db.ПесниПлейлистовs.Add(new Classes.ПесниПлейлистов
                        {
                            КодПесни = music,
                            КодПлейлиста = playlistid
                        });
                        db.SaveChanges();
                        var music2 = db.Песниs.Where(p => p.КодПесни == music).First();
                        var genre = db.Жанрыs.Where(p => p.КодЖанра == music2.Жанр).First();
                        var perf = db.Исполнителиs.Where(p => p.КодИсполнителя == music2.Автор).First();
                        var username = db.Пользователиs.Where(p => p.КодПользователя == userid).First();
                        var playlist = db.Плейлистыs.Where(p => p.КодПлейлиста == playlistid).First().Плейлист;
                        db.ПесниПользователяs.Add(new ПесниПользователя
                        {
                            Жанр = genre.Жанр,
                            Альбом = "",
                            UserId = username.КодПользователя,
                            Исполнитель = perf.Исполнитель,
                            ПутьПесни = music2.Путь,
                            КодМузыки = music2.КодПесни,
                            Плейлист = playlist,
                            UserName = username.Login
                        });
                        db.SaveChanges();
                    }
                }
                var musicofplaylist = (from pl in db.ПесниПлейлистовs
                                       join song in db.Песниs on pl.КодПесни equals song.КодПесни
                                       join perfomer in db.Исполнителиs on song.Автор equals perfomer.КодИсполнителя
                                       join g in db.Жанрыs on song.Жанр equals g.КодЖанра
                                       where pl.КодПлейлиста == playlistid
                                       select new
                                       {
                                           НазваниеПесни = song.НазваниеПесни,
                                           Жанр = g.Жанр,
                                           Исполнитель = perfomer.Исполнитель,
                                           КодПесни = song.КодПесни
                                       }).ToList();
                dataGridView2.DataSource = musicofplaylist;
                dataGridView2.Columns["КодПесни"].Visible = false;
                musicid = music;
            }
        }
    }
}
