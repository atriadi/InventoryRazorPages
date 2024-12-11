using Microsoft.EntityFrameworkCore;

namespace InventorySandbox.Models
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
#if DEBUG
            services.AddDbContext<PersistenceDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("LOCAL"), opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)));
#else
           // SET ConnectionString From App Service
            services.AddDbContext<PersistenceDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("Connection"), opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)));
#endif


            return services;
        }

        public static async Task ApplyMigrations(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            await using PersistenceDbContext dbContext = scope.ServiceProvider.GetRequiredService<PersistenceDbContext>();

            await dbContext.Database.MigrateAsync();


            #region Run Create or Alter Views

            //await dbContext.RunQueryAsync(View.ViewCollection.ViewBookActivity());

            #endregion

            #region Run Create or Alter Store Procedure

            //await dbContext.RunQueryAsync(Persistence.StoreProcedure.StoreProcedureCollection.GetNextId());

            #endregion
        }
    }
}
