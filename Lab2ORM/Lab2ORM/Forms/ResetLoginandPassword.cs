using Lab2ORM.Classes;
using Lab2ORM.Presenters;
using Newtonsoft.Json;
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
    public partial class ResetLoginandPassword : Form
    {
        private int UserId;
        private MusicPlayerContext db;
        private string Login;
        private ResetPresenter presenter;
        public ResetLoginandPassword(int userId, MusicPlayerContext db)
        {
            InitializeComponent();
            this.db = db;
            presenter = new ResetPresenter(this, db);
            UserId = userId;
            textBox2.PasswordChar = '*';
            textBox3.PasswordChar = '*';
            Refresh();
        }
        public void Refresh()
        {
            textBox1.Text = presenter.CheckUser(UserId);
            Login = textBox1.Text;
            ClearTextboxes();
        }
        public void ClearTextboxes()
        {
            textBox2.Text = "";
            textBox3.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "") throw new ArgumentNullException();
                presenter.CheckData(textBox2.Text, textBox1.Text, Login, textBox3.Text);
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Пожалуйста введите данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Такой пользователь уже есть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            catch
            {
                MessageBox.Show("Введен неверный пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}
