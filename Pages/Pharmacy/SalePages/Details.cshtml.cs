using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InventorySandbox.Models.Pharmacy;

namespace InventorySandbox.Pages.Pharmacy.SalePages
{
    public class DetailsModel : PageModel
    {
        private readonly InventorySandbox.Models.PersistenceDbContext _context;

        public DetailsModel(InventorySandbox.Models.PersistenceDbContext context)
        {
            _context = context;
        }

        public Sale Sale { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales.AsNoTracking()
                            .Include(m => m.Medication)  // This eagerly loads the Supplier data
                            .FirstOrDefaultAsync(m => m.Id == id);

            if (sale == null)
            {
                return NotFound();
            }
            else
            {
                Sale = sale;
            }
            return Page();
        }
    }
}
