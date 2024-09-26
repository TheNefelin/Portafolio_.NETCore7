using System.ComponentModel.DataAnnotations;

namespace ClassLibraryApplication.DTOs
{
    public class ProjectTechnologyDTO
    {
        [Required]
        public int Id_Project { get; set; }
        [Required]
        public int Id_Technology { get; set; }
    }
}
