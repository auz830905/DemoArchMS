using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MSUsers.Models.InitDb
{
    public class UserSeedConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
           builder.HasData( //Password Qwerty#123
               new IdentityUser
               {
                   Id = "c4302009-0d0d-43e8-b346-481292c36d9f",
                   Email = "root@root.com",
                   NormalizedEmail = "ROOT@ROOT.COM",
                   UserName = "root@root.com",
                   NormalizedUserName = "ROOT@ROOT.COM",
                   SecurityStamp = "FR4PSJYKQXHU3TXU6DOPPY2JADGCBJ25",
                   ConcurrencyStamp = "df167685-805d-4e24-b130-29eb533935af",
                   LockoutEnabled = true,
                   PasswordHash = "AQAAAAEAACcQAAAAECAG831SfGWWcdZXeZHbsQJaQSBaGCMHyS88pK5FwlBgF+EKGKyfb80QGBQqAvkEtA==",                   
               },
               new IdentityUser
               {
                   Id = "69784241-9efa-4bd2-b9d2-0c0bd113fe58",
                   Email = "arley@gmail.com",
                   NormalizedEmail = "ARLEY@GMAIL.COM",
                   UserName = "arley@gmail.com",
                   NormalizedUserName = "ARLEY@GMAIL.COM",
                   SecurityStamp = "PXCBFNWAFCD7QNWQEERTRYWFRWQJQTOO",
                   ConcurrencyStamp = "faa36d56-8b9e-4951-a1db-c15657b32c37",
                   LockoutEnabled = true,
                   PasswordHash = "AQAAAAEAACcQAAAAEG/EQ7Wl31cyyrPLnAUgOLUO+NooKeWb+PKVo9QaxlZqTQGUAGgh/YW+rrrxRVIeLg==",
               },
               new IdentityUser
               {
                   Id = "7123840b-fa5a-47b1-a173-37c5c9c8f01e",
                   Email = "adael@gmail.com",
                   NormalizedUserName = "ADAEL@GMAIL.COM",
                   UserName = "adael@gmail.com",
                   NormalizedEmail = "ADAEL@GMAIL.COM",
                   SecurityStamp = "YYXL2MOUPBTQ7QUTG7KLHFUMGYL76T66",
                   ConcurrencyStamp = "7ecb50b0-7177-4888-a7ab-d62d6742cfe4",
                   LockoutEnabled = true,
                   PasswordHash = "AQAAAAEAACcQAAAAEMQ5hPwbzt0TKgBefudF1qed6no3tPj6WnAjojVQN3c2h2wmcTGfyG/fK/ocVIoxaA==",
               }
           );
        }
    }
}