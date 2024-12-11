using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventorySandbox.Models.Pharmacy;
using InventorySandbox.Helper;

namespace InventorySandbox.Pages.Pharmacy.MedicationPages
{
    public class EditModel : PageModel
    {
        private readonly InventorySandbox.Models.PersistenceDbContext _context;

        public EditModel(InventorySandbox.Models.PersistenceDbContext context)
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

            var medication = await _context.Medications.FirstOrDefaultAsync(m => m.Id == id);
            if (medication == null)
            {
                return NotFound();
            }

            Medication = medication;

            ViewData["SupplierList"] = new SelectList(
                _context.Suppliers.Select(s => new
                {
                    Id = s.Id,
                    DisplayText = $"{s.Code} - {s.Name}"  // Concatenate Code and Name
                }),
                "Id",
                "DisplayText"
            );

            var categoryList = new SelectList(EnumList.GetEnumSelectList<EnumItemCategory>(), "Value", "Text");

            ViewData["CategoryList"] = new SelectList(categoryList, "Value", "Text");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var results = await _context.Medications.FirstOrDefaultAsync(m => m.Id == Medication.Id);
            if (results == null)
            {
                return NotFound();
            }

            results.SupplierId = Medication.SupplierId;
            results.Code = Medication.Code;
            results.Name = Medication.Name;
            results.Category = Medication.Category;
            results.Qty = Medication.Qty;
            results.Price = Medication.Price;
            results.Note = Medication.Note;
            results.ModifiedUser = "SYS";
            results.ModifiedDate = DateTime.Now;

            _context.Attach(results).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicationExists(Medication.Id))
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

        private bool MedicationExists(int id)
        {
            return _context.Medications.Any(e => e.Id == id);
        }
    }
}
