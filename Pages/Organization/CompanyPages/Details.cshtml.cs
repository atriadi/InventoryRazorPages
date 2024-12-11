using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InventorySandbox.Models.Organization;

namespace InventorySandbox.Pages.Organization.CompanyPages
{
    public class DetailsModel : PageModel
    {
        private readonly InventorySandbox.Models.PersistenceDbContext _context;

        public DetailsModel(InventorySandbox.Models.PersistenceDbContext context)
        {
            _context = context;
        }

        public Company Company { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            else
            {
                Company = company;
            }
            return Page();
        }
    }
}
