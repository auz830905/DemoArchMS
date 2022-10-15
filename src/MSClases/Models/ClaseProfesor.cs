namespace MSClases.Models
{
	public class ClaseProfesor
	{
		public string Ci { get; set; } = null!;
		public int IdClase { get; set; }

		[System.Text.Json.Serialization.JsonIgnore]
		public virtual Clase IdClaseNavigation { get; set; } = null!;
	}
}