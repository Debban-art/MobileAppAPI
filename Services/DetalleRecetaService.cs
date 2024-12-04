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
    public class DetalleRecetaService
    {
         private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

         public DetalleRecetaService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

        public List<GetDetalleRecetaModel> GetDetalleRecetas(int IdReceta)
        {

            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@IdReceta", SqlDbType = SqlDbType.Int, Value = IdReceta });

            List<GetDetalleRecetaModel> lista = new List<GetDetalleRecetaModel>();
            try
            {
                DataSet ds = dac.Fill("sp_get_detalleReceta", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new GetDetalleRecetaModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        IdReceta = int.Parse(dataRow["IdReceta"].ToString()),
                        CodigoInsumo = dataRow["CodigoInsumo"].ToString(),
                        Insumo = dataRow["Insumo"].ToString(),
                        Cantidad = float.Parse(dataRow["Cantidad"].ToString()),
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

        public void InsertDetalleReceta(InsertDetalleRecetaModel DetalleReceta) 
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@IdReceta", SqlDbType = SqlDbType.Int, Value = DetalleReceta.IdReceta });
            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = SqlDbType.VarChar, Value = DetalleReceta.Insumo });
            parametros.Add(new SqlParameter { ParameterName = "@Cantidad", SqlDbType = SqlDbType.Decimal, Value = DetalleReceta.Cantidad });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = DetalleReceta.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_insert_detalleReceta", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

         public void UpdateDetalleReceta(UpdateDetalleRecetaModel DetalleReceta) 
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = DetalleReceta.Id });
            parametros.Add(new SqlParameter { ParameterName = "@IdReceta", SqlDbType = SqlDbType.Int, Value = DetalleReceta.IdReceta });
            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = SqlDbType.VarChar, Value = DetalleReceta.Insumo });
            parametros.Add(new SqlParameter { ParameterName = "@Cantidad", SqlDbType = SqlDbType.Decimal, Value = DetalleReceta.Cantidad });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = DetalleReceta.Estatus });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = DetalleReceta.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_update_detalleReceta", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

         public void DeleteDetalleReceta(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Id });

            try
            {
                dac.ExecuteNonQuery("sp_delete_detalleReceta", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        
        }
    }
}