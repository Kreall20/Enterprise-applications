using Lab2ORM.Classes;
using Lab2ORM.Forms;
using Lab2ORM.Presenters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace Lab2ORM
{
    public partial class MainForm : Form
    {
        private bool flagofinsertmusic = false;
        private LogIn login;
        private MusicPlayerContext db;
        private int UserId;
        MediaPlayer player = new MediaPlayer();
        private MainPresenter presenter;
        bool flag = false;
        public MainForm(LogIn login, MusicPlayerContext db,int userId)
        {
            InitializeComponent();
            this.login = login;
            this.db = db;
            UserId = userId;
            presenter = new MainPresenter(this, db);
            Refresh();
        }

        public MainForm(MusicPlayerContext db,int userId)
        {
            InitializeComponent();
            login = new LogIn(db);
            this.db = db;
            UserId = userId;
            presenter = new MainPresenter(this, db);
            Refresh();
        }
        public void Refresh()
        {
            presenter.Allinf(dataGridView2,comboBox1, comboBox2, comboBox3, comboBox4, UserId);
        }
        private void MainForm_Load(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e){}
        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists("user.json"))
                File.Delete("user.json");
            this.Hide();
            login.Show();
            player.Pause();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                flag = true;
                string genre = "%" + comboBox4.Text + "%";
                string playlist = "%" + comboBox3.Text + "%";
                string album = "%" + comboBox2.Text + "%";
                string performer = "%" + comboBox1.Text + "%";
                string songname = "%" + textBox1.Text+"%";

                presenter.Search(playlist, album, performer, genre, songname, UserId, dataGridView1);

            }
            catch
            {
                MessageBox.Show("Введите условие поиска", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PlayLists playLists = new PlayLists(db,UserId);
            playLists.ShowDialog();
            Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Musics musics = new Musics(db, UserId);
            musics.ShowDialog();
            Refresh();
        }

        private void button7_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            player.Pause();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var music = presenter.SongDobleclick(dataGridView1, dataGridView2, flag);
                label1.Text = music.НазваниеПесни;
                if (File.Exists(music.Путь))
                {
                    player.Open(new Uri(music.Путь));
                    player.Play();
                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            player.Play();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            flag = false;
            Refresh();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 1)
            {
                int playlistid = (int)dataGridView2.SelectedRows[0].Cells["КодПлейлиста"].Value;
                presenter.SongofPlaylist(dataGridView1, playlistid);
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            ResetLoginandPassword reset = new ResetLoginandPassword(UserId, db);
            reset.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                flag = true;
                string genre = "%" + comboBox4.Text + "%";
                string playlist = "%" + comboBox3.Text + "%";
                string album = "%" + comboBox2.Text + "%";
                string performer = "%" + comboBox1.Text + "%";
                string songname = "%" + textBox1.Text + "%";

                var user = db.Пользователиs.Where(p => p.КодПользователя == UserId).First().Login;
                presenter.Search(playlist, album, performer, genre, songname,UserId, dataGridView1);
            }
            catch
            {

            }
        }
    }
}
