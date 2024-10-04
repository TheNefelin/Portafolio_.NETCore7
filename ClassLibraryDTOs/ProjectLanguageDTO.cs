using System.ComponentModel.DataAnnotations;

namespace ClassLibraryDTOs
{
    public class ProjectLanguageDTO
    {
        [Required]
        public int Id_Project { get; set; }
        [Required]
        public int Id_Language { get; set; }
    }
}
