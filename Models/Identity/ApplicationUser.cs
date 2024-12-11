using InventorySandbox.BaseEntity;
using InventorySandbox.Models.HumanResource;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InventorySandbox.Models.Identity
{
    public class ApplicationUser : IdentityUser<Guid>, IAudited, IActivatable
    {
        [MaxLength(125)]
        public string? FirstName { get; set; }
        [MaxLength(125)]
        public string? LastName { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public bool IsAzureUser { get; set; } = true;
        public bool? IsActive { get; set; } = true;
        [MaxLength(255)]
        public string? CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public byte[]? Version { get; set; }
        public string? SVersion { get; set; }
        [MaxLength(255)]
        public string? ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [MaxLength(255)]
        public string? PasswordOwn { get; set; }
        public int? EmployeeId { get; set; }
        [AdaptIgnore]
        public virtual Employee? Employee { get; set; }
        public ApplicationUser()
        {
            Companies = new HashSet<ApplicationUserCompany>();
            Roles = new HashSet<ApplicationUserRole>();
            Tokens = new HashSet<ApplicationUserToken>();
            Logins = new HashSet<ApplicationUserLogin>();
            Claims = new HashSet<ApplicationUserClaim>();
            Infos = new HashSet<ApplicationUserInfo>();
        }
        public virtual ICollection<ApplicationUserCompany> Companies { get; set; }
        public virtual ICollection<ApplicationUserClaim> Claims { get; set; }
        public virtual ICollection<ApplicationUserLogin> Logins { get; set; }
        public virtual ICollection<ApplicationUserToken> Tokens { get; set; }
        public virtual ICollection<ApplicationUserRole> Roles { get; set; }
        public virtual ICollection<ApplicationUserInfo> Infos { get; set; }
    }
}
