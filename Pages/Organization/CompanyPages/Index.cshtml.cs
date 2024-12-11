using InventorySandbox.Helper;
using InventorySandbox.Models.Organization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InventorySandbox.Pages.Organization.CompanyPages
{
    public class IndexModel : PageModel
    {
        private readonly InventorySandbox.Models.PersistenceDbContext _context;

        public IndexModel(InventorySandbox.Models.PersistenceDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        public PaginatedListHelper<Company> Company { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int pageNumber = 1)
        {
            var companies = _context.Companies.AsQueryable();

            // Filter based on search query if provided
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                companies = companies.Where(c => c.Name.Contains(SearchQuery) || c.Code.Contains(SearchQuery));
            }

            var pageSize = 10;
            TotalPages = (int)Math.Ceiling(await companies.CountAsync() / (double)pageSize);
            CurrentPage = pageNumber;

            Company = await PaginatedListHelper<Company>.CreateAsync(companies, pageNumber, pageSize);
        }
    }
}
