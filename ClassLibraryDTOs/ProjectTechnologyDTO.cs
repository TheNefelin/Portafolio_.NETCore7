using System.ComponentModel.DataAnnotations;

namespace ClassLibraryDTOs
{
    public class ProjectTechnologyDTO
    {
        [Required]
        public int Id_Project { get; set; }
        [Required]
        public int Id_Technology { get; set; }
    }
}
