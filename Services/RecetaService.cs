using System.Collections;
using System.Data;
using System.Data.SqlClient;
using reportesApi.DataContext;
using reportesApi.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System;

namespace reportesApi.Services
{
    public class RecetaService
    {
        private string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

        public RecetaService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
            connection = settings.ConnectionString;
            _webHostEnvironment = webHostEnvironment;
        }

        public void InsertReceta(InsertRecetaModel receta)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Nombre", SqlDbType = SqlDbType.VarChar, Value = receta.Nombre  });
            parametros.Add(new SqlParameter { ParameterName = "UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = receta.Usuario_Registra  });

            try
            {
                dac.ExecuteNonQuery("sp_InsertReceta", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public List<GetRecetasModel> GetRecetas()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            List<GetRecetasModel> lista = new List<GetRecetasModel>();

            try
            {
                DataSet ds = dac.Fill("sp_GetRecetas", parametros);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new GetRecetasModel
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Nombre = dr["Nombre"].ToString(),
                            Fecha_Creacion = dr["FechaCreacion"].ToString(),
                            Estatus = int.Parse(dr["Estatus"].ToString()),
                            Usuario_Registra = dr["UsuarioRegistra"].ToString(),
                            Fecha_Registro = dr["FechaRegistro"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return lista;
        }


    }
}