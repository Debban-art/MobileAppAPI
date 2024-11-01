namespace reportesApi.Models
{
    public class InsertExistenciaModel
    {
        public string Insumo {get; set;}
        public decimal Cantidad {get; set;}
        public int IdAlmacen {get; set;}
        public int IdUsuario {get; set;}

    }

    public class GetExistenciasModel
    {
        public int Id {get; set;}
        public string Fecha {get; set;}
        public string Insumo {get; set;}
        public string DescripcionInsumo {get;set;}
        public decimal Cantidad {get; set;}
        public int IdAlmacen {get; set;}
        public string Almacen {get; set;}
        public int Estatus {get; set;}
        public string FechaRegistro {get; set;}
        public int IdUsuario {get; set;}
        public string Usuario {get; set;}
    }

    public class UpdateExistenciaModel
    {
        public int Id {get; set;}
        public decimal Cantidad {get; set;}
        public int Estatus {get; set;}
        public int IdAlmacen {get; set;}
        public int IdUsuario {get; set;}
    }
}