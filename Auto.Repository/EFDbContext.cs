using Auto.Model.Entities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace Auto.Repository
{
    public class EFDbContext : DbContext
    {
        public EFDbContext(DbContextOptions contextOptions) : base(contextOptions)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Corporation>().ToTable("Corporation");//.Property(s=>s.ID).HasValueGenerator();
            //modelBuilder.Entity<Corporation>().Property(c => c.Name).HasMaxLength(200).IsRequired();
            //// modelBuilder.Entity<Corporation>().Property(c => c.ID)HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
            //modelBuilder.Entity<Corporation>().Property(c => c.CorporationAddress).HasMaxLength(1000);
            //modelBuilder.Entity<Department>().ToTable("Department");
            //modelBuilder.Entity<Department>().Property(d => d.Name).HasMaxLength(200).IsRequired();
            //modelBuilder.Entity<Employee>().ToTable("Employee");
            //modelBuilder.Entity<Employee>().Property(e => e.Name).HasMaxLength(200).IsRequired();
            //modelBuilder.Entity<Employee>().Property(e => e.EmployeeGender).IsRequired().ValueGeneratedOnAdd();
            //modelBuilder.Entity<Area>().ToTable("Area");
            // modelBuilder.Entity<City>()
            //    .HasOne(x => x.Province)   //指向外键表的导航属性
            //    .WithMany(x => x.Cities);  //外键表的导航属性指向自己
            //   // .HasForeignKey(x => x.ProvinceId);             //外键表Id

            //// modelBuilder.Entity<CityCompany>()
            //    .HasKey(x => new { x.CityId, x.CompanyId });         //创建联合主键

            // modelBuilder.Entity<CityCompany>()
            //     .HasOne(x => x.City)             //指向外键表的导航属性
            //     .WithMany(x => x.CityCompanies);  //外键表的导航属性指向自己
            //     .HasForeignKey(x => x.CityId);   //外键表Id

            // modelBuilder.Entity<CityCompany>()
            //     .HasOne(x => x.Company)         //指向外键表的导航属性
            //     .WithMany(x => x.CityCompanies); //外键表的导航属性指向自己
            //   //  .HasForeignKey(x => x.CompanyId);                 //外键表Id

            // modelBuilder.Entity<Mayor>()
            //     .HasOne(x => x.City)             //指向外键表的导航属性
            //     .WithOne(x => x.Mayor)           //外键表的导航属性指向自己
            //     .HasForeignKey<Mayor>("CityIDString");   //外键表Id
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Corporation> Corporations { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Position> Positions { get; set; }
    }

}
