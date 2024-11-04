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
    public class INV_RenglonesMovimientoService
    {
         private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

         public INV_RenglonesMovimientoService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

        public List<GetINV_RenglonesMovimientoModel> GetINV_RenglonesMovimientos(int IdMovimiento)
        {

            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@IdMovimiento", SqlDbType = SqlDbType.Int, Value = IdMovimiento });

            List<GetINV_RenglonesMovimientoModel> lista = new List<GetINV_RenglonesMovimientoModel>();
            try
            {
                DataSet ds = dac.Fill("sp_get_INV_RenMovimiento", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new GetINV_RenglonesMovimientoModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        IdMovimiento = int.Parse(dataRow["IdMovimiento"].ToString()),
                        Insumo = dataRow["Insumo"].ToString(),
                        Cantidad = float.Parse(dataRow["Cantidad"].ToString()),
                        Costo = float.Parse(dataRow["Costo"].ToString()),
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

        public void InsertINV_RenglonesMovimiento(InsertINV_RenglonesMovimientoModel INV_RenglonesMovimiento) 
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@IdMovimiento", SqlDbType = SqlDbType.Int, Value = INV_RenglonesMovimiento.IdMovimiento });
            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = SqlDbType.VarChar, Value = INV_RenglonesMovimiento.Insumo });
            parametros.Add(new SqlParameter { ParameterName = "@Cantidad", SqlDbType = SqlDbType.Decimal, Value = INV_RenglonesMovimiento.Cantidad });
            parametros.Add(new SqlParameter { ParameterName = "@Costo", SqlDbType = SqlDbType.Decimal, Value = INV_RenglonesMovimiento.Costo });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = INV_RenglonesMovimiento.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_insert_INV_RenMovimiento", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

         public void UpdateINV_RenglonesMovimiento(UpdateINV_RenglonesMovimientoModel INV_RenglonesMovimiento) 
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = INV_RenglonesMovimiento.Id });
            parametros.Add(new SqlParameter { ParameterName = "@IdMovimiento", SqlDbType = SqlDbType.Int, Value = INV_RenglonesMovimiento.IdMovimiento });
            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = SqlDbType.VarChar, Value = INV_RenglonesMovimiento.Insumo });
            parametros.Add(new SqlParameter { ParameterName = "@Cantidad", SqlDbType = SqlDbType.Decimal, Value = INV_RenglonesMovimiento.Cantidad });
            parametros.Add(new SqlParameter { ParameterName = "@Costo", SqlDbType = SqlDbType.Decimal, Value = INV_RenglonesMovimiento.Costo });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = INV_RenglonesMovimiento.Estatus });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = INV_RenglonesMovimiento.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_update_INV_RenMovimiento", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

         public void DeleteINV_RenglonesMovimiento(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Id });

            try
            {
                dac.ExecuteNonQuery("sp_delete_INV_RenMovimiento", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        
        }
    }
}