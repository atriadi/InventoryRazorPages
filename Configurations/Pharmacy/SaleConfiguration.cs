using InventorySandbox.Models.Pharmacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySandbox.Configurations.Pharmacy
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sale", schema: "Pharmacy");

            builder.HasIndex(x => x.MedicationId).IsUnique(true);
            builder.HasIndex(x => new { x.MedicationId, x.DocumentDate}).IsUnique(true);

            builder.Property(x => x.DocumentDate).HasColumnType("date");
            builder.Property(x => x.Price).HasColumnType("decimal").HasPrecision(18, 2);
            builder.Property(x => x.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("CAST(sysdatetimeoffset() AT TIME ZONE 'SE Asia Standard Time' AS DATETIME)");
            builder.Property(x => x.ModifiedDate).HasColumnType("datetime");
            builder.Property(x => x.Version).IsRowVersion();
        }
    }
}