using Lab2ORM.Classes;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2ORM.Presenters
{
    public class AddMusicPresenter
    {
        private MusicPlayerContext db;
        private AddMusic view;
        public AddMusicPresenter(MusicPlayerContext db, AddMusic view)
        {
            this.view = view;
            this.db = db;
        }
        public void UpdateSong(string oldname,string newname,int musicid,string performer,string genre)
        {
            var Music = db.Песниs.Any(u => u.НазваниеПесни == newname);
            if (Music && newname != oldname)
            {
                MessageBox.Show("Такоя музыка уже есть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            if (newname != oldname)
            {
                var autorid = db.Исполнителиs.Where(p => p.Исполнитель == performer).First();
                var genreid = db.Жанрыs.Where(p => p.Жанр == genre).First();
                var music = db.Песниs.Where(p => p.КодПесни == musicid).First();
                music.НазваниеПесни = newname;
                music.Автор = autorid.КодИсполнителя;
                music.Жанр = genreid.КодЖанра;
                db.SaveChanges();
            }
            view.Refresh();
        }
        public void AddMusic(string name,string Performer,string genre,int Userid)
        {
            string filePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы mp3 (*.mp3)|*.mp3";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string musicFolderPath = Directory.GetCurrentDirectory() + "\\Music";
                filePath = Path.Combine(musicFolderPath, openFileDialog.SafeFileName);
                if (!Directory.Exists(musicFolderPath))
                {
                    Directory.CreateDirectory(musicFolderPath);
                }
                File.Copy(openFileDialog.FileName, filePath, true);
            }
            var autorid = db.Исполнителиs.Where(p => p.Исполнитель == Performer).First();
            if (name.Trim() == "" || genre == "" || Performer == "") throw new Exception();
            var Music = db.Песниs.Any(u => u.НазваниеПесни == name && u.Автор == autorid.КодИсполнителя);
            if (Music)
            {
                MessageBox.Show("Музыка уже существует");
                return;
            }
            var genreid = db.Жанрыs.Where(p => p.Жанр == genre).First();
            db.Песниs.Add(new Classes.Песни
            {
                Автор = autorid.КодИсполнителя,
                НазваниеПесни = name,
                Жанр = genreid.КодЖанра,
                Путь = filePath
            });
            db.SaveChanges();
            var song = db.Песниs.Where(p => p.НазваниеПесни == name).First();
            var username = db.Пользователиs.Where(p => p.КодПользователя == Userid).First();
            db.ПесниПользователяs.Add(new ПесниПользователя
            {
                Исполнитель = autorid.Исполнитель,
                Жанр = genreid.Жанр,
                ПутьПесни = filePath,
                КодМузыки = song.КодПесни,
                UserName = username.Login,
                UserId = Userid,
                Плейлист = "",
                Альбом = ""
            });
            db.SaveChanges();
            view.Refresh();
        }
        public void Allinf(DataGridView dataGridView1,ComboBox comboBox1, ComboBox comboBox2)
        {
            var musics = (from music in db.Песниs
                          join perfomer in db.Исполнителиs on music.Автор equals perfomer.КодИсполнителя
                          join genre in db.Жанрыs on music.Жанр equals genre.КодЖанра
                          select new
                          {
                              Песня = music.НазваниеПесни,
                              Жанр = genre.Жанр,
                              Автор = perfomer.Исполнитель,
                              КодПесни = music.КодПесни
                          }).ToList();
            dataGridView1.DataSource = musics;
            dataGridView1.Columns["КодПесни"].Visible = false;
            var perfomers = db.Исполнителиs.ToList();
            comboBox1.DataSource = perfomers;
            comboBox1.DisplayMember = "Исполнитель";
            var genres = db.Жанрыs.ToList();
            comboBox2.DataSource = genres;
            comboBox2.DisplayMember = "Жанр";
        }
        public void DeleteSongs(DataGridView dataGridView1)
        {
            var selectedRows = dataGridView1.SelectedRows;
            var musicToRemove = new List<Песни>();
            foreach (DataGridViewRow row in selectedRows)
            {
                var songid = (int)row.Cells["КодПесни"].Value;
                var songsin = db.ПесниПлейлистовs.Any(p => p.КодПесни == songid);
                if (!songsin)
                {
                    var music = db.Песниs.Where(p => p.КодПесни == songid).First();
                    musicToRemove.Add(music);
                }
            }
            db.Песниs.RemoveRange(musicToRemove);
            db.SaveChanges();
            view.Refresh();
        }
    }
}
