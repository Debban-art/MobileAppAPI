using System;
namespace reportesApi.Models
{
     public class InsertInsumoModel 
    {
        public string Insumo { get; set; }
        public string DescripcionInsumo { get; set; }
        public float Costo { get; set; }
        public int UnidadMedida { get; set; }
        public string InsumoUp { get; set; }
        public int UsuarioRegistra { get; set; } 
    }

      public class GetInsumoModel 
    {
        public int Id { get; set; }
        public string Insumo { get; set; }
        public string DescripcionInsumo { get; set; }
        public float Costo { get; set; }
        public int UnidadMedida { get; set; }
        public string InsumoUp { get; set; }
        public int Estatus { get; set; }
        public string UsuarioRegistra { get; set; } 
        public string FechaRegistro { get; set; }
    }

     public class UpdateInsumoModel 
    {
        public int Id { get; set; }
        public string Insumo { get; set; }
        public string DescripcionInsumo { get; set; }
        public float Costo { get; set; }
        public int UnidadMedida { get; set; }
        public string InsumoUp { get; set; }
        public int Estatus { get; set; }
        public int UsuarioRegistra { get; set; } 
    }
}