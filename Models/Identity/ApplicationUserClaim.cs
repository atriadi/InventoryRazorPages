using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InventorySandbox.Models.Identity
{
    public class ApplicationUserClaim : IdentityUserClaim<Guid>
    {
        public int ControllerMethodId { get; set; }
        public virtual ApplicationController? ControllerMethod { get; set; }
        public virtual ApplicationUser? User { get; set; }
        [MaxLength(255)]
        public string? CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        [MaxLength(255)]
        public string? ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
