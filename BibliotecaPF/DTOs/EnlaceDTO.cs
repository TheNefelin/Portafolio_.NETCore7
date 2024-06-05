using BibliotecaPortafolio.Interfaces;

namespace BibliotecaPortafolio.DTOs
{
    public class EnlaceDTO : IEnlaceGrp
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public List<EnlaceDTO_Get> Enlaces { get; set; } = new();
    }
}
