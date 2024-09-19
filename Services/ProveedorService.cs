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
    public class ProveedorService 
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

        public ProveedorService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

        public List<GetProveedorModel> GetProveedores()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            List<GetProveedorModel> lista = new List<GetProveedorModel>();
            try
            {
                DataSet ds = dac.Fill("sp_get_proveedores", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new GetProveedorModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        Nombre = dataRow["Nombre"].ToString(),
                        Direccion = dataRow["Direccion"].ToString(),
                        Email = dataRow["Email"].ToString(),
                        RFC = dataRow["RFC"].ToString(),
                        PlazoPago = int.Parse(dataRow["PlazoPago"].ToString()),
                        PorcentajeRetencion = float.Parse(dataRow["PorcentajeRetencion"].ToString()),
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

        public void InsertProveedor(InsertProveedorModel Proveedor) 
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = SqlDbType.VarChar, Value = Proveedor.Nombre });
            parametros.Add(new SqlParameter { ParameterName = "@Direccion", SqlDbType = SqlDbType.VarChar, Value = Proveedor.Direccion });
            parametros.Add(new SqlParameter { ParameterName = "@Email", SqlDbType = SqlDbType.VarChar, Value = Proveedor.Email });
            parametros.Add(new SqlParameter { ParameterName = "@RFC", SqlDbType = SqlDbType.VarChar, Value = Proveedor.RFC });
            parametros.Add(new SqlParameter { ParameterName = "@PlazoPago", SqlDbType = SqlDbType.Int, Value = Proveedor.PlazoPago });
            parametros.Add(new SqlParameter { ParameterName = "@PorcentajeRetencion", SqlDbType = SqlDbType.Decimal, Value = Proveedor.PorcentajeRetencion });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = Proveedor.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_insert_proveedores", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

        public void UpdateProveedor(UpdateProveedorModel Proveedor) 
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Proveedor.Id });
            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = SqlDbType.VarChar, Value = Proveedor.Nombre });
            parametros.Add(new SqlParameter { ParameterName = "@Direccion", SqlDbType = SqlDbType.VarChar, Value = Proveedor.Direccion });
            parametros.Add(new SqlParameter { ParameterName = "@Email", SqlDbType = SqlDbType.VarChar, Value = Proveedor.Email });
            parametros.Add(new SqlParameter { ParameterName = "@RFC", SqlDbType = SqlDbType.VarChar, Value = Proveedor.RFC });
            parametros.Add(new SqlParameter { ParameterName = "@PlazoPago", SqlDbType = SqlDbType.Int, Value = Proveedor.PlazoPago });
            parametros.Add(new SqlParameter { ParameterName = "@PorcentajeRetencion", SqlDbType = SqlDbType.Decimal, Value = Proveedor.PorcentajeRetencion });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = Proveedor.Estatus });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = Proveedor.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_update_proveedores", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

        public void DeleteProveedor(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Id });

            try
            {
                dac.ExecuteNonQuery("sp_delete_proveedores", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        
        }
    }
}