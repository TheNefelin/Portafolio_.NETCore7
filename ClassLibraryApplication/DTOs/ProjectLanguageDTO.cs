using System.ComponentModel.DataAnnotations;

namespace ClassLibraryApplication.DTOs
{
    public class ProjectLanguageDTO
    {
        [Required]
        public int Id_Project { get; set; }
        [Required]
        public int Id_Language { get; set; }
    }
}
