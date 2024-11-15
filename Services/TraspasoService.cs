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
    public class TraspasoService
    {
        private string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

        public TraspasoService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
            connection = settings.ConnectionString;
            _webHostEnvironment = webHostEnvironment;
        }

        public int InsertTraspaso(InsertTraspasoModel traspaso)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            int idTraspaso=0;
            
            parametros.Add(new SqlParameter { ParameterName = "IdAlmacenEntrada", SqlDbType = SqlDbType.Int, Value = traspaso.IdAlmacenEntrada  });
            parametros.Add(new SqlParameter { ParameterName = "IdAlmacenSalida", SqlDbType = SqlDbType.Int, Value = traspaso.IdAlmacenSalida  });
            parametros.Add(new SqlParameter { ParameterName = "IdUsuario", SqlDbType = SqlDbType.Int, Value = traspaso.IdUsuarioRegistra  });

            try
            {
                DataSet ds = dac.Fill("sp_InsertTraspaso", parametros);
                idTraspaso = int.Parse(ds.Tables[0].Rows[0]["IdTraspaso"].ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            return idTraspaso;
            
        }

        public List<GetTraspasoModel> GetTraspasos(GetTraspasoRequest req)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "IdAlmacen", SqlDbType = SqlDbType.Int, Value = req.IdAlmacen  });
            parametros.Add(new SqlParameter { ParameterName = "FechaInicio", SqlDbType = SqlDbType.Date, Value = req.FechaInicio  });
            parametros.Add(new SqlParameter { ParameterName = "FechaFin", SqlDbType = SqlDbType.Date, Value = req.FechaFin  });
            parametros.Add(new SqlParameter { ParameterName = "TipoTraspaso", SqlDbType = SqlDbType.Int, Value = req.TipoTraspaso  });
            List<GetTraspasoModel> lista = new List<GetTraspasoModel>();

            try
            {
                DataSet ds = dac.Fill("sp_GetTraspasos", parametros);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new GetTraspasoModel
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            IdAlmacenEntrada = int.Parse(dr["IdAlmacenEntrada"].ToString()),
                            IdAlmacenSalida = int.Parse(dr["IdAlmacenSalida"].ToString()),
                            AlmacenEntrada = dr["AlmacenEntrada"].ToString(),
                            AlmacenSalida = dr["AlmacenSalida"].ToString(),
                            IdEstatusTraspaso = int.Parse(dr["IdEstatusTraspaso"].ToString()),
                            EstatusTraspaso = dr["EstatusTraspaso"].ToString(),
                            FechaInicio = dr["FechaInicio"].ToString(),
                            FechaSalida = dr["FechaSalida"].ToString(),
                            FechaEntrega = dr["FechaEntrega"].ToString(),
                            FechaRegistro = dr["FechaRegistro"].ToString(),
                            Estatus = int.Parse(dr["Estatus"].ToString()),
                            IdUsuarioRegistra = int.Parse(dr["UsuarioRegistra"].ToString()),
                            UsuarioRegistra = dr["Usuario"].ToString(),
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

        public void UpdateTraspaso(UpdateTraspasoModel traspaso)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = traspaso.Id  });
            parametros.Add(new SqlParameter { ParameterName = "IdAlmacenEntrada", SqlDbType = SqlDbType.Int, Value = traspaso.IdAlmacenEntrada  });
            parametros.Add(new SqlParameter { ParameterName = "IdAlmacenSalida", SqlDbType = SqlDbType.Int, Value = traspaso.IdAlmacenSalida  });
            parametros.Add(new SqlParameter { ParameterName = "IdUsuario", SqlDbType = SqlDbType.Int, Value = traspaso.IdUsuarioRegistra  });
            parametros.Add(new SqlParameter { ParameterName = "Estatus", SqlDbType = SqlDbType.Int, Value = traspaso.Estatus  });

            try
            {
                dac.ExecuteNonQuery("sp_UpdateTraspaso", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void UpdateTraspasoEstatus(UpdateTraspasoEstatusModel traspaso)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = traspaso.Id  });
            parametros.Add(new SqlParameter { ParameterName = "IdEstatusTraspaso", SqlDbType = SqlDbType.Int, Value = traspaso.IdEstatusTraspaso  });
            parametros.Add(new SqlParameter { ParameterName = "IdUsuario", SqlDbType = SqlDbType.Int, Value = traspaso.IdUsuarioRegistra  });
            parametros.Add(new SqlParameter { ParameterName = "Estatus", SqlDbType = SqlDbType.Int, Value = traspaso.Estatus  });

            try
            {
                dac.ExecuteNonQuery("sp_UpdateTraspaso", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void DeleteTraspaso(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = Id  });

            try
            {
                DataSet ds = dac.Fill("sp_DeleteTraspaso", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }


    }


}