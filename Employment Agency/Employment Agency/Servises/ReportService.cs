using Employment_Agency.Models;
using Employment_Agency.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment_Agency.Servises
{
    public class ReportService
    {
        private Reports reports;
        private EmploymentAgencyContext db;
        private int userid;
        private ReportsforUser reportsforUser;

        public ReportService(Reports reports, EmploymentAgencyContext db, int userid)
        {
            this.reports = reports;
            this.db = db;
            this.userid = userid;
        }

        public ReportService(ReportsforUser reportsforUser, EmploymentAgencyContext db, int userid)
        {
            this.reportsforUser = reportsforUser;
            this.db = db;
            this.userid = userid;
        }

        public void GetDeals(DataGridView deals,int userid)
        {
            var mydeals = (from deal in db.Deals
                         join vac in db.Vacancies on deal.Vacancy equals vac.Vacancyid
                         join emp in db.Employers on vac.Employer equals emp.Empoyerid
                           join educ in db.Educations on vac.Education equals educ.Educationid
                         join pos in db.Positions on vac.Position equals pos.Positionid
                         where emp.Userid == userid && vac.Openness == false
                         select new
                         {
                             Компания = emp.Company,
                             Зарплата = vac.Salary,
                             Коммисионные = deal.Commission,
                             Vacancyid = vac.Vacancyid,
                             Dealid = deal.Dealid,
                             образование = educ.Education1,
                             Должность = pos.Position1,
                             ДатаРазмещение = vac.PlacementDate
                         }).ToList();
            deals.DataSource = mydeals;
            deals.Columns["Vacancyid"].Visible = false;
            deals.Columns["Dealid"].Visible = false;
        }
        public void GetDeals(DataGridView deals)
        {
            var mydeals = (from deal in db.Deals
                           join vac in db.Vacancies on deal.Vacancy equals vac.Vacancyid
                           join emp in db.Employers on vac.Employer equals emp.Empoyerid
                           join educ in db.Educations on vac.Education equals educ.Educationid
                           join pos in db.Positions on vac.Position equals pos.Positionid
                           where vac.Openness == false
                           select new
                           {
                               Компания = emp.Company,
                               Зарплата = vac.Salary,
                               Коммисионные = deal.Commission,
                               Vacancyid = vac.Vacancyid,
                               Dealid = deal.Dealid,
                               образование = educ.Education1,
                               Должность = pos.Position1,
                               ДатаРазмещение = vac.PlacementDate
                           }).ToList();
            deals.DataSource = mydeals;
            deals.Columns["Vacancyid"].Visible = false;
            deals.Columns["Dealid"].Visible = false;
        }
        public void GetDealApplicants(DataGridView deals,DataGridView applicants)
        {
            try
            {
                int deal = (int)deals.SelectedRows[0].Cells["Dealid"].Value;
                var dealapplicants = (from dealappl in db.DealApplicants
                                      join appl in db.Applicants on dealappl.Applicantid equals appl.Applicantid
                                      join educ in db.Educations on appl.Education equals educ.Educationid
                                      join pos in db.Positions on appl.Position equals pos.Positionid
                                      where dealappl.Dealid == deal
                                      select new
                                      {
                                          Fio = appl.Fio,
                                          Salary = appl.Salary,
                                          График = appl.Schedule,
                                          ВодПрава = appl.DriverLicense,
                                          Языки = appl.Languages,
                                          Образование = educ.Education1,
                                          Пол = appl.Gender,
                                          Должность = pos.Position1,
                                          Город = appl.City,
                                          Опыт = appl.WorkExperience,
                                          ЗнаниПк = appl.PcKnowledge
                                      }).ToList();
                applicants.DataSource = dealapplicants;
            }
            catch
            {

            }
        }

        public void GetDealApplicantsDate(DataGridView deals,DateTimePicker f, DateTimePicker s,int userid)
        {
            var mydeals = (from deal in db.Deals
                           join vac in db.Vacancies on deal.Vacancy equals vac.Vacancyid
                           join emp in db.Employers on vac.Employer equals emp.Empoyerid
                           join educ in db.Educations on vac.Education equals educ.Educationid
                           join pos in db.Positions on vac.Position equals pos.Positionid
                           where emp.Userid == userid && vac.Openness == false && deal.Dateofpreparation >= f.Value && deal.Dateofpreparation <= s.Value
                           select new
                           {
                               Компания = emp.Company,
                               Зарплата = vac.Salary,
                               Коммисионные = deal.Commission,
                               Vacancyid = vac.Vacancyid,
                               Dealid = deal.Dealid,
                               образование = educ.Education1,
                               Должность = pos.Position1,
                               ДатаРазмещение = vac.PlacementDate
                           }).ToList();
            deals.DataSource = mydeals;
            deals.Columns["Vacancyid"].Visible = false;
            deals.Columns["Dealid"].Visible = false;
        }
        public void GetDealApplicantsDate(DataGridView deals, DateTimePicker f, DateTimePicker s)
        {
            var mydeals = (from deal in db.Deals
                           join vac in db.Vacancies on deal.Vacancy equals vac.Vacancyid
                           join emp in db.Employers on vac.Employer equals emp.Empoyerid
                           join educ in db.Educations on vac.Education equals educ.Educationid
                           join pos in db.Positions on vac.Position equals pos.Positionid
                           where vac.Openness == false && deal.Dateofpreparation >= f.Value && deal.Dateofpreparation <= s.Value
                           select new
                           {
                               Компания = emp.Company,
                               Зарплата = vac.Salary,
                               Коммисионные = deal.Commission,
                               Vacancyid = vac.Vacancyid,
                               Dealid = deal.Dealid,
                               образование = educ.Education1,
                               Должность = pos.Position1,
                               ДатаРазмещение = vac.PlacementDate
                           }).ToList();
            deals.DataSource = mydeals;
            deals.Columns["Vacancyid"].Visible = false;
            deals.Columns["Dealid"].Visible = false;
        }
        public void GetEmpouersforRequest(int userid, DataGridView employers)
        {
            var mydeals = (from mydeal in db.DealApplicants
                           join deal in db.Deals on mydeal.Dealid equals deal.Dealid
                           join vac in db.Vacancies on deal.Vacancy equals vac.Vacancyid
                           join emp in db.Employers on vac.Employer equals emp.Empoyerid
                           join educ in db.Educations on vac.Education equals educ.Educationid
                           join pos in db.Positions on vac.Position equals pos.Positionid
                           where vac.Openness == false && mydeal.Applicantid == userid
                           select new
                           {
                               Компания = emp.Company,
                               Зарплата = vac.Salary,
                               Коммисионные = deal.Commission,
                               образование = educ.Education1,
                               Должность = pos.Position1,
                               ДатаРазмещение = vac.PlacementDate
                           }).ToList();
            employers.DataSource = mydeals;
        }
        public void GetEmpouersforRequestDate(int userid, DataGridView employers,DateTimePicker f, DateTimePicker s)
        {
            var mydeals = (from mydeal in db.DealApplicants
                           join deal in db.Deals on mydeal.Dealid equals deal.Dealid
                           join vac in db.Vacancies on deal.Vacancy equals vac.Vacancyid
                           join emp in db.Employers on vac.Employer equals emp.Empoyerid
                           join educ in db.Educations on vac.Education equals educ.Educationid
                           join pos in db.Positions on vac.Position equals pos.Positionid
                           where vac.Openness == false && mydeal.Applicantid == userid && deal.Dateofpreparation >= f.Value && deal.Dateofpreparation <= s.Value
                           select new
                           {
                               Компания = emp.Company,
                               Зарплата = vac.Salary,
                               Коммисионные = deal.Commission,
                               образование = educ.Education1,
                               Должность = pos.Position1,
                               ДатаРазмещение = vac.PlacementDate
                           }).ToList();
            employers.DataSource = mydeals;
        }
        public void GetEmpouersforRequest(DataGridView employers)
        {
            var mydeals = (from mydeal in db.DealApplicants
                           join deal in db.Deals on mydeal.Dealid equals deal.Dealid
                           join vac in db.Vacancies on deal.Vacancy equals vac.Vacancyid
                           join emp in db.Employers on vac.Employer equals emp.Empoyerid
                           join educ in db.Educations on vac.Education equals educ.Educationid
                           join pos in db.Positions on vac.Position equals pos.Positionid
                           where vac.Openness == false 
                           select new
                           {
                               Компания = emp.Company,
                               Зарплата = vac.Salary,
                               Коммисионные = deal.Commission,
                               образование = educ.Education1,
                               Должность = pos.Position1,
                               ДатаРазмещение = vac.PlacementDate
                           }).ToList();
            employers.DataSource = mydeals;
        }
        public void GetEmpouersforRequestDate(DataGridView employers, DateTimePicker f, DateTimePicker s)
        {
            var mydeals = (from mydeal in db.DealApplicants
                           join deal in db.Deals on mydeal.Dealid equals deal.Dealid
                           join vac in db.Vacancies on deal.Vacancy equals vac.Vacancyid
                           join emp in db.Employers on vac.Employer equals emp.Empoyerid
                           join educ in db.Educations on vac.Education equals educ.Educationid
                           join pos in db.Positions on vac.Position equals pos.Positionid
                           where vac.Openness == false && deal.Dateofpreparation >= f.Value && deal.Dateofpreparation <= s.Value
                           select new
                           {
                               Компания = emp.Company,
                               Зарплата = vac.Salary,
                               Коммисионные = deal.Commission,
                               образование = educ.Education1,
                               Должность = pos.Position1,
                               ДатаРазмещение = vac.PlacementDate
                           }).ToList();
            employers.DataSource = mydeals;
        }
    }
}
