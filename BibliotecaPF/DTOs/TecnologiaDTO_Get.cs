using BibliotecaPortafolio.Interfaces;

namespace BibliotecaPortafolio.DTOs
{
    public class TecnologiaDTO_Get : IKeyBase, IProyecto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ImgUrl { get; set; } = string.Empty;
    }
}
