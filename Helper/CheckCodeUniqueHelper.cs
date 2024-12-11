using InventorySandbox.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySandbox.Helper
{
    public static class CheckCodeUniqueHelper
    {
        // Generic method to check for duplicate based on the entity type and the property value
        public static async Task<bool> IsDuplicateAsync<TEntity>(
            PersistenceDbContext context,
            string propertyName,
            string propertyValue) where TEntity : class
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            // Get the DbSet for the given entity type
            var dbSet = context.Set<TEntity>();

            // Dynamically build the lambda expression to check for duplicate
            var parameter = System.Linq.Expressions.Expression.Parameter(typeof(TEntity), "e");
            var propertyExpression = System.Linq.Expressions.Expression.Property(parameter, propertyName);
            var valueExpression = System.Linq.Expressions.Expression.Constant(propertyValue);

            // Build the condition e => e.Property == value
            var condition = System.Linq.Expressions.Expression.Lambda<System.Func<TEntity, bool>>(
                System.Linq.Expressions.Expression.Equal(propertyExpression, valueExpression),
                parameter);

            // Check if any record matches the condition
            return await dbSet.AnyAsync(condition);
        }
    }

}
