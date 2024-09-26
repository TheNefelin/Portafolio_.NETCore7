using System.ComponentModel.DataAnnotations;

namespace ClassLibraryApplication.DTOs
{
    public class UrlGrpDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public bool Status { get; set; }
    }
}
