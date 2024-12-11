using System.ComponentModel.DataAnnotations;
using InventorySandbox.BaseEntity;

namespace InventorySandbox.Models.Identity
{
    public class ApplicationControllerMethod : BaseIdInt, IAudited
    {
        [MaxLength(255)]
        public string Name { get; set; } = default!;
        public int ControllerId { get; set; }
        public virtual ApplicationController Controller { get; set; } = default!;
        [MaxLength(255)]
        public string? CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        [MaxLength(255)]
        public string? ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
