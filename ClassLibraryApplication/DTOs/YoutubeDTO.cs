using System.ComponentModel.DataAnnotations;

namespace ClassLibraryApplication.DTOs
{
    public class YoutubeDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(256)]
        public string EmbedUrl { get; set; }
    }
}
