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
    public class DetallesEntradaService
    {
        private string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

        public DetallesEntradaService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
            connection = settings.ConnectionString;
            _webHostEnvironment = webHostEnvironment;
        }

        public void InsertDetallesEntrada(InsertDetallesEntradaModel DetallesEntrada)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@IdEntrada", SqlDbType = System.Data.SqlDbType.Int, Value = DetallesEntrada.IdEntrada});
            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = System.Data.SqlDbType.VarChar, Value = DetallesEntrada.Insumo});
            parametros.Add(new SqlParameter { ParameterName = "@Cantidad", SqlDbType = System.Data.SqlDbType.Decimal, Value = DetallesEntrada.Cantidad});
            parametros.Add(new SqlParameter { ParameterName = "@Costo", SqlDbType = System.Data.SqlDbType.Decimal, Value = DetallesEntrada.Costo});
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = System.Data.SqlDbType.Int, Value = DetallesEntrada.UsuarioRegistra });

            try
            {
                DataSet ds = dac.Fill("sp_InsertDetallesEntrada", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public List<GetDetallesEntradaModel> GetDetallesEntradas(int IdEntrada)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter {ParameterName = "IdEntrada", SqlDbType = SqlDbType.Int, Value = IdEntrada});
            List<GetDetallesEntradaModel> lista = new List<GetDetallesEntradaModel>();

            try
            {
                DataSet ds = dac.Fill("sp_GetDetallesEntradas", parametros);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new GetDetallesEntradaModel
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Insumo = dr["Insumo"].ToString(),
                            Cantidad = decimal.Parse(dr["Cantidad"].ToString()),
                            SinCargo = decimal.Parse(dr["SinCargo"].ToString()),
                            Costo = decimal.Parse(dr["Costo"].ToString()),
                            IdEntrada = int.Parse(dr["IdEntrada"].ToString()),
                            Estatus = int.Parse(dr["Estatus"].ToString()),
                            UsuarioRegistra = dr["UsuarioRegistra"].ToString(),
                            FechaRegistro = dr["FechaRegistro"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return lista;
        }

        public void UpdateDetallesEntrada(UpdateDetallesEntradaModel detallesEntrada)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = System.Data.SqlDbType.Int, Value = detallesEntrada.Id });
            parametros.Add(new SqlParameter { ParameterName = "@IdEntrada", SqlDbType = System.Data.SqlDbType.Int, Value = detallesEntrada.IdEntrada});
            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = System.Data.SqlDbType.VarChar, Value = detallesEntrada.Insumo});
            parametros.Add(new SqlParameter { ParameterName = "@Cantidad", SqlDbType = System.Data.SqlDbType.Decimal, Value = detallesEntrada.Cantidad});
            parametros.Add(new SqlParameter { ParameterName = "@Costo", SqlDbType = System.Data.SqlDbType.Decimal, Value = detallesEntrada.Costo});
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = System.Data.SqlDbType.Int, Value = detallesEntrada.UsuarioRegistra });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = System.Data.SqlDbType.Int, Value = detallesEntrada.Estatus });

            try
            {
                DataSet ds = dac.Fill("sp_UpdateDetallesEntrada", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void DeleteDetallesEntrada(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = Id  });

            try
            {
                DataSet ds = dac.Fill("sp_DeleteDetallesEntrada", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }


    }


}