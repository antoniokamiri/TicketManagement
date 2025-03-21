using System.ComponentModel;
using System.Reflection;

namespace Infrastructure.PermissionSet;

public static partial class Permissions
{
    /// <summary>
    ///     Returns a list of Permissions.
    /// </summary>
    /// <returns></returns>
    public static List<string> GetRegisteredPermissions()
    {
        var permissions = new List<string>();
        foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c =>
                     c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
        {
            var propertyValue = prop.GetValue(null);
            if (propertyValue is not null)
                permissions.Add((string)propertyValue);
        }

        return permissions;
    }

    [DisplayName("Audit Permissions")]
    [Description("Set permissions for audit operations.")]
    public static class AuditTrails
    {
        public const string View = "Permissions.AuditTrails.View";
        public const string Search = "Permissions.AuditTrails.Search";
        public const string Export = "Permissions.AuditTrails.Export";
    }

    [DisplayName("Log Permissions")]
    [Description("Set permissions for log operations.")]
    public static class Logs
    {
        public const string View = "Permissions.Logs.View";
        public const string Search = "Permissions.Logs.Search";
        public const string Export = "Permissions.Logs.Export";
        public const string Purge = "Permissions.Logs.Purge";
    }

    [DisplayName("User Permissions")]
    [Description("Set permissions for user operations.")]
    public static class Users
    {
        public const string View = "Permissions.Users.View";
        public const string Create = "Permissions.Users.Create";
        public const string Edit = "Permissions.Users.Edit";
        public const string Delete = "Permissions.Users.Delete";
        public const string Search = "Permissions.Users.Search";
        public const string Import = "Permissions.Users.Import";
        public const string Export = "Permissions.Dictionaries.Export";
        public const string ManageRoles = "Permissions.Users.ManageRoles";
        public const string RestPassword = "Permissions.Users.RestPassword";
        public const string SendRestPasswordMail = "Permissions.Users.SendRestPasswordMail";
        public const string ManagePermissions = "Permissions.Users.Permissions";
        public const string Deactivation = "Permissions.Users.Activation/Deactivation";
    }

    [DisplayName("Role Permissions")]
    [Description("Set permissions for role operations.")]
    public static class Roles
    {
        public const string View = "Permissions.Roles.View";
        public const string Create = "Permissions.Roles.Create";
        public const string Edit = "Permissions.Roles.Edit";
        public const string Delete = "Permissions.Roles.Delete";
        public const string Search = "Permissions.Roles.Search";
        public const string Export = "Permissions.Roles.Export";
        public const string Import = "Permissions.Roles.Import";
        public const string ManagePermissions = "Permissions.Roles.Permissions";
        public const string ManageNavigation = "Permissions.Roles.Navigation";
    }


    [DisplayName("Dashboard Permissions")]
    [Description("Set permissions for dashboard operations.")]
    public static class Dashboards
    {
        public const string View = "Permissions.Dashboards.View";
    }

    [DisplayName("Job Permissions")]
    [Description("Set permissions for job operations.")]
    public static class Hangfire
    {
        public const string View = "Permissions.Hangfire.View";
        public const string Jobs = "Permissions.Hangfire.Jobs";
    }
}

public static partial class Permissions
{
    [DisplayName("discussion Permissions")]
    [Description("Set permissions for discussion operations.")]
    public static class Discussions
    {

        [Description("Allows viewing discussion details.")]
        public const string View = "Permissions.Discussions.View";

        [Description("Allows creating discussion details.")]
        public const string Create = "Permissions.Discussions.Create";

        [Description("Allows editing discussion details.")]
        public const string Edit = "Permissions.Discussions.Edit";

        [Description("Allows deleting discussion details.")]
        public const string Delete = "Permissions.Discussions.Delete";

        [Description("Allows print discussion details.")]
        public const string Print = "Permissions.Discussions.Print";

        [Description("Allows searching discussion details.")]
        public const string Search = "Permissions.Discussions.Search";

        [Description("Allows exporting discussion details.")]
        public const string Export = "Permissions.Discussions.Export";
    }



    [DisplayName("Category Permissions")]
    [Description("Set permissions for category operations.")]
    public static class Categories
    {
        public const string View = "Permissions.Categories.View";
        public const string Create = "Permissions.Categories.Create";
        public const string Edit = "Permissions.Categories.Edit";
        public const string Delete = "Permissions.Categories.Delete";
        public const string Search = "Permissions.Categories.Search";
        public const string Export = "Permissions.Categories.Export";
        public const string Import = "Permissions.Categories.Import";
    }
}