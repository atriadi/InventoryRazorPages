using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using InventorySandbox.Models.HumanResource;

namespace InventorySandbox.Pages.HumanResource.EmployeePages
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
        public Employee Employee { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Employees.Add(Employee);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
