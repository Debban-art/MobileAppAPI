namespace reportesApi.Models
{
    public class GetRenglonesTraspasoModel
    {
        public int Id { get; set; }
        public int IdTraspaso {get; set;}
        public string Insumo {get; set;}
        public string DescripcionInsumo {get; set;}
        public decimal Cantidad {get; set;}
        public string FechaRegistro {get; set;}
        public int Estatus {get; set;}
        public int IdUsuarioRegistra {get; set;}
        public string UsuarioRegistra {get; set;}
    }

    public class InsertRenglonTraspasoModel
    {
        public int IdTraspaso { get; set; }
        public string Insumo { get; set; }
        public decimal Cantidad {get; set;}
        public int IdUsuarioRegistra {get; set;}
    }

    public class UpdateRenglonTraspasoModel
    {
        public int Id { get; set; }
        public string Insumo {get; set;}
        public decimal Cantidad {get; set;}
        public int Estatus {get; set;}
        public int IdUsuarioRegistra {get; set;}
    }

}