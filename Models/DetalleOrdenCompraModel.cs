using System;
namespace reportesApi.Models
{
    public class InsertDetalleOrdenCompraModel
    {
        public int IdOrdenCompra {get; set;}
        public string Insumo {get; set;}
        public float Cantidad {get; set;}
        public float CantidadRecibida {get; set;}
        public float Costo {get; set;}
        public int UsuarioRegistra {get; set;}
    }

    public class GetDetalleOrdenCompraModel
    {
        public int Id {get; set;}
        public int IdOrdenCompra {get; set;}
        public string Insumo {get; set;}
        public float Cantidad {get; set;}
        public float CantidadRecibida {get; set;}
        public float Costo {get; set;}
        public float CostoRenglon {get; set;}
        public int Estatus {get; set;}
        public string UsuarioRegistra {get; set;}
        public string FechaRegistro {get; set;}
    }

    public class UpdateDetalleOrdenCompraModel
    {
        public int Id {get; set;}
        public int IdOrdenCompra {get; set;}
        public string Insumo {get; set;}
        public float Cantidad {get; set;}
        public float CantidadRecibida {get; set;}
        public float Costo {get; set;}
        public int Estatus {get; set;}
        public int UsuarioRegistra {get; set;}
    }
}