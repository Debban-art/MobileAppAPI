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
    public class DetalleOrdenCompraService
    {
         private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

         public DetalleOrdenCompraService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

        public List<GetDetalleOrdenCompraModel> GetDetalleOrdenCompras(int IdOrdenCompra)
        {

            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@IdOrdenCompra", SqlDbType = SqlDbType.Int, Value = IdOrdenCompra });

            List<GetDetalleOrdenCompraModel> lista = new List<GetDetalleOrdenCompraModel>();
            try
            {
                DataSet ds = dac.Fill("sp_get_detalleOrdenCompra", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new GetDetalleOrdenCompraModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        IdOrdenCompra = int.Parse(dataRow["IdOrdenCompra"].ToString()),
                        Insumo = dataRow["Insumo"].ToString(),
                        Cantidad = float.Parse(dataRow["Cantidad"].ToString()),
                        CantidadRecibida = float.Parse(dataRow["CantidadRecibida"].ToString()),
                        Costo = float.Parse(dataRow["Costo"].ToString()),
                        CostoRenglon = float.Parse(dataRow["CostoRenglon"].ToString()),
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

        public void InsertDetalleOrdenCompra(InsertDetalleOrdenCompraModel DetalleOrdenCompra) 
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@IdOrdenCompra", SqlDbType = SqlDbType.Int, Value = DetalleOrdenCompra.IdOrdenCompra });
            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = SqlDbType.VarChar, Value = DetalleOrdenCompra.Insumo });
            parametros.Add(new SqlParameter { ParameterName = "@Cantidad", SqlDbType = SqlDbType.Decimal, Value = DetalleOrdenCompra.Cantidad });
            parametros.Add(new SqlParameter { ParameterName = "@CantidadRecibida", SqlDbType = SqlDbType.Decimal, Value = DetalleOrdenCompra.CantidadRecibida });
            parametros.Add(new SqlParameter { ParameterName = "@Costo", SqlDbType = SqlDbType.Decimal, Value = DetalleOrdenCompra.Costo });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = DetalleOrdenCompra.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_insert_detalleOrdenCompra", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

         public void UpdateDetalleOrdenCompra(UpdateDetalleOrdenCompraModel DetalleOrdenCompra) 
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = DetalleOrdenCompra.Id });
            parametros.Add(new SqlParameter { ParameterName = "@IdOrdenCompra", SqlDbType = SqlDbType.Int, Value = DetalleOrdenCompra.IdOrdenCompra });
            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = SqlDbType.VarChar, Value = DetalleOrdenCompra.Insumo });
            parametros.Add(new SqlParameter { ParameterName = "@Cantidad", SqlDbType = SqlDbType.Decimal, Value = DetalleOrdenCompra.Cantidad });
            parametros.Add(new SqlParameter { ParameterName = "@CantidadRecibida", SqlDbType = SqlDbType.Decimal, Value = DetalleOrdenCompra.CantidadRecibida });
            parametros.Add(new SqlParameter { ParameterName = "@Costo", SqlDbType = SqlDbType.Decimal, Value = DetalleOrdenCompra.Costo });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = DetalleOrdenCompra.Estatus });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = DetalleOrdenCompra.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_update_detalleOrdenCompra", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

         public void DeleteDetalleOrdenCompra(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Id });

            try
            {
                dac.ExecuteNonQuery("sp_delete_detalleOrdenCompra", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        
        }
    }
}