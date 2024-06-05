using BibliotecaPortafolio.Interfaces;

namespace BibliotecaPortafolio.DTOs
{
    public class YoutubeDTO_Get : IKeyBase, IYoutube
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Embed { get; set; } = string.Empty;
    }
}
