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
    public class ExistenciasService
    {
        private string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

        public ExistenciasService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
            connection = settings.ConnectionString;
            _webHostEnvironment = webHostEnvironment;
        }

        public void InsertExistencia(InsertExistenciaModel existencia)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "insumo", SqlDbType = SqlDbType.VarChar, Value = existencia.Insumo  });
            parametros.Add(new SqlParameter { ParameterName = "cantidad", SqlDbType = SqlDbType.Decimal, Value = existencia.Cantidad  });
            parametros.Add(new SqlParameter { ParameterName = "idAlmacen", SqlDbType = SqlDbType.Int, Value = existencia.IdAlmacen  });
            parametros.Add(new SqlParameter { ParameterName = "UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = existencia.IdUsuario  });

            try
            {
                dac.ExecuteNonQuery("sp_InsertExistencia", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

        public List<GetExistenciasModel> GetExistencias()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            List<GetExistenciasModel> lista = new List<GetExistenciasModel>();

            try
            {
                DataSet ds = dac.Fill("sp_GetExistencias", parametros);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new GetExistenciasModel
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Fecha = dr["Fecha"].ToString(),
                            Insumo = dr["Insumo"].ToString(),
                            DescripcionInsumo = dr["DescripcionInsumo"].ToString(),
                            Cantidad = decimal.Parse(dr["cantidad"].ToString()),
                            IdAlmacen = int.Parse(dr["IdAlmacen"].ToString()),
                            Almacen = dr["almacen"].ToString(),
                            Estatus = int.Parse(dr["Estatus"].ToString()),
                            FechaRegistro = dr["FechaRegistro"].ToString(),
                            IdUsuario = int.Parse(dr["UsuarioRegistra"].ToString()),
                            Usuario = dr["Usuario"].ToString(),

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

        public void UpdateExistencia(UpdateExistenciaModel existencia)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.VarChar, Value = existencia.Id  });
            parametros.Add(new SqlParameter { ParameterName = "cantidad", SqlDbType = SqlDbType.Decimal, Value = existencia.Cantidad  });
            parametros.Add(new SqlParameter { ParameterName = "idAlmacen", SqlDbType = SqlDbType.Int, Value = existencia.IdAlmacen  });
            parametros.Add(new SqlParameter { ParameterName = "UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = existencia.IdUsuario  });
            parametros.Add(new SqlParameter { ParameterName = "Estatus", SqlDbType = SqlDbType.Int, Value = existencia.Estatus  });


            try
            {
                dac.ExecuteNonQuery("sp_UpdateExistencia", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void DeleteExistencia(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = Id  });

            try
            {
                DataSet ds = dac.Fill("sp_DeleteExistencia", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

    }
}