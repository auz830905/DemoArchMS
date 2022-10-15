using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public class Profesor
    {
        [Required(ErrorMessage = "El Carné de identidad es obligatorio")]
        [StringLength(maximumLength: 11, ErrorMessage = "Carné de identidad no válido. Tiene que tener 11 caracteres numéricos", MinimumLength = 11)]
        [RegularExpression(@"\d*", ErrorMessage = "Solo admite números el Carné de identidad")]
        public string CI { get; set; } = null!;
        [Required(ErrorMessage = "El nombre del profesor es obligatorio")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "Los Apellidos del profesor son obligatorios")]
        public string Apellidos { get; set; } = null!;
        public List<Clase> Clases { get; set; } = null!;
    }
}