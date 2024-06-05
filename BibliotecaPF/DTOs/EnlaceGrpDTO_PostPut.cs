using BibliotecaPortafolio.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaPortafolio.DTOs
{
    public class EnlaceGrpDTO_PostPut : IEnlaceGrp
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public bool Estado { get; set; }
    }
}
