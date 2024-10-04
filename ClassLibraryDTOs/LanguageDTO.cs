using System.ComponentModel.DataAnnotations;

namespace ClassLibraryDTOs
{
    public class LanguageDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string ImgUrl { get; set; }
    }
}
