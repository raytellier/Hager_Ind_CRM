﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hager_Ind_CRM.Data.HIMigrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "HI");

            migrationBuilder.CreateTable(
                name: "BillingTerms",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Terms = table.Column<string>(nullable: false),
                    OrderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingTerms", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Catagories",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    OrderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catagories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    OrderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    OrderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentTypes",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(nullable: true),
                    OrderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "JobPositions",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    OrderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPositions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    OrderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    OrderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Location = table.Column<string>(maxLength: 50, nullable: false),
                    CredCheck = table.Column<bool>(nullable: false),
                    BillingTermsID = table.Column<int>(nullable: false),
                    CurrencyID = table.Column<int>(nullable: false),
                    Phone = table.Column<long>(nullable: false),
                    Website = table.Column<string>(maxLength: 2000, nullable: false),
                    BillingAddress1 = table.Column<string>(maxLength: 50, nullable: false),
                    BillingAddress2 = table.Column<string>(maxLength: 50, nullable: true),
                    BillingProvinceID = table.Column<int>(nullable: false),
                    BillingPostalCode = table.Column<string>(nullable: false),
                    BillingCountryID = table.Column<int>(nullable: false),
                    ShippingAddress1 = table.Column<string>(maxLength: 50, nullable: false),
                    ShippingAddress2 = table.Column<string>(maxLength: 50, nullable: true),
                    ShippingProvinceID = table.Column<int>(nullable: false),
                    ShippingPostalCode = table.Column<string>(nullable: false),
                    ShippingCountryID = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Notes = table.Column<string>(maxLength: 511, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Companies_Countries_BillingCountryID",
                        column: x => x.BillingCountryID,
                        principalSchema: "HI",
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Companies_Provinces_BillingProvinceID",
                        column: x => x.BillingProvinceID,
                        principalSchema: "HI",
                        principalTable: "Provinces",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Companies_BillingTerms_BillingTermsID",
                        column: x => x.BillingTermsID,
                        principalSchema: "HI",
                        principalTable: "BillingTerms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Companies_Currencies_CurrencyID",
                        column: x => x.CurrencyID,
                        principalSchema: "HI",
                        principalTable: "Currencies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Companies_Countries_ShippingCountryID",
                        column: x => x.ShippingCountryID,
                        principalSchema: "HI",
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Companies_Provinces_ShippingProvinceID",
                        column: x => x.ShippingProvinceID,
                        principalSchema: "HI",
                        principalTable: "Provinces",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(maxLength: 30, nullable: false),
                    LastName = table.Column<string>(maxLength: 30, nullable: false),
                    JobPositionID = table.Column<int>(nullable: false),
                    EmploymentTypeID = table.Column<int>(nullable: false),
                    Address1 = table.Column<string>(maxLength: 30, nullable: false),
                    Address2 = table.Column<string>(maxLength: 30, nullable: true),
                    BillingProvinceID = table.Column<int>(nullable: false),
                    ProvinceID = table.Column<int>(nullable: true),
                    BillingPostal = table.Column<string>(nullable: false),
                    BillingCountryID = table.Column<int>(nullable: false),
                    CountryID = table.Column<int>(nullable: true),
                    CellPhone = table.Column<long>(nullable: false),
                    HomePhone = table.Column<long>(nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Wage = table.Column<decimal>(nullable: false),
                    Expense = table.Column<decimal>(nullable: false),
                    DateJoined = table.Column<DateTime>(nullable: false),
                    KeyFobNumber = table.Column<long>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    IsUser = table.Column<bool>(nullable: false),
                    PermissionLevel = table.Column<string>(maxLength: 30, nullable: false),
                    EmergencyContactName = table.Column<string>(maxLength: 30, nullable: false),
                    EmergencyContactPhone = table.Column<long>(nullable: false),
                    Note = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_Countries_CountryID",
                        column: x => x.CountryID,
                        principalSchema: "HI",
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_EmploymentTypes_EmploymentTypeID",
                        column: x => x.EmploymentTypeID,
                        principalSchema: "HI",
                        principalTable: "EmploymentTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_JobPositions_JobPositionID",
                        column: x => x.JobPositionID,
                        principalSchema: "HI",
                        principalTable: "JobPositions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Provinces_ProvinceID",
                        column: x => x.ProvinceID,
                        principalSchema: "HI",
                        principalTable: "Provinces",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubType",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    OrderID = table.Column<int>(nullable: false),
                    TypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubType_Types_TypeID",
                        column: x => x.TypeID,
                        principalSchema: "HI",
                        principalTable: "Types",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTypes",
                schema: "HI",
                columns: table => new
                {
                    CompanyID = table.Column<int>(nullable: false),
                    TypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTypes", x => new { x.TypeID, x.CompanyID });
                    table.ForeignKey(
                        name: "FK_CompanyTypes_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalSchema: "HI",
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyTypes_Types_TypeID",
                        column: x => x.TypeID,
                        principalSchema: "HI",
                        principalTable: "Types",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    JobTitle = table.Column<string>(nullable: true),
                    CellPhone = table.Column<long>(nullable: false),
                    WorkPhone = table.Column<long>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contacts_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalSchema: "HI",
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactCatagories",
                schema: "HI",
                columns: table => new
                {
                    CatagoryID = table.Column<int>(nullable: false),
                    ContactID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactCatagories", x => new { x.CatagoryID, x.ContactID });
                    table.ForeignKey(
                        name: "FK_ContactCatagories_Catagories_CatagoryID",
                        column: x => x.CatagoryID,
                        principalSchema: "HI",
                        principalTable: "Catagories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactCatagories_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "HI",
                        principalTable: "Contacts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_BillingCountryID",
                schema: "HI",
                table: "Companies",
                column: "BillingCountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_BillingProvinceID",
                schema: "HI",
                table: "Companies",
                column: "BillingProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_BillingTermsID",
                schema: "HI",
                table: "Companies",
                column: "BillingTermsID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CurrencyID",
                schema: "HI",
                table: "Companies",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ShippingCountryID",
                schema: "HI",
                table: "Companies",
                column: "ShippingCountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ShippingProvinceID",
                schema: "HI",
                table: "Companies",
                column: "ShippingProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTypes_CompanyID",
                schema: "HI",
                table: "CompanyTypes",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactCatagories_ContactID",
                schema: "HI",
                table: "ContactCatagories",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CompanyID",
                schema: "HI",
                table: "Contacts",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CountryID",
                schema: "HI",
                table: "Employees",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmploymentTypeID",
                schema: "HI",
                table: "Employees",
                column: "EmploymentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobPositionID",
                schema: "HI",
                table: "Employees",
                column: "JobPositionID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ProvinceID",
                schema: "HI",
                table: "Employees",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_SubType_TypeID",
                schema: "HI",
                table: "SubType",
                column: "TypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyTypes",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "ContactCatagories",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "SubType",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Catagories",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "EmploymentTypes",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "JobPositions",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Types",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Provinces",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "BillingTerms",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Currencies",
                schema: "HI");
        }
    }
}
