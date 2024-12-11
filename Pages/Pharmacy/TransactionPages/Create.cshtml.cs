using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using InventorySandbox.Models.Pharmacy;
using Microsoft.AspNetCore.Mvc.Rendering;
using InventorySandbox.Helper;

namespace InventorySandbox.Pages.Pharmacy.TransactionPages
{
    public class CreateModel : PageModel
    {
        private readonly InventorySandbox.Models.PersistenceDbContext _context;

        public CreateModel(InventorySandbox.Models.PersistenceDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Transaction Transaction { get; set; }
        public List<TransactionDetail> TransactionDetails { get; set; } = new List<TransactionDetail>();

        public void OnGet()
        {
            // Initialize Transaction if needed
            Transaction = new Transaction();

            Transaction.DocumentDate = DateTime.Now;

            ViewData["MedicationList"] = new SelectList(
                    _context.Medications.Select(s => new
                    {
                        Id = s.Id,
                        DisplayText = $"{s.Code} - {s.Name}"  // Concatenate Code and Name
                    }),
                    "Id",
                    "DisplayText"
                );
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Use the helper method to generate the document number
            Transaction.DocumentNumber = await DocumentNumberHelper.GenerateDocumentNumberAsync(_context);

            // Add the new transaction to the database
            _context.Transactions.Add(Transaction);
            await _context.SaveChangesAsync();

            // Redirect back to the index page
            return RedirectToPage("./Index");
        }

    }
}
