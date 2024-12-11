using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InventorySandbox.Models.Identity;

namespace InventorySandbox.Pages.Identity.ApplicationUserPages
{
    public class DetailsModel : PageModel
    {
        private readonly InventorySandbox.Models.PersistenceDbContext _context;

        public DetailsModel(InventorySandbox.Models.PersistenceDbContext context)
        {
            _context = context;
        }

        public ApplicationUser ApplicationUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationuser = await _context.ApplicationUsers.FirstOrDefaultAsync(m => m.Id == id);
            if (applicationuser == null)
            {
                return NotFound();
            }
            else
            {
                ApplicationUser = applicationuser;
            }
            return Page();
        }
    }
}
