using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using reportesApi.DataContext;
using reportesApi.Models;
using System.Collections.Generic;
using OfficeOpenXml;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
namespace reportesApi.Services 
{
    public class RecetaService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

         public RecetaService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

        public List<GetRecetaModel> GetRecetas()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            List<GetRecetaModel> lista = new List<GetRecetaModel>();
            try
            {
                DataSet ds = dac.Fill("sp_get_recetas", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new GetRecetaModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        Nombre = dataRow["Nombre"].ToString(),
                        Estatus = int.Parse(dataRow["Estatus"].ToString()),
                        FechaCreacion = dataRow["FechaCreacion"].ToString(),
                        UsuarioRegistra = dataRow["UsuarioRegistra"].ToString(),
                        FechaRegistro= dataRow["FechaRegistro"].ToString()
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            return lista;
        }

        public int InsertReceta(InsertRecetaModel Receta) 
        public List<GetReporteRecetasModel> GetReporteRecetas(string FechaInicio, string FechaFin)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter{ParameterName="FechaInicio", SqlDbType = SqlDbType.Date, Value=FechaInicio});
            parametros.Add(new SqlParameter{ParameterName="FechaFin", SqlDbType = SqlDbType.Date, Value=FechaFin});
            List<GetReporteRecetasModel> lista = new List<GetReporteRecetasModel>();

            try
            {
                DataSet ds = dac.Fill("sp_GetReporteRecetas", parametros);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new GetReporteRecetasModel
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Nombre = dr["Nombre"].ToString(),
                            Insumo = dr["Insumo"].ToString(),
                            Cantidad = float.Parse(dr["Cantidad"].ToString()),
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

        public void UpdateReceta(UpdateRecetaModel receta)
        {
            int IdReceta;
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = SqlDbType.VarChar, Value = Receta.Nombre });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = Receta.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_insert_recetas", parametros);
                IdReceta = ds.Tables[0].AsEnumerable().Select(dataRow=>int.Parse(dataRow["IdReceta"].ToString())).ToList()[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            return IdReceta;

        }

         public void UpdateReceta(UpdateRecetaModel Receta) 
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Receta.Id });
            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = SqlDbType.VarChar, Value = Receta.Nombre });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = Receta.Estatus });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = Receta.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_update_recetas", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

         public void DeleteReceta(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Id });

            try
            {
                dac.ExecuteNonQuery("sp_delete_recetas", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        
        }
    }

}