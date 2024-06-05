using BibliotecaPortafolio.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaPortafolio.DTOs
{
    public class ProLengDTO : IProLeng
    {
        [Key]
        [Required]
        public int Id_Proyecto { get; set; }

        [Key]
        [Required]
        public int Id_Lenguaje { get; set; }
    }
}
