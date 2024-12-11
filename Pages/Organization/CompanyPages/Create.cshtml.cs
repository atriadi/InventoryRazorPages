using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using InventorySandbox.Models.Organization;
using InventorySandbox.Helper;

namespace InventorySandbox.Pages.Organization.CompanyPages
{
    public class CreateModel : PageModel
    {
        private readonly InventorySandbox.Models.PersistenceDbContext _context;
        public MessageHelper MessageHelper { get; set; } // MessageHelper for status messages

        // Initialize the context and the helper via Dependency Injection
        public CreateModel(InventorySandbox.Models.PersistenceDbContext context)
        {
            _context = context;
            MessageHelper = new MessageHelper(); // Ensure MessageHelper is initialized
        }

        [BindProperty]
        public Company Company { get; set; } = default!; // Ensures Company is not null

        public IActionResult OnGet()
        {
            return Page();
        }

        // OnPostAsync method for handling form submissions
        public async Task<IActionResult> OnPostAsync()
        {
            // Check if the form is valid
            if (!ModelState.IsValid)
            {
                // Set error message if the form validation fails
                MessageHelper.SetErrorMessage("There were errors in the form.");
                return Page();
            }

            // Check if the company code already exists (using the generic duplicate checker)
            var isCodeDuplicate = await CheckCodeUniqueHelper.IsDuplicateAsync<Company>(_context, "Code", Company.Code);

            if (isCodeDuplicate)
            {
                // If the code exists, show an error message
                MessageHelper.SetErrorMessage("A company with this code already exists.");
                return Page();
            }

            try
            {
                // Add the new company if no duplicate code is found
                _context.Companies.Add(Company);
                await _context.SaveChangesAsync();

                // If creation is successful, set a success message
                MessageHelper.SetSuccessMessage("Company created successfully!");
                return RedirectToPage("./Index"); // Redirect after successful creation
            }
            catch (Exception ex)
            {
                // Log the exception (optional) and set a failure message
                // Here, you can log the exception to a file, or use a logging service like Serilog or NLog
                MessageHelper.SetErrorMessage($"An error occurred while creating the company. Details: {ex.Message}");
                return Page(); // Return the page with an error message
            }
        }
    }
}