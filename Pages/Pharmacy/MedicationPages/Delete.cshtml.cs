using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InventorySandbox.Models.Pharmacy;

namespace InventorySandbox.Pages.Pharmacy.MedicationPages
{
    public class DeleteModel : PageModel
    {
        private readonly InventorySandbox.Models.PersistenceDbContext _context;

        public DeleteModel(InventorySandbox.Models.PersistenceDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Medication Medication { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await _context.Medications.AsNoTracking()
                            .Include(m => m.Supplier)  // This eagerly loads the Supplier data
                            .FirstOrDefaultAsync(m => m.Id == id);

            if (medication == null)
            {
                return NotFound();
            }
            else
            {
                Medication = medication;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await _context.Medications.FindAsync(id);
            if (medication != null)
            {
                Medication = medication;
                _context.Medications.Remove(Medication);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
