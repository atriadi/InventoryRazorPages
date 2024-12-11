using System.ComponentModel.DataAnnotations;
using InventorySandbox.BaseEntity;
using InventorySandbox.Models.Organization;

namespace InventorySandbox.Models.Identity
{
    public class ApplicationUserCompany : BaseIdInt, IAudited
    {
        public Guid UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public int CompanyId { get; set; }
        public virtual Company? Company { get; set; }
        [MaxLength(255)]
        public string? CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        [MaxLength(255)]
        public string? ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
