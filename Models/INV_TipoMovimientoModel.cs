namespace reportesApi.Models
{
    public class InsertINV_TipoMovimientoModel
    {
        public string Nombre {get;set;}
        public int EntradaSalida {get;set;}
        public int UsuarioRegistra {get;set;}
    }

    public class GetINV_TipoMovimientoModel
    {
        public int Id {get;set;}
        public string Nombre {get;set;}
        public int EntradaSalida {get;set;}
        public int Estatus {get;set;}
        public string UsuarioRegistra {get;set;}
        public string FechaRegistro {get;set;}
    }

    public class UpdateINV_TipoMovimientoModel
    {
        public int Id {get;set;}
        public string Nombre {get;set;}
        public int EntradaSalida {get;set;}
        public int Estatus {get;set;}
        public int UsuarioRegistra {get;set;}
    
    }
}