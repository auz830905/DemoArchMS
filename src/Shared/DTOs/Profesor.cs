namespace Shared.DTOs
{
    public class Profesor
    {
        public string CI { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public List<Clase> Clases { get; set; } = null!;
    }
}
