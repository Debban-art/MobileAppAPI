using System;
namespace reportesApi.Models
{
    public class InsertDetalleEntradaModel
    {
       public int IdEntrada  {get; set;}
       public string  Insumo {get; set;}
       public float Cantidad  {get; set;}
       public float SinCargo  {get; set;}
       public float Costo  {get; set;}
       public int UsuarioRegistra  {get; set;}
    }

    public class GetDetalleEntradaModel
    {
       public int Id {get; set;}
       public int IdEntrada  {get; set;}
       public string  Insumo {get; set;}
       public float Cantidad  {get; set;}
       public float SinCargo  {get; set;}
       public float Costo  {get; set;}
       public int Estatus {get;set;}
       public string UsuarioRegistra  {get; set;}
       public string FechaRegistro {get;set;}
    }

    public class UpdateDetalleEntradaModel
    {
       public int Id {get; set;}
       public int IdEntrada  {get; set;}
       public string  Insumo {get; set;}
       public float Cantidad  {get; set;}
       public float SinCargo  {get; set;}
       public float Costo  {get; set;}
       public int Estatus {get;set;}
       public int UsuarioRegistra  {get; set;}
    }
}