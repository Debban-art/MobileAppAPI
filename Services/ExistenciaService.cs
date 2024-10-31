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
    public class ExistenciaService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

         public ExistenciaService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

        public List<GetExistenciaModel> GetExistencia()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            List<GetExistenciaModel> lista = new List<GetExistenciaModel>();
            try
            {
                DataSet ds = dac.Fill("sp_get_existencias", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new GetExistenciaModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        Fecha = dataRow["Fecha"].ToString(),
                        Insumo = dataRow["Insumo"].ToString(),
                        Cantidad = float.Parse(dataRow["Cantidad"].ToString()),
                        IdAlmacen = int.Parse(dataRow["IdAlmacen"].ToString()),
                        NombreAlmacen = dataRow["NombreAlmacen"].ToString(),
                        Estatus = int.Parse(dataRow["Estatus"].ToString()),
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

         public void InsertExistencia(InsertExistenciaModel Existencia) 
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = SqlDbType.VarChar, Value = Existencia.Insumo });
            parametros.Add(new SqlParameter { ParameterName = "@Cantidad", SqlDbType = SqlDbType.Decimal, Value = Existencia.Cantidad });
            parametros.Add(new SqlParameter { ParameterName = "@IdAlmacen", SqlDbType = SqlDbType.Int, Value = Existencia.IdAlmacen });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = Existencia.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_insert_existencia", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

        public void UpdateExistencia(UpdateExistenciaModel Existencia) 
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Existencia.Id });
            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = SqlDbType.VarChar, Value = Existencia.Insumo });
            parametros.Add(new SqlParameter { ParameterName = "@Cantidad", SqlDbType = SqlDbType.Decimal, Value = Existencia.Cantidad });
            parametros.Add(new SqlParameter { ParameterName = "@IdAlmacen", SqlDbType = SqlDbType.Int, Value = Existencia.IdAlmacen });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = Existencia.Estatus });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = Existencia.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_update_existencia", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

        public void DeleteExistencia(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Id });

            try
            {
                dac.ExecuteNonQuery("sp_delete_existencia", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        
        }
    }
}