using Employment_Agency.Controllers;
using Employment_Agency.Models;
using Employment_Agency.Servises;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace Employment_Agency.Views
{
    public partial class EducationPosition : Form
    {
        private EmploymentAgencyContext db;
        private int userid;
        private EducationPositionService educationPositionService;
        private string educname;
        private int educid;
        private string posname;
        private int posid;
        public EducationPosition(EmploymentAgencyContext db,int userid)
        {
            InitializeComponent();
            this.db = db;
            this.userid = userid;
            educationPositionService = new EducationPositionService(this, db, userid);
            Confeduc.Enabled = false;
            Confpos.Enabled = false;
            educationPositionService.AllEducations(Educations);
            educationPositionService.AllPositions(Positions);
        }
        private void Inserdeduc_Click(object sender, EventArgs e)
        {
            try
            {
                if (Education.Text.Trim() == "") throw new Exception();
                educationPositionService.AddEducation(Education.Text,Educations);
            }
            catch
            {
                MessageBox.Show("Введите Название образования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void InsertPos_Click(object sender, EventArgs e)
        {
            try
            {
                if (Position.Text.Trim() == "") throw new Exception();
                educationPositionService.AddPosition(Position.Text, Positions);
            }
            catch
            {
                MessageBox.Show("Введите Название должности", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        public void UpdateEducations()
        {
            if (Educations.SelectedRows.Count > 1) MessageBox.Show("Нужно выбрать одну строку");
            else
            {
                Confeduc.Enabled = true;
                Education.Text = Educations.SelectedRows[0].Cells["Education1"].Value.ToString();
                educname = Educations.SelectedRows[0].Cells["Education1"].Value.ToString();
                educid = (int)Educations.SelectedRows[0].Cells["Educationid"].Value;
            }
        }
        private void UpdateEdcu_Click(object sender, EventArgs e)
        {
            UpdateEducations();
            Confeduc.Enabled = true;
        }
        public void UpdatePositions()
        {
            if (Positions.SelectedRows.Count > 1) MessageBox.Show("Нужно выбрать одну строку");
            else
            {
                Confpos.Enabled = true;
                Education.Text = Positions.SelectedRows[0].Cells["Position1"].Value.ToString();
                posname = Positions.SelectedRows[0].Cells["Position1"].Value.ToString();
                posid = (int)Positions.SelectedRows[0].Cells["Positionid"].Value;
            }
        }
        private void UpdatePos_Click(object sender, EventArgs e)
        {
            UpdatePositions();
            Confpos.Enabled = true;
        }

        private void Confeduc_Click(object sender, EventArgs e)
        {
            try
            {
                if (Education.Text.Trim() == "") throw new Exception();
                educationPositionService.UpdateEducation(educname, Education.Text, educid,Educations);
                Confeduc.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Введите название Образования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void Confpos_Click(object sender, EventArgs e)
        {
            try
            {
                if (Position.Text.Trim() == "") throw new Exception();
                educationPositionService.UpdatePosition(posname, Positions.Text, posid, Positions);
                Confpos.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Введите название должности", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void DeleteEduc_Click(object sender, EventArgs e)
        {
            try
            {
                if (Educations.SelectedRows.Count == 0) throw new Exception();
                educationPositionService.DeleteEduc(Educations);
            }
            catch
            {
                MessageBox.Show("Выберите Вид образования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void DeletePos_Click(object sender, EventArgs e)
        {
            try
            {
                if (Positions.SelectedRows.Count == 0) throw new Exception();
                educationPositionService.Deletepos(Positions);
            }
            catch
            {
                MessageBox.Show("Выберите Должность", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}
