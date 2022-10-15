using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MSUsers.Models.InitDb
{
    public class ClaimsSeedConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
    {
        public void Configure(EntityTypeBuilder<Microsoft.AspNetCore.Identity.IdentityUserClaim<string>> builder)
        {            
            builder.HasData(
                new IdentityUserClaim<string>
                {
                    Id = 1,
                    ClaimValue = "admin",
                    UserId = "c4302009-0d0d-43e8-b346-481292c36d9f",
                    ClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                },
                new IdentityUserClaim<string>
                {
                    Id = 2,
                    ClaimValue = "adminclases",
                    UserId = "69784241-9efa-4bd2-b9d2-0c0bd113fe58",
                    ClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                },
                new IdentityUserClaim<string>
                {
                    Id = 3,
                    ClaimValue = "adminprofesores",
                    UserId = "7123840b-fa5a-47b1-a173-37c5c9c8f01e",
                    ClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                },
                new IdentityUserClaim<string>
                {
                    Id = 4,
                    ClaimValue = "adminclases",
                    UserId = "c4302009-0d0d-43e8-b346-481292c36d9f",
                    ClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                },
                new IdentityUserClaim<string>
                {
                    Id = 5,
                    ClaimValue = "adminprofesores",
                    UserId = "c4302009-0d0d-43e8-b346-481292c36d9f",
                    ClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                },
                new IdentityUserClaim<string>
                {
                    Id = 6,
                    ClaimValue = "getprofesores",
                    UserId = "69784241-9efa-4bd2-b9d2-0c0bd113fe58",
                    ClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                }
            );            
        }
    }
}