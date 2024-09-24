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
    public class InsumoService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

         public InsumoService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

        public List<GetInsumoModel> GetInsumo()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            List<GetInsumoModel> lista = new List<GetInsumoModel>();
            try
            {
                DataSet ds = dac.Fill("sp_get_insumos", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new GetInsumoModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        Insumo = dataRow["Insumo"].ToString(),
                        DescripcionInsumo = dataRow["DescripcionInsumo"].ToString(),
                        Costo = float.Parse(dataRow["Costo"].ToString()),
                        UnidadMedida = int.Parse(dataRow["UnidadMedida"].ToString()),
                        InsumoUp = dataRow["InsumoUp"].ToString(),
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

         public void InsertInsumo(InsertInsumoModel Insumo) 
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = SqlDbType.VarChar, Value = Insumo.Insumo });
            parametros.Add(new SqlParameter { ParameterName = "@DescripcionInsumo", SqlDbType = SqlDbType.VarChar, Value = Insumo.DescripcionInsumo });
            parametros.Add(new SqlParameter { ParameterName = "@Costo", SqlDbType = SqlDbType.Decimal, Value = Insumo.Costo });
            parametros.Add(new SqlParameter { ParameterName = "@UnidadMedida", SqlDbType = SqlDbType.Int, Value = Insumo.UnidadMedida });
            parametros.Add(new SqlParameter { ParameterName = "@InsumoUp", SqlDbType = SqlDbType.VarChar, Value = Insumo.InsumoUp });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = Insumo.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_insert_insumos", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

        public void UpdateInsumo(UpdateInsumoModel Insumo) 
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Insumo.Id });
            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = SqlDbType.VarChar, Value = Insumo.Insumo });
            parametros.Add(new SqlParameter { ParameterName = "@DescripcionInsumo", SqlDbType = SqlDbType.VarChar, Value = Insumo.DescripcionInsumo });
            parametros.Add(new SqlParameter { ParameterName = "@Costo", SqlDbType = SqlDbType.Decimal, Value = Insumo.Costo });
            parametros.Add(new SqlParameter { ParameterName = "@UnidadMedida", SqlDbType = SqlDbType.Int, Value = Insumo.UnidadMedida });
            parametros.Add(new SqlParameter { ParameterName = "@InsumoUp", SqlDbType = SqlDbType.VarChar, Value = Insumo.InsumoUp });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = Insumo.Estatus });
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = Insumo.UsuarioRegistra });

            try 
            {
                DataSet ds = dac.Fill("sp_update_insumos", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

        public void DeleteInsumo(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Id });

            try
            {
                dac.ExecuteNonQuery("sp_delete_insumos", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        
        }
    }
}