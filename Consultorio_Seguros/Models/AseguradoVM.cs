namespace Consultorio_Seguros.Models
{
    public class AseguradoVM
    {
        public int Id { get; set; }
        public string CedulaCliente { get; set; }
        public string NombreCliente {  get; set; }
        public string CodigoSeguro {  get; set; }
        public string NombreSeguro { get; set; }
        public string Asegurada {  get; set; }
        public decimal Prima { get; set; }
    }
}
