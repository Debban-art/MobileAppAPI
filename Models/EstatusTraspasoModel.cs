namespace reportesApi.Models
{
    public class GetEstatusTraspasoModel
    {
        public int Id {get; set;}
        public string Nombre {get; set;}
        public int Estatus {get; set;}
        public string FechaRegistro {get; set;}
        public int IdUsuarioRegistra {get; set;}
        public string UsuarioRegistra {get; set;}
    }

    public class InsertEstatusTraspasoModel
    {
        public string Nombre {get; set;}
        public int IdUsuarioRegistra {get; set;}
    }

    public class UpdateEstatusTraspasoModel
    {
        public int Id {get; set;}
        public string Nombre {get; set;}
        public int Estatus {get; set;}
        public int IdUsuarioRegistra {get; set;}
    }
}