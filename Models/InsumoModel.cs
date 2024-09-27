using System;
namespace reportesApi.Models
{
    public class InsertInsumoModel
    {
        public string Insumo {get; set;}
        public string Insumo_Descripcion {get; set;}
        public float Insumo_Costo {get; set;}
        public int Insumo_UnidadMedida {get; set;}
        public string Insumo_InsumoUp {get; set;}
        public int Usuario_Registra {get; set;}
    }

    public class GetInsumosModel
    {
        public int Insumo_Id { get; set; }
        public string Insumo {get; set;}
        public string Insumo_Descripcion {get; set;}
        public float Insumo_Costo {get; set;}
        public int Insumo_UnidadMedida {get; set;}
        public string Insumo_InsumoUp {get; set;}
        public int Insumo_Estatus {get; set;}
        public string Usuario_Registra {get; set;}
        public string Fecha_Registro {get; set;} 
    }

    public class UpdateInsumoModel
    {
        public int Insumo_Id { get; set; }
        public string Insumo {get; set;}
        public string Insumo_Descripcion {get; set;}
        public float Insumo_Costo {get; set;}
        public int Insumo_UnidadMedida {get; set;}
        public string Insumo_InsumoUp {get; set;}
        public int Insumo_Estatus {get; set;}
        public int Usuario_Registra {get; set;}
    }
}