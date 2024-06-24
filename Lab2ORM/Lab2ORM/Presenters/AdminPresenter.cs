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
    public class AdminPresenter
    {
        private MusicPlayerContext db;
        private AdminPanel view;
        public AdminPresenter(AdminPanel view, MusicPlayerContext db)
        {
            this.view = view;
            this.db = db;
        }
        public void Allinf(DataGridView dataGridView2, DataGridView dataGridView3,
            ComboBox comboBox1, ComboBox comboBox2, ComboBox comboBox3, ComboBox comboBox4, ComboBox comboBox5,int UserId)
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
            albums.Insert(0, new Альбомы { НазваниеАльбома = "" });
            comboBox2.DataSource = albums;
            var playlists = db.Плейлистыs.Where(p => p.UserId == UserId).ToList();
            playlists.Insert(0, new Плейлисты { Плейлист = "" });
            comboBox3.DataSource = playlists;
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
        public void PlaylistsofUser(DataGridView dataGridView1,ref int user,int userid)
        {
            var playlistsofUser = db.Плейлистыs.Where(p => p.UserId == userid).ToList();
            dataGridView1.DataSource = playlistsofUser;
            dataGridView1.Columns["КодПлейлиста"].Visible = false;
            dataGridView1.Columns["UserId"].Visible = false;
            dataGridView1.Columns["User"].Visible = false;
            dataGridView1.Columns["ПесниПлейлистовs"].Visible = false;
            user = userid;
        }
        public void SongsOfPlaylist(DataGridView dataGridView3,ref int playlist,int playlistid)
        {
            var musicofplaylist = (from playlists in db.ПесниПлейлистовs
                                   join song in db.Песниs on playlists.КодПесни equals song.КодПесни
                                   join perfomer in db.Исполнителиs on song.Автор equals perfomer.КодИсполнителя
                                   join genre in db.Жанрыs on song.Жанр equals genre.КодЖанра
                                   where playlists.КодПлейлиста == playlistid
                                   select new
                                   {
                                       НазваниеПесни = song.НазваниеПесни,
                                       Автор = genre.Жанр,
                                       Исполнитель = perfomer.Исполнитель,
                                       КодПесни = song.КодПесни,
                                       Жанр = genre.Жанр
                                   }).ToList();
            dataGridView3.DataSource = musicofplaylist;
            dataGridView3.Columns["КодПесни"].Visible = false;
            playlist = playlistid;
        }
        public void Musicstart(ref int mucisdel,bool flag,DataGridView dataGridView3)
        {
            string music;
            var song = new Песни();
            if (flag)
            {
                var song1 = dataGridView3.SelectedRows[0].Cells["SongName"].Value.ToString();
                var music1 = db.Песниs.Where(p => p.НазваниеПесни == song1).First();
                music = music1.НазваниеПесни;
                song = music1;
            }
            else
            {
                music = dataGridView3.SelectedRows[0].Cells["Песня"].Value.ToString();
                var music1 = db.Песниs.Where(p => p.НазваниеПесни == music).First();
                music = music1.НазваниеПесни;
                song = music1;
            }

            //var name = dataGridView3.SelectedRows[0].Cells["Песня"].Value.ToString();
            var songid = db.Песниs.Where(p => p.НазваниеПесни == song.НазваниеПесни).First();
            mucisdel = songid.КодПесни;
        }
        public Песни MusicDoubleclick(bool flag,DataGridView dataGridView3)
        {
            string music;
            var song = new Песни();
            if (flag)
            {
                var song1 = dataGridView3.SelectedRows[0].Cells["SongName"].Value.ToString();
                var music1 = db.Песниs.Where(p => p.НазваниеПесни == song1).First();
                music = music1.НазваниеПесни;
                song = music1;
            }
            else
            {
                music = dataGridView3.SelectedRows[0].Cells["Песня"].Value.ToString();
                var music1 = db.Песниs.Where(p => p.НазваниеПесни == music).First();
                music = music1.НазваниеПесни;
                song = music1;
            }
            return song;
            //var song = db.Песниs.Where(p => p.НазваниеПесни == dataGridView3.SelectedRows[0].Cells["SongName"].Value).First();
        }
        public void Search(string user, string playlist, string album, string performer, string genre, string songname, DataGridView dataGridView3)
        {
            var songs = db.ПесниПользователяs.FromSqlRaw("SELECT НазваниеПесни,id,КодМузыки,UserId,ПутьПесни,ПесниПользователя.Жанр,Альбом,Плейлист,ПесниПользователя.Исполнитель,UserName from ПесниПользователя " +
                    "Inner Join Песни on Песни.КодПесни = ПесниПользователя.КодМузыки " +
                "WHERE ПесниПользователя.UserName like @user and ПесниПользователя.Плейлист like @playlist and ПесниПользователя.Исполнитель like @performer and ПесниПользователя.Жанр like @genre " +
            "and ПесниПользователя.Альбом like @album and Песни.НазваниеПесни like @song", new SqlParameter("@user", user), new SqlParameter("@playlist", playlist), new SqlParameter("@performer", performer), new SqlParameter("@genre", genre), new SqlParameter("@album", album), new SqlParameter("@song", songname)).ToList();
            songs = songs.GroupBy(s => s.КодМузыки)
            .Select(g => g.First())
                   .ToList();
            var list = new List<Song>();
            bool plisem = false;
            bool alisem = false;
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
        public void Delete(int userf,int playlistf,int musicf,DataGridView dataGridView1, DataGridView dataGridView2, DataGridView dataGridView3)
        {
            if (userf == 0 && playlistf == 0 && musicf == 0) throw new Exception();
            if (playlistf == 0 && musicf == 0)
            {
                var selectedRows = dataGridView2.SelectedRows;
                var Userstodelete = new List<Пользователи>();
                foreach (DataGridViewRow row in selectedRows)
                {
                    // var user = (Пользователи)row.DataBoundItem;
                    var user = db.Пользователиs.Where(p => p.КодПользователя == (int)row.Cells["КодПользователя"].Value).First();
                    Userstodelete.Add(user);
                }
                db.Пользователиs.RemoveRange(Userstodelete);
                db.SaveChanges();
                view.Refresh();
            }
            else if (playlistf != 0 && musicf == 0)
            {
                var selectedRows = dataGridView1.SelectedRows;
                var playlists = new List<Плейлисты>();
                foreach (DataGridViewRow row in selectedRows)
                {
                    //var playlist = (Плейлисты)row.DataBoundItem;
                    var pl = db.Плейлистыs.Where(p => p.КодПлейлиста == playlistf && p.UserId == userf).First();
                    playlists.Add(pl);
                    var playlistname = row.Cells["Плейлист"].Value.ToString();
                    var playliststodel = db.ПесниПользователяs.Where(p => p.Плейлист == playlistname && p.UserId == userf).ToList();
                    db.ПесниПользователяs.RemoveRange(playliststodel);
                    db.SaveChanges();
                }
                db.Плейлистыs.RemoveRange(playlists);
                db.SaveChanges();
                view.Refresh();
            }
            else
            {
                var selectedRows = dataGridView3.SelectedRows;
                var musicofplaylist = new List<ПесниПлейлистов>();
                foreach (DataGridViewRow row in selectedRows)
                {
                    var playlistname = dataGridView1.SelectedRows[0].Cells["Плейлист"].Value.ToString();
                    var playlistid = (int)dataGridView1.SelectedRows[0].Cells["КодПлейлиста"].Value;
                    var song = (int)row.Cells["КодПесни"].Value;
                    var music = db.ПесниПлейлистовs.Where(p => p.КодПесни == song && p.КодПлейлиста == playlistid).First();
                    musicofplaylist.Add(music);
                    var songstodel = db.ПесниПользователяs.Where(p => p.Плейлист == playlistname && p.UserId == userf && p.КодМузыки == song).ToList();
                    db.ПесниПользователяs.RemoveRange(songstodel);
                    db.SaveChanges();
                }
                db.ПесниПлейлистовs.RemoveRange(musicofplaylist);
                db.SaveChanges();
                view.Refresh();
            }
        }
    }
}
