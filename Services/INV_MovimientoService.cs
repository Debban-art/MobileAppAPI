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
    public class INV_MovimientoService
    {
         private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

         public INV_MovimientoService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

        public List<GetINV_MovimientoModel> GetINV_Movimientos()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            List<GetINV_MovimientoModel> lista = new List<GetINV_MovimientoModel>();
            try
            {
                DataSet ds = dac.Fill("sp_get_INV_Movimientos", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new GetINV_MovimientoModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        IdTipoMovimiento = int.Parse(dataRow["IdTipoMovimiento"].ToString()),
                        TipoMovimiento = dataRow["TipoMovimiento"].ToString(),
                        IdAlmacen = int.Parse(dataRow["IdAlmacen"].ToString()),
                        NombreAlmacen = dataRow["NombreAlmacen"].ToString(),
                        Fecha = dataRow["Fecha"].ToString(),
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

        public int InsertINV_Movimiento(InsertINV_MovimientoModel INV_Movimiento) 
        {
            int IdINV_Movimiento;
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@IdTipoMovimiento", SqlDbType = SqlDbType.Int, Value = INV_Movimiento.IdTipoMovimiento });
            parametros.Add(new SqlParameter { ParameterName = "@IdAlmacen", SqlDbType = SqlDbType.Int, Value = INV_Movimiento.IdAlmacen });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = INV_Movimiento.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_insert_INV_Movimientos", parametros);
                IdINV_Movimiento = ds.Tables[0].AsEnumerable().Select(dataRow=>int.Parse(dataRow["IdMovimiento"].ToString())).ToList()[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            return IdINV_Movimiento;

        }

         public void UpdateINV_Movimiento(UpdateINV_MovimientoModel INV_Movimiento) 
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = INV_Movimiento.Id });
            parametros.Add(new SqlParameter { ParameterName = "@IdTipoMovimiento", SqlDbType = SqlDbType.Int, Value = INV_Movimiento.IdTipoMovimiento });
            parametros.Add(new SqlParameter { ParameterName = "@IdAlmacen", SqlDbType = SqlDbType.Int, Value = INV_Movimiento.IdAlmacen });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = INV_Movimiento.Estatus });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = INV_Movimiento.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_update_INV_Movimientos", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

         public void DeleteINV_Movimiento(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Id });

            try
            {
                dac.ExecuteNonQuery("sp_delete_INV_Movimiento", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        
        }
    }

}