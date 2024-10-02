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
    public class DetalleEntradaService
    {
         private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

         public DetalleEntradaService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

        public List<GetDetalleEntradaModel> GetDetalleEntradas(int IdEntrada)
        {

            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@IdEntrada", SqlDbType = SqlDbType.Int, Value = IdEntrada });

            List<GetDetalleEntradaModel> lista = new List<GetDetalleEntradaModel>();
            try
            {
                DataSet ds = dac.Fill("sp_get_detalleEntrada", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new GetDetalleEntradaModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        IdEntrada = int.Parse(dataRow["IdEntrada"].ToString()),
                        Insumo = dataRow["Insumo"].ToString(),
                        Cantidad = float.Parse(dataRow["Cantidad"].ToString()),
                        SinCargo = float.Parse(dataRow["SinCargo"].ToString()),
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

        public void InsertDetalleEntrada(InsertDetalleEntradaModel DetalleEntrada) 
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@IdEntrada", SqlDbType = SqlDbType.Int, Value = DetalleEntrada.IdEntrada });
            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = SqlDbType.VarChar, Value = DetalleEntrada.Insumo });
            parametros.Add(new SqlParameter { ParameterName = "@Cantidad", SqlDbType = SqlDbType.Decimal, Value = DetalleEntrada.Cantidad });
            parametros.Add(new SqlParameter { ParameterName = "@SinCargo", SqlDbType = SqlDbType.Decimal, Value = DetalleEntrada.SinCargo });
            parametros.Add(new SqlParameter { ParameterName = "@Costo", SqlDbType = SqlDbType.Decimal, Value = DetalleEntrada.Costo });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = DetalleEntrada.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_insert_detalleEntradas", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

         public void UpdateDetalleEntrada(UpdateDetalleEntradaModel DetalleEntrada) 
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = DetalleEntrada.Id });
            parametros.Add(new SqlParameter { ParameterName = "@IdEntrada", SqlDbType = SqlDbType.Int, Value = DetalleEntrada.IdEntrada });
            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = SqlDbType.VarChar, Value = DetalleEntrada.Insumo });
            parametros.Add(new SqlParameter { ParameterName = "@Cantidad", SqlDbType = SqlDbType.Decimal, Value = DetalleEntrada.Cantidad });
            parametros.Add(new SqlParameter { ParameterName = "@SinCargo", SqlDbType = SqlDbType.Decimal, Value = DetalleEntrada.SinCargo });
            parametros.Add(new SqlParameter { ParameterName = "@Costo", SqlDbType = SqlDbType.Decimal, Value = DetalleEntrada.Costo });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = DetalleEntrada.Estatus });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = DetalleEntrada.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_update_detalleEntrada", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

         public void DeleteDetalleEntrada(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Id });

            try
            {
                dac.ExecuteNonQuery("sp_delete_detalleEntrada", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        
        }
    }
}