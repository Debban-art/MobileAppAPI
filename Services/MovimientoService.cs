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
    public class MovimientoService
    {
        private string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

        public MovimientoService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
            connection = settings.ConnectionString;
            _webHostEnvironment = webHostEnvironment;
        }

        public void InsertMovimiento(InsertMovimientoModel movimiento)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "IdTipoMovimiento", SqlDbType = SqlDbType.Int, Value = movimiento.IdTipoMovimiento  });
            parametros.Add(new SqlParameter { ParameterName = "IdAlmacen", SqlDbType = SqlDbType.Int, Value = movimiento.IdAlmacen  });
            parametros.Add(new SqlParameter { ParameterName = "IdUsuario", SqlDbType = SqlDbType.Int, Value = movimiento.IdUsuario  });

            try
            {
                DataSet ds = dac.Fill("sp_InsertMovimiento", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public List<GetMovimientosModel> GetMovimientos()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            List<GetMovimientosModel> lista = new List<GetMovimientosModel>();

            try
            {
                DataSet ds = dac.Fill("sp_GetMovimientos", parametros);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new GetMovimientosModel
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            IdTipoMovimiento = int.Parse(dr["IdTipoMovimiento"].ToString()),
                            TipoMovimiento = dr["TipoMovimiento"].ToString(),
                            IdAlmacen = int.Parse(dr["IdAlmacen"].ToString()),
                            Almacen = dr["Almacen"].ToString(),
                            Estatus = int.Parse(dr["Estatus"].ToString()),
                            IdUsuario = int.Parse(dr["IdUsuario"].ToString()),
                            Usuario = dr["Usuario"].ToString(),
                            Fecha = dr["Fecha"].ToString()
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

        public void UpdateMovimiento(UpdateMovimientoModel movimiento)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = movimiento.Id  });
            parametros.Add(new SqlParameter { ParameterName = "IdTipoMovimiento", SqlDbType = SqlDbType.Int, Value = movimiento.IdTipoMovimiento  });
            parametros.Add(new SqlParameter { ParameterName = "IdAlmacen", SqlDbType = SqlDbType.Int, Value = movimiento.IdAlmacen  });
            parametros.Add(new SqlParameter { ParameterName = "IdUsuario", SqlDbType = SqlDbType.Int, Value = movimiento.IdUsuario  });
            parametros.Add(new SqlParameter { ParameterName = "Estatus", SqlDbType = SqlDbType.Int, Value = movimiento.Estatus  });

            try
            {
                DataSet ds = dac.Fill("sp_UpdateMovimiento", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void DeleteMovimiento(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = Id  });

            try
            {
                DataSet ds = dac.Fill("sp_DeleteMovimiento", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }


    }


}