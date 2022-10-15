using System.ComponentModel.DataAnnotations;

namespace Auth.DTOs
{
    public class UserCredentials
    {
        [Required(ErrorMessage = "El correo es requerido")]
        [StringLength(maximumLength: 256)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Correo no válido")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(maximumLength: 250, ErrorMessage = "La contraseña debe tener mínimo 8 caracteres ", MinimumLength = 8)]
        public string Password { get; set; } = null!;
    }
}