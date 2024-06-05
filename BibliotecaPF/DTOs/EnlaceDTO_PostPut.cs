using BibliotecaPortafolio.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaPortafolio.DTOs
{
    public class EnlaceDTO_PostPut : IEnlace
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string EnlaceUrl { get; set; } = string.Empty;

        [Required]
        public bool Estado { get; set; }

        [Required]
        public int Id_EnlaceGrp { get; set; }
    }
}
