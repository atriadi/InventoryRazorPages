using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InventorySandbox.Models.Pharmacy;
using InventorySandbox.Helper;
using Microsoft.AspNetCore.Mvc;

namespace InventorySandbox.Pages.Pharmacy.SupplierPages
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

        public PaginatedListHelper<Supplier> Supplier { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int pageNumber = 1)
        {
            var suppliers = _context.Suppliers.AsQueryable();

            // Filter based on search query if provided
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                suppliers = suppliers.Where(c => c.Name.Contains(SearchQuery) || c.Code.Contains(SearchQuery));
            }

            var pageSize = 10;
            TotalPages = (int)Math.Ceiling(await suppliers.CountAsync() / (double)pageSize);
            CurrentPage = pageNumber;

            Supplier = await PaginatedListHelper<Supplier>.CreateAsync(suppliers, pageNumber, pageSize);
        }
    }
}