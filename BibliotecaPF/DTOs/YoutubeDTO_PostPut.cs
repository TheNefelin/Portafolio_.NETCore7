using BibliotecaPortafolio.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaPortafolio.DTOs
{
    public class YoutubeDTO_PostPut : IYoutube
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Embed { get; set; } = string.Empty;
    }
}
