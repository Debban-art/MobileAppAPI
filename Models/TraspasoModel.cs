namespace reportesApi.Models
{
    public class GetTraspasoModel
    {
        public int Id { get; set; }
        public int IdAlmacenEntrada {get; set;}
        public string AlmacenEntrada {get; set;}
        public int IdAlmacenSalida {get; set;}
        public string AlmacenSalida {get; set;}
        public int IdEstatusTraspaso {get; set;}
        public string EstatusTraspaso {get; set;}
        public string Insumo {get; set;}
        public string DescripcionInsumo {get; set;}
        public decimal Cantidad {get; set;}
        public string FechaInicio {get; set;}
        public string FechaSalida {get; set;}
        public string FechaEntrega {get; set;}
        public string FechaRegistro {get; set;}
        public int Estatus {get; set;}
        public int IdUsuarioRegistra {get; set;}
        public string UsuarioRegistra {get; set;}
    }

    public class GetTraspasoRequest
    {
        public int IdAlmacen {get; set;}
        public string FechaInicio {get; set;}
        public string FechaFin {get; set;}
        public int TipoTraspaso {get; set;}
    }

    public class InsertTraspasoModel
    {
        public int IdAlmacenEntrada { get; set; }
        public int IdAlmacenSalida {get; set;}
        public int IdUsuarioRegistra {get; set;}
    }

    public class UpdateTraspasoModel
    {
        public int Id { get; set; }
        public int IdAlmacenEntrada {get; set;}
        public int IdAlmacenSalida {get; set;}
        public int Estatus {get; set;}
        public int IdUsuarioRegistra {get; set;}
    }

    public class UpdateTraspasoEstatusModel
    {
        public int Id { get; set; }
        public int IdEstatusTraspaso {get; set;}
        public int IdUsuarioRegistra {get; set;}
        public int Estatus {get; set;}
    }
}