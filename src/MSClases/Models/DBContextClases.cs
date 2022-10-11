using Microsoft.EntityFrameworkCore;

namespace MSClases.Models
{
	public partial class DBContextClases : DbContext
    {
		public DBContextClases()
		{
		}

        public DBContextClases(DbContextOptions<DBContextClases> options)
            : base(options)
        {
        }

        public virtual DbSet<Clase> Clases { get; set; } = null!;
        public virtual DbSet<ClaseProfesor> ClaseProfesors { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clase>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_Clase");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.ToTable("Clases");
            });

            modelBuilder.Entity<ClaseProfesor>(entity =>
            {            
                entity.HasKey(c => new { c.IdClase, c.Ci });
                entity.Property(e => e.Ci).HasMaxLength(11);

                entity.ToTable("ClaseProfesores");
                
                entity.HasOne(d => d.IdClaseNavigation)
                   .WithMany(p => p.ClaseProfesor)
                   .HasForeignKey(d => d.IdClase)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_CLASE");
            });

            modelBuilder.Entity<ClaseProfesor>(entity =>
            {
                entity.ToTable("ClaseProfesores");

                /*entity.HasMany(d => d.IdClaseNavigation)
                    .WithMany(p => p.Id)
                    .HasForeignKey(d => d.IdPlayer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PLAYERTRANSACTION_PLAYER");*/

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}

