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
    public class EmployerService
    {
        private EmploymentAgencyContext db;
        private int userid;
        private Views.Employer employer;
        private AllVacancises allVacancises;
        private MyReq myReq;
        private Responded responded;

        public EmployerService(Views.Employer employer, EmploymentAgencyContext db, int userid)
        {
            this.db = db;
            this.userid = userid;
            this.employer = employer;
        }

        public EmployerService(AllVacancises allVacancises, EmploymentAgencyContext db, int userid)
        {
            this.allVacancises = allVacancises;
            this.db = db;
            this.userid = userid;
        }

        public EmployerService(MyReq myReq, EmploymentAgencyContext db, int userid)
        {
            this.myReq = myReq;
            this.db = db;
            this.userid = userid;
        }

        public EmployerService(Responded responded, EmploymentAgencyContext db, int userid)
        {
            this.responded = responded;
            this.db = db;
            this.userid = userid;
        }

        public void ViewEmployer(int vacancyid)
        {
            /*var isEmployer = db.Employers.Any(p => p.Userid == userid);
            var empid = db.Employers.Where(p => p.Userid == userid).First();*/
            var isVacancy = db.Vacancies.Any(p =>p.Vacancyid == vacancyid);
            if (isVacancy)
            {
                var vacid = db.Vacancies.Where(p => p.Vacancyid == vacancyid).First();
                var emp = db.Employers.Where(p => p.Empoyerid == vacid.Employer).First();
                //var vac = db.Vacancies.Where(p => p.Employer == emp.Empoyerid && p.Vacancyid == vacancyid).First();
                var educ = db.Educations.Where(p => p.Educationid == vacid.Education).First().Education1;
                var Pos = db.Positions.Where(p => p.Positionid == vacid.Position).First().Position1;
                bool? dl = vacid.DriverLicense;
                bool? Pc = vacid.PcKnowledge;
                employer.ViewthisEmployer(emp.Fio, vacid.Gender, emp.PhoneNumber, emp.Company, emp.Address,
                    educ, Pos, dl.Value, vacid.Salary.ToString(),
                    vacid.City, Pc.Value, vacid.Languages, vacid.WorkExperience, vacid.Schedule);
            }
        }
        public void AddEmployer(string Fio, string Gender, string NameofCompany, string Address, string Phonenumber, string education, string Pos, bool drivelic, string salary, string city, bool Pc_kn, string Languages, string WorkExp, string Schedule,int userid)
        {
            try
            {
                if (Fio == "") throw new Exception();
                if (Phonenumber == "") throw new Exception();
                if (Address == "") throw new Exception();
                var Positionindb = db.Positions.Where(p => p.Position1 == Pos).First().Positionid;
                var educindb = db.Educations.Where(p => p.Education1 == education).First().Educationid;
                int salarytodb;
                bool salaryparse = int.TryParse(salary, out salarytodb);
                if (salary == "" || salaryparse == false) throw new Exception();
                var Emp = db.Employers.Where(p => p.Userid == userid && p.Company == NameofCompany).Count();
                string[] Lang = Languages.Split(',');
                if (Emp == 0)
                {
                    db.Employers.Add(new Models.Employer
                    {
                        Fio = Fio,
                        Userid = userid,
                        Company = NameofCompany,
                        PhoneNumber = Phonenumber,
                        Address = Address
                    });
                    db.SaveChanges();
                }
                var employers = db.Employers.OrderByDescending(p => p.Empoyerid).First();
                db.Vacancies.Add(new Models.Vacancy
                {
                    Employer = employers.Empoyerid,
                    Openness = true,
                    PlacementDate = DateTime.Now,
                    Salary = int.Parse(salary),
                    Schedule = Schedule,
                    DriverLicense = drivelic,
                    Languages = Languages,
                    Gender = Gender,
                    Position = Positionindb,
                    Education = educindb,
                    City = city,
                    WorkExperience = WorkExp,
                    PcKnowledge = Pc_kn
                });
                db.SaveChanges();
                var vac = db.Vacancies.OrderByDescending(x => x.Vacancyid).FirstOrDefault();
                foreach (var lang in Lang)
                {
                    var langindb = db.VacancyLanguages.Any(p => p.Vacancy == vac.Vacancyid && p.Language == lang);
                    if (!langindb)
                    {
                        db.VacancyLanguages.Add(new VacancyLanguage
                        {
                            Vacancy = vac.Vacancyid,
                            Language = lang
                        });
                        db.SaveChanges();
                    }
                }
                MessageBox.Show("Ваша анкета добавлена");
            }
            catch
            {
                MessageBox.Show("Проверьте, все ли поля заполнены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        public void GetVacancis(DataGridView gridView, int userid)
        {
            var vacancisofuser =    (from vac in db.Vacancies
                                    join emp in db.Employers on vac.Employer equals emp.Empoyerid
                                    join educ in db.Educations on vac.Education equals educ.Educationid
                                    join pos in db.Positions on vac.Position equals pos.Positionid
                                    where emp.Userid == userid && vac.Openness == true
                                    select new
                                    {
                                        Fio = emp.Fio,
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
            gridView.DataSource = vacancisofuser;
            gridView.Columns["Vacancyid"].Visible = false;
        }

        public void GetAll(DataGridView vacs,int userid)
        {
            var applicants = (from appl in db.Applicants
                              join educ in db.Educations on appl.Education equals educ.Educationid
                              join pos in db.Positions on appl.Position equals pos.Positionid
                              where appl.Applicantid != userid
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
            vacs.DataSource = applicants;
            vacs.Columns["Applicantid"].Visible = false;
        }
        public void GetByVacancy(DataGridViewRow row,DataGridView gridView)
        {
            int vacancyid = (int)row.Cells["Vacancyid"].Value;
            var Applvacansyrequests = (from requests in db.ApplicantToVacancies
                                       join appl in db.Applicants on requests.Userid equals appl.Applicantid
                                       join educ in db.Educations on appl.Education equals educ.Educationid
                                       join pos in db.Positions on appl.Position equals pos.Positionid
                                       where requests.Userflag == "applicant" && requests.Vacancyid == vacancyid
                                       select new {
                                            Fio = appl.Fio,
                                            Applicantid = appl.Applicantid,
                                            Vacancyid = vacancyid,
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
            gridView.DataSource = Applvacansyrequests;
            gridView.Columns["Vacancyid"].Visible = false;
        }
        public void Accept(DataGridView gridView,int vacancyid)
        {
            try
            {
                var selectedRows = gridView.SelectedRows;
                foreach (DataGridViewRow row in selectedRows)
                {
                    int applicantid = (int)row.Cells["Applicantid"].Value;
                    var appl = db.ApplicantToVacancies.Any(p => p.Userid == (int)row.Cells["Applicantid"].Value && p.Vacancyid == vacancyid && p.Userflag == "Employer");
                    var vac = db.ApplicantToVacancies.Any(p => p.Vacancyid == vacancyid && p.Userflag == "applicant" && p.Userid == (int)row.Cells["Applicantid"].Value);
                    if (vac)
                    {
                        var vacsalary = db.Vacancies.Where(p => p.Vacancyid == vacancyid).First().Salary;
                        db.Deals.Add(new Deal
                        {
                            Vacancy = vacancyid,
                            Commission = vacsalary * 0.3m,
                            Dateofpreparation = DateTime.Now
                        });
                        db.SaveChanges();
                        var vacopen = db.Vacancies.Where(p => p.Vacancyid == vacancyid).First();
                        vacopen.Openness = false;
                        db.SaveChanges();
                        var deal = db.Deals.OrderByDescending(p => p.Dealid).First();
                        db.DealApplicants.Add(new DealApplicant
                        {
                            Dealid = deal.Dealid,
                            Applicantid = (int)row.Cells["Applicantid"].Value
                        });
                        db.SaveChanges();
                        var applfio = (from applic in db.Applicants
                                       join appltovac in db.ApplicantToVacancies on applic.Applicantid equals appltovac.Userid
                                       where appltovac.Userid == (int)row.Cells["Applicantid"].Value
                                       select applic.Fio).First();
                        MessageBox.Show("Заявка для Соискателя " + applfio + " одобрена");
                        db.ApplicantToVacancies.RemoveRange(db.ApplicantToVacancies.Where(p => p.Vacancyid == vacancyid && p.Userid == (int)row.Cells["Applicantid"].Value).ToList());
                        db.SaveChanges();
                        /*var vacancytodel = db.Vacancies.Where(p => p.Vacancyid == vacancyid).First();
                        db.Vacancies.Remove(vacancytodel);
                        db.SaveChanges();
                        var applicant = db.Applicants.Remove(db.Applicants.Where(p => p.Applicantid == (int)row.Cells["Applicantid"].Value).First());
                        db.SaveChanges();*/
                        break;
                    }
                    if (!appl)
                    {
                        var vacancynew = db.Vacancies.Where(p => p.Vacancyid == vacancyid).First();
                        db.ApplicantToVacancies.Add(new ApplicantToVacancy
                        {
                            Employerid = vacancynew.Employer,
                            Vacancyid = vacancyid,
                            Userflag = "Employer",
                            Userid = applicantid
                        });
                        db.SaveChanges();
                        MessageBox.Show("Ваша заявка подана");
                    }
                    else
                    {
                        MessageBox.Show("Ваша заявка уже подана");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Необходимо выбрать соискателя");
            }
        }
        public void DeleteRequest(DataGridViewSelectedRowCollection selectedRows)
        {
            try
            {
                foreach(DataGridViewRow item in selectedRows)
                {
                    var vac = db.Vacancies.Where(p => p.Vacancyid == (int)item.Cells["Vacancyid"].Value).First();
                    db.Vacancies.Remove(vac);
                    db.SaveChanges();
                    var vacancisofemp = db.Vacancies.Where(p => p.Employer == (int)item.Cells["Employer"].Value).ToList();
                    if (vacancisofemp.Count == 0)
                    {
                        var emp = db.Employers.Where(p => p.Empoyerid == (int)item.Cells["Employer"].Value).First();
                        db.Employers.Remove(emp);
                        db.SaveChanges();
                    }
                }
            }
            catch
            {

            }
        }

        public void DeleteOffersofAppl(DataGridView vacAppl)
        {
            try
            {
                foreach (DataGridViewRow item in vacAppl.SelectedRows)
                {
                    var appl = db.Applicants.Where(p => p.Applicantid == (int)item.Cells["Applicantid"].Value).First();
                    var vac = db.Vacancies.Where(p => p.Vacancyid == (int)item.Cells["Vacancyid"].Value).First();
                    var vacofappl = db.ApplicantToVacancies.Where(p => p.Userid == appl.Applicantid && p.Vacancyid == vac.Vacancyid && p.Userflag == "applicant").First();
                    db.ApplicantToVacancies.Remove(vacofappl);
                    db.SaveChanges();
                }
            }
            catch
            {

            }
        }
        public void Update(string Fio, string Gender, string NameofCompany, string Address, string Phonenumber, string education, string Pos, bool drivelic, string salary, string city, bool Pc_kn, string Languages, string WorkExp, string Schedule,int vacacnyid)
        {
            try
            {
                if (Fio == "") throw new Exception();
                if (Phonenumber == "") throw new Exception();
                if (Address == "") throw new Exception();
                var Positionindb = db.Positions.Where(p => p.Position1 == Pos).First().Positionid;
                var educindb = db.Educations.Where(p => p.Education1 == education).First().Educationid;
                int salarytodb;
                bool salaryparse = int.TryParse(salary, out salarytodb);
                if (salary == "" || salaryparse == false || salarytodb <= 0 || int.Parse(WorkExp) < 0) throw new Exception();
                string[] Lang = Languages.Split(',');
                var vacemp = db.Vacancies.Where(p => p.Vacancyid == vacacnyid).First();
                var employer = db.Employers.Where(p => p.Userid == userid && p.Empoyerid == vacemp.Employer).First();
                employer.Company = NameofCompany;
                employer.Address = Address;
                employer.Fio = Fio;
                employer.PhoneNumber = Phonenumber;
                db.SaveChanges();
                var vac = db.Vacancies.Where(p => p.Vacancyid == vacacnyid).First();
                vac.City = city;
                vac.Education = educindb;
                vac.Gender = Gender;
                vac.PcKnowledge = Pc_kn;
                vac.Position = Positionindb;
                vac.Salary = salarytodb;
                vac.Schedule = Schedule;
                vac.WorkExperience = WorkExp;
                vac.Languages = Languages;
                db.VacancyLanguages.RemoveRange(db.VacancyLanguages.Where(p => p.Vacancy == vacacnyid).ToList());
                db.SaveChanges();
                foreach (var lang in Lang)
                {
                    var langindb = db.VacancyLanguages.Any(p => p.Vacancy == vac.Vacancyid && p.Language == lang);
                    if (!langindb)
                    {
                        db.VacancyLanguages.Add(new VacancyLanguage
                        {
                            Vacancy = vac.Vacancyid,
                            Language = lang
                        });
                        db.SaveChanges();
                    }
                }
                MessageBox.Show("Ваша анкета обновлена");
            }
            catch
            {
                MessageBox.Show("Проверьте, все ли поля заполнены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        public void Serch(string Gender, string education, string Pos, bool drivelic, int salary, string city, bool Pc_kn, DataGridViewSelectedRowCollection selectedLanguages, int WorkExp, string Schedule,DataGridView applicants, int userid)
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
                            educationParam, positionParam, genderParam, driverLicenseParam, salaryParam, cityParam, pcKnowledgeParam, workExperienceParam, scheduleParam,userparam).ToList();
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
        public void Serchmy(string Gender, string education, string Pos, bool drivelic, int salary, string city, bool Pc_kn, string Languages, int WorkExp, string Schedule, DataGridView applicants, int userid,int vacancyid)
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

            var selectedLanguageNames = Languages.Split(",");
            var languagesParam = new SqlParameter("@languages", string.Join(" OR ", selectedLanguageNames.Select(lang => $"CONTAINS(a.Languages, '{lang}')")));

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
