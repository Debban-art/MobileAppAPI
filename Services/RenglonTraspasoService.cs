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
    public class RenglonTraspasoService
    {
        private string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

        public RenglonTraspasoService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
            connection = settings.ConnectionString;
            _webHostEnvironment = webHostEnvironment;
        }

        public void InsertRenglonTraspaso(InsertRenglonTraspasoModel renglonTraspaso)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "IdTraspaso", SqlDbType = SqlDbType.Int, Value = renglonTraspaso.IdTraspaso});
            parametros.Add(new SqlParameter { ParameterName = "Insumo", SqlDbType = SqlDbType.VarChar, Value = renglonTraspaso.Insumo});
            parametros.Add(new SqlParameter { ParameterName = "Cantidad", SqlDbType = SqlDbType.Decimal, Value = renglonTraspaso.Cantidad});
            parametros.Add(new SqlParameter { ParameterName = "IdUsuario", SqlDbType = SqlDbType.Int, Value = renglonTraspaso.IdUsuarioRegistra  });

            try
            {
                dac.ExecuteNonQuery("sp_insert_renglonTraspaso", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public List<GetRenglonesTraspasoModel> GetRenglonesTraspaso(int IdTraspaso)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "IdTraspaso", SqlDbType = SqlDbType.Int, Value = IdTraspaso  });
            List<GetRenglonesTraspasoModel> lista = new List<GetRenglonesTraspasoModel>();

            try
            {
                DataSet ds = dac.Fill("sp_get_renglonesTraspaso", parametros);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new GetRenglonesTraspasoModel
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            IdTraspaso = int.Parse(dr["IdTraspaso"].ToString()),
                            Insumo = dr["Insumo"].ToString(),
                            DescripcionInsumo = dr["DescripcionInsumo"].ToString(),
                            Cantidad = decimal.Parse(dr["Cantidad"].ToString()),
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
         public List<GetInsumosTraspasoModel> GetInsumosTraspasoEntrada(int IdAlmacen, string FechaInicio, string FechaFin)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "IdAlmacen", SqlDbType = SqlDbType.Int, Value = IdAlmacen  });
            parametros.Add(new SqlParameter { ParameterName = "FechaInicio", SqlDbType = SqlDbType.Date, Value = FechaInicio  });
            parametros.Add(new SqlParameter { ParameterName = "FechaFin", SqlDbType = SqlDbType.Date, Value = FechaFin  });
            List<GetInsumosTraspasoModel> lista = new List<GetInsumosTraspasoModel>();

            try
            {
                DataSet ds = dac.Fill("sp_GetReporteInsumosTraspasosEntrada", parametros);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new GetInsumosTraspasoModel
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Insumo = dr["Insumo"].ToString(),
                            Cantidad = float.Parse(dr["Cantidad"].ToString()),
                            FechaMovimiento = dr["FechaEntrega"].ToString(),
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
         public List<GetInsumosTraspasoModel> GetInsumosTraspasoSalida(int IdAlmacen, string FechaInicio, string FechaFin)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "IdAlmacen", SqlDbType = SqlDbType.Int, Value = IdAlmacen  });
            parametros.Add(new SqlParameter { ParameterName = "FechaInicio", SqlDbType = SqlDbType.Date, Value = FechaInicio  });
            parametros.Add(new SqlParameter { ParameterName = "FechaFin", SqlDbType = SqlDbType.Date, Value = FechaFin  });
            List<GetInsumosTraspasoModel> lista = new List<GetInsumosTraspasoModel>();

            try
            {
                DataSet ds = dac.Fill("sp_GetReporteInsumosTraspasosSalida", parametros);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new GetInsumosTraspasoModel
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Insumo = dr["Insumo"].ToString(),
                            Cantidad = float.Parse(dr["Cantidad"].ToString()),
                            FechaMovimiento = dr["FechaSalida"].ToString(),
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

        
        public void UpdateRenglonTraspaso(UpdateRenglonTraspasoModel renglonTraspaso)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = renglonTraspaso.Id  });
            parametros.Add(new SqlParameter { ParameterName = "Insumo", SqlDbType = SqlDbType.VarChar, Value = renglonTraspaso.Insumo});
            parametros.Add(new SqlParameter { ParameterName = "Cantidad", SqlDbType = SqlDbType.Decimal, Value = renglonTraspaso.Cantidad});
            parametros.Add(new SqlParameter { ParameterName = "IdUsuario", SqlDbType = SqlDbType.Int, Value = renglonTraspaso.IdUsuarioRegistra  });
            parametros.Add(new SqlParameter { ParameterName = "Estatus", SqlDbType = SqlDbType.Int, Value = renglonTraspaso.Estatus  });

            try
            {
                dac.ExecuteNonQuery("sp_update_renglonTraspaso", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void DeleteRenglonTraspaso(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = Id  });

            try
            {
                DataSet ds = dac.Fill("sp_delete_renglonTraspaso", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

    }


}