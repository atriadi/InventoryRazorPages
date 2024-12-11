using InventorySandbox.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySandbox.Helper
{
    public static class DocumentNumberHelper
    {
        // Method to generate a document number in the format INV/YYYYMMDD/001
        public static async Task<string> GenerateDocumentNumberAsync(PersistenceDbContext context)
        {
            // Get the current date in YYYYMMDD format
            string currentDate = DateTime.Now.ToString("yyyyMMdd");

            // Get the last transaction that was created today
            var lastTransaction = await context.Transactions
                .Where(t => t.DocumentNumber.StartsWith("INV/" + currentDate))
                .OrderByDescending(t => t.DocumentNumber)
                .FirstOrDefaultAsync();

            // If a transaction exists for today, extract the last number and increment it
            int newTransactionNumber = 1;
            if (lastTransaction != null)
            {
                // Extract the number part of the DocumentNumber (e.g., from "INV/YYYYMMDD/001" get "001")
                string lastNumberString = lastTransaction.DocumentNumber.Substring(12); // After "INV/YYYYMMDD/"

                // Try to parse the number, if it fails (e.g., if it's not a valid number), set the counter to 1
                if (int.TryParse(lastNumberString, out int lastNumber))
                {
                    newTransactionNumber = lastNumber + 1;
                }
            }

            // Format the new transaction number (pad with leading zeros if necessary)
            string formattedNumber = newTransactionNumber.ToString("D3");  // Ensures it's always 3 digits

            // Generate the DocumentNumber (e.g., INV/20231211/001)
            return $"INV/{currentDate}/{formattedNumber}";
        }
    }
}