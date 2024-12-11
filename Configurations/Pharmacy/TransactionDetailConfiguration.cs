using InventorySandbox.Models.Pharmacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySandbox.Configurations.Pharmacy
{
    public class TransactionDetailConfiguration : IEntityTypeConfiguration<TransactionDetail>
    {
        public void Configure(EntityTypeBuilder<TransactionDetail> builder)
        {
            builder.ToTable("TransactionDetail", schema: "Pharmacy");

            builder.HasIndex(x => new { x.MedicationId }).IsUnique(true);

            builder.Property(x => x.Price).HasColumnType("decimal").HasPrecision(18, 2);
        }
    }
}