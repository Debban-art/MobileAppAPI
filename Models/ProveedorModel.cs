using System;
namespace reportesApi.Models
{
    public class InsertProveedorModel 
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string RFC { get; set; }
        public int PlazoPago { get; set; }
        public float PorcentajeRetencion { get; set; }
        public int UsuarioRegistra { get; set; } 
    }

    public class GetProveedorModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string RFC { get; set; }
        public int PlazoPago { get; set; }
        public float PorcentajeRetencion { get; set; }
        public int Estatus { get; set; }
        public string UsuarioRegistra { get; set; } 
        public string FechaRegistro { get; set; }
    }

    public class UpdateProveedorModel 
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string RFC { get; set; }
        public int PlazoPago { get; set; }
        public float PorcentajeRetencion { get; set; }
        public int Estatus { get; set; }
        public int UsuarioRegistra { get; set; } 
    }
}