using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using InventorySandbox.Models.Pharmacy;

namespace InventorySandbox.Pages.Pharmacy.SupplierPages
{
    public class CreateModel : PageModel
    {
        private readonly InventorySandbox.Models.PersistenceDbContext _context;

        public CreateModel(InventorySandbox.Models.PersistenceDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Supplier Supplier { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Suppliers.Add(Supplier);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
