using System.ComponentModel.DataAnnotations;

namespace BibliotecaAuth.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        //[SwaggerSchema(Description = "Contraseña", Format = "password")]
        public string Clave1 { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Clave2 { get; set; } = string.Empty;
    }
}
