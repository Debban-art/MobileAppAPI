namespace reportesApi.Models
{
    public class InsertRenglonMovimientoModel
    {
        public int IdMovimiento {get; set;}
        public string Insumo {get; set;}
        public decimal Cantidad {get; set;}
        public decimal Costo {get; set;}
        public int IdUsuarioRegistra {get; set;}
    }

    public class GetRenglonesMovimientoModel
    {
        public int Id {get; set;}
        public int IdMovimiento {get; set;}
        public string Insumo {get; set;}
        public string DescripcionInsumo {get; set;}
        public decimal Cantidad {get; set;}
        public decimal Costo {get; set;}
        public int Estatus {get; set;}
        public string FechaRegistro {get; set;}
        public int IdUsuarioRegistra {get; set;}
        public string UsuarioRegistra {get; set;}
    }

    public class UpdateRenglonMovimientoModel
    {
        public int Id {get; set;}
        public string Insumo {get; set;}
        public decimal Cantidad {get; set;}
        public decimal Costo {get; set;}
        public int Estatus {get; set;}
        public int IdUsuarioRegistra {get; set;}
    }
}