using System;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
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

                entity.ToTable("Clases");
            });

            modelBuilder.Entity<ClaseProfesor>()
            .HasKey(c => new { c.IdClase, c.Ci });

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

