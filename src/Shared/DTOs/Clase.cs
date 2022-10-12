using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public class Clase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de la clase es obligatorio")]
        public string Nombre { get; set; } = null!;
        public virtual Profesor Profesor { get; set; } = null!;
    }
}
