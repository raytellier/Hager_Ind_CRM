using Hager_Ind_CRM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.Data
{
    public static class HISeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new HagerIndContext(
                serviceProvider.GetRequiredService<DbContextOptions<HagerIndContext>>()))
            {
                Random random = new Random();

                if (!context.Countries.Any())
                {
                    context.Countries.AddRange(
                     new Country
                     {
                         Name = "Canada",
                         OrderID = 1
                     },

                     new Country
                     {
                         Name = "United States",
                         OrderID = 2
                     },
                     new Country
                     {
                         Name = "Russia",
                         OrderID = 3
                     },
                     new Country
                     {
                         Name = "India",
                         OrderID = 4
                     }
                );
                    context.SaveChanges();
                }
                int[] countryIDs = context.Countries.Select(s => s.ID).ToArray();

                string[] CanadianProvinces = new string[] { "AB", "BC", "MB", "NB", "NL", "NT", "NS", "NU", "ON", "PE", "QC", "SK", "YT" };
                if (!context.Provinces.Any())
                {
                    int star = 1;
                    foreach (string s in CanadianProvinces)
                    {
                        Province prov = new Province
                        {
                            Name = s,
                            OrderID = star
                        };
                        context.Provinces.Add(prov);

                        star += 1;
                    }
                    context.SaveChanges();
                }
                //Create collection of the primary keys of the Provinces
                int[] specialtyIDs = context.Provinces.Select(s => s.ID).ToArray();

                string[] EmpTypes = new string[] { "Part-Time", "Full-Time", "Seasonal", "Co-op Stundent", "Contract" };
                if (!context.EmploymentTypes.Any())
                {
                    int star = 1;
                    foreach (string s in EmpTypes)
                    {
                        EmploymentType prov = new EmploymentType
                        {
                            Type = s,
                            OrderID = star
                        };
                        context.EmploymentTypes.Add(prov);

                        star += 1;
                    }
                    context.SaveChanges();
                }
                //Create collection of the primary keys of the Provinces
                int[] empTypeIDs = context.EmploymentTypes.Select(s => s.ID).ToArray();

                string[] JobPositions = new string[] { "Jr. Fabricator","Fabricator","Sr. Fabricator","Foreman","Apprentice Plumber","Plumber","Field Supervisor","General Labourer","Shipping Receiving","Controller","President","Vice President","Jr. Draftsperson","Mechanical Designer","Professional Engineer","Engineering Manager","Mechanical Estimator / Purchaser","Estimator","Sales Manager" };
                if (!context.JobPositions.Any())
                {
                    int star = 1;
                    foreach (string s in JobPositions)
                    {

                        JobPosition prov = new JobPosition
                        {
                            Name = s,
                            OrderID = star
                        };
                        context.JobPositions.Add(prov);

                        star += 1;
                    }
                    context.SaveChanges();
                }
                //Create collection of the primary keys of the Provinces
                int[] jobPositionIDs = context.JobPositions.Select(s => s.ID).ToArray();

                if (!context.Employees.Any())
                {
                    context.Employees.AddRange(
                    new Employee
                    {
                        FirstName = "Mitchel",
                        LastName = "Indigo",
                        JobPositionID = random.Next(1, JobPositions.Length),
                        EmploymentTypeID = random.Next(1, EmpTypes.Length),
                        Address1 = "123 Random Street",
                        BillingProvinceID = random.Next(1, CanadianProvinces.Length),
                        BillingPostal = "L5H2J8",
                        BillingCountryID = context.Countries.FirstOrDefault(d => d.Name == "Canada").ID,
                        CellPhone = 2899098901,
                        HomePhone = 2899098901,
                        Email = "mitchel@gmail.com",
                        DateOfBirth = DateTime.Parse("1999-02-09"),
                        Wage = 90000.00M,
                        Expense = 5000.00M,
                        DateJoined = DateTime.Parse("2021-01-01"),
                        KeyFobNumber = 1234567,
                        Active = true,
                        IsUser = true,
                        PermissionLevel = "Unlimited",
                        EmergencyContactName = "Mitch",
                        EmergencyContactPhone = 2892892829,
                        Note = "Testing purposes"
                    },
                    new Employee
                    {
                        FirstName = "Ray",
                        LastName = "Indigo",
                        JobPositionID = random.Next(1, JobPositions.Length),
                        EmploymentTypeID = random.Next(1, EmpTypes.Length),
                        Address1 = "321 Random Street",
                        BillingProvinceID = random.Next(1, CanadianProvinces.Length),
                        BillingPostal = "B5B2B9",
                        BillingCountryID = context.Countries.FirstOrDefault(d => d.Name == "United States").ID,
                        CellPhone = 2899098902,
                        HomePhone = 2899098902,
                        Email = "ray@gmail.com",
                        DateOfBirth = DateTime.Parse("1999-02-09"),
                        Wage = 57575.25M,
                        Expense = 5000.09M,
                        DateJoined = DateTime.Parse("2021-01-01"),
                        KeyFobNumber = 1234566,
                        Active = true,
                        IsUser = true,
                        PermissionLevel = "Unlimited",
                        EmergencyContactName = "Ray",
                        EmergencyContactPhone = 2892892820,
                        Note = "Testing purposes"
                    },
                    new Employee
                    {
                        FirstName = "Harry",
                        LastName = "Indigo",
                        JobPositionID = random.Next(1, JobPositions.Length),
                        EmploymentTypeID = random.Next(1, EmpTypes.Length),
                        Address1 = "32 Random Street",
                        BillingProvinceID = random.Next(1, CanadianProvinces.Length),
                        BillingPostal = "A5A2A9",
                        BillingCountryID = context.Countries.FirstOrDefault(d => d.Name == "Canada").ID,
                        CellPhone = 2899098912,
                        HomePhone = 2899098912,
                        Email = "harry@gmail.com",
                        DateOfBirth = DateTime.Parse("1999-02-09"),
                        Wage = 57575.25M,
                        Expense = 5000.09M,
                        DateJoined = DateTime.Parse("2021-01-01"),
                        KeyFobNumber = 1234666,
                        Active = true,
                        IsUser = true,
                        PermissionLevel = "Unlimited",
                        EmergencyContactName = "Harry",
                        EmergencyContactPhone = 2892891820,
                        Note = "Testing purposes"
                    },
                    new Employee
                    {
                        FirstName = "Artem",
                        LastName = "Indigo",
                        JobPositionID = random.Next(1, JobPositions.Length),
                        EmploymentTypeID = random.Next(1, EmpTypes.Length),
                        Address1 = "3 Random Street",
                        BillingProvinceID = random.Next(1, CanadianProvinces.Length),
                        BillingPostal = "L4H2J9",
                        BillingCountryID = context.Countries.FirstOrDefault(d => d.Name == "Russia").ID,
                        CellPhone = 2819098902,
                        HomePhone = 2819098902,
                        Email = "ray@gmail.com",
                        DateOfBirth = DateTime.Parse("1999-02-09"),
                        Wage = 57575.25M,
                        Expense = 5000.09M,
                        DateJoined = DateTime.Parse("2021-01-01"),
                        KeyFobNumber = 1634566,
                        Active = true,
                        IsUser = true,
                        PermissionLevel = "Unlimited",
                        EmergencyContactName = "Artem",
                        EmergencyContactPhone = 2892192820,
                        Note = "Testing purposes"
                    },
                    new Employee
                    {
                        FirstName = "Priya",
                        LastName = "Indigo",
                        JobPositionID = random.Next(1, JobPositions.Length),
                        EmploymentTypeID = random.Next(1, EmpTypes.Length),
                        Address1 = "21 Random Street",
                        BillingProvinceID = random.Next(1, CanadianProvinces.Length),
                        BillingPostal = "L5H7J9",
                        BillingCountryID = context.Countries.FirstOrDefault(d => d.Name == "United States").ID,
                        CellPhone = 2899098982,
                        HomePhone = 2899098982,
                        Email = "ray@gmail.com",
                        DateOfBirth = DateTime.Parse("1999-02-09"),
                        Wage = 57575.25M,
                        Expense = 5000.09M,
                        DateJoined = DateTime.Parse("2021-01-01"),
                        KeyFobNumber = 1234776,
                        Active = true,
                        IsUser = true,
                        PermissionLevel = "Unlimited",
                        EmergencyContactName = "Priya",
                        EmergencyContactPhone = 2892892820,
                        Note = "Testing purposes"
                    }
                    );
                    context.SaveChanges();
                }
                string[] BillingTerms = new string[] { "40% down, balance net 15", "40% down, balance net 30", "40% down, balance net 45", "40% down, balance net 90", "Due on receipt", "Net 15","Net 30","Net 45","Net 90"};
                if (!context.BillingTerms.Any())
                {
                    int star = 1;
                    foreach (string s in BillingTerms)
                    {
                        BillingTerms billingTerms = new BillingTerms
                        {
                            Terms = s,
                            OrderID = star
                        };
                        context.BillingTerms.Add(billingTerms);
                        star += 1;
                    }
                    context.SaveChanges();
                }
                //Create collection of the primary keys of the Provinces
                int[] BillingTermIDs = context.BillingTerms.Select(s => s.ID).ToArray();

                string[] Currencies = new string[] { "USD", "CAD", "GBP", "JPY", "CNY" };
                if (!context.Currencies.Any())
                {
                    foreach (string s in Currencies)
                    {
                        int star = 1;
                        Currency moneyType = new Currency
                        {
                            Name = s,
                            OrderID = star
                        };
                        context.Currencies.Add(moneyType);

                        star += 1;
                    }
                    context.SaveChanges();
                }
                //Create collection of the primary keys of the Provinces
                int[] CurrencyIDs = context.Currencies.Select(s => s.ID).ToArray();

                if (!context.Companies.Any())
                {
                    context.Companies.AddRange(
                    new Company
                    {
                        Name = "No Indigo",
                        Location = "123 Random Street",
                        CredCheck = true,
                        BillingTermsID = random.Next(1, BillingTerms.Length),
                        CurrencyID = random.Next(1, Currencies.Length),
                        Phone = 2899098901,
                        Website = "www.google.ca",
                        BillingAddress1 = "21 Random Street",
                        BillingProvinceID = random.Next(1, CanadianProvinces.Length),
                        BillingPostalCode = "LLLLLL",
                        BillingCountryID = context.Countries.FirstOrDefault(d => d.Name == "United States").ID,
                        ShippingAddress1 = "424 Random Street",
                        ShippingAddress2 = "848 John Street",
                        ShippingProvinceID = random.Next(1, CanadianProvinces.Length),
                        ShippingPostalCode = "L2A3A1",
                        ShippingCountryID = context.Countries.FirstOrDefault(d => d.Name == "United States").ID,
                        Active = true,
                        Notes = "Good Company"
                    },
                    new Company
                    {
                        Name = "Amazon",
                        Location = "400 Random Street",
                        CredCheck = true,
                        BillingTermsID = random.Next(1, BillingTerms.Length),
                        CurrencyID = random.Next(1, Currencies.Length),
                        Phone = 1234567894,
                        Website = "www.amazon.ca",
                        BillingAddress1 = "506 Random Street",
                        BillingProvinceID = random.Next(1, CanadianProvinces.Length),
                        BillingPostalCode = "VVVVVV",
                        BillingCountryID = context.Countries.FirstOrDefault(d => d.Name == "United States").ID,
                        ShippingAddress1 = "888 Random Street",
                        ShippingProvinceID = random.Next(1, CanadianProvinces.Length),
                        ShippingPostalCode = "L2A4G4",
                        ShippingCountryID = context.Countries.FirstOrDefault(d => d.Name == "United States").ID,
                        Active = true,
                        Notes = "Bad Company"
                    },
                    new Company
                    {
                        Name = "Walmart",
                        Location = "999 Random Street",
                        CredCheck = true,
                        BillingTermsID = random.Next(1, BillingTerms.Length),
                        CurrencyID = random.Next(1, Currencies.Length),
                        Phone = 7894561234,
                        Website = "www.walmart.ca",
                        BillingAddress1 = "777 Random Street",
                        BillingProvinceID = random.Next(1, CanadianProvinces.Length),
                        BillingPostalCode = "L3G3G3",
                        BillingCountryID = context.Countries.FirstOrDefault(d => d.Name == "United States").ID,
                        ShippingAddress1 = "777 Random Street",
                        ShippingAddress2 = "666 John Street",
                        ShippingProvinceID = random.Next(1, CanadianProvinces.Length),
                        ShippingPostalCode = "L0B1B1",
                        ShippingCountryID = context.Countries.FirstOrDefault(d => d.Name == "United States").ID,
                        Active = true,
                        Notes = "Alright Company"
                    },
                    new Company
                    {
                        Name = "Maple Leaf Foods",
                        Location = "111 Random Street",
                        CredCheck = true,
                        BillingTermsID = random.Next(1, BillingTerms.Length),
                        CurrencyID = random.Next(1, Currencies.Length),
                        Phone = 4561237894,
                        Website = "www.mapleleaffoods.ca",
                        BillingAddress1 = "222 Random Street",
                        BillingProvinceID = random.Next(1, CanadianProvinces.Length),
                        BillingPostalCode = "L2A3G3",
                        BillingCountryID = context.Countries.FirstOrDefault(d => d.Name == "United States").ID,
                        ShippingAddress1 = "333 Random Street",
                        ShippingAddress2 = "334 John Street",
                        ShippingProvinceID = random.Next(1, CanadianProvinces.Length),
                        ShippingPostalCode = "L2A3G3",
                        ShippingCountryID = context.Countries.FirstOrDefault(d => d.Name == "United States").ID,
                        Active = true,
                        Notes = "Strong Company"
                    }
                    );
                    context.SaveChanges();
                }
                if (!context.Contacts.Any())
                {
                    context.Contacts.AddRange(
                    new Contact
                    {
                        FirstName = "John",
                        LastName = "Adams",
                        JobTitle = "Labourer",
                        CellPhone = 2899098901,
                        WorkPhone = 1234567891,
                        Email = "john@email.com",
                        Active = true,
                        CompanyID = context.Companies.FirstOrDefault(d => d.Name == "Maple Leaf Foods").ID,
                    },
                    new Contact
                    {
                        FirstName = "Mary",
                        LastName = "Ann",
                        JobTitle = "Manager",
                        CellPhone = 9874569877,
                        WorkPhone = 1234567892,
                        Email = "mary@email.com",
                        Active = true,
                        CompanyID = context.Companies.FirstOrDefault(d => d.Name == "Walmart").ID,
                    },
                    new Contact
                    {
                        FirstName = "Beth",
                        LastName = "Doe",
                        JobTitle = "Electrician",
                        CellPhone = 1231231234,
                        WorkPhone = 1234567893,
                        Email = "beth@email.com",
                        Active = true,
                        CompanyID = context.Companies.FirstOrDefault(d => d.Name == "No Indigo").ID,
                    },
                    new Contact
                    {
                        FirstName = "Bob",
                        LastName = "Murphy",
                        JobTitle = "Labourer",
                        CellPhone = 4561237894,
                        WorkPhone = 1234567894,
                        Email = "bob@email.com",
                        Active = true,
                        CompanyID = context.Companies.FirstOrDefault(d => d.Name == "Amazon").ID,
                    },
                    new Contact
                    {
                        FirstName = "Link",
                        LastName = "Zelda",
                        JobTitle = "CEO",
                        CellPhone = 9517531234,
                        WorkPhone = 1234567895,
                        Email = "link@email.com",
                        Active = true,
                        CompanyID = context.Companies.FirstOrDefault(d => d.Name == "Maple Leaf Foods").ID,
                    }
                    );
                    context.SaveChanges();
                }
                string[] Catagories = new string[] { "Christmas Card", "News letter", "Marketing Material" };
                if (!context.Catagories.Any())
                {
                    foreach (string s in Catagories)
                    {
                        int star = 1;
                        Catagory list = new Catagory
                        {
                            Name = s,
                            OrderID = star
                        };
                        context.Catagories.Add(list);
                        star += 1;
                    }
                    context.SaveChanges();
                }
                //Create collection of the primary keys of the Provinces
                int[] CatagoryIDs = context.Catagories.Select(s => s.ID).ToArray();

                if (!context.ContactCatagories.Any())
                {
                    context.ContactCatagories.AddRange(
                    new ContactCatagory
                    {
                        CatagoryID = random.Next(1, Catagories.Length),
                        ContactID = context.Contacts.FirstOrDefault(d => d.CellPhone == 9517531234).ID
                    },
                    new ContactCatagory
                    {
                        CatagoryID = random.Next(1, Catagories.Length),
                        ContactID = context.Contacts.FirstOrDefault(d => d.CellPhone == 4561237894).ID
                    },
                    new ContactCatagory
                    {
                        CatagoryID = random.Next(1, Catagories.Length),
                        ContactID = context.Contacts.FirstOrDefault(d => d.CellPhone == 2899098901).ID
                    }
                    );
                    context.SaveChanges();
                }


                string[] Types = new string[] { "Customer", "Vendor", "Contractor" };
                if (!context.Types.Any())
                {
                    foreach (string s in Types)
                    {
                        int star = 1;
                        Models.CType list = new Models.CType
                        {
                            Name = s,
                            OrderID = star
                        };
                        context.Types.Add(list);

                        star += 1;
                    }
                    context.SaveChanges();
                }

                string[] SubTypesCustomer = new string[] { "Poultry", "Beef", "Pork", "Bakery", "Vegetarian", "Vegetables & Produce", "Other Food", "Compressed Gas", "Cryogenic Pipe", "Custom Fabrication", "IQF Exhaust", "NFPA Exhaust", "Construction", "Conveyors", "Manifolds", "Plumbing", "Beverage", "HPP" };
                string[] SubTypesVendor = new string[] { "Conveyor & Fabrication","Professional Service","Office Supplies","Shop Supplies","Cryogenic","Plumbing / Piping","Transportation","HVAC & Exhaust Systems","Outsourced Fabrication & Services","Electrical Components" };
                string[] SubTypesContractor = new string[] { "Welding","Plumbing","Electrical","Engineering","Fabrication","General Contractor","Metal Forming","Metal Cutting" };

                if (!context.SubTypes.Any())
                {
                    if(SubTypesCustomer.Length != 0)
                    {
                        int star = 1;
                        foreach (string s in SubTypesCustomer)
                        {

                            SubType list = new SubType
                            {
                                Name = s,
                                TypeID = context.Types.FirstOrDefault(d => d.Name == "Customer").ID,
                                OrderID = star
                            };
                            context.SubTypes.Add(list);
                            star += 1;
                        }
                    }

                    if (SubTypesVendor.Length != 0)
                    {
                        int star = 1;
                        foreach (string s in SubTypesVendor)
                        {
                            SubType list = new SubType
                            {
                                Name = s,
                                TypeID = context.Types.FirstOrDefault(d => d.Name == "Vendor").ID,
                                OrderID = star
                            };
                            context.SubTypes.Add(list);
                            star += 1;
                        }
                    }


                    if (SubTypesContractor.Length != 0)
                    {
                        int star = 1;
                        foreach (string s in SubTypesContractor)
                        {

                            SubType list = new SubType
                            {
                                Name = s,
                                TypeID = context.Types.FirstOrDefault(d => d.Name == "Contractor").ID,
                                OrderID = star
                            };
                            context.SubTypes.Add(list);
                            star += 1;
                        }
                    }


                        
                    context.SaveChanges();
                }
                //Create collection of the primary keys of the types
                int[] SubTypeIDs = context.SubTypes.Select(s => s.ID).ToArray();



                if (!context.CompanyTypes.Any())
                {
                    context.CompanyTypes.AddRange(
                    new CompanyType
                    {
                        CompanyID = context.Companies.FirstOrDefault(d => d.Name == "Maple Leaf Foods").ID,
                        TypeID = context.Types.FirstOrDefault(d => d.Name == "Customer").ID
                    },
                    new CompanyType
                    {
                        CompanyID = context.Companies.FirstOrDefault(d => d.Name == "Walmart").ID,
                        TypeID = context.Types.FirstOrDefault(d => d.Name == "Customer").ID
                    },
                    new CompanyType
                    {
                        CompanyID = context.Companies.FirstOrDefault(d => d.Name == "No Indigo").ID,
                        TypeID = context.Types.FirstOrDefault(d => d.Name == "Customer").ID
                    },
                    new CompanyType
                    {
                        CompanyID = context.Companies.FirstOrDefault(d => d.Name == "Maple Leaf Foods").ID,
                        TypeID = context.Types.FirstOrDefault(d => d.Name == "Vendor").ID
                    },
                    new CompanyType
                    {
                        CompanyID = context.Companies.FirstOrDefault(d => d.Name == "Walmart").ID,
                        TypeID = context.Types.FirstOrDefault(d => d.Name == "Contractor").ID
                    }
                    ,
                    new CompanyType
                    {
                        CompanyID = context.Companies.FirstOrDefault(d => d.Name == "No Indigo").ID,
                        TypeID = context.Types.FirstOrDefault(d => d.Name == "Vendor").ID
                    },
                    new CompanyType
                    {
                        CompanyID = context.Companies.FirstOrDefault(d => d.Name == "Amazon").ID,
                        TypeID = context.Types.FirstOrDefault(d => d.Name == "Contractor").ID
                    }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
