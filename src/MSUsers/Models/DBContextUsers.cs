using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MSUsers.Models.InitDb;

namespace MSUsers.Models
{
    public class DBContextUsers : IdentityDbContext
    {        
        public DBContextUsers(DbContextOptions<DBContextUsers> options) : base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserSeedConfiguration());
            modelBuilder.ApplyConfiguration(new RolesSeedConfiguration());
            modelBuilder.ApplyConfiguration(new UserRolesSeedConfiguration());               
            modelBuilder.ApplyConfiguration(new ClaimsSeedConfiguration());
        }        
    }
}