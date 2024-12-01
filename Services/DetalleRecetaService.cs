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
    public class DetalleRecetaService
    {
        private string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

        public DetalleRecetaService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
            connection = settings.ConnectionString;
            _webHostEnvironment = webHostEnvironment;
        }

        public void InsertDetalleReceta(InsertDetalleRecetaModel detalleReceta)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter {ParameterName = "IdReceta",SqlDbType = SqlDbType.Int, Value = detalleReceta.IdReceta});
            parametros.Add(new SqlParameter {ParameterName = "Insumo", SqlDbType = SqlDbType.VarChar, Value = detalleReceta.Insumo});
            parametros.Add(new SqlParameter {ParameterName = "Cantidad", SqlDbType = SqlDbType.Decimal, Value = detalleReceta.Cantidad});
            parametros.Add(new SqlParameter {ParameterName = "UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = detalleReceta.UsuarioRegistra});

            try
            {
                dac.ExecuteNonQuery("sp_InsertDetalleReceta", parametros);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public List<GetDetallesRecetaModel> GetDetallesReceta(int IdReceta)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            List<GetDetallesRecetaModel> lista = new List<GetDetallesRecetaModel>();
            parametros = new ArrayList();

            parametros.Add(new SqlParameter {ParameterName = "IdReceta", SqlDbType = SqlDbType.Int, Value = IdReceta});

            try
            {
                DataSet ds = dac.Fill("sp_GetDetallesRecetaById", parametros);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new GetDetallesRecetaModel{
                            Id = int.Parse(dr["Id"].ToString()),
                            IdReceta = int.Parse(dr["IdReceta"].ToString()),
                            Receta = dr["Receta"].ToString(),
                            CodigoInsumo = dr["CodigoInsumo"].ToString(),
                            Insumo = dr["Insumo"].ToString(),
                            Cantidad = float.Parse(dr["Cantidad"].ToString()),
                            UsuarioRegistra = dr["UsuarioRegistra"].ToString(),
                            FechaRegistro = dr["FechaRegistro"].ToString(),
                            Estatus = int.Parse(dr["Estatus"].ToString())
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

        public void UpdateDetalleReceta(UpdateDetalleRecetaModel detalleReceta)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter{ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = detalleReceta.Id});
            parametros.Add(new SqlParameter {ParameterName = "Insumo", SqlDbType = SqlDbType.VarChar, Value = detalleReceta.Insumo});
            parametros.Add(new SqlParameter {ParameterName = "Cantidad", SqlDbType = SqlDbType.Decimal, Value = detalleReceta.Cantidad});
            parametros.Add(new SqlParameter {ParameterName = "Estatus", SqlDbType = SqlDbType.Int, Value = detalleReceta.Estatus});
            parametros.Add(new SqlParameter {ParameterName = "UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = detalleReceta.UsuarioRegistra});

            try
            {
                dac.ExecuteNonQuery("sp_UpdateDetalleReceta", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void DeleteDetalleReceta(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter{ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = Id});

            try
            {
                dac.ExecuteNonQuery("sp_DeleteDetalleReceta", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}