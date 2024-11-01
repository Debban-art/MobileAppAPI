namespace reportesApi.Models
{
    public class InsertTipoMovimientoModel
    {
        public string Nombre {get;set;}
        public int EntradaSalida {get;set;}
        public int IdUsuarioRegistra {get; set;}
    }

    public class GetTiposMovimientoModel
    {
        public int Id {get; set;}
        public string Nombre {get;set;}
        public int EntradaSalida {get;set;}
        public int Estatus {get; set;}
        public string FechaRegistro {get; set;}
        public int IdUsuarioRegistra {get; set;}
        public string UsuarioRegistra {get; set;}
    }

    public class UpdateTipoMovimientoModel
    {
        public int Id {get; set;}
        public string Nombre {get;set;}
        public int EntradaSalida {get;set;}
        public int Estatus {get; set;}
        public int IdUsuarioRegistra {get; set;}
    }
}