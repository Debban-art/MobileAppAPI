namespace reportesApi.Models
{
    public class InsertMovimientoModel
    {
        public int IdTipoMovimiento {get; set;}
        public int IdAlmacen {get; set;}
        public int IdUsuario {get; set;}
    }

    public class GetMovimientosModel
    {
        public int Id {get; set;}
        public int IdTipoMovimiento {get; set;}
        public string TipoMovimiento {get; set;}
        public int IdAlmacen {get; set;}
        public string Almacen {get; set;}
        public string Fecha {get; set;}
        public int Estatus {get; set;}
        public int IdUsuario {get; set;}
        public string Usuario {get; set;}
    }

    public class UpdateMovimientoModel
    {
        public int Id {get; set;}
        public int IdTipoMovimiento {get; set;}
        public int IdAlmacen {get; set;}
        public int Estatus {get; set;}
        public int IdUsuario {get; set;}
    }
}