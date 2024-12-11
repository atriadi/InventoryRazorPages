using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InventorySandbox.Models.Pharmacy;
using InventorySandbox.Helper;
using Microsoft.AspNetCore.Mvc;

namespace InventorySandbox.Pages.Pharmacy.SalePages
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

        public PaginatedListHelper<Sale> Sale { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int pageNumber = 1)
        {
            var Sales = _context.Sales.AsNoTracking().Include(x => x.Medication).AsQueryable();

            // Filter based on search query if provided
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                Sales = Sales.AsNoTracking()
                    .Include(x => x.Medication)
                    .Where(c => c.DocumentDate.ToString("dd-MMMM-yyyy").Contains(SearchQuery) ||
                                c.Medication.Name.Contains(SearchQuery));
            }

            var pageSize = 10;
            TotalPages = (int)Math.Ceiling(await Sales.CountAsync() / (double)pageSize);
            CurrentPage = pageNumber;

            Sale = await PaginatedListHelper<Sale>.CreateAsync(Sales, pageNumber, pageSize);
        }
    }
}
