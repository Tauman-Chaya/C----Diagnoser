using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class DbManager : DbContext
{
    public DbManager()
    {
    }

    public DbManager(DbContextOptions<DbManager> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Diagnoser> Diagnosers { get; set; }

    public virtual DbSet<Lead> Leads { get; set; }

    public virtual DbSet<OrderWorkShop> OrderWorkShops { get; set; }

    public virtual DbSet<Reference> References { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<TypeGroup> TypeGroups { get; set; }

    public virtual DbSet<Workshop> Workshops { get; set; }

    // נקבע פעם אחת בעליית השרת (Program.cs), מתוך appsettings.json
    public static string? ConnectionString { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;

        if (string.IsNullOrEmpty(ConnectionString))
            throw new InvalidOperationException("Connection string 'DefaultConnection' is missing in appsettings.json");

        optionsBuilder.UseSqlServer(ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__Courses__A25C5AA692326674");

            entity.Property(e => e.Code).ValueGeneratedNever();
            entity.Property(e => e.Adress)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Day)
                .HasMaxLength(10)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Desciption)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Name)
                .HasMaxLength(1)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__tmp_ms_x__A25C5AA6A8C310F6");

            entity.Property(e => e.Code).ValueGeneratedNever();
            entity.Property(e => e.Mail)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            entity.HasOne(d => d.CodeTypeNavigation).WithMany(p => p.Customers)
                .HasForeignKey(d => d.CodeType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customers_ToTable");
        });

        modelBuilder.Entity<Diagnoser>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__tmp_ms_x__A25C5AA6540B9B84");

            entity.ToTable("Diagnoser");

            entity.Property(e => e.Mail)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.PrecentagePayment)
                .HasDefaultValue(0)
                .HasColumnName("precentagePayment");
        });

        modelBuilder.Entity<Lead>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__Leads__A25C5AA655ED2BE9");

            entity.Property(e => e.Code).ValueGeneratedNever();
            entity.Property(e => e.Mail)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            entity.HasOne(d => d.CodeTypeNavigation).WithMany(p => p.Leads)
                .HasForeignKey(d => d.CodeType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Leads_ToTable");
        });

        modelBuilder.Entity<OrderWorkShop>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__tmp_ms_x__A25C5AA6D564FA7D");

            entity.ToTable("Order_WorkShops");

            entity.Property(e => e.Adress)
                .HasMaxLength(50)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.CodeCustomer).HasColumnName("Code_Customer");
            entity.Property(e => e.CodeDiagnoser).HasColumnName("Code_Diagnoser");
            entity.Property(e => e.Paied).HasColumnName("paied");
            entity.Property(e => e.Parsent).HasColumnName("parsent");

            entity.HasOne(d => d.CodeCustomerNavigation).WithMany(p => p.OrderWorkShops)
                .HasForeignKey(d => d.CodeCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_WorkShops_ToTable_2");

            entity.HasOne(d => d.CodeDiagnoserNavigation).WithMany(p => p.OrderWorkShops)
                .HasForeignKey(d => d.CodeDiagnoser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_WorkShops_ToTable");

            entity.HasOne(d => d.TypeWorkShopNavigation).WithMany(p => p.OrderWorkShops)
                .HasForeignKey(d => d.TypeWorkShop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_WorkShops_ToTable_1");
        });

        modelBuilder.Entity<Reference>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__tmp_ms_x__A25C5AA6E88B88D2");

            entity.ToTable("Reference");

            entity.Property(e => e.Adress)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.CodeCustomer).HasColumnName("Code_Customer");
            entity.Property(e => e.CodeWorkShop).HasColumnName("Code_WorkShop");
            entity.Property(e => e.Comments)
                .HasMaxLength(200)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            entity.HasOne(d => d.CodeCustomerNavigation).WithMany(p => p.References)
                .HasForeignKey(d => d.CodeCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reference_ToTable_1");

            entity.HasOne(d => d.CodeWorkShopNavigation).WithMany(p => p.References)
                .HasForeignKey(d => d.CodeWorkShop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reference_ToTable");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.References)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reference_ToTable_2");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__tmp_ms_x__A25C5AA6861288D1");

            entity.ToTable("Status");

            entity.Property(e => e.Description)
                .HasMaxLength(10)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Adress)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Mail)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            entity.HasOne(d => d.CodeCoursesNavigation).WithMany()
                .HasForeignKey(d => d.CodeCourses)
                .HasConstraintName("FK_Students_ToTable");
        });

        modelBuilder.Entity<TypeGroup>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__tmp_ms_x__A25C5AA684A30D73");

            entity.ToTable("TypeGroup");

            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<Workshop>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__tmp_ms_x__A25C5AA60B17022E");

            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            entity.HasOne(d => d.CodeDiagnoserNavigation).WithMany(p => p.Workshops)
                .HasForeignKey(d => d.CodeDiagnoser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Workshops_ToTable_1");

            entity.HasOne(d => d.TypeGroupNavigation).WithMany(p => p.Workshops)
                .HasForeignKey(d => d.TypeGroup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Workshops_ToTable");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
