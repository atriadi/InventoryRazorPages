using InventorySandbox.Models.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InventorySandbox.Configurations.Identity
{
    public class UserApprovalDetailConfiguration : IEntityTypeConfiguration<UserApprovalDetail>
    {
        public void Configure(EntityTypeBuilder<UserApprovalDetail> builder)
        {
            builder.ToTable("UserApprovalDetail", schema: "Identity");

            builder.Property(x => x.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("CAST(sysdatetimeoffset() AT TIME ZONE 'SE Asia Standard Time' AS DATETIME)");
            builder.Property(x => x.ModifiedDate).HasColumnType("datetime");
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.Version).IsRowVersion();
        }
    }
}