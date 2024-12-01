using System.Collections;
using System.Data;
using System.Data.SqlClient;
using reportesApi.DataContext;
using reportesApi.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Linq;

namespace reportesApi.Services
{
    public class RecetaService
    {
        private string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

        public RecetaService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
            connection = settings.ConnectionString;
            _webHostEnvironment = webHostEnvironment;
        }

        public int InsertReceta(InsertRecetaModel receta)
        {
            int IdReceta;
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Nombre", SqlDbType = SqlDbType.VarChar, Value = receta.Nombre  });
            parametros.Add(new SqlParameter { ParameterName = "UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = receta.Usuario_Registra  });

            try
            {
                DataSet ds = dac.Fill("sp_InsertReceta", parametros);
                IdReceta = ds.Tables[0].AsEnumerable().Select(dataRow => int.Parse(dataRow["IdReceta"].ToString())).ToList()[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return IdReceta;
        }

        public List<GetRecetasModel> GetRecetas()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            List<GetRecetasModel> lista = new List<GetRecetasModel>();

            try
            {
                DataSet ds = dac.Fill("sp_GetRecetas", parametros);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new GetRecetasModel
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Nombre = dr["Nombre"].ToString(),
                            Fecha_Creacion = dr["FechaCreacion"].ToString(),
                            Estatus = int.Parse(dr["Estatus"].ToString()),
                            Usuario_Registra = dr["UsuarioRegistra"].ToString(),
                            Fecha_Registro = dr["FechaRegistro"].ToString()
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

        public List<GetReporteRecetasModel> GetReporteRecetas(string FechaInicio, string FechaFin)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter{ParameterName="FechaInicio", SqlDbType = SqlDbType.Date, Value=FechaInicio});
            parametros.Add(new SqlParameter{ParameterName="FechaFin", SqlDbType = SqlDbType.Date, Value=FechaFin});
            List<GetReporteRecetasModel> lista = new List<GetReporteRecetasModel>();

            try
            {
                DataSet ds = dac.Fill("sp_GetReporteRecetas", parametros);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new GetReporteRecetasModel
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Nombre = dr["Nombre"].ToString(),
                            Insumo = dr["Insumo"].ToString(),
                            Cantidad = float.Parse(dr["Cantidad"].ToString()),
                            Fecha_Creacion = dr["FechaCreacion"].ToString(),
                            Estatus = int.Parse(dr["Estatus"].ToString()),
                            Usuario_Registra = dr["UsuarioRegistra"].ToString(),
                            Fecha_Registro = dr["FechaRegistro"].ToString()
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

        public void UpdateReceta(UpdateRecetaModel receta)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.VarChar, Value = receta.Id  });
            parametros.Add(new SqlParameter { ParameterName = "Nombre", SqlDbType = SqlDbType.VarChar, Value = receta.Nombre  });
            parametros.Add(new SqlParameter { ParameterName = "UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = receta.Usuario_Registra  });
            parametros.Add(new SqlParameter { ParameterName = "Estatus", SqlDbType = SqlDbType.Int, Value = receta.Estatus  });


            try
            {
                dac.ExecuteNonQuery("sp_UpdateReceta", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void DeleteReceta(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = Id  });

            try
            {
                DataSet ds = dac.Fill("sp_DeleteReceta", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

    }
}