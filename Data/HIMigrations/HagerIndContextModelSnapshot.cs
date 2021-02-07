﻿// <auto-generated />
using System;
using Hager_Ind_CRM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hager_Ind_CRM.Data.HIMigrations
{
    [DbContext(typeof(HagerIndContext))]
    partial class HagerIndContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("HI")
                .HasAnnotation("ProductVersion", "3.1.11");

            modelBuilder.Entity("Hager_Ind_CRM.Models.BillingTerms", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Terms")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("BillingTerms");
                });

            modelBuilder.Entity("Hager_Ind_CRM.Models.CType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<int>("OrderID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("Hager_Ind_CRM.Models.Catagory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Catagories");
                });

            modelBuilder.Entity("Hager_Ind_CRM.Models.Company", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BillingAddress1")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("BillingAddress2")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<int>("BillingCountryID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BillingPostalCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("BillingProvinceID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BillingTermsID")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CredCheck")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrencyID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT")
                        .HasMaxLength(511);

                    b.Property<long>("Phone")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ShippingAddress1")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("ShippingAddress2")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<int>("ShippingCountryID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ShippingPostalCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ShippingProvinceID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(2000);

                    b.HasKey("ID");

                    b.HasIndex("BillingCountryID");

                    b.HasIndex("BillingProvinceID");

                    b.HasIndex("BillingTermsID");

                    b.HasIndex("CurrencyID");

                    b.HasIndex("ShippingCountryID");

                    b.HasIndex("ShippingProvinceID");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Hager_Ind_CRM.Models.CompanyType", b =>
                {
                    b.Property<int>("TypeID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompanyID")
                        .HasColumnType("INTEGER");

                    b.HasKey("TypeID", "CompanyID");

                    b.HasIndex("CompanyID");

                    b.ToTable("CompanyTypes");
                });

            modelBuilder.Entity("Hager_Ind_CRM.Models.Contact", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<long>("CellPhone")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompanyID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.Property<long>("WorkPhone")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("CompanyID");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Hager_Ind_CRM.Models.ContactCatagory", b =>
                {
                    b.Property<int>("CatagoryID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ContactID")
                        .HasColumnType("INTEGER");

                    b.HasKey("CatagoryID", "ContactID");

                    b.HasIndex("ContactID");

                    b.ToTable("ContactCatagories");
                });

            modelBuilder.Entity("Hager_Ind_CRM.Models.Country", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Hager_Ind_CRM.Models.Currency", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("Hager_Ind_CRM.Models.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.Property<string>("Address2")
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.Property<int>("BillingCountryID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BillingPostal")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("BillingProvinceID")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("CellPhone")
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateJoined")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("EmergencyContactName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.Property<long?>("EmergencyContactPhone")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmploymentTypeID")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("Expense")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.Property<long?>("HomePhone")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("InactiveDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsUser")
                        .HasColumnType("INTEGER");

                    b.Property<int>("JobPositionID")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("KeyFobNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.Property<string>("PermissionLevel")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.Property<decimal?>("Wage")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("BillingCountryID");

                    b.HasIndex("BillingProvinceID");

                    b.HasIndex("EmploymentTypeID");

                    b.HasIndex("JobPositionID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Hager_Ind_CRM.Models.EmploymentType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("EmploymentTypes");
                });

            modelBuilder.Entity("Hager_Ind_CRM.Models.JobPosition", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("JobPositions");
                });

            modelBuilder.Entity("Hager_Ind_CRM.Models.Province", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CountryID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("CountryID");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("Hager_Ind_CRM.Models.SubType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<int>("OrderID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TypeID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("TypeID");

                    b.ToTable("SubType");
                });

            modelBuilder.Entity("Hager_Ind_CRM.Models.Company", b =>
                {
                    b.HasOne("Hager_Ind_CRM.Models.Country", "BillingCountry")
                        .WithMany("BillingCompanies")
                        .HasForeignKey("BillingCountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hager_Ind_CRM.Models.Province", "BillingProvince")
                        .WithMany("BillingCompanies")
                        .HasForeignKey("BillingProvinceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hager_Ind_CRM.Models.BillingTerms", "BillingTerms")
                        .WithMany("Companies")
                        .HasForeignKey("BillingTermsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hager_Ind_CRM.Models.Currency", "Currency")
                        .WithMany("Companies")
                        .HasForeignKey("CurrencyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hager_Ind_CRM.Models.Country", "ShippingCountry")
                        .WithMany("ShippingCompanies")
                        .HasForeignKey("ShippingCountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hager_Ind_CRM.Models.Province", "ShippingProvince")
                        .WithMany("ShippingCompanies")
                        .HasForeignKey("ShippingProvinceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hager_Ind_CRM.Models.CompanyType", b =>
                {
                    b.HasOne("Hager_Ind_CRM.Models.Company", "Company")
                        .WithMany("CompanyTypes")
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hager_Ind_CRM.Models.CType", "Type")
                        .WithMany("CompanyTypes")
                        .HasForeignKey("TypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hager_Ind_CRM.Models.Contact", b =>
                {
                    b.HasOne("Hager_Ind_CRM.Models.Company", "Company")
                        .WithMany("Contacts")
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Hager_Ind_CRM.Models.ContactCatagory", b =>
                {
                    b.HasOne("Hager_Ind_CRM.Models.Catagory", "Catagory")
                        .WithMany("ContactCatagories")
                        .HasForeignKey("CatagoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hager_Ind_CRM.Models.Contact", "Contact")
                        .WithMany("ContactCatagories")
                        .HasForeignKey("ContactID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hager_Ind_CRM.Models.Employee", b =>
                {
                    b.HasOne("Hager_Ind_CRM.Models.Country", "Country")
                        .WithMany("Employees")
                        .HasForeignKey("BillingCountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hager_Ind_CRM.Models.Province", "Province")
                        .WithMany("Employees")
                        .HasForeignKey("BillingProvinceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hager_Ind_CRM.Models.EmploymentType", "EmploymentType")
                        .WithMany("Employees")
                        .HasForeignKey("EmploymentTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hager_Ind_CRM.Models.JobPosition", "JobPosition")
                        .WithMany("Employees")
                        .HasForeignKey("JobPositionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hager_Ind_CRM.Models.Province", b =>
                {
                    b.HasOne("Hager_Ind_CRM.Models.Country", "Country")
                        .WithMany("Provinces")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Hager_Ind_CRM.Models.SubType", b =>
                {
                    b.HasOne("Hager_Ind_CRM.Models.CType", "Type")
                        .WithMany("SubTypes")
                        .HasForeignKey("TypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
