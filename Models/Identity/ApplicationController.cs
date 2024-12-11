using InventorySandbox.BaseEntity;
using System.ComponentModel.DataAnnotations;

namespace InventorySandbox.Models.Identity
{
    public class ApplicationController : BaseIdInt, IAudited
    {
        public ApplicationController()
        {
            Methods = new HashSet<ApplicationControllerMethod>();
        }
        [MaxLength(255)]
        public string Name { get; set; } = default!;
        [MaxLength(255)]
        public string? CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        [MaxLength(255)]
        public string? ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public virtual ICollection<ApplicationControllerMethod> Methods { get; set; }
    }
}
