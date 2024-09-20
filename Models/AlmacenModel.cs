using System;
namespace reportesApi.Models
{
    public class InsertAlmacenModel 
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int UsuarioRegistra { get; set; } 
    }

    public class GetAlmacenModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int Estatus { get; set; }
        public string UsuarioRegistra { get; set; } 
        public string FechaRegistro { get; set; }
    }

    public class UpdateAlmacenModel 
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int Estatus { get; set; }
        public int UsuarioRegistra { get; set; } 
    }
}