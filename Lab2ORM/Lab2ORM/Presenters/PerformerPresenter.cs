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
    public  class PerformerPresenter
    {
        private Perfomers view; 
        private MusicPlayerContext db;
        public PerformerPresenter(Perfomers performer,MusicPlayerContext db)
        {
            view = performer;
            this.db = db;
        }
        public void AllPerformers(DataGridView gridView)
        {
            var users = db.Исполнителиs.ToList();
            gridView.DataSource = users;
            gridView.Columns["КодИсполнителя"].Visible = false;
            gridView.Columns["Песниs"].Visible = false;
        }
        public void AddPerformer(string perfomername)
        {
            var Perfomer = db.Исполнителиs.Any(u => u.Исполнитель == perfomername);
            if (Perfomer)
            {
                MessageBox.Show("Плейлист уже существует");
                return;
            }
            db.Исполнителиs.Add(new Classes.Исполнители
            {
                Исполнитель = perfomername
            });
            db.SaveChanges();
            view.Refresh();
        }
        public void DeletePerformer(DataGridView gridView)
        {
            var selectedRows = gridView.SelectedRows;
            var perfomersToRemove = new List<Исполнители>();
            foreach (DataGridViewRow row in selectedRows)
            {
                var performerid = (int)row.Cells["КодИсполнителя"].Value;
                var perfomer = db.Исполнителиs.Where(p => p.КодИсполнителя == performerid).First();
                perfomersToRemove.Add(perfomer);
                var songs = db.ПесниПользователяs.Where(p => p.Исполнитель == perfomer.Исполнитель).ToList();
                db.ПесниПользователяs.RemoveRange(songs);
                db.SaveChanges();
            }
            db.Исполнителиs.RemoveRange(perfomersToRemove);
            db.SaveChanges();
            view.Refresh();
        }
        public void UpdatePerformer(string oldname,string newname,int performerid)
        {
            var Perfomer = db.Исполнителиs.Any(u => u.Исполнитель == newname);
            if (Perfomer && newname != oldname)
            {
                MessageBox.Show("Такой исполнитель уже есть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            if (newname != oldname)
            {
                var perfomer = db.Исполнителиs.Where(p => p.КодИсполнителя == performerid).First();
                var songs = db.ПесниПользователяs.Where(p => p.Исполнитель == perfomer.Исполнитель).ToList();
                foreach (var item in songs)
                {
                    item.Исполнитель = newname;
                    db.SaveChanges();
                }
                perfomer.Исполнитель = newname;
                db.SaveChanges();
            }
            view.Refresh();
        }
    }
}
