namespace reportesApi.Models
{
    public class InsertOrdenCompraModel
    {
        public int IdProveedor {get; set;}
        public int IdSucursal {get; set;}
        public int IdComprador {get; set;}
        public string FechaLlegada {get; set;}
        public int UsuarioRegistra {get; set;}
    }

    public class GetOrdenCompraModel
    {
        public int Id {get; set;}
        public int IdProveedor {get; set;}
        public string Proveedor {get; set;}
        public int IdSucursal {get; set;}
        public string Sucursal {get; set;}
        public int IdComprador {get; set;}
        public string FechaLlegada {get; set;}
        public string Fecha {get; set;}
        public float Total {get; set;}
        public int Estatus {get; set;}
        public string FechaRegistro {get; set;}
        public string UsuarioRegistra {get; set;}
    }

    public class UpdateOrdenCompraModel
    {
        public int Id {get; set;}
        public int IdProveedor {get; set;}
        public int IdSucursal {get; set;}
        public string FechaLlegada {get; set;}
        public int Estatus {get; set;}
        public int UsuarioRegistra {get; set;}
    }
}