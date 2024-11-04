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
    public class INV_TipoMovimientoService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

         public INV_TipoMovimientoService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

        public List<GetINV_TipoMovimientoModel> GetINV_TipoMovimiento()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            List<GetINV_TipoMovimientoModel> lista = new List<GetINV_TipoMovimientoModel>();
            try
            {
                DataSet ds = dac.Fill("sp_get_INV_TiposMovimiento", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new GetINV_TipoMovimientoModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        Nombre = dataRow["Nombre"].ToString(),
                        EntradaSalida = int.Parse(dataRow["EntradaSalida"].ToString()),
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

         public void InsertINV_TipoMovimiento(InsertINV_TipoMovimientoModel INV_TipoMovimiento) 
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = SqlDbType.VarChar, Value = INV_TipoMovimiento.Nombre });
            parametros.Add(new SqlParameter { ParameterName = "@EntradaSalida", SqlDbType = SqlDbType.Int, Value = INV_TipoMovimiento.EntradaSalida });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = INV_TipoMovimiento.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_insert_INV_TiposMovimiento", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

        public void UpdateINV_TipoMovimiento(UpdateINV_TipoMovimientoModel INV_TipoMovimiento) 
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = INV_TipoMovimiento.Id });
            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = SqlDbType.VarChar, Value = INV_TipoMovimiento.Nombre });
            parametros.Add(new SqlParameter { ParameterName = "@EntradaSalida", SqlDbType = SqlDbType.Int, Value = INV_TipoMovimiento.EntradaSalida });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = INV_TipoMovimiento.Estatus });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = INV_TipoMovimiento.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_update_INV_TiposMovimiento", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

        public void DeleteINV_TipoMovimiento(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Id });

            try
            {
                dac.ExecuteNonQuery("sp_delete_INV_TiposMovimiento", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        
        }
    }
}