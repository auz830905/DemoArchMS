namespace Shared.DTOs
{
    public class Clase
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public virtual Profesor Profesor { get; set; } = null!;
    }
}
