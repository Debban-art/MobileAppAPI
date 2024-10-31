namespace reportesApi.Models
{
    public class InsertExistenciaModel
    {
        public string Insumo {get;set;}
        public float Cantidad {get;set;}
        public int IdAlmacen {get;set;}
        public int UsuarioRegistra {get;set;}
    }

    public class GetExistenciaModel
    {
        public int Id {get;set;}
        public string Fecha {get;set;}
        public string Insumo {get;set;}
        public float Cantidad {get;set;}
        public int IdAlmacen {get;set;}
        public string NombreAlmacen {get;set;}
        public int Estatus {get;set;}
        public string UsuarioRegistra {get;set;}
        public string FechaRegistro {get;set;}
    }

    public class UpdateExistenciaModel
    {
        public int Id {get;set;}
        public string Insumo {get;set;}
        public float Cantidad {get;set;}
        public int IdAlmacen {get;set;}
        public int Estatus {get;set;}
        public int UsuarioRegistra {get;set;}
    }
}
