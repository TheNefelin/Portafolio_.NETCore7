namespace BibliotecaPortafolio.Interfaces
{
    internal interface IEnlace
    {
        public string Nombre { get; set; }
        public string EnlaceUrl { get; set; }
        public bool Estado { get; set; }
        public int Id_EnlaceGrp { get; set; }
    }
}
