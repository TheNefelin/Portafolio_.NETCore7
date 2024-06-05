using BibliotecaPortafolio.Interfaces;

namespace BibliotecaPortafolio.DTOs
{
    public class EnlaceGrpDTO_Get : IKeyBase, IEnlaceGrp
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}
