using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.Data
{
    public class PolicyTypes
    {
        public static class Companies
        {
            public const string Create = "companies.create.policy";
            public const string Read = "companies.read.policy";
            public const string Detail = "companies.detail.policy";
            public const string Update = "companies.update.policy";
            public const string Delete = "companies.delete.policy";
        }

        public static class Contacts
        {
            public const string Create = "contacts.create.policy";
            public const string Read = "contacts.read.policy";
            public const string Detail = "contacts.detail.policy";
            public const string Update = "contacts.update.policy";
            public const string Delete = "contacts.delete.policy";
        }

        public static class Employees
        {
            public const string Create = "employees.create.policy";
            public const string Read = "employees.read.policy";
            public const string Detail = "employees.detail.policy";
            public const string Update = "employees.update.policy";
            public const string Delete = "employees.delete.policy";
            public const string Privacy = "employees.privacy.policy";
        }

        public static class Users
        {
            public const string Create = "users.create.policy";
            public const string Read = "users.read.policy";
            public const string Detail = "users.detail.policy";
            public const string Update = "users.update.policy";
            public const string Delete = "users.delete.policy";
        }

        public static class Lists
        {
            public const string Manage = "lists.manage.policy";
        }
    }
}
