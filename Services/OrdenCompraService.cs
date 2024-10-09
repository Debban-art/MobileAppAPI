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
        private string connection;
        private IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

        public OrdenCompraService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
            connection = settings.ConnectionString;
            _webHostEnvironment = webHostEnvironment;
        }

        public int InsertOrdenCompra (InsertOrdenCompraModel ordenCompra)
        {
            int IdOrdenCompra = 0;
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter {ParameterName = "IdProveedor", SqlDbType = SqlDbType.Int, Value = ordenCompra.IdProveedor});
            parametros.Add(new SqlParameter {ParameterName = "IdSucursal", SqlDbType = SqlDbType.Int, Value = ordenCompra.IdSucursal}); 
            parametros.Add(new SqlParameter {ParameterName = "IdComprador", SqlDbType = SqlDbType.Int, Value = ordenCompra.IdComprador}); 
            parametros.Add(new SqlParameter {ParameterName = "FechaLlegada", SqlDbType = SqlDbType.Date, Value = ordenCompra.FechaLlegada}); 
            parametros.Add(new SqlParameter {ParameterName = "UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = ordenCompra.UsuarioRegistra}); 

            try
            {
                DataSet ds = dac.Fill("sp_InsertOrdenCompra", parametros);
                IdOrdenCompra = ds.Tables[0].AsEnumerable().Select(dr => int.Parse(dr["IdOrdenCompra"].ToString())).ToList()[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return IdOrdenCompra;
        }

        public List<GetOrdenCompraModel> GetOrdenCompra()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            List<GetOrdenCompraModel> lista = new List<GetOrdenCompraModel>();
            parametros = new ArrayList();

            try
            {
                DataSet ds = dac.Fill("sp_GetOrdenCompra", parametros);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new GetOrdenCompraModel{
                            Id = int.Parse(dr["Id"].ToString()),
                            IdProveedor = int.Parse(dr["IdProveedor"].ToString()),
                            Proveedor = dr["Proveedor"].ToString(),
                            IdSucursal = int.Parse(dr["IdSucursal"].ToString()),
                            Sucursal = dr["Sucursal"].ToString(),
                            IdComprador = int.Parse(dr["IdComprador"].ToString()),
                            FechaLlegada = dr["FechaLlegada"].ToString(),
                            Fecha = dr["Fecha"].ToString(),
                            Total = float.Parse(dr["Total"].ToString()),
                            Estatus = int.Parse(dr["Estatus"].ToString()),
                            FechaRegistro = dr["FechaRegistro"].ToString(),
                            UsuarioRegistra = dr["UsuarioRegistra"].ToString() 
                        });
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            return lista;
        }

        public void UpdateOrdenCompra(UpdateOrdenCompraModel ordenCompra)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter {ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = ordenCompra.Id});
            parametros.Add(new SqlParameter {ParameterName = "IdProveedor", SqlDbType = SqlDbType.Int, Value = ordenCompra.IdProveedor});
            parametros.Add(new SqlParameter {ParameterName = "IdSucursal", SqlDbType = SqlDbType.Int, Value = ordenCompra.IdSucursal}); 
            parametros.Add(new SqlParameter {ParameterName = "FechaLlegada", SqlDbType = SqlDbType.Date, Value = ordenCompra.FechaLlegada}); 
            parametros.Add(new SqlParameter {ParameterName = "Estatus", SqlDbType = SqlDbType.Int, Value = ordenCompra.Estatus}); 
            parametros.Add(new SqlParameter {ParameterName = "UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = ordenCompra.UsuarioRegistra}); 

            try
            {
                dac.ExecuteNonQuery("sp_UpdateOrdenCompra", parametros);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void DeleteOrdenCompra(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = Id});
            try
            {
                dac.ExecuteNonQuery("sp_DeleteOrdenCompra", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}