using InventorySandbox.BaseEntity;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InventorySandbox.Models.Identity
{
    public class ApplicationRoleClaim : IdentityRoleClaim<Guid>, IAudited
    {
        public int ControllerMethodId { get; set; }
        public virtual ApplicationControllerMethod? ControllerMethod { get; set; }
     
        public virtual ApplicationRole? Role { get; set; }
        [MaxLength(255)]
        public string? CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        [MaxLength(255)]
        public string? ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
