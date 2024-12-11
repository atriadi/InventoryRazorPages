using InventorySandbox.Models.HumanResource;
using InventorySandbox.Models.Identity;
using InventorySandbox.Models.Organization;
using InventorySandbox.Models.Pharmacy;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InventorySandbox.Models
{
    public sealed class PersistenceDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public PersistenceDbContext(DbContextOptions<PersistenceDbContext> options) : base(options)
        {

        }

        #region Schema HumanResource

        public DbSet<Employee> Employees { get; set; }

        #endregion

        #region Schema Identity

        public DbSet<ApplicationController> ApplicationControllers { get; set; }
        public DbSet<ApplicationControllerMethod> ApplicationControllerMethods { get; set; }
        public DbSet<ApplicationEndpoint> ApplicationEndpoints { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationRoleClaim> ApplicationRoleClaims { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationUserClaim> ApplicationUserClaims { get; set; }
        public DbSet<ApplicationUserCompany> ApplicationUserCompanies { get; set; }
        public DbSet<ApplicationUserInfo> ApplicationUserInfos { get; set; }
        public DbSet<ApplicationUserLogin> ApplicationUserLogins { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<ApplicationUserToken> ApplicationUserTokens { get; set; }

        #endregion

        #region Schema Organization

        public DbSet<Company> Companies { get; set; }

        #endregion

        #region Schema Pharmacy

        public DbSet<Medication> Medications { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // ---> sintaks ini sangat diperlukan bila menggunakan Identity, kalau dirumah update-database selalu error

            // define DeleteBehaviour
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            // apply configuration dengan membaca assembly 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistenceDbContext).Assembly);
        }

        
    }
}