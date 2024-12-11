using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InventorySandbox.Models.Pharmacy;
using InventorySandbox.Helper;
using Microsoft.AspNetCore.Mvc;

namespace InventorySandbox.Pages.Pharmacy.TransactionPages
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

        public PaginatedListHelper<Transaction> Transaction { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int pageNumber = 1)
        {
            var transactions = _context.Transactions.AsQueryable();

            // Filter based on search query if provided
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                transactions = transactions.AsNoTracking()
                    .Include(i => i.TransactionDetails)
                    .Where(c => c.DocumentDate.ToString("dd-MMMM-yyyy").Contains(SearchQuery) ||
                                c.DocumentNumber.Contains(SearchQuery));
            }

            var pageSize = 10;
            TotalPages = (int)Math.Ceiling(await transactions.CountAsync() / (double)pageSize);
            CurrentPage = pageNumber;

            Transaction = await PaginatedListHelper<Transaction>.CreateAsync(transactions, pageNumber, pageSize);
        }
    }
}
