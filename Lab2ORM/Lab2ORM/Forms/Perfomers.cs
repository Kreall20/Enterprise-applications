using Lab2ORM.Classes;
using Lab2ORM.Presenters;
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
    public partial class Perfomers : Form
    {
        private MusicPlayerContext db;
        private int perfomerid;
        private string perfomername;
        private PerformerPresenter presenter;
        public void Refresh()
        {
            button2.Enabled = false;
            textBox1.Text = "";
            presenter.AllPerformers(dataGridView1);
        }
        public Perfomers(MusicPlayerContext db)
        {
            InitializeComponent();
            button2.Enabled = false;
            this.db = db;
            presenter = new PerformerPresenter(this,db);
            Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                presenter.UpdatePerformer(perfomername, textBox1.Text, perfomerid);
            }
            catch
            {
                MessageBox.Show("Введите Имя исполнителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == "") throw new Exception();
                presenter.AddPerformer(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("Введите имя исполнителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        public void Update()
        {
            if (dataGridView1.SelectedRows.Count > 1) MessageBox.Show("Нужно выбрать одну строку");
            else
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["Исполнитель"].Value.ToString();
                perfomername = dataGridView1.SelectedRows[0].Cells["Исполнитель"].Value.ToString();
                perfomerid = (int)dataGridView1.SelectedRows[0].Cells["КодИсполнителя"].Value;

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0) throw new Exception();
                presenter.DeletePerformer(dataGridView1);
            }
            catch
            {
                MessageBox.Show("Выберите имя Исполнителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Update();
            button2.Enabled = true;
        }
    }
}
