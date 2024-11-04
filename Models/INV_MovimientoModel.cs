namespace reportesApi.Models
{
    public class InsertINV_MovimientoModel
    {
        public int IdTipoMovimiento {get; set;}
        public int IdAlmacen {get; set;}
        public int UsuarioRegistra {get; set;}
    }

    public class GetINV_MovimientoModel
    {
        public int Id {get; set;}
        public int IdTipoMovimiento {get; set;}
        public string TipoMovimiento {get; set;}
        public int IdAlmacen {get; set;}
        public string NombreAlmacen {get; set;}
        public string Fecha {get; set;}
        public int Estatus {get; set;}
        public string UsuarioRegistra {get; set;}
        public string FechaRegistro {get; set;}
    }

    public class UpdateINV_MovimientoModel
    {
        public int Id {get; set;}
        public int IdTipoMovimiento {get; set;}
        public int IdAlmacen {get; set;}
        public int Estatus {get; set;}
        public int UsuarioRegistra {get; set;}
    }
}