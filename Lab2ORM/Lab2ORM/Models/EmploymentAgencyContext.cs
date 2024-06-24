using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab2ORM.Models;

public partial class EmploymentAgencyContext : DbContext
{
    public EmploymentAgencyContext()
    {
    }

    public EmploymentAgencyContext(DbContextOptions<EmploymentAgencyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Applicant> Applicants { get; set; }

    public virtual DbSet<ApplicantLanguage> ApplicantLanguages { get; set; }

    public virtual DbSet<ApplicantToVacancy> ApplicantToVacancies { get; set; }

    public virtual DbSet<Deal> Deals { get; set; }

    public virtual DbSet<DealApplicant> DealApplicants { get; set; }

    public virtual DbSet<Education> Educations { get; set; }

    public virtual DbSet<Employer> Employers { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<TypesofUser> TypesofUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vacancy> Vacancies { get; set; }

    public virtual DbSet<VacancyLanguage> VacancyLanguages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=HOME-PC\\SQLEXPRESS01;Database=Employment agency;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Applicant>(entity =>
        {
            entity.Property(e => e.Applicantid).ValueGeneratedNever();
            entity.Property(e => e.City).HasMaxLength(30);
            entity.Property(e => e.DateofBirth).HasColumnType("date");
            entity.Property(e => e.DriverLicense).HasColumnName("Driver_license");
            entity.Property(e => e.Fio)
                .HasMaxLength(200)
                .HasColumnName("FIO");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .HasColumnName("gender");
            entity.Property(e => e.PcKnowledge).HasColumnName("PC_knowledge");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(60)
                .HasColumnName("Phone_number");
            entity.Property(e => e.PlacementDate)
                .HasColumnType("datetime")
                .HasColumnName("Placement_Date");
            entity.Property(e => e.Salary).HasColumnType("money");
            entity.Property(e => e.Schedule).HasMaxLength(20);
            entity.Property(e => e.WorkExperience)
                .HasMaxLength(20)
                .HasColumnName("work_experience");

            entity.HasOne(d => d.ApplicantNavigation).WithOne(p => p.Applicant)
                .HasForeignKey<Applicant>(d => d.Applicantid)
                .HasConstraintName("FK_Applicants_Users");

            entity.HasOne(d => d.EducationNavigation).WithMany(p => p.Applicants)
                .HasForeignKey(d => d.Education)
                .HasConstraintName("FK_Applicants_Education");

            entity.HasOne(d => d.PositionNavigation).WithMany(p => p.Applicants)
                .HasForeignKey(d => d.Position)
                .HasConstraintName("FK_Applicants_Positions");
        });

        modelBuilder.Entity<ApplicantLanguage>(entity =>
        {
            entity.ToTable("Applicant_Languages");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Language).HasMaxLength(40);

            entity.HasOne(d => d.ApplicantNavigation).WithMany(p => p.ApplicantLanguages)
                .HasForeignKey(d => d.Applicant)
                .HasConstraintName("FK_Applicant_Languages_Applicants1");
        });

        modelBuilder.Entity<ApplicantToVacancy>(entity =>
        {
            entity.ToTable("ApplicantToVacancy");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Userflag).HasMaxLength(40);

            entity.HasOne(d => d.User).WithMany(p => p.ApplicantToVacancies)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicantToVacancy_Applicants1");

            entity.HasOne(d => d.Vacancy).WithMany(p => p.ApplicantToVacancies)
                .HasForeignKey(d => d.Vacancyid)
                .HasConstraintName("FK_ApplicantToVacancy_Vacancies");
        });

        modelBuilder.Entity<Deal>(entity =>
        {
            entity.Property(e => e.Commission).HasColumnType("money");
            entity.Property(e => e.Dateofpreparation).HasColumnType("datetime");

            entity.HasOne(d => d.VacancyNavigation).WithMany(p => p.Deals)
                .HasForeignKey(d => d.Vacancy)
                .HasConstraintName("FK_Deals_Vacancies");
        });

        modelBuilder.Entity<DealApplicant>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            entity.HasOne(d => d.Applicant).WithMany(p => p.DealApplicants)
                .HasForeignKey(d => d.Applicantid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DealApplicants_Applicants");

            entity.HasOne(d => d.Deal).WithMany(p => p.DealApplicants)
                .HasForeignKey(d => d.Dealid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DealApplicants_Deals");
        });

        modelBuilder.Entity<Education>(entity =>
        {
            entity.ToTable("Education");

            entity.Property(e => e.Educationid).HasColumnName("educationid");
            entity.Property(e => e.Education1)
                .HasMaxLength(50)
                .HasColumnName("Education");
        });

        modelBuilder.Entity<Employer>(entity =>
        {
            entity.Property(e => e.Employerid).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Company).HasMaxLength(10);
            entity.Property(e => e.Fio).HasMaxLength(200);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(60)
                .HasColumnName("Phone_number");

            entity.HasOne(d => d.EmployerNavigation).WithOne(p => p.Employer)
                .HasForeignKey<Employer>(d => d.Employerid)
                .HasConstraintName("FK_Employers_Users");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.Property(e => e.Positionid).HasColumnName("positionid");
            entity.Property(e => e.Position1)
                .HasMaxLength(50)
                .HasColumnName("Position");
        });

        modelBuilder.Entity<TypesofUser>(entity =>
        {
            entity.HasKey(e => e.Typeid);

            entity.HasIndex(e => new { e.Typeid, e.Type }, "IX_TypesofUsers").IsUnique();

            entity.Property(e => e.Type).HasMaxLength(40);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Login).HasMaxLength(40);

            entity.HasOne(d => d.TypeofUserNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.TypeofUser)
                .HasConstraintName("FK_Users_TypesofUsers");
        });

        modelBuilder.Entity<Vacancy>(entity =>
        {
            entity.Property(e => e.City).HasMaxLength(30);
            entity.Property(e => e.DriverLicense).HasColumnName("Driver_license");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .HasColumnName("gender");
            entity.Property(e => e.Openness).HasColumnName("openness");
            entity.Property(e => e.PcKnowledge).HasColumnName("PC_knowledge");
            entity.Property(e => e.PlacementDate)
                .HasColumnType("datetime")
                .HasColumnName("Placement_Date");
            entity.Property(e => e.Salary).HasColumnType("money");
            entity.Property(e => e.Schedule).HasMaxLength(20);
            entity.Property(e => e.WorkExperience)
                .HasMaxLength(20)
                .HasColumnName("work_experience");

            entity.HasOne(d => d.EducationNavigation).WithMany(p => p.Vacancies)
                .HasForeignKey(d => d.Education)
                .HasConstraintName("FK_Vacancies_Education");

            entity.HasOne(d => d.EmployerNavigation).WithMany(p => p.Vacancies)
                .HasForeignKey(d => d.Employer)
                .HasConstraintName("FK_Vacancies_Employers1");

            entity.HasOne(d => d.PositionNavigation).WithMany(p => p.Vacancies)
                .HasForeignKey(d => d.Position)
                .HasConstraintName("FK_Vacancies_Positions");
        });

        modelBuilder.Entity<VacancyLanguage>(entity =>
        {
            entity.ToTable("Vacancy_Languages");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Language).HasMaxLength(40);

            entity.HasOne(d => d.VacancyNavigation).WithMany(p => p.VacancyLanguages)
                .HasForeignKey(d => d.Vacancy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vacancy_Languages_Vacancies");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
