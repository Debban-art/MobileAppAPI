namespace reportesApi.Models
{
    public class InsertRecetaModel
    {
        public string Nombre{get; set;}
        public int Usuario_Registra{get; set;}
    }

    public class GetRecetasModel
    {
        public int Id {get; set;}
        public string Nombre {get; set;}
        public string Fecha_Creacion {get; set;}
        public int Estatus {get; set;}
        public string Usuario_Registra {get; set;}
        public string Fecha_Registro {get; set;}
    }
}