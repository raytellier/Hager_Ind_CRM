using Hager_Ind_CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.Data
{
    public class HagerIndContext : DbContext
    {
        public HagerIndContext(DbContextOptions<HagerIndContext> options)
            : base(options)
        {
        }
        public DbSet<BillingTerms> BillingTerms { get; set; }
        public DbSet<Catagory> Catagories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactCatagory> ContactCatagories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmploymentType> EmploymentTypes { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<SubType> SubTypes { get; set; }
        public DbSet<Hager_Ind_CRM.Models.CType> Types { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("HI");

            ////Prevent Cascade Delete from Doctor to Patient
            ////so we are prevented from deleting a Doctor with
            ////Patients assigned
            //modelBuilder.Entity<Doctor>()
            //    .HasMany<Patient>(d => d.Patients)
            //    .WithOne(p => p.Doctor)
            //    .HasForeignKey(p => p.DoctorID)
            //    .OnDelete(DeleteBehavior.Restrict);

            ////Add a unique index to the OHIP Number
            //modelBuilder.Entity<Patient>()
            //.HasIndex(p => p.OHIP)
            //.IsUnique();

            //Many to Many Contact Category  Primary Key
            modelBuilder.Entity<ContactCatagory>()
            .HasKey(t => new { t.CatagoryID, t.ContactID });

            //Many to Many Company Type  Primary Key
            modelBuilder.Entity<CompanyType>()
            .HasKey(t => new { t.TypeID, t.CompanyID });


            modelBuilder.Entity<Province>()
                    .HasMany(m => m.BillingCompanies)
                    .WithOne(t => t.BillingProvince)
                    .HasForeignKey(m => m.BillingProvinceID);

            modelBuilder.Entity<Province>()
                    .HasMany(m => m.ShippingCompanies)
                    .WithOne(t => t.ShippingProvince)
                    .HasForeignKey(m => m.ShippingProvinceID);

            modelBuilder.Entity<Province>()
                    .HasMany(m => m.Employees)
                    .WithOne(t => t.Province)
                    .HasForeignKey(m => m.BillingProvinceID);

            modelBuilder.Entity<Country>()
                    .HasMany(m => m.BillingCompanies)
                    .WithOne(t => t.BillingCountry)
                    .HasForeignKey(m => m.BillingCountryID);

            modelBuilder.Entity<Country>()
                    .HasMany(m => m.ShippingCompanies)
                    .WithOne(t => t.ShippingCountry)
                    .HasForeignKey(m => m.ShippingCountryID);

            modelBuilder.Entity<Country>()
                    .HasMany(m => m.Employees)
                    .WithOne(t => t.Country)
                    .HasForeignKey(m => m.BillingCountryID);

            modelBuilder.Entity<Company>()
                .HasMany<Contact>(d => d.Contacts)
                .WithOne(p => p.Company)
                .HasForeignKey(p => p.CompanyID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Country>()
              .HasMany<Province>(d => d.Provinces)
              .WithOne(p => p.Country)
              .HasForeignKey(p => p.CountryID)
              .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<Hager_Ind_CRM.Models.SubType> SubType { get; set; }
    }
}
