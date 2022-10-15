using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MSUsers.Models.InitDb
{
    public class RolesSeedConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
               new IdentityRole
               {
                   Id = "7a262f9a-c6b3-467d-8a0e-bb707aa4a550",
                   Name = "admin",
                   NormalizedName = "Administrador del Sistema"                  
               },
               new IdentityRole
               {
                   Id = "2a661a16-041c-440c-8321-9eb7f679b9ed",
                   Name = "adminclases",
                   NormalizedName = "Administrador de Clases"
               },
               new IdentityRole
               {
                   Id = "1975b5c3-297e-41a3-9027-863dfbedc670",
                   Name = "adminprofesores",
                   NormalizedName = "Administrador de Profesores"
               },
               new IdentityRole
               {
                   Id = "b2bdc85e-2ef8-48d4-bfb0-181a215f1f9e",
                   Name = "getprofesores",
                   NormalizedName = "Recuperar Profesores"
               }
           ); 
        }
    }
}