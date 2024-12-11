using Microsoft.AspNetCore.Identity;

namespace InventorySandbox.Models.Identity
{
    public class ApplicationUserLogin : IdentityUserLogin<Guid>
    {
        public virtual ApplicationUser? User { get; set; }
    }
}
