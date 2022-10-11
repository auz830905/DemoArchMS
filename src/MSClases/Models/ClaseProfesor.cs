namespace MSClases.Models
{
	public class ClaseProfesor
	{
		public string Ci { get; set; } = null!;
		public int IdClase { get; set; }
		public virtual Clase IdClaseNavigation { get; set; }
    }
}

