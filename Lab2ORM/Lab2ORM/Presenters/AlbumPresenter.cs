using Lab2ORM.Classes;
using Lab2ORM.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2ORM.Presenters
{
    public class AlbumPresenter
    {
        private MusicPlayerContext db;
        private Albums view;
        public AlbumPresenter(Albums view, MusicPlayerContext db)
        {
            this.view = view;
            this.db = db;
        }
        public void Allinf(ComboBox comboBox1,DataGridView dataGridView1, DataGridView dataGridView3)
        {
            var users = db.Исполнителиs.ToList();
            comboBox1.DataSource = users;
            comboBox1.DisplayMember = "Исполнитель";
            var albums = (from album in db.Альбомыs
                          join performer in db.Исполнителиs on album.Автор equals performer.КодИсполнителя
                          select new
                          {
                              КодАльбома = album.КодАльбома,
                              Альбом = album.НазваниеАльбома,
                              Автор = performer.Исполнитель
                          }).ToList();
            dataGridView1.DataSource = albums;
            dataGridView1.Columns["КодАльбома"].Visible = false;
            var music = (from song in db.Песниs
                         join perfomer in db.Исполнителиs on song.Автор equals perfomer.КодИсполнителя
                         join genre in db.Жанрыs on song.Жанр equals genre.КодЖанра
                         select new
                         {
                             Песня = song.НазваниеПесни,
                             Жанр = genre.Жанр,
                             Автор = perfomer.Исполнитель,
                             КодПесни = song.КодПесни
                         }).ToList();
            dataGridView3.DataSource = music;
            dataGridView3.Columns["КодПесни"].Visible = false;
        }
        public void AddAlbum(string name,string performer)
        {
            var autorid = db.Исполнителиs.Where(p => p.Исполнитель == performer).First();
            if (name.Trim() == "" || performer == "") throw new Exception();
            var Album = db.Альбомыs.Any(u => u.НазваниеАльбома == name && u.Автор == autorid.КодИсполнителя);
            if (Album)
            {
                MessageBox.Show("Альбом уже существует");
                return;
            }
            db.Альбомыs.Add(new Classes.Альбомы
            {
                НазваниеАльбома = name,
                Автор = autorid.КодИсполнителя
            });
            db.SaveChanges();
            view.Refresh();
        }
        public void DeleteAlbum(DataGridView dataGridView1)
        {
            var selectedRows = dataGridView1.SelectedRows;
            var albumsToRemove = new List<Альбомы>();
            foreach (DataGridViewRow row in selectedRows)
            {
                var albumid = (int)row.Cells["КодАльбома"].Value;
                var album = db.Альбомыs.Where(p => p.КодАльбома == albumid).First();
                albumsToRemove.Add(album);
                var songs = db.ПесниПользователяs.Where(p => p.Альбом == album.НазваниеАльбома);
                db.ПесниПользователяs.RemoveRange(songs);
                db.SaveChanges();
            }
            db.Альбомыs.RemoveRange(albumsToRemove);
            db.SaveChanges();
            view.Refresh();
        }
        public void UpdateAlbum(string oldname,string newname,int albumid,string performer)
        {
            var autorid = db.Исполнителиs.Where(p => p.Исполнитель == performer).First();
            var Album = db.Альбомыs.Any(u => u.НазваниеАльбома == newname && u.Автор == autorid.КодИсполнителя);
            if (Album && newname != oldname)
            {
                MessageBox.Show("Такой Альбом уже есть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            if (newname != oldname)
            {
                var album = db.Альбомыs.Where(p => p.КодАльбома == albumid && p.Автор == autorid.КодИсполнителя).First();
                var songs = db.ПесниПользователяs.Where(p => p.Альбом == album.НазваниеАльбома).ToList();
                foreach (var item in songs)
                {
                    item.Альбом = newname;
                    db.SaveChanges();
                    item.Исполнитель = autorid.Исполнитель;
                    db.SaveChanges();
                }
                album.НазваниеАльбома = newname;
                album.Автор = autorid.КодИсполнителя;
                db.SaveChanges();
            }
            view.Refresh();
        }
        public void AlbumSongs(DataGridView dataGridView1,DataGridView dataGridView2)
        {
            int albumid = (int)dataGridView1.SelectedRows[0].Cells["КодАльбома"].Value;
            var albumsongs = (from albumsong in db.ПесниАльбомаs
                              join song in db.Песниs on albumsong.КодПесни equals song.КодПесни
                              join album in db.Альбомыs on albumsong.КодАльбома equals album.КодАльбома
                              join performer in db.Исполнителиs on album.Автор equals performer.КодИсполнителя
                              join genre in db.Жанрыs on song.Жанр equals genre.КодЖанра
                              where album.КодАльбома == albumid
                              select new
                              {
                                  КодАльбома = albumid,
                                  Песня = song.НазваниеПесни,
                                  Исполнитель = performer.Исполнитель,
                                  Жанр = genre.Жанр,
                                  КодПесни = song.КодПесни
                              }).ToList();
            dataGridView2.DataSource = albumsongs;
            dataGridView2.Columns["КодАльбома"].Visible = false;
            dataGridView2.Columns["КодПесни"].Visible = false;
        }
        public void DeletesongsinAlbum(DataGridView dataGridView1,DataGridView dataGridView2)
        {
            if (dataGridView1.SelectedRows.Count > 1 || dataGridView1.SelectedRows.Count == 0 || dataGridView2.SelectedRows.Count == 0) throw new Exception();
            int albumid = (int)dataGridView1.SelectedRows[0].Cells["КодАльбома"].Value;
            var selectedRows = dataGridView2.SelectedRows;
            var musicToRemove = new List<ПесниАльбома>();
            var listofmusic = new List<ПесниПользователя>();
            foreach (DataGridViewRow row in selectedRows)
            {
                var songid = (int)row.Cells["КодПесни"].Value;
                var music = db.ПесниАльбомаs.Where(p => p.КодПесни == songid && p.КодАльбома == albumid).First();
                musicToRemove.Add(music);
                var songs = db.ПесниПользователяs.Where(p => p.Альбом == dataGridView1.SelectedRows[0].Cells["Альбом"].Value.ToString() && p.КодМузыки == songid).First();
                listofmusic.Add(songs);
            }
            db.ПесниАльбомаs.RemoveRange(musicToRemove);
            db.SaveChanges();
            db.ПесниПользователяs.RemoveRange(listofmusic);
            db.SaveChanges();
        }
        public void AddSongsinAlbum(DataGridView dataGridView1,DataGridView dataGridView3,int Userid)
        {
            var selectedRows = dataGridView3.SelectedRows;
            var musicToInsert = new List<Песни>();
            foreach (DataGridViewRow row in selectedRows)
            {
                var songid = (int)row.Cells["КодПесни"].Value;
                var songinalbum = db.ПесниАльбомаs.Any(p => p.КодАльбома == (int)dataGridView1.SelectedRows[0].Cells["КодАльбома"].Value && p.КодПесни == songid);
                if (!songinalbum)
                {
                    var music = db.Песниs.Where(p => p.КодПесни == songid).First();
                    var genre = db.Жанрыs.Where(p => p.КодЖанра == music.Жанр).First();
                    var perf = db.Исполнителиs.Where(p => p.КодИсполнителя == music.Автор).First();
                    var album = (int)dataGridView1.SelectedRows[0].Cells["КодАльбома"].Value;
                    musicToInsert.Add(music);
                    var username = db.Пользователиs.Where(p => p.КодПользователя == Userid).First();
                    db.ПесниПользователяs.Add(new ПесниПользователя
                    {
                        Жанр = genre.Жанр,
                        Альбом = dataGridView1.SelectedRows[0].Cells["Альбом"].Value.ToString(),
                        UserId = Userid,
                        Исполнитель = perf.Исполнитель,
                        ПутьПесни = music.Путь,
                        КодМузыки = music.КодПесни,
                        Плейлист = "",
                        UserName = username.Login
                    });
                    db.SaveChanges();
                }
            }
            foreach (var item in musicToInsert)
            {
                db.ПесниАльбомаs.Add(new ПесниАльбома
                {
                    КодАльбома = (int)dataGridView1.SelectedRows[0].Cells["КодАльбома"].Value,
                    КодПесни = item.КодПесни
                });
                db.SaveChanges();
            }
        }
    }
}
