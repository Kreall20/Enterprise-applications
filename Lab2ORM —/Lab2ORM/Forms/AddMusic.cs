using Lab2ORM.Classes;
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
    public partial class AddMusic : Form
    {
        private MusicPlayerContext db;
        private int musicid;
        private string musicname;
        public AddMusic(MusicPlayerContext db)
        {
            InitializeComponent();
            button2.Enabled = false;
            this.db = db;
            Refresh();
        }
        public void Refresh()
        {
            textBox1.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            button2.Enabled = false;
            var musics = (from music in db.Песниs
                         join perfomer in db.Исполнителиs on music.Автор equals perfomer.КодИсполнителя
                         join genre in db.Жанрыs on music.Жанр equals genre.КодЖанра
                         select new
                         {
                            Песня =  music.НазваниеПесни,
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
        private void button7_Click(object sender, EventArgs e)
        {
            try
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
                var autorid = db.Исполнителиs.Where(p => p.Исполнитель == comboBox1.Text).First();
                if (textBox1.Text.Trim() == "" || comboBox2.Text == "" || comboBox1.Text == "") throw new Exception();
                var Music = db.Песниs.Any(u => u.НазваниеПесни == textBox1.Text && u.Автор == autorid.КодИсполнителя);
                if (Music)
                {
                    MessageBox.Show("Музыка уже существует");
                    return;
                }
                var genreid = db.Жанрыs.Where(p => p.Жанр == comboBox2.Text).First();
                db.Песниs.Add(new Classes.Песни
                {
                    Автор = autorid.КодИсполнителя,
                    НазваниеПесни = textBox1.Text,
                    Жанр = genreid.КодЖанра,
                    Путь = filePath
                });
                db.SaveChanges();
                Refresh();
            }
            catch
            {
                MessageBox.Show("Введите все данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        public void Update()
        {
            if (dataGridView1.SelectedRows.Count > 1) MessageBox.Show("Нужно выбрать одну строку");
            else
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["НазваниеПесни"].Value.ToString();
                musicname = dataGridView1.SelectedRows[0].Cells["НазваниеПесни"].Value.ToString();
                musicid = (int)dataGridView1.SelectedRows[0].Cells["КодПесни"].Value;
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            Update();
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var Music = db.Песниs.Any(u => u.НазваниеПесни == textBox1.Text);
                if (Music && textBox1.Text != musicname)
                {
                    MessageBox.Show("Такоя музыка уже есть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
                if (textBox1.Text != musicname)
                {
                    var autorid = db.Исполнителиs.Where(p => p.Исполнитель == comboBox1.Text).First();
                    var genreid = db.Жанрыs.Where(p => p.Жанр == comboBox2.Text).First();
                    var music = db.Песниs.Where(p => p.КодПесни == musicid).First();
                    music.НазваниеПесни = textBox1.Text;
                    music.Автор = autorid.КодИсполнителя;
                    music.Жанр = genreid.КодЖанра;
                    db.SaveChanges();
                }
                Refresh();
            }
            catch
            {
                MessageBox.Show("Введите Все данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0) throw new Exception();
                var selectedRows = dataGridView1.SelectedRows;
                var musicToRemove = new List<Песни>();
                foreach (DataGridViewRow row in selectedRows)
                {
                    var songid = (int)row.Cells["КодПесни"].Value;
                    var music = db.Песниs.Where(p => p.КодПесни == songid).First();
                    musicToRemove.Add(music);
                }
                db.Песниs.RemoveRange(musicToRemove);
                db.SaveChanges();
                Refresh();
            }
            catch
            {
                MessageBox.Show("Выберите музыку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}
