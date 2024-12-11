using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventorySandbox.Models.Identity;
using InventorySandbox.Models.Pharmacy;
using Microsoft.AspNetCore.Identity;

namespace InventorySandbox.Pages.Identity.ApplicationUserPages
{
    public class EditModel : PageModel
    {
        private readonly InventorySandbox.Models.PersistenceDbContext _context;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;


        public EditModel(InventorySandbox.Models.PersistenceDbContext context, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationuser = await _context.ApplicationUsers.FirstOrDefaultAsync(m => m.Id == id);
            if (applicationuser == null)
            {
                return NotFound();
            }
            ApplicationUser = applicationuser;

            ViewData["EmployeeList"] = new SelectList(
                     _context.Employees.Select(s => new
                     {
                         Id = s.Id,
                         DisplayText = $"{s.Code} - {s.Name}"  // Concatenate Code and Name
                     }),
                     "Id",
                     "DisplayText"
                 );

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

            var results = await _context.ApplicationUsers.FirstOrDefaultAsync(m => m.Id == ApplicationUser.Id);
            if (results == null)
            {
                return NotFound();
            }

            results.EmployeeId = ApplicationUser.EmployeeId;
            results.FirstName = ApplicationUser.FirstName;
            results.LastName = ApplicationUser.LastName;
            results.IsAzureUser = ApplicationUser.IsAzureUser;
            results.UserName = ApplicationUser.UserName;
            results.PasswordOwn = ApplicationUser.PasswordOwn;
            results.Email = ApplicationUser.Email;
            results.EmailConfirmed = ApplicationUser.EmailConfirmed;
            results.PhoneNumber = ApplicationUser.PhoneNumber;
            results.PhoneNumberConfirmed = ApplicationUser.PhoneNumberConfirmed;
            results.ModifiedUser = "SYS";
            results.ModifiedDate = DateTime.Now;

            // Hash the password before saving
            if (results.PasswordOwn != null)
            {
                var hashedPassword = _passwordHasher.HashPassword(ApplicationUser, results.PasswordOwn);
                results.PasswordHash = hashedPassword;  // Save the hashed password instead of the plain one
            }

            _context.Attach(results).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(ApplicationUser.Id))
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

        private bool ApplicationUserExists(Guid id)
        {
            return _context.ApplicationUsers.Any(e => e.Id == id);
        }
    }
}
