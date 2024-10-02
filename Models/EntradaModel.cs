using System;
namespace reportesApi.Models
{
    public class InsertEntradaModel
    {
        public int IdProveedor {get;set;}
        public int IdSucursal {get;set;}
        public string Factura {get;set;}
        public int UsuarioRegistra {get;set;}
    }

    public class GetEntradaModel
    {
        public int Id {get;set;}
        public int IdProveedor {get;set;}
        public int IdSucursal {get;set;}
        public string Factura {get;set;}
        public float Total {get;set;}
        public string FechaEntrada {get;set;}
        public int Estatus {get;set;}
        public string UsuarioRegistra {get;set;}
        public string FechaRegistro {get;set;}
    }

    public class UpdateEntradaModel
    {
        public int Id {get;set;}
        public int IdProveedor {get;set;}
        public int IdSucursal {get;set;}
        public string Factura {get;set;}
        public int Estatus {get;set;}
        public int UsuarioRegistra {get;set;}
    }
}