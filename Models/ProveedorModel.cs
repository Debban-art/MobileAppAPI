using System;
namespace reportesApi.Models
{
    public class InsertProveedorModel
    {
        public string Proveedor_Nombre {get; set;}
        public string Proveedor_Direccion {get; set;}
        public string Proveedor_Email {get; set;}
        public string Proveedor_RFC {get; set;}
        public int Proveedor_PlazoPago {get; set;}
        public float Proveedor_PorcentajeRetencion {get; set;}
        public int Usuario_Registra {get; set;}
    }

    public class GetProveedoresModel
    {
        public int Proveedor_Id { get; set; }
        public string Proveedor_Nombre {get; set;}
        public string Proveedor_Direccion {get; set;}
        public string Proveedor_Email {get; set;}
        public string Proveedor_RFC {get; set;}
        public int Proveedor_PlazoPago {get; set;}
        public float Proveedor_PorcentajeRetencion {get; set;}
        public int Proveedor_Estatus {get; set;}
        public string Usuario_Registra {get; set;}
        public string Fecha_Registro {get; set;} 
    }
}