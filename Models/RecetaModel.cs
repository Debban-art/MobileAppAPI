using System;
namespace reportesApi.Models
{
    public class InsertRecetaModel
    {
        public string Nombre {get;set;}
        public int UsuarioRegistra {get;set;}
    }

    public class GetRecetaModel
    {
        public int Id {get;set;}
        public string Nombre {get;set;}
        public int Estatus {get;set;}
        public string FechaCreacion {get;set;}
        public string UsuarioRegistra {get;set;}
        public string FechaRegistro {get;set;}
    }

    public class GetReporteRecetasModel
    {
        public int Id {get; set;}
        public string Nombre {get; set;}
        public string Insumo {get; set;}
        public float Cantidad {get; set;}
        public string Fecha_Creacion {get; set;}
        public int Estatus {get; set;}
        public string Usuario_Registra {get; set;}
        public string Fecha_Registro {get; set;}
    }

    public class UpdateRecetaModel
    {
        public int Id {get;set;}
        public string Nombre {get;set;}
        public int Estatus {get;set;}
        public int UsuarioRegistra {get;set;}
    }
}