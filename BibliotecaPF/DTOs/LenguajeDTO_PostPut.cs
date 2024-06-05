using BibliotecaPortafolio.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaPortafolio.DTOs
{
    public class LenguajeDTO_PostPut : IProyecto
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string ImgUrl { get; set; } = string.Empty;
    }
}
