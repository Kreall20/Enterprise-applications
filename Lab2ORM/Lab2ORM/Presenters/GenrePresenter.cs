using Lab2ORM.Classes;
using Lab2ORM.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2ORM.Presenters
{
    public class GenrePresenter
    {
        private Genre view;
        private MusicPlayerContext db;
        public GenrePresenter(Genre view,MusicPlayerContext db)
        {
            this.view = view;
            this.db = db;
        }
        public void AllGenres(DataGridView gridView)
        {
            var genres = db.Жанрыs.ToList();
            gridView.DataSource = genres;
            gridView.Columns["КодЖанра"].Visible = false;
            gridView.Columns["Песниs"].Visible = false;
        }
        public void AddGenre(string Name)
        {
            var Genre = db.Жанрыs.Any(u => u.Жанр == Name);
            if (Genre)
            {
                MessageBox.Show("Плейлист уже существует");
                return;
            }
            db.Жанрыs.Add(new Classes.Жанры
            {
                Жанр = Name
            });
            db.SaveChanges();
            view.Refresh();
        }
        public void DeleteGenres(DataGridView gridView)
        {
            var selectedRows = gridView.SelectedRows;
            var genresToRemove = new List<Жанры>();
            foreach (DataGridViewRow row in selectedRows)
            {
                var genreid = (int)row.Cells["КодЖанра"].Value;
                var genre = db.Жанрыs.Where(p => p.КодЖанра == genreid).First();
                genresToRemove.Add(genre);
                var songs = db.ПесниПользователяs.Where(p => p.Жанр == genre.Жанр).ToList();
                db.ПесниПользователяs.RemoveRange(songs);
                db.SaveChanges();
            }
            db.Жанрыs.RemoveRange(genresToRemove);
            db.SaveChanges();
            view.Refresh();
        }
        public void UpdateGenre(string oldname,string newname,int genreid)
        {
            var Genre = db.Жанрыs.Any(u => u.Жанр == newname);
            if (Genre && newname != oldname)
            {
                MessageBox.Show("Такой Жанр уже есть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            if (newname != oldname)
            {
                var genre = db.Жанрыs.Where(p => p.КодЖанра == genreid).First();
                var songs = db.ПесниПользователяs.Where(p => p.Жанр == genre.Жанр).ToList();
                foreach (var item in songs)
                {
                    item.Жанр = newname;
                    db.SaveChanges();
                }
                genre.Жанр = newname;
                db.SaveChanges();
            }
            view.Refresh();
        }
    }
}
