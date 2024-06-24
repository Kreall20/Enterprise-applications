using Employment_Agency.Models;
using Employment_Agency.Views;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment_Agency.Controllers
{
    public class AdminService
    {
        private EmploymentAgencyContext db;
        private int userid;
        private Views.AdminPanel panel;

        public AdminService(Views.AdminPanel adminPanel, EmploymentAgencyContext db, int userid)
        {
            this.db = db;
            panel = adminPanel;
            this.userid = userid;
        }
        public void GetApplicantsEmployers(DataGridView applicants,DataGridView Employers)
        {
            GetApplicants(applicants);
            GetVac(Employers);
        }
        public void DeleteVacancy(DataGridView gridview)
        {
            try
            {
                var selectedRows = gridview.SelectedRows;
                foreach (DataGridViewRow row in selectedRows)
                {
                    int vacancyid = (int)row.Cells["Vacancyid"].Value;
                    var vacancis = db.Vacancies.Where(p => p.Vacancyid == vacancyid).ToList();
                    db.Vacancies.RemoveRange(vacancis);
                    db.SaveChanges();
                }
            }
            catch
            {

            }
        }
        public void GetApplicants(DataGridView gridView)
        {
            var applicants = (from appl in db.Applicants
                              join educ in db.Educations on appl.Education equals educ.Educationid
                              join pos in db.Positions on appl.Position equals pos.Positionid
                              select new
                                 {
                                     Fio = appl.Fio,
                                     Applicantid = appl.Applicantid,
                                     Salary = appl.Salary,
                                     График = appl.Schedule,
                                     ВодПрава = appl.DriverLicense,
                                     Языки = appl.Languages,
                                     ДатаРождения = appl.DateofBirth,
                                     Образование = educ.Education1,
                                     Пол = appl.Gender,
                                     Должность = pos.Position1,
                                     Город = appl.City,
                                     Опыт = appl.WorkExperience,
                                     ЗнаниПк = appl.PcKnowledge
                                 }).ToList();
            gridView.DataSource = applicants;
            gridView.Columns["Applicantid"].Visible = false;
        }
        public void GetVac(DataGridView Employers)
        {
            Employers.DataSource = (from vac in db.Vacancies
                                    join emp in db.Employers on vac.Employer equals emp.Empoyerid
                                    join educ in db.Educations on vac.Education equals educ.Educationid
                                    join pos in db.Positions on vac.Position equals pos.Positionid
                                    select new
                                    {
                                        Fio = emp.Fio,
                                        Employerid = vac.Employer,
                                        Vacancyid = vac.Vacancyid,
                                        Salary = vac.Salary,
                                        График = vac.Schedule,
                                        ВодПрава = vac.DriverLicense,
                                        Языки = vac.Languages,
                                        Образование = educ.Education1,
                                        Пол = vac.Gender,
                                        Должность = pos.Position1,
                                        Город = vac.City,
                                        Опыт = vac.WorkExperience,
                                        ЗнаниПк = vac.PcKnowledge
                                    }).ToList();
            Employers.Columns["Vacancyid"].Visible = false;
            Employers.Columns["Employerid"].Visible = false;
        }
        public void GetEmployers(DataGridView Employers)
        {
            Employers.DataSource = db.Employers.ToList();
            Employers.Columns["Userid"].Visible = false;
            Employers.Columns["Empoyerid"].Visible = false;
            Employers.Columns["User"].Visible = false;
            Employers.Columns["Vacancies"].Visible = false;
        }
        public void DelteApplicants(DataGridView gridView)
        {
            try
            {
                var selectedRows = gridView.SelectedRows;
                foreach (DataGridViewRow row in selectedRows)
                {
                    var applicant = db.Applicants.Where(p => p.Applicantid == (int)row.Cells["Applicantid"].Value).First();
                    db.Applicants.Remove(applicant);
                    db.SaveChanges();
                }
                GetApplicants(gridView);
            }
            catch
            {

            }
        }
        public void DelteEmployers(DataGridView gridView)
        {
            try
            {
                var selectedRows = gridView.SelectedRows;
                foreach (DataGridViewRow row in selectedRows)
                {
                    var employers = db.Employers.Where(p => p.Empoyerid == (int)row.Cells["Employerid"].Value).First();
                    db.Employers.Remove(employers);
                    db.SaveChanges();
                }
                GetEmployers(gridView);
            }
            catch
            {

            }
        }
        public void Serchvac(string Gender, string Employer, string education, string Pos, bool drivelic, int salary, string city, bool Pc_kn, DataGridViewSelectedRowCollection selectedLanguages, int WorkExp, string Schedule, DataGridView vacs, int userid)
        {
            var educationParam = new SqlParameter("@educationparam", education);
            var positionParam = new SqlParameter("@position", Pos);
            var genderParam = new SqlParameter("@gender", Gender);
            var driverLicenseParam = new SqlParameter("@driverLicense", drivelic);
            var salaryParam = new SqlParameter("@salary", salary);
            var cityParam = new SqlParameter("@city", "%" + city + "%");
            var pcKnowledgeParam = new SqlParameter("@pcKnowledge", Pc_kn);
            var workExperienceParam = new SqlParameter("@workExperience", WorkExp);
            var scheduleParam = new SqlParameter("@schedule", Schedule);
            var companyParam = new SqlParameter("@company", Employer);
            var userparam = new SqlParameter("@user", userid);
            var selectedLanguageNames = selectedLanguages.Cast<DataGridViewRow>()
            .Select(row => row.Cells["Language"].Value.ToString())
            .ToList();

            var languagesParam = new SqlParameter("@languages", string.Join(" OR ", selectedLanguageNames.Select(lang => $"CONTAINS(v.Languages, '{lang}')")));

            var result = db.Vacancies
                .FromSqlRaw("SELECT v.* " +
                            "FROM Vacancies v " +
                            "INNER JOIN Employers e ON v.Employer = e.Empoyerid " +
                            "INNER JOIN Education educ ON v.Education = educ.educationid " +
                            "INNER JOIN Positions pos ON v.Position = pos.positionid " +
                            "INNER JOIN Vacancy_Languages vl ON v.Vacancyid = vl.Vacancy " +
                            "WHERE educ.Education LIKE @educationparam " +
                            "AND pos.Position LIKE  @position " +
                            "AND v.Openness = 1 " +
                            "AND v.gender LIKE @gender " +
                            "AND v.Driver_license = @driverLicense " +
                            "AND v.Salary >= @salary " +
                            "AND v.City LIKE @city " +
                            "AND v.PC_knowledge = @pcKnowledge " +
                            "AND v.work_experience <= @workExperience " +
                            "AND v.Schedule LIKE @schedule " +
                            "AND e.Userid != @user " +
                            "AND e.Company LIKE @company",
                            educationParam, positionParam, genderParam, driverLicenseParam, salaryParam, cityParam, pcKnowledgeParam, workExperienceParam, scheduleParam, companyParam, userparam).ToList();
            var filteredVacancies = result.Where(vacancy => selectedLanguageNames.All(language => vacancy.Languages.Contains(language))).ToList();
            var vacsearch = (from vac in filteredVacancies
                             join emp in db.Employers on vac.Employer equals emp.Empoyerid
                             join educ in db.Educations on vac.Education equals educ.Educationid
                             join pos in db.Positions on vac.Position equals pos.Positionid
                             select new
                             {
                                 Company = emp.Company,
                                 НомерТелефона = emp.PhoneNumber,
                                 Vacancyid = vac.Vacancyid,
                                 Salary = vac.Salary,
                                 График = vac.Schedule,
                                 ВодПрава = vac.DriverLicense,
                                 Языки = vac.Languages,
                                 Образование = educ.Education1,
                                 Пол = vac.Gender,
                                 Должность = pos.Position1,
                                 Город = vac.City,
                                 Опыт = vac.WorkExperience,
                                 ЗнаниПк = vac.PcKnowledge
                             }).ToList();
            vacs.DataSource = vacsearch;
            if (vacsearch.Count() != 0) vacs.Columns["Vacancyid"].Visible = false;
        }
        public void Serchappl(string Gender, string education, string Pos, bool drivelic, int salary, string city, bool Pc_kn, DataGridViewSelectedRowCollection selectedLanguages, int WorkExp, string Schedule, DataGridView applicants, int userid)
        {
            var educationParam = new SqlParameter("@educationparam", education);
            var positionParam = new SqlParameter("@position", Pos);
            var genderParam = new SqlParameter("@gender", Gender);
            var driverLicenseParam = new SqlParameter("@driverLicense", drivelic);
            var salaryParam = new SqlParameter("@salary", salary);
            var cityParam = new SqlParameter("@city", "%" + city + "%");
            var pcKnowledgeParam = new SqlParameter("@pcKnowledge", Pc_kn);
            var workExperienceParam = new SqlParameter("@workExperience", WorkExp);
            var scheduleParam = new SqlParameter("@schedule", Schedule);
            var userparam = new SqlParameter("@user", userid);
            var selectedLanguageNames = selectedLanguages.Cast<DataGridViewRow>()
            .Select(row => row.Cells["Language"].Value.ToString())
            .ToList();

            var languagesParam = new SqlParameter("@languages", string.Join(" OR ", selectedLanguageNames.Select(lang => $"CONTAINS(v.Languages, '{lang}')")));

            var result = db.Applicants
                .FromSqlRaw("SELECT a.* " +
                            "FROM Applicants a " +
                            "INNER JOIN Education educ ON a.Education = educ.educationid " +
                            "INNER JOIN Positions pos ON a.Position = pos.positionid " +
                            "WHERE educ.Education LIKE @educationparam " +
                            "AND pos.Position LIKE  @position " +
                            "AND a.gender LIKE @gender " +
                            "AND a.Driver_license = @driverLicense " +
                            "AND a.Salary >= @salary " +
                            "AND a.City LIKE @city " +
                            "AND a.PC_knowledge = @pcKnowledge " +
                            "AND a.work_experience <= @workExperience " +
                            "AND a.Schedule LIKE @schedule " +
                            "AND a.Applicantid != @user ",
                            educationParam, positionParam, genderParam, driverLicenseParam, salaryParam, cityParam, pcKnowledgeParam, workExperienceParam, scheduleParam, userparam).ToList();
            var filteredVacancies = result.Where(vacancy => selectedLanguageNames.All(language => vacancy.Languages.Contains(language))).ToList();

            var vacsearch = (from appl in filteredVacancies
                             join educ in db.Educations on appl.Education equals educ.Educationid
                             join pos in db.Positions on appl.Position equals pos.Positionid
                             select new
                             {
                                 Salary = appl.Salary,
                                 График = appl.Schedule,
                                 Applicantid = appl.Applicantid,
                                 ВодПрава = appl.DriverLicense,
                                 Языки = appl.Languages,
                                 Образование = educ.Education1,
                                 Пол = appl.Gender,
                                 Должность = pos.Position1,
                                 Город = appl.City,
                                 Опыт = appl.WorkExperience,
                                 ЗнаниПк = appl.PcKnowledge
                             }).ToList();
            applicants.DataSource = vacsearch;
            if (vacsearch.Count() != 0) applicants.Columns["Applicantid"].Visible = false;
        }
    }
}
