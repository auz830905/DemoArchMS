using Microsoft.EntityFrameworkCore;

namespace MSProfesores.Models
{
	public partial class DBContextProfesores : DbContext
	{
		public DBContextProfesores(){}

        public DBContextProfesores(DbContextOptions<DBContextProfesores> options)
            : base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}

        public virtual DbSet<Profesor> Profesores { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profesor>(entity =>
            {
                entity.HasKey(e => e.CI)
                    .HasName("PK_CI");

                entity.Property(e => e.CI).HasMaxLength(11);
                entity.Property(e => e.Nombre).HasMaxLength(100);
                entity.Property(e => e.Apellidos).HasMaxLength(250);

                entity.ToTable("Profesores");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}