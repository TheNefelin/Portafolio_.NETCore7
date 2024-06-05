using BibliotecaPortafolio.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaPortafolio.DTOs
{
    public class ProTecDTO : IProTec
    {
        [Key]
        [Required]
        public int Id_Proyecto { get; set; }

        [Key]
        [Required]
        public int Id_Tecnologia { get; set; }
    }
}
