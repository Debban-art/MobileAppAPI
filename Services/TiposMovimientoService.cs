using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using reportesApi.DataContext;
using reportesApi.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;

namespace reportesApi.Services
{
    public class TiposMovimientoService
    {
        private string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

        public TiposMovimientoService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
            connection = settings.ConnectionString;
            _webHostEnvironment = webHostEnvironment;
        }

        public void InsertTipoMovimiento(InsertTipoMovimientoModel tipoMovimiento)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Nombre", SqlDbType = SqlDbType.VarChar, Value = tipoMovimiento.Nombre  });
            parametros.Add(new SqlParameter { ParameterName = "EntradaSalida", SqlDbType = SqlDbType.VarChar, Value = tipoMovimiento.EntradaSalida  });
            parametros.Add(new SqlParameter { ParameterName = "UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = tipoMovimiento.IdUsuarioRegistra  });

            try
            {
                DataSet ds = dac.Fill("sp_InsertTipoMovimiento", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public List<GetTiposMovimientoModel> GetTiposMovimientos()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            List<GetTiposMovimientoModel> lista = new List<GetTiposMovimientoModel>();

            try
            {
                DataSet ds = dac.Fill("sp_GetTiposMovimiento", parametros);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new GetTiposMovimientoModel
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Nombre = dr["Nombre"].ToString(),
                            EntradaSalida = int.Parse(dr["EntradaSalida"].ToString()),
                            Estatus = int.Parse(dr["Estatus"].ToString()),
                            IdUsuarioRegistra = int.Parse(dr["UsuarioRegistra"].ToString()),
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

        public void UpdateTipoMovimiento(UpdateTipoMovimientoModel tipoMovimiento)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = tipoMovimiento.Id  });
            parametros.Add(new SqlParameter { ParameterName = "Nombre", SqlDbType = SqlDbType.VarChar, Value = tipoMovimiento.Nombre  });
            parametros.Add(new SqlParameter { ParameterName = "EntradaSalida", SqlDbType = SqlDbType.VarChar, Value = tipoMovimiento.EntradaSalida  });
            parametros.Add(new SqlParameter { ParameterName = "UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = tipoMovimiento.IdUsuarioRegistra  });
            parametros.Add(new SqlParameter { ParameterName = "Estatus", SqlDbType = SqlDbType.Int, Value = tipoMovimiento.Estatus  });

            try
            {
                DataSet ds = dac.Fill("sp_UpdateTipoMovimiento", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void DeleteTipoMovimiento(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = Id  });

            try
            {
                DataSet ds = dac.Fill("sp_DeleteTipoMovimiento", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }


    }


}