using System;
namespace reportesApi.Models
{
    public class InsertRecetaModel
    {
        public string Nombre {get;set;}
        public int UsuarioRegistra {get;set;}
    }

    public class GetRecetaModel
    {
        public int Id {get;set;}
        public string Nombre {get;set;}
        public int Estatus {get;set;}
        public string FechaCreacion {get;set;}
        public string UsuarioRegistra {get;set;}
        public string FechaRegistro {get;set;}
    }

    public class UpdateRecetaModel
    {
        public int Id {get;set;}
        public string Nombre {get;set;}
        public int Estatus {get;set;}
        public int UsuarioRegistra {get;set;}
    }
}