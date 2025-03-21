namespace Infrastructure.Common;
public static class Constants
{
    public const string DEFAULT_PASSWORD = "Reset!@123";
}

public abstract class RoleName
{
    public const string Admin = nameof(Admin);
    public const string Basic = nameof(Basic);
    public const string Users = nameof(Users);
}

public static class ApplicationClaimTypes
{
    public const string Status = "Status";
    public const string Permission = "Permission";
    public const string AssignedRoles = "AssignedRoles";
    public const string ProfilePictureDataUrl = "ProfilePictureDataUrl";
}