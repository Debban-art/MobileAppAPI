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
    public class EstatusTraspasoService
    {
        private string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

        public EstatusTraspasoService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
            connection = settings.ConnectionString;
            _webHostEnvironment = webHostEnvironment;
        }

        public void InsertEstatusTraspaso(InsertEstatusTraspasoModel estatus)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Nombre", SqlDbType = SqlDbType.VarChar, Value = estatus.Nombre  });
            parametros.Add(new SqlParameter { ParameterName = "IdUsuario", SqlDbType = SqlDbType.Int, Value = estatus.IdUsuarioRegistra  });

            try
            {
                dac.ExecuteNonQuery("sp_InsertEstatusTraspaso", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public List<GetEstatusTraspasoModel> GetEstatusTraspaso()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            List<GetEstatusTraspasoModel> lista = new List<GetEstatusTraspasoModel>();

            try
            {
                DataSet ds = dac.Fill("sp_GetEstatusTraspaso", parametros);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new GetEstatusTraspasoModel
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Nombre = dr["NombreEstatus"].ToString(),
                            Estatus = int.Parse(dr["Estatus"].ToString()),
                            IdUsuarioRegistra = int.Parse(dr["UsuarioRegistra"].ToString()),
                            UsuarioRegistra = dr["Usuario"].ToString(),
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

        public void UpdateEstatusTraspaso(UpdateEstatusTraspasoModel estatus)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = estatus.Id  });
            parametros.Add(new SqlParameter { ParameterName = "Nombre", SqlDbType = SqlDbType.VarChar, Value = estatus.Nombre  });
            parametros.Add(new SqlParameter { ParameterName = "IdUsuario", SqlDbType = SqlDbType.Int, Value = estatus.IdUsuarioRegistra  });
            parametros.Add(new SqlParameter { ParameterName = "Estatus", SqlDbType = SqlDbType.Int, Value = estatus.Estatus  });

            try
            {
                dac.Fill("sp_UpdateEstatusTraspaso", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void DeleteEstatusTraspaso(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = Id  });

            try
            {
                dac.ExecuteNonQuery("sp_DeleteEstatusTraspaso", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }


    }


}