using BibliotecaPortafolio.Interfaces;

namespace BibliotecaPortafolio.DTOs
{
    public class EnlaceDTO_Get : IKeyBase, IEnlace
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string EnlaceUrl { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public int Id_EnlaceGrp { get; set; }
    }
}
