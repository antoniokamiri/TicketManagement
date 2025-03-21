using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    public User()
    {
        UserClaims = new HashSet<ApplicationUserClaim>();
        UserRoles = new HashSet<ApplicationUserRole>();
        Logins = new HashSet<ApplicationUserLogin>();
        Tokens = new HashSet<ApplicationUserToken>();
    }

    public string? DisplayName { get; set; }

    public string? ProfilePictureDataUrl { get; set; }

    public bool AccountConfirmed { get; set; }
    public bool IsLive { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public virtual ICollection<ApplicationUserClaim> UserClaims { get; set; }
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    public virtual ICollection<ApplicationUserLogin> Logins { get; set; }
    public virtual ICollection<ApplicationUserToken> Tokens { get; set; }

    public DateTime? Created { get; set; }
    public string? CreatedBy { get; set; }
    public User? CreatedByUser { get; set; } = null;
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public User? LastModifiedByUser { get; set; } = null;

    public string? TimeZoneId { get; set; }
    public string? LanguageCode { get; set; }
}
public class ApplicationRole : IdentityRole
{
    public ApplicationRole()
    {
        RoleClaims = new HashSet<ApplicationRoleClaim>();
        UserRoles = new HashSet<ApplicationUserRole>();
    }

    public ApplicationRole(string roleName) : base(roleName)
    {
        RoleClaims = new HashSet<ApplicationRoleClaim>();
        UserRoles = new HashSet<ApplicationUserRole>();
    }
    public string? Description { get; set; }
    public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }

}

public class ApplicationUserRole : IdentityUserRole<string>
{
    public virtual User User { get; set; } = default!;
    public virtual ApplicationRole Role { get; set; } = default!;
}

public class ApplicationRoleClaim : IdentityRoleClaim<string>
{
    public string? Description { get; set; }
    public string? Group { get; set; }
    public virtual ApplicationRole Role { get; set; } = default!;
}

public class ApplicationUserClaim : IdentityUserClaim<string>
{
    public string? Description { get; set; }
    public virtual User User { get; set; } = default!;
}

public class ApplicationUserLogin : IdentityUserLogin<string>
{
    public virtual User User { get; set; } = default!;
}
public class ApplicationUserToken : IdentityUserToken<string>
{
    public virtual User User { get; set; } = default!;
}