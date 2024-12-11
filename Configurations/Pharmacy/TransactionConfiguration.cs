using InventorySandbox.Models.Pharmacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySandbox.Configurations.Pharmacy
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction", schema: "Pharmacy");

            builder.HasIndex(x => new { x.DocumentNumber }).IsUnique(true);
            builder.HasIndex(x => new { x.DocumentDate, x.DocumentNumber });
            builder.HasMany(x => x.TransactionDetails).WithOne(y => y.Transaction).OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.DocumentDate).HasColumnType("date");
            builder.Property(x => x.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("CAST(sysdatetimeoffset() AT TIME ZONE 'SE Asia Standard Time' AS DATETIME)");
            builder.Property(x => x.ModifiedDate).HasColumnType("datetime");
            builder.Property(x => x.Version).IsRowVersion();
        }
    }
}