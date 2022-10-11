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
		public virtual ICollection<ClaseProfesor> ClaseProfesor { get; set; }
	}
}

