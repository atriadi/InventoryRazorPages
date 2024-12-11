using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventorySandbox.Models.Pharmacy;

namespace InventorySandbox.Pages.Pharmacy.SalePages
{
    public class EditModel : PageModel
    {
        private readonly InventorySandbox.Models.PersistenceDbContext _context;

        public EditModel(InventorySandbox.Models.PersistenceDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Sale Sale { get; set; } = default!;
        public int previousQty { get; set; }
        public decimal previouPrice { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales.FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            Sale = sale;

            ViewData["MedicationList"] = new SelectList(
                    _context.Medications.Select(s => new
                    {
                        Id = s.Id,
                        DisplayText = $"{s.Code} - {s.Name}"  // Concatenate Code and Name
                    }),
                    "Id",
                    "DisplayText"
                );

            // Check for previous sales of the same medication
            previousQty = sale.Qty;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int previousQty, decimal previouPrice)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var results = await _context.Sales.FirstOrDefaultAsync(m => m.Id == Sale.Id);
            if (results == null)
            {
                return NotFound();
            }

            // Update Sale properties
            results.MedicationId = Sale.MedicationId;
            results.DocumentDate = Sale.DocumentDate;
            results.Qty = Sale.Qty;
            results.Price = Sale.Price;
            results.ModifiedUser = "SYS";
            results.ModifiedDate = DateTime.Now;

            _context.Attach(results).State = EntityState.Modified;

            try
            {
                // Save updated Sale record to the database
                await _context.SaveChangesAsync();

                // Find the associated medication for this sale
                var medication = await _context.Medications.FirstOrDefaultAsync(m => m.Id == Sale.MedicationId);

                if (medication != null)
                {

                    // Update the medication's quantity
                    if (Sale.Qty >= previousQty)
                    {
                        var resultQty = Sale.Qty - previousQty;
                        medication.Qty = medication.Qty + resultQty;
                    }
                    else
                    {
                        var resultQty = previousQty - Sale.Qty;
                        medication.Qty = medication.Qty - resultQty;
                    }

                    // Save the updated medication details to the database
                    _context.Medications.Update(medication);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(Sale.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }


        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }
    }
}