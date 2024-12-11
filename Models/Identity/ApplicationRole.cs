using InventorySandbox.BaseEntity;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InventorySandbox.Models.Identity
{
    public class ApplicationRole : IdentityRole<Guid>, IAudited
    {
        [MaxLength(255)]
        public string? Description { get; set; }
        [MaxLength(255)]
        public string? CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        [MaxLength(255)]
        public string? ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[]? Version { get; set; }
        public string? SVersion { get; set; }

        public ApplicationRole()
        {
            Users = new HashSet<ApplicationUserRole>();
            Claims = new HashSet<ApplicationRoleClaim>();
        }
        public ApplicationRole(string name, string? description = null)
        {
            Description = description;
            Name = name;

            Users = new HashSet<ApplicationUserRole>();
            Claims = new HashSet<ApplicationRoleClaim>();
        }

        public virtual ICollection<ApplicationUserRole> Users { get; set; }
        public virtual ICollection<ApplicationRoleClaim> Claims { get; set; }
    }
}
