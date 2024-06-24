using Azure.Core;
using Employment_Agency.Models;
using Employment_Agency.Views;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employment_Agency.Controllers
{
    public class ApplicantService
    {
        private AddApplicant addApplicant;
        private EmploymentAgencyContext db;
        private AllVacancises allVacancises;
        private int userid;
        private Employers_offers employers_offers;
        private Responded responded;

        public ApplicantService(AddApplicant addApplicant, EmploymentAgencyContext db, int userid)
        {
            this.addApplicant = addApplicant;
            this.db = db;
            this.userid = userid;
        }

        public ApplicantService(Employers_offers employers_offers, EmploymentAgencyContext db, int userid)
        {
            this.employers_offers = employers_offers;
            this.db = db;
            this.userid = userid;
        }

        public ApplicantService(Responded responded, EmploymentAgencyContext db, int userid)
        {
            this.responded = responded;
            this.db = db;
            this.userid = userid;
        }
        public ApplicantService(AllVacancises allVacancises, EmploymentAgencyContext db, int userid)
        {
            this.allVacancises = allVacancises;
            this.db = db;
            this.userid = userid;
        }
        public void ViewApplicant()
        {
            var ISApplicant = db.Applicants.Any(p => p.Applicantid == userid);
            if (ISApplicant)
            {
                var Applicant = db.Applicants.Where(p => p.Applicantid == userid).First();
                var educ = db.Educations.Where(p => p.Educationid == Applicant.Education).First().Education1;
                var Pos = db.Positions.Where(p => p.Positionid == Applicant.Position).First().Position1;
                bool? dl = Applicant.DriverLicense;
                bool? Pc = Applicant.PcKnowledge;
                var date = Applicant.DateofBirth;
                addApplicant.ViewthisApplicant(Applicant.Fio, Applicant.Gender, date.Value, Applicant.PhoneNumber,
                    educ, Pos, dl.Value, Applicant.Salary.ToString(),
                    Applicant.City, Pc.Value, Applicant.Languages, Applicant.WorkExperience, Applicant.Schedule);
            }
        }
        public void AddApplicant(string Fio, string Gender, DateTime dateofbirth, string Phonenumber, string education, string Pos, bool drivelic, string salary, string city, bool Pc_kn, string Languages, string WorkExp, string Schedule)
        {
            try
            {
                var Appl = db.Applicants.Any(p => p.Applicantid == userid);
                if (Appl)
                {
                    if (Fio == "") throw new Exception();
                    if (Phonenumber == "") throw new Exception();
                    var Positionindbup = db.Positions.Where(p => p.Position1 == Pos).First().Positionid;
                    var educindbup = db.Educations.Where(p => p.Education1 == education).First().Educationid;
                    int salarytodbup;
                    bool salaryparseup = int.TryParse(salary, out salarytodbup);
                    if (salary == "" || salaryparseup == false || salarytodbup <= 0 || int.Parse(WorkExp) < 0) throw new Exception();
                    string[] Langup = Languages.Split(',');
                    var aplupdate = db.Applicants.Where(p => p.Applicantid == userid).First();
                    aplupdate.Languages = Languages;
                    aplupdate.Fio = Fio;
                    aplupdate.Gender = Gender;
                    aplupdate.DateofBirth = dateofbirth;
                    aplupdate.PhoneNumber = Phonenumber;
                    aplupdate.Education = educindbup;
                    aplupdate.Position = Positionindbup;
                    aplupdate.DriverLicense = drivelic;
                    aplupdate.Salary = salarytodbup;
                    aplupdate.City = city;
                    aplupdate.PcKnowledge = Pc_kn;
                    aplupdate.WorkExperience = WorkExp;
                    aplupdate.Schedule = Schedule;
                    db.SaveChanges();
                    db.ApplicantLanguages.RemoveRange(db.ApplicantLanguages.Where(p => p.Applicant == userid).ToList());
                    db.SaveChanges();
                    foreach (var lang in Langup)
                    {
                        var islangofAppl = db.ApplicantLanguages.Any(p => p.Applicant == userid && p.Language == lang);
                        if (!islangofAppl)
                        {
                            db.ApplicantLanguages.Add(new ApplicantLanguage
                            {
                                Applicant = userid,
                                Language = lang
                            });
                            db.SaveChanges();
                        }
                    }
                    MessageBox.Show("Анкета изменена");
                    return;
                }
                if (Fio == "") throw new Exception();
                if (Phonenumber == "") throw new Exception();
                var Positionindb = db.Positions.Where(p => p.Position1 == Pos).First().Positionid;
                var educindb = db.Educations.Where(p => p.Education1 == education).First().Educationid;
                int salarytodb;
                bool salaryparse = int.TryParse(salary, out salarytodb);
                if (salary == "" || salaryparse == false) throw new Exception();
                string[] Lang = Languages.Split(',');
                db.Applicants.Add(new Applicant
                {
                    Applicantid = userid,
                    Fio = Fio,
                    PlacementDate = DateTime.Now,
                    DateofBirth = dateofbirth,
                    Salary = int.Parse(salary),
                    Schedule = Schedule,
                    DriverLicense = drivelic,
                    Languages = Languages,
                    Gender = Gender,
                    Position = Positionindb,
                    Education = educindb,
                    City = city,
                    PhoneNumber = Phonenumber,
                    WorkExperience = WorkExp,
                    PcKnowledge = Pc_kn
                });
                db.SaveChanges();
                foreach (var lang in Lang)
                {
                    var islangofAppl = db.ApplicantLanguages.Any(p => p.Applicant == userid && p.Language == lang);
                    if (!islangofAppl)
                    {
                        db.ApplicantLanguages.Add(new ApplicantLanguage
                        {
                            Applicant = userid,
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
            gridView.DataSource = (from vac in db.Vacancies
                                   join emp in db.Employers on vac.Employer equals emp.Empoyerid
                                   join educ in db.Educations on vac.Education equals educ.Educationid
                                   join pos in db.Positions on vac.Position equals pos.Positionid
                                   where emp.Userid != userid && vac.Openness == true
                                   select new
                                   {
                                       Fio = emp.Fio,
                                       Company = emp.Company,
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
            gridView.Columns["Vacancyid"].Visible = false;
        }
        public void Send(DataGridView vacancises, int userid)
        {
            try
            {
                var selectedRows = vacancises.SelectedRows;
                foreach (DataGridViewRow row in selectedRows)
                {
                    var vacancy = db.Vacancies.Where(p => p.Vacancyid == (int)row.Cells["Vacancyid"].Value).First();
                    var appl = db.ApplicantToVacancies.Any(p => p.Userid == userid && p.Vacancyid == (int)row.Cells["Vacancyid"].Value && p.Userflag == "applicant");
                    var vac = db.ApplicantToVacancies.Any(p => p.Userid == userid && p.Vacancyid == (int)row.Cells["Vacancyid"].Value && p.Userflag == "Employer");
                    if (vac)
                    {
                        var vacsalary = db.Vacancies.Where(p => p.Vacancyid == (int)row.Cells["Vacancyid"].Value).First().Salary;
                        db.Deals.Add(new Deal
                        {
                            Vacancy = (int)row.Cells["Vacancyid"].Value,
                            Commission = vacsalary * 0.3m,
                            Dateofpreparation = DateTime.Now
                        });
                        db.SaveChanges();
                        var vacopen = db.Vacancies.Where(p => p.Vacancyid == (int)row.Cells["Vacancyid"].Value).First();
                        vacopen.Openness = false;
                        db.SaveChanges();
                        var deals = db.Deals.OrderByDescending(p => p.Dealid).First();
                        db.DealApplicants.Add(new DealApplicant
                        {
                            Dealid = deals.Dealid,
                            Applicantid = userid
                        });
                        db.SaveChanges();
                        var company = (from vacan in db.Vacancies
                                       join empl in db.Employers on vacan.Employer equals empl.Empoyerid
                                       where vacan.Vacancyid == (int)row.Cells["Vacancyid"].Value
                                       select empl.Company).First();
                        MessageBox.Show("Заявка для компании " + company + " одобрена");
                        db.ApplicantToVacancies.RemoveRange(db.ApplicantToVacancies.Where(p => p.Vacancyid == (int)row.Cells["Vacancyid"].Value && p.Userid == userid).ToList());
                        db.SaveChanges();
                        /*var vacancytodel = db.Vacancies.Where(p => p.Vacancyid == (int)row.Cells["Vacancyid"].Value).First();
                        db.Vacancies.Remove(vacancytodel);
                        db.SaveChanges();
                        var applicant = db.Applicants.Remove(db.Applicants.Where(p => p.Applicantid == (int)row.Cells["Applicantid"].Value).First());
                        db.SaveChanges();*/
                        break;
                    }
                    var deal = db.Deals.Any(p => p.Vacancy == (int)row.Cells["Vacancyid"].Value);
                    if (!appl && !deal)
                    {
                        var Appl = db.Applicants.Any(p => p.Applicantid == userid);
                        if (!Appl)
                        {
                            MessageBox.Show("Прежде нужно создать заявку соискателя");
                            return;
                        }
                        var vacancyemployer = db.Vacancies.Where(p => p.Vacancyid == (int)row.Cells["Vacancyid"].Value).First();
                        db.ApplicantToVacancies.Add(new ApplicantToVacancy
                        {
                            Employerid = vacancyemployer.Employer,
                            Vacancyid = (int)row.Cells["Vacancyid"].Value,
                            Userflag = "applicant",
                            Userid = userid
                        });
                        db.SaveChanges();
                        MessageBox.Show("Ваша заявка подана");
                        break;
                    }
                    if (deal)
                    {
                        MessageBox.Show("Сделка уже заключена");
                    }
                    else
                    {
                        MessageBox.Show("Ваша заявка уже подана");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Необходимо выбрать вакансии");
            }
        }
        public void Deleteappl(int userid)
        {
            var appl = db.Applicants.Where(p => p.Applicantid == userid).First();
            db.Applicants.Remove(appl);
            db.SaveChanges();
            addApplicant.Close();
        }
        public void GetVacancistome(int userid, DataGridView gridView)
        {
            var Applvacansyrequests = (from requests in db.ApplicantToVacancies
                                       join emp in db.Employers on requests.Employerid equals emp.Empoyerid
                                       join appl in db.Applicants on requests.Userid equals appl.Applicantid
                                       join educ in db.Educations on appl.Education equals educ.Educationid
                                       join pos in db.Positions on appl.Position equals pos.Positionid
                                       join vac in db.Vacancies on requests.Vacancyid equals vac.Vacancyid
                                       where requests.Userflag == "Employer" && requests.Userid == userid
                                       select new
                                       {
                                           Fio = emp.Fio,
                                           Company = emp.Company,
                                           Vacancyid = requests.Vacancyid,
                                           Salary = vac.Salary,
                                           График = vac.Schedule,
                                           ВодПрава = vac.DriverLicense,
                                           Языки = vac.Languages,
                                           Образование = educ.Education1,
                                           Пол = appl.Gender,
                                           Должность = pos.Position1,
                                           Город = vac.City,
                                           Опыт = vac.WorkExperience,
                                           ЗнаниеПк = vac.PcKnowledge
                                       }).ToList();
            gridView.DataSource = Applvacansyrequests;
            gridView.Columns["Vacancyid"].Visible = false;
        }
        public void DeleteOffersofAppl(DataGridView vaclist)
        {
            try
            {
                foreach (DataGridViewRow item in vaclist.SelectedRows)
                {
                    var vac = db.ApplicantToVacancies.Where(p => p.Vacancyid == (int)item.Cells["Vacancyid"].Value && p.Userid == userid && p.Userflag == "Employer").First();
                    db.ApplicantToVacancies.Remove(vac);
                    db.SaveChanges();
                    /*var appl = db.Applicants.Where(p => p.Applicantid == (int)item.Cells["Employerid"].Value).First();
                    var vac = db.Vacancies.Where(p => p.Vacancyid == (int)item.Cells["Vacancyid"].Value).First();
                    var vacofappl = db.ApplicantToVacancies.Where(p => p.Userid == appl.Applicantid && p.Vacancyid == vac.Vacancyid && p.Userflag == "Employer").First();
                    db.ApplicantToVacancies.Remove(vacofappl);
                    db.SaveChanges();*/
                }
            }
            catch
            {

            }
        }

        public void Serch(string Gender, string Employer, string education, string Pos, bool drivelic, int salary, string city, bool Pc_kn, DataGridViewSelectedRowCollection selectedLanguages, int WorkExp, string Schedule, DataGridView vacs,int userid)
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
            if(vacsearch.Count() != 0) vacs.Columns["Vacancyid"].Visible = false;
        }

        public void Serchmy(string gender, string educ, string pos, bool checked1, int s, string text, bool checked2, string? languages, int w, string sched, DataGridView vacs, int userid)
        {
            var genderParam = new SqlParameter("@gender", gender);
            var educParam = new SqlParameter("@education", educ);
            var posParam = new SqlParameter("@position", pos);
            var salaryParam = new SqlParameter("@salary", s);
            var cityParam = new SqlParameter("@city", "%" + text + "%");
            var workExpParam = new SqlParameter("@workExperience", w);
            var scheduleParam = new SqlParameter("@schedule", sched);
            var userParam = new SqlParameter("@user", userid);

            var selectedLanguageNames = languages.Split(",");
            var languagesParam = new SqlParameter("@languages", string.Join(" OR ", selectedLanguageNames.Select(lang => $"CONTAINS(v.Languages, '{lang}')")));

            var result = db.Vacancies
                .FromSqlRaw("SELECT v.* " +
                            "FROM Vacancies v " +
                            "INNER JOIN Employers e ON v.Employer = e.Empoyerid " +
                            "INNER JOIN Education educ ON v.Education = educ.educationid " +
                            "INNER JOIN Positions pos ON v.Position = pos.positionid " +
                            "WHERE v.gender LIKE @gender " +
                            "AND (educ.Education LIKE @education) " +
                            "AND (pos.Position LIKE @position) " +
                            "AND (v.Openness = 1) " +
                            "AND (v.Salary >= @salary) " +
                            "AND (v.City LIKE @city) " +
                            "AND (v.work_experience <= @workExperience) " +
                            "AND (v.Schedule LIKE @schedule) " +
                            "AND (e.Userid != @user) ",
                            genderParam, educParam, posParam, salaryParam, cityParam, workExpParam, scheduleParam, userParam, languagesParam)
                .ToList();
            var filteredVacancies = result.Where(vacancy => selectedLanguageNames.All(language => vacancy.Languages.Contains(language))).ToList();

            var vacsearch = (from vac in filteredVacancies
                             join emp in db.Employers on vac.Employer equals emp.Empoyerid
                             join educs in db.Educations on vac.Education equals educs.Educationid
                             join poss in db.Positions on vac.Position equals poss.Positionid
                             select new
                             {
                                 Company = emp.Company,
                                 НомерТелефона = emp.PhoneNumber,
                                 Vacancyid = vac.Vacancyid,
                                 Salary = vac.Salary,
                                 График = vac.Schedule,
                                 ВодПрава = vac.DriverLicense,
                                 Языки = vac.Languages,
                                 Образование = educs.Education1,
                                 Пол = vac.Gender,
                                 Должность = poss.Position1,
                                 Город = vac.City,
                                 Опыт = vac.WorkExperience,
                                 ЗнаниеПК = vac.PcKnowledge
                             }).ToList();

            vacs.DataSource = vacsearch;
            if (vacsearch.Count() != 0)
            {
                vacs.Columns["Vacancyid"].Visible = false;
            }
        }
    }
}
