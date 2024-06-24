using Lab2ORM.Classes;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2ORM.Forms
{
    public partial class Albums : Form
    {
        private MusicPlayerContext db;
        private int albumid;
        private string albumname;
        private int Userid;
        public void Refresh()
        {
            textBox1.Text = "";
            comboBox1.Text = "";
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
        public Albums(MusicPlayerContext db,int User)
        {
            InitializeComponent();
            button2.Enabled = false;
            this.db = db;
            Userid = User;
            Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var autorid = db.Исполнителиs.Where(p => p.Исполнитель == comboBox1.Text).First();
                var Album = db.Альбомыs.Any(u => u.НазваниеАльбома == textBox1.Text && u.Автор == autorid.КодИсполнителя);
                if (Album && textBox1.Text != albumname)
                {
                    MessageBox.Show("Такой Жанр уже есть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
                if (textBox1.Text != albumname)
                {
                    var album = db.Альбомыs.Where(p => p.КодАльбома == albumid && p.Автор == autorid.КодИсполнителя).First();
                    album.НазваниеАльбома = textBox1.Text;
                    album.Автор = autorid.КодИсполнителя;
                    db.SaveChanges();
                }
                Refresh();
            }
            catch
            {
                MessageBox.Show("Введите название Альбома", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                var autorid = db.Исполнителиs.Where(p => p.Исполнитель == comboBox1.Text).First();
                if (textBox1.Text.Trim() == "" || comboBox1.Text == "") throw new Exception();
                var Album = db.Альбомыs.Any(u => u.НазваниеАльбома == textBox1.Text && u.Автор == autorid.КодИсполнителя);
                if (Album)
                {
                    MessageBox.Show("Плейлист уже существует");
                    return;
                }
                db.Альбомыs.Add(new Classes.Альбомы
                {
                    НазваниеАльбома = textBox1.Text,
                    Автор = autorid.КодИсполнителя
                });
                db.SaveChanges();
                Refresh();
            }
            catch
            {
                MessageBox.Show("Введите Альбом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        public void Update()
        {
            if (dataGridView1.SelectedRows.Count > 1) MessageBox.Show("Нужно выбрать одну строку");
            else
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["НазваниеАльбома"].Value.ToString();
                int perfomer = (int)dataGridView1.SelectedRows[0].Cells["Автор"].Value;
                var autor = db.Исполнителиs.Where(p => p.КодИсполнителя == perfomer).First();
                comboBox1.Text = autor.Исполнитель;
                albumname = dataGridView1.SelectedRows[0].Cells["НазваниеАльбома"].Value.ToString();
                albumid = (int)dataGridView1.SelectedRows[0].Cells["КодАльбома"].Value;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0) throw new Exception();
                var selectedRows = dataGridView1.SelectedRows;
                var albumsToRemove = new List<Альбомы>();
                foreach (DataGridViewRow row in selectedRows)
                {
                    var albumid = (int)row.Cells["КодАльбома"].Value;
                    var album = db.Альбомыs.Where(p => p.КодАльбома == albumid).First();
                    albumsToRemove.Add(album);
                }
                db.Альбомыs.RemoveRange(albumsToRemove);
                db.SaveChanges();
                Refresh();
            }
            catch
            {
                MessageBox.Show("Выберите Альбом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Update();
            button2.Enabled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 1) throw new Exception();
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
            catch
            {
                MessageBox.Show("Выберите Альбом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 1 || dataGridView1.SelectedRows.Count == 0 || dataGridView3.SelectedRows.Count == 0) throw new Exception();
                var selectedRows = dataGridView3.SelectedRows;
                var musicToInsert = new List<Песни>();
                foreach (DataGridViewRow row in selectedRows)
                {
                    var songid = (int)row.Cells["КодПесни"].Value;
                    var songinalbum = db.ПесниАльбомаs.Any(p => p.КодАльбома == (int)dataGridView1.SelectedRows[0].Cells["КодАльбома"].Value && p.КодПесни == songid);
                    if (!songinalbum)
                    {
                        var music = db.Песниs.Where(p => p.КодПесни == songid).First();
                        musicToInsert.Add(music);
                    }
                }
                foreach(var item in musicToInsert)
                {
                    db.ПесниАльбомаs.Add(new ПесниАльбома
                    {
                        КодАльбома = (int)dataGridView1.SelectedRows[0].Cells["КодАльбома"].Value,
                        КодПесни = item.КодПесни
                    });
                    db.ПесниПользователяs.Add(new ПесниПользователя
                    {

                    });
                    db.SaveChanges();
                }
            }
            catch
            {
                MessageBox.Show("Не выбраны нужные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 1 || dataGridView1.SelectedRows.Count == 0 || dataGridView2.SelectedRows.Count == 0) throw new Exception();
                int albumid = (int)dataGridView1.SelectedRows[0].Cells["КодАльбома"].Value;
                var selectedRows = dataGridView2.SelectedRows;
                var musicToRemove = new List<ПесниАльбома>();
                foreach (DataGridViewRow row in selectedRows)
                {
                    var songid = (int)row.Cells["КодПесни"].Value;
                    var music = db.ПесниАльбомаs.Where(p => p.КодПесни == songid && p.КодАльбома == albumid).First();
                    musicToRemove.Add(music);
                }
                db.ПесниАльбомаs.RemoveRange(musicToRemove);
                db.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Не выбраны нужные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}
