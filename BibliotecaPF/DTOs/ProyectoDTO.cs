using BibliotecaPortafolio.Interfaces;

namespace BibliotecaPortafolio.DTOs
{
    public class ProyectoDTO : IKeyBase, IProyecto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ImgUrl { get; set; } = string.Empty;
        public List<LenguajeDTO_Get> Lenguajes { get; set; } = new();
        public List<TecnologiaDTO_Get> Tecnologias { get; set; } = new();
    }
}
