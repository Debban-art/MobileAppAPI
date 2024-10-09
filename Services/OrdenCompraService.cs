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
    public class OrdenCompraService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

         public OrdenCompraService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

        public List<GetOrdenCompraModel> GetOrdenCompras()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            List<GetOrdenCompraModel> lista = new List<GetOrdenCompraModel>();
            try
            {
                DataSet ds = dac.Fill("sp_get_ordenCompra", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new GetOrdenCompraModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        IdProveedor = int.Parse(dataRow["IdProveedor"].ToString()),
                        FechaLlegada = dataRow["FechaLlegada"].ToString(),
                        IdSucursal = int.Parse(dataRow["IdSucursal"].ToString()),
                        IdComprador = int.Parse(dataRow["IdComprador"].ToString()),
                        Fecha = dataRow["Fecha"].ToString(),
                        Total = float.Parse(dataRow["Total"].ToString()),
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

        public int InsertOrdenCompra(InsertOrdenCompraModel OrdenCompra) 
        {
            int IdOrdenCompra;
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@IdProveedor", SqlDbType = SqlDbType.Int, Value = OrdenCompra.IdProveedor });
            parametros.Add(new SqlParameter { ParameterName = "@FechaLlegada", SqlDbType = SqlDbType.VarChar, Value = OrdenCompra.FechaLlegada });
            parametros.Add(new SqlParameter { ParameterName = "@IdSucursal", SqlDbType = SqlDbType.Int, Value = OrdenCompra.IdSucursal });
            parametros.Add(new SqlParameter { ParameterName = "@IdComprador", SqlDbType = SqlDbType.Int, Value = OrdenCompra.IdComprador });
            parametros.Add(new SqlParameter { ParameterName = "@Fecha", SqlDbType = SqlDbType.VarChar, Value = OrdenCompra.Fecha });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = OrdenCompra.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_insert_ordenCompra", parametros);
                IdOrdenCompra = ds.Tables[0].AsEnumerable().Select(dataRow=>int.Parse(dataRow["IdOrdenCompra"].ToString())).ToList()[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            return IdOrdenCompra;

        }

         public void UpdateOrdenCompra(UpdateOrdenCompraModel OrdenCompra) 
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = OrdenCompra.Id });
            parametros.Add(new SqlParameter { ParameterName = "@IdProveedor", SqlDbType = SqlDbType.Int, Value = OrdenCompra.IdProveedor });
            parametros.Add(new SqlParameter { ParameterName = "@IdSucursal", SqlDbType = SqlDbType.Int, Value = OrdenCompra.IdSucursal });
            parametros.Add(new SqlParameter { ParameterName = "@IdComprador", SqlDbType = SqlDbType.Int, Value = OrdenCompra.IdComprador });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = OrdenCompra.Estatus });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = OrdenCompra.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_update_ordenCompra", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

         public void DeleteOrdenCompra(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Id });

            try
            {
                dac.ExecuteNonQuery("sp_delete_ordenCompra", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        
        }
    }

}