namespace reportesApi.Models
{
    public class InsertDetallesEntradaModel
    {
        public int IdEntrada {get; set;}
        public string Insumo {get; set;}
        public decimal Cantidad {get; set;}
        public decimal Costo {get; set;}
        public int UsuarioRegistra {get; set;}
    }
    public class GetDetallesEntradaModel
    {
        public int Id {get; set;}
        public int IdEntrada {get; set;}
        public string Insumo {get; set;}
        public decimal Cantidad {get; set;}
        public decimal SinCargo {get; set;}
        public decimal Costo{get; set;}
        public int Estatus {get; set;}
        public string FechaRegistro {get; set;}
        public string UsuarioRegistra {get; set;}
    }

    public class UpdateDetallesEntradaModel
    {
        public int Id {get; set;}
        public int IdEntrada {get; set;}
        public string Insumo {get; set;}
        public decimal Cantidad {get; set;}
        public decimal Costo {get; set;}
        public int Estatus {get; set;}
        public int UsuarioRegistra {get; set;}
    }
}