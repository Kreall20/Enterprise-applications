using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab2ORM.Classes;

public partial class MusicPlayerContext : DbContext
{
    public MusicPlayerContext()
    {
    }

    public MusicPlayerContext(DbContextOptions<MusicPlayerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Альбомы> Альбомыs { get; set; }

    public virtual DbSet<Жанры> Жанрыs { get; set; }

    public virtual DbSet<Исполнители> Исполнителиs { get; set; }

    public virtual DbSet<Песни> Песниs { get; set; }

    public virtual DbSet<ПесниАльбома> ПесниАльбомаs { get; set; }

    public virtual DbSet<ПесниПлейлистов> ПесниПлейлистовs { get; set; }

    public virtual DbSet<ПесниПользователя> ПесниПользователяs { get; set; }

    public virtual DbSet<Плейлисты> Плейлистыs { get; set; }

    public virtual DbSet<Пользователи> Пользователиs { get; set; }

    public virtual DbSet<ТипыПользователей> ТипыПользователейs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=HOME-PC\\SQLEXPRESS01;Database=MusicPlayer;Trusted_Connection=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Альбомы>(entity =>
        {
            entity.HasKey(e => e.КодАльбома);

            entity.ToTable("Альбомы");

            entity.Property(e => e.НазваниеАльбома).HasMaxLength(30);
        });

        modelBuilder.Entity<Жанры>(entity =>
        {
            entity.HasKey(e => e.КодЖанра);

            entity.ToTable("Жанры");

            entity.Property(e => e.Жанр).HasMaxLength(30);
        });

        modelBuilder.Entity<Исполнители>(entity =>
        {
            entity.HasKey(e => e.КодИсполнителя);

            entity.ToTable("Исполнители");

            entity.Property(e => e.Исполнитель).HasMaxLength(30);
        });

        modelBuilder.Entity<Песни>(entity =>
        {
            entity.HasKey(e => e.КодПесни);

            entity.ToTable("Песни");

            entity.Property(e => e.НазваниеПесни).HasMaxLength(30);

            entity.HasOne(d => d.АвторNavigation).WithMany(p => p.Песниs)
                .HasForeignKey(d => d.Автор)
                .HasConstraintName("FK_Песни_Исполнители");

            entity.HasOne(d => d.ЖанрNavigation).WithMany(p => p.Песниs)
                .HasForeignKey(d => d.Жанр)
                .HasConstraintName("FK_Песни_Жанры");
        });

        modelBuilder.Entity<ПесниАльбома>(entity =>
        {
            entity.ToTable("ПесниАльбома");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.HasOne(d => d.КодАльбомаNavigation).WithMany(p => p.ПесниАльбомаs)
                .HasForeignKey(d => d.КодАльбома)
                .HasConstraintName("FK_ПесниАльбома_Альбомы");

            entity.HasOne(d => d.КодПесниNavigation).WithMany(p => p.ПесниАльбомаs)
                .HasForeignKey(d => d.КодПесни)
                .HasConstraintName("FK_ПесниАльбома_Песни");
        });

        modelBuilder.Entity<ПесниПлейлистов>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_МузыкаПлейлистов");

            entity.ToTable("ПесниПлейлистов");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.КодПесниNavigation).WithMany(p => p.ПесниПлейлистовs)
                .HasForeignKey(d => d.КодПесни)
                .HasConstraintName("FK_ПесниПлейлистов_Песни");

            entity.HasOne(d => d.КодПлейлистаNavigation).WithMany(p => p.ПесниПлейлистовs)
                .HasForeignKey(d => d.КодПлейлиста)
                .HasConstraintName("FK_МузыкаПлейлистов_Плейлисты");
        });

        modelBuilder.Entity<ПесниПользователя>(entity =>
        {
            entity.ToTable("ПесниПользователя");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.Альбом).HasMaxLength(50);
            entity.Property(e => e.Жанр).HasMaxLength(50);
            entity.Property(e => e.Исполнитель).HasMaxLength(50);
            entity.Property(e => e.Плейлист).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.ПесниПользователяs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_ПесниПользователя_Пользователи");

            entity.HasOne(d => d.КодМузыкиNavigation).WithMany(p => p.ПесниПользователяs)
                .HasForeignKey(d => d.КодМузыки)
                .HasConstraintName("FK_ПесниПользователя_Песни");
        });

        modelBuilder.Entity<Плейлисты>(entity =>
        {
            entity.HasKey(e => e.КодПлейлиста).HasName("PK_Плейлисты_1");

            entity.ToTable("Плейлисты");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Плейлист).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.Плейлистыs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Плейлисты_Пользователи");
        });

        modelBuilder.Entity<Пользователи>(entity =>
        {
            entity.HasKey(e => e.КодПользователя);

            entity.ToTable("Пользователи");

            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Password).HasColumnName("password");

            entity.HasOne(d => d.ТипПользователяNavigation).WithMany(p => p.Пользователиs)
                .HasForeignKey(d => d.ТипПользователя)
                .HasConstraintName("FK_Пользователи_ТипыПользователей");
        });

        modelBuilder.Entity<ТипыПользователей>(entity =>
        {
            entity.HasKey(e => e.КодТипаПользователя);

            entity.ToTable("ТипыПользователей");

            entity.Property(e => e.ТипПользователя).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
