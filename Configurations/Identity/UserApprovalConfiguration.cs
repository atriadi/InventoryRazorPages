using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using InventorySandbox.Models.Identity;

namespace InventorySandbox.Configurations.Identity
{
    public class UserApprovalConfiguration : IEntityTypeConfiguration<UserApproval>
    {
        public void Configure(EntityTypeBuilder<UserApproval> builder)
        {
            builder.ToTable("UserApproval", schema: "Identity");

            builder.HasMany(x => x.UserApprovalDetails).WithOne(y => y.UserApproval).OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.DocumentDate).HasColumnType("date");
            builder.Property(x => x.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("CAST(sysdatetimeoffset() AT TIME ZONE 'SE Asia Standard Time' AS DATETIME)");
            builder.Property(x => x.ModifiedDate).HasColumnType("datetime");
            builder.Property(x => x.Version).IsRowVersion();
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
        }
    }
}