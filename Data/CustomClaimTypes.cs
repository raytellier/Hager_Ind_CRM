﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.Data
{
    public class CustomClaimTypes
    {
        public const string Permission = "hager/permission";
    }
    public static class Companies
    {
        public const string Create = "companies.create";
        public const string Read = "companies.read";
        public const string Detail = "companies.detail";
        public const string Update = "companies.update";
        public const string Delete = "companies.delete";
    }
    public static class Contacts
    {
        public const string Create = "contacts.create";
        public const string Read = "contacts.read";
        public const string Detail = "contacts.detail";
        public const string Update = "contacts.update";
        public const string Delete = "contacts.delete";
    }

    public static class Employees
    {
        public const string Create = "employees.create";
        public const string Read = "employees.read";
        public const string Detail = "employees.detail";
        public const string Update = "employees.update";
        public const string Delete = "employees.delete";
        public const string Privacy = "employees.privacy";
    }

    public static class Users
    {
        public const string Create = "users.create";
        public const string Read = "users.read";
        public const string Detail = "users.detail";
        public const string Update = "users.update";
        public const string Delete = "users.delete";
    }

    public static class Roles
    {
        public const string Create = "roles.create";
        public const string Read = "roles.read";
        public const string Detail = "roles.detail";
        public const string Update = "roles.update";
        public const string Delete = "roles.delete";
    }

    public static class Lists
    {
        public const string Manage = "lists.manage";
    }
}
