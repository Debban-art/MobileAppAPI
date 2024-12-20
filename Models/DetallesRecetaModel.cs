namespace  reportesApi.Models
{
    public class InsertDetalleRecetaModel
    {
        public int IdReceta {get; set;}
        public string Insumo {get; set;}
        public float Cantidad {get; set;}
        public int UsuarioRegistra {get; set;}
    }

    public class GetDetallesRecetaModel
    {
        public int Id {get; set;}
        public int IdReceta {get; set;}
        public string Receta {get; set;}
        public string CodigoInsumo {get; set;}
        public string Insumo {get; set;}
        public float Cantidad {get; set;}
        public int Estatus {get; set;}
        public string FechaRegistro {get; set;}
        public string UsuarioRegistra {get; set;}
    }

    public class UpdateDetalleRecetaModel
    {
        public int Id {get; set;}
        public string Insumo {get; set;}
        public float Cantidad {get; set;}
        public int Estatus {get; set;}
        public int UsuarioRegistra {get; set;}
    }
}