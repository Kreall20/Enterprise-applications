using Employment_Agency.Models;
using Employment_Agency.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment_Agency.Servises
{
    public class EducationPositionService
    {
        private EmploymentAgencyContext? db;
        private EducationPosition educationPositionview;
        private int userid;

        public EducationPositionService(EducationPosition educationPositionview, EmploymentAgencyContext? db, int userid)
        {
            this.educationPositionview = educationPositionview;
            this.db = db;
            this.userid = userid;
        }
        public void AllEducations(DataGridView gridView)
        {
            var educ = db.Educations.ToList();
            gridView.DataSource = educ;
            gridView.Columns["Educationid"].Visible = false;
            gridView.Columns["Applicants"].Visible = false;
            gridView.Columns["Vacancies"].Visible = false;
        }
        public void AddEducation(string Education,DataGridView educations)
        {
            var isEduc = db.Educations.Any(u => u.Education1 == Education);
            if (isEduc)
            {
                MessageBox.Show("Данный вид образовния уже существует");
                return;
            }
            db.Educations.Add(new Education
            {
                Education1 = Education
            });
            db.SaveChanges();
            AllEducations(educations);
        }

        public void AddPosition(string Position, DataGridView Positions)
        {
            var isEduc = db.Positions.Any(u => u.Position1 == Position);
            if (isEduc)
            {
                MessageBox.Show("Данная должность уже существует");
                return;
            }
            db.Positions.Add(new Position
            {
                Position1 = Position
            });
            db.SaveChanges();
            AllPositions(Positions);
        }

        public void AllPositions(DataGridView gridView)
        {
            var pos = db.Positions.ToList();
            gridView.DataSource = pos;
            gridView.Columns["Positionid"].Visible = false;
            gridView.Columns["Applicants"].Visible = false;
            gridView.Columns["Vacancies"].Visible = false;
        }
        public void UpdateEducation(string educname,string educnewname,int educid,DataGridView Educations)
        {
            var Educ = db.Educations.Any(u => u.Education1 == educname);
            if (Educ && educname != educnewname)
            {
                MessageBox.Show("Такой Вид Образования уже есть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            if (educname != educnewname)
            {
                var educ = db.Educations.Where(p => p.Educationid == educid).First();
                educ.Education1 = educnewname;
                db.SaveChanges();
            }
            AllEducations(Educations);
        }
        public void UpdatePosition(string posname, string posnewname, int posid, DataGridView positions)
        {
            var Pos = db.Positions.Any(u => u.Position1 == posnewname);
            if (Pos && posname != posnewname)
            {
                MessageBox.Show("Такоя должность уже есть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            if (posname != posnewname)
            {
                var pos = db.Positions.Where(p => p.Positionid == posid).First();
                pos.Position1 = posnewname;
                db.SaveChanges();
            }
            AllPositions(positions);
        }

        public void DeleteEduc(DataGridView educations)
        {
            var selectedRows = educations.SelectedRows;
            var Eductoremove = new List<Education>();
            foreach (DataGridViewRow row in selectedRows)
            {
                var educid = (int)row.Cells["Educationid"].Value;
                var educ = db.Educations.Where(p => p.Educationid == educid).First();
                Eductoremove.Add(educ);
            }
            db.Educations.RemoveRange(Eductoremove);
            db.SaveChanges();
            AllEducations(educations);
        }

        public void Deletepos(DataGridView positions)
        {
            var selectedRows = positions.SelectedRows;
            var Postoremove = new List<Position>();
            foreach (DataGridViewRow row in selectedRows)
            {
                var posid = (int)row.Cells["Positionid"].Value;
                var educ = db.Positions.Where(p => p.Positionid == posid).First();
                Postoremove.Add(educ);
            }
            db.Positions.RemoveRange(Postoremove);  
            db.SaveChanges();
            AllPositions(positions);
        }
    }
}
