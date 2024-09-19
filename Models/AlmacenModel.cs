using System;
namespace reportesApi.Models
{
    public class InsertAlmacenModel
    {
        public string Almacen_Nombre {get; set;}
        public string Almacen_Direccion {get; set;}
        public int Usuario_Registra {get; set;}
    }

    public class GetAlmacenesModel
    {
        public int Almacen_Id { get; set; }
        public string Almacen_Nombre {get; set;}
        public string Almacen_Direccion {get; set;}
        public int Almacen_Estatus {get; set;}
        public string Usuario_Registra {get; set;}
        public string Fecha_Registro {get; set;} 
    }

    public class UpdateAlmacenModel
    {
        public int Almacen_Id { get; set; }
        public string Almacen_Nombre {get; set;}
        public string Almacen_Direccion {get; set;}
        public int Almacen_Estatus {get; set;}
        public int Usuario_Registra {get; set;}
    }
}