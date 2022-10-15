using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MSUsers.Models.InitDb
{
    public class UserRolesSeedConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
               new IdentityUserRole<string>
               {
                   RoleId = "7a262f9a-c6b3-467d-8a0e-bb707aa4a550", //User root@root.com
                   UserId = "c4302009-0d0d-43e8-b346-481292c36d9f"
               },
               new IdentityUserRole<string>
               {
                   RoleId = "2a661a16-041c-440c-8321-9eb7f679b9ed", //User arley@gmail.com
                   UserId = "69784241-9efa-4bd2-b9d2-0c0bd113fe58"
               },
               new IdentityUserRole<string>
               {
                   RoleId = "1975b5c3-297e-41a3-9027-863dfbedc670", //User adael@gmail.com
                   UserId = "7123840b-fa5a-47b1-a173-37c5c9c8f01e"
               }
           );
        }
    }
}