using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData
        (
            new IdentityRole{Id = "1", Name = "Admin", NormalizedName= "ADMIN"},
            new IdentityRole{Id = "2", Name = "Customer", NormalizedName= "CUSTOMER"}
        );
    }
}