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
    public class PlaylistsPresenter
    {
        private MusicPlayerContext db;
        private PlayLists view;
        public PlaylistsPresenter(MusicPlayerContext db, PlayLists view)
        {
            this.db = db;
            this.view = view;
        }
        public void AddPlaylist(string playlistName,int userid)
        {
            var Playlist = db.Плейлистыs.Any(u => u.Плейлист == playlistName && u.UserId == userid);
            if (Playlist)
            {
                MessageBox.Show("Плейлист уже существует");
                return;
            }
            db.Плейлистыs.Add(new Classes.Плейлисты
            {
                UserId = userid,
                Плейлист = playlistName.Trim()
            }); ;
            db.SaveChanges();
        }

        public void RemovePlaylist(DataGridView gridView)
        {
            var selectedRows = gridView.SelectedRows;
            var playlistsToRemove = new List<Плейлисты>();
            foreach (DataGridViewRow row in selectedRows)
            {
                var playlistid = (int)row.Cells["КодПлейлиста"].Value;
                var playlist = db.Плейлистыs.Where(p => p.КодПлейлиста == playlistid).First();
                playlistsToRemove.Add(playlist);
                var songs = db.ПесниПользователяs.Where(p => p.Плейлист == playlist.Плейлист).ToList();
                db.ПесниПользователяs.RemoveRange(songs);
                db.SaveChanges();
            }
            db.Плейлистыs.RemoveRange(playlistsToRemove);
            db.SaveChanges();
            view.Refresh();
        }
        public void UpdatePlaylist(string playlistNameold,int userid,string PlaylistNamenew,int playlistid) 
        {
            var Playlist = db.Плейлистыs.Any(u => u.Плейлист == PlaylistNamenew && u.UserId == userid);
            if (Playlist && PlaylistNamenew == playlistNameold)
            {
                MessageBox.Show("Такой плейлист уже есть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            if (PlaylistNamenew != playlistNameold)
            {
                var playlist = db.Плейлистыs.Where(p => p.UserId == userid && p.КодПлейлиста == playlistid).First();
                var songs = db.ПесниПользователяs.Where(p => p.Плейлист == playlist.Плейлист).ToList();
                foreach (var item in songs)
                {
                    item.Плейлист = PlaylistNamenew;
                    db.SaveChanges();
                }
                playlist.Плейлист = PlaylistNamenew;
                db.SaveChanges();
            }
            view.Refresh();
        }
        public void SongsofPlaylist(DataGridView gridViewplaylist,DataGridView songsgridview)
        {
            if (gridViewplaylist.SelectedRows.Count > 1) MessageBox.Show("Нужно выбрать одну строку");
            else
            {
                int playlistid = (int)gridViewplaylist.SelectedRows[0].Cells["КодПлейлиста"].Value;
                var musicofplaylist = (from playlist in db.ПесниПлейлистовs
                                       join music in db.Песниs on playlist.КодПесни equals music.КодПесни
                                       where playlist.КодПлейлиста == playlistid
                                       select new
                                       {
                                           НазваниеПесни = music.НазваниеПесни,
                                           Путь = music.Путь,
                                           КодПесни = music.КодПесни
                                       }).ToList();
                songsgridview.DataSource = musicofplaylist;
                songsgridview.Columns["КодПесни"].Visible = false;
                songsgridview.Columns["Путь"].Visible = false;
                view.music = -1;
            }
        }
        public void PlaylistsofUser(int userid,DataGridView gridView)
        {
            var playlistofuser = (from playlist in db.Плейлистыs
                                  where playlist.UserId == userid
                                  select new
                                  {
                                      Плейлист = playlist.Плейлист,
                                      КодПлейлиста = playlist.КодПлейлиста,
                                      UserID = playlist.UserId
                                  }).ToList();
            gridView.DataSource = playlistofuser;
            gridView.Columns["КодПлейлиста"].Visible = false;
            gridView.Columns["UserID"].Visible = false;
        }
        public void DeletesongsinPlaylist(DataGridView songsgridvie,DataGridView playlistgridview,List<ПесниПлейлистов> songstoRemove)
        {
            var playl = (int)playlistgridview.SelectedRows[0].Cells["КодПлейлиста"].Value;
            var selectedRows = songsgridvie.SelectedRows;
            foreach (DataGridViewRow row in selectedRows)
            {
                var songid = (int)row.Cells["КодПесни"].Value;
                var song = db.ПесниПлейлистовs.Where(p => p.КодПесни == songid && p.КодПлейлиста == playl).First();
                songstoRemove.Add(song);
            }
            db.ПесниПлейлистовs.RemoveRange(songstoRemove);
            db.SaveChanges();
            foreach (var item in songstoRemove)
            {
                var pl = db.Плейлистыs.Where(p => p.КодПлейлиста == item.КодПлейлиста).First();
                var song = db.ПесниПользователяs.Where(p => p.Плейлист == pl.Плейлист && p.КодМузыки == item.КодПесни && p.UserName == "Admin").ToList();
                db.ПесниПользователяs.RemoveRange(song);
                db.SaveChanges();
            }
            view.Refresh();
        }  
    }
}
