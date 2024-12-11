using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InventorySandbox.Models.HumanResource;
using InventorySandbox.Models.Organization;

namespace InventorySandbox.Pages.HumanResource.EmployeePages
{
    public class EditModel : PageModel
    {
        private readonly InventorySandbox.Models.PersistenceDbContext _context;

        public EditModel(InventorySandbox.Models.PersistenceDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Employee Employee { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee =  await _context.Employees.FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            Employee = employee;
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

            var results = await _context.Employees.FirstOrDefaultAsync(m => m.Id == Employee.Id);
            if (results == null)
            {
                return NotFound();
            }

            results.Code = Employee.Code;
            results.Name = Employee.Name;
            results.Note = Employee.Note;
            results.ModifiedUser = "SYS";
            results.ModifiedDate = DateTime.Now;

            _context.Attach(results).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(Employee.Id))
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

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
