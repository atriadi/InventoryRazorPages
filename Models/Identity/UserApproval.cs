using InventorySandbox.BaseEntity;
using System.ComponentModel.DataAnnotations;

namespace InventorySandbox.Models.Identity
{
    public class UserApproval : BaseDomainDetail
    {

        public UserApproval()
        {
            UserApprovalDetails = new HashSet<UserApprovalDetail>();
        }
        public Guid UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public Guid RoleId { get; set; }
        public virtual ApplicationRole? Role { get; set; }
        public DateTime DocumentDate { get; set; }
        [MaxLength(1000)]
        public string? Note { get; set; }
        public virtual ICollection<UserApprovalDetail> UserApprovalDetails { get; set; }
    }

}
