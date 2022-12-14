using System.Collections.Generic;

namespace PermissionManagement.MVC.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }

        // public static class Products
        // {
        //     public const string View = "Permissions.Products.View";
        //     public const string Create = "Permissions.Products.Create";
        //     public const string Edit = "Permissions.Products.Edit";
        //     public const string Delete = "Permissions.Products.Delete";
        // }

        public static class Dues
        {
            public const string View = "Permissions.Dues.View";
            public const string Create = "Permissions.Dues.Create";
            public const string Edit = "Permissions.Dues.Edit";
            public const string Delete = "Permissions.Dues.Delete";
        }
        
        public static class LeaseHolder
        {
            public const string View = "Permissions.LeaseHolder.View";
            public const string Create = "Permissions.LeaseHolder.Create";
            public const string Edit = "Permissions.LeaseHolder.Edit";
            public const string Delete = "Permissions.LeaseHolder.Delete";
        }
        public static class File
        {
            public const string View = "Permissions.File.View";
            public const string Create = "Permissions.File.Create";
            public const string Edit = "Permissions.File.Edit";
            public const string Delete = "Permissions.File.Delete";
        }
        
    }
}