using InventorySandbox.Models.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InventorySandbox.Configurations.Identity
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            // note : untuk ApplicationRole (di inherit dari IdentityRole)
            // bawaan default column NormalizedName sudah di index oleh Microsoft.AspNetCore.Identity
            builder.ToTable("ApplicationRole", schema: "Identity");

            // Each Role can have many entries in the UserRole join table
            builder.HasMany(e => e.Users)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            // Each Role can have many associated RoleClaims
            builder.HasMany(e => e.Claims)
                .WithOne(e => e.Role)
                .HasForeignKey(rc => rc.RoleId)
                .IsRequired();

            builder.HasMany(x => x.Claims).WithOne(y => y.Role).OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.Version).IsRowVersion();
            builder.Property(x => x.SVersion).HasComputedColumnSql("CONVERT(NVARCHAR(MAX), CONVERT(BINARY(8), Version), 1)");
        }
    }

    public class IdentityRoleClaimConfiguration : IEntityTypeConfiguration<ApplicationRoleClaim>
    {
        public void Configure(EntityTypeBuilder<ApplicationRoleClaim> builder)
        {
            builder.ToTable("ApplicationRoleClaim", schema: "Identity");
            builder.HasIndex(x => new { x.RoleId, x.ControllerMethodId }).IsUnique(true);
        }
    }

    public class IdentityUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        // note : untuk ApplicationUser (di inherit dari IdentityUser)
        // bawaan default column NormalizedUserName sudah di index oleh Microsoft.AspNetCore.Identity
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("ApplicationUser", schema: "Identity");
            builder.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

            builder.HasIndex(x => new { x.EmployeeId, x.UserName }).IsUnique(true);

            // Each User can have many UserLogins
            builder.HasMany(e => e.Logins)
                .WithOne(e => e.User)
                .HasForeignKey(ul => ul.UserId)
                .IsRequired();

            // Each User can have many UserTokens
            builder.HasMany(e => e.Tokens)
                .WithOne(e => e.User)
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany(e => e.Roles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("CAST(sysdatetimeoffset() AT TIME ZONE 'SE Asia Standard Time' AS DATETIME)");
            builder.Property(x => x.ModifiedDate).HasColumnType("datetime");
            builder.Property(x => x.Version).IsRowVersion();
            builder.Property(x => x.SVersion).HasComputedColumnSql("CONVERT(NVARCHAR(MAX), CONVERT(BINARY(8), Version), 1)");
        }
    }

    public class IdentityUserClaimConfiguration : IEntityTypeConfiguration<ApplicationUserClaim>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserClaim> builder)
        {
            builder.ToTable("ApplicationUserClaim", schema: "Identity");
            builder.HasIndex(x => new { x.UserId, x.ControllerMethodId }).IsUnique(true);
        }
    }

    public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            builder.ToTable("ApplicationUserRole", schema: "Identity");

            builder.Property(x => x.Version).IsRowVersion();
            builder.Property(x => x.SVersion).HasComputedColumnSql("CONVERT(NVARCHAR(MAX), CONVERT(BINARY(8), Version), 1)");
        }
    }

    public class IdentityUserLoginConfiguration : IEntityTypeConfiguration<ApplicationUserLogin>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserLogin> builder)
        {
            builder.ToTable("ApplicationUserLogin", schema: "Identity");
        }
    }

    public class IdentityUserTokenConfiguration : IEntityTypeConfiguration<ApplicationUserToken>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserToken> builder)
        {
            builder.ToTable("ApplicationUserToken", schema: "Identity");
        }
    }

    public class IdentityEndpointConfiguration : IEntityTypeConfiguration<ApplicationEndpoint>
    {
        public void Configure(EntityTypeBuilder<ApplicationEndpoint> builder)
        {
            builder.ToTable("ApplicationEndPoint", schema: "Identity");
            builder.HasIndex(x => new { x.Name }).IsUnique(true);
            builder.Property("Name").HasMaxLength(255);
        }
    }

    public class IdentityUserPlantConfiguration : IEntityTypeConfiguration<ApplicationUserCompany>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserCompany> builder)
        {
            builder.ToTable("ApplicationUserCompany", schema: "Identity");
            builder.HasIndex(x => new { x.UserId, x.CompanyId }).IsUnique(true);
        }
    }

    public class IdentityUserInfoConfiguration : IEntityTypeConfiguration<ApplicationUserInfo>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserInfo> builder)
        {
            builder.ToTable("ApplicationUserInfo", schema: "Identity");
            builder.HasIndex(x => new { x.UserId, x.IpAddress, x.DeviceName });
            builder.Property(x => x.LoginDate).HasColumnType("datetime").HasDefaultValueSql("getdate()");
        }
    }

    public class IdentityControllerConfiguration : IEntityTypeConfiguration<ApplicationController>
    {
        public void Configure(EntityTypeBuilder<ApplicationController> builder)
        {
            builder.ToTable("ApplicationController", schema: "Identity");
            builder.HasMany(x => x.Methods).WithOne(y => y.Controller).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.Name }).IsUnique(true);
        }
    }

    public class IdentityControllerMethodConfiguration : IEntityTypeConfiguration<ApplicationControllerMethod>
    {
        public void Configure(EntityTypeBuilder<ApplicationControllerMethod> builder)
        {
            builder.ToTable("ApplicationControllerMethod", schema: "Identity");
            builder.HasIndex(x => new { x.Name, x.ControllerId }).IsUnique(true);
        }
    }
}