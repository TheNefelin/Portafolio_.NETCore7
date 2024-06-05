using System.ComponentModel.DataAnnotations;

namespace BibliotecaPasswordManager.DTOs
{
    public class CoreDTO_PostPut
    {
        [Required]
        [MaxLength(256)]
        public string Data01 { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Data02 { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Data03 { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Id_Usuario { get; set; } = string.Empty;
    }
}
