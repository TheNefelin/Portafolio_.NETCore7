namespace ClassLibraryApplication.DTOs
{
    public class ProjectsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public List<LanguageDTO> Languages { get; set; } = new List<LanguageDTO>();
        public List<TechnologyDTO> Technologies { get; set; } = new List<TechnologyDTO>();
    }
}
