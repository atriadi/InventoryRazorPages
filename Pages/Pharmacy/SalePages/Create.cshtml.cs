using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using InventorySandbox.Models.Pharmacy;
using Microsoft.EntityFrameworkCore;

namespace InventorySandbox.Pages.Pharmacy.SalePages
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
            ViewData["MedicationList"] = new SelectList(
                     _context.Medications.Select(s => new
                     {
                         Id = s.Id,
                         DisplayText = $"{s.Code} - {s.Name}"  // Concatenate Code and Name
                     }),
                     "Id",
                     "DisplayText"
                 );

            return Page();
        }

        [BindProperty]
        public Sale Sale { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Add the sale to the Sales table
            _context.Sales.Add(Sale);
            await _context.SaveChangesAsync();

            // Find the associated medication for this sale
            var medication = await _context.Medications.FirstOrDefaultAsync(m => m.Id == Sale.MedicationId);

            if (medication != null)
            {
                // Update the medication's quantity and price
                medication.Qty += Sale.Qty;  // Deduct the sold quantity

                // Save the updated medication details to the database
                _context.Medications.Update(medication);
                await _context.SaveChangesAsync();
            }

            // Redirect to the Index page after successful save
            return RedirectToPage("./Index");
        }

    }
}