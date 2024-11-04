namespace reportesApi.Models
{
    public class InsertINV_RenglonesMovimientoModel
    {
        public int IdMovimiento {get; set;}
        public string Insumo {get; set;}
        public float Cantidad {get; set;}
        public float Costo {get; set;}
        public int UsuarioRegistra {get; set;}
    }

    public class GetINV_RenglonesMovimientoModel
    {
        public int Id {get; set;}
        public int IdMovimiento {get; set;}
        public string Insumo {get; set;}
        public float Cantidad {get; set;}
        public float Costo {get; set;}
        public int Estatus {get; set;}
        public string UsuarioRegistra {get; set;}
        public string FechaRegistro {get; set;}
    }

    public class UpdateINV_RenglonesMovimientoModel
    {
        public int Id {get; set;}
        public int IdMovimiento {get; set;}
        public string Insumo {get; set;}
        public float Cantidad {get; set;}
        public float Costo {get; set;}
        public int Estatus {get; set;}
        public int UsuarioRegistra {get; set;}
    }
}