using System.ComponentModel.DataAnnotations.Schema;

namespace MSClases.Models
{
	public class Clase
	{
		public Clase()
		{
			ClaseProfesor = new HashSet<ClaseProfesor>();
		}
		public int Id { get; set; }
		public string Nombre { get; set; } = null!;
		[NotMapped]
		public virtual ICollection<ClaseProfesor> ClaseProfesor { get; set; } = new HashSet<ClaseProfesor>();
	}
}