using System.ComponentModel.DataAnnotations;

namespace BibliotecaPasswordManager.DTOs
{
    public class CoreDTO_Get
    {
        public int Id { get; set; }
        public string Data01 { get; set; } = string.Empty;
        public string Data02 { get; set; } = string.Empty;
        public string Data03 { get; set; } = string.Empty;
        public string Id_Usuario { get; set; } = string.Empty;
    }
}
