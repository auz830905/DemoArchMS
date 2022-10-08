namespace Shared.DTOs
{
    public class Profesor
    {
        public string CI { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public List<Clase> Clases { get; set; }
    }
}
