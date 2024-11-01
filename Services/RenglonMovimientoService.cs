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
    public class RenglonMovimientoService
    {
        private string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

        public RenglonMovimientoService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
            connection = settings.ConnectionString;
            _webHostEnvironment = webHostEnvironment;
        }

        public void InsertRenglonMovimiento(InsertRenglonMovimientoModel renglonMovimiento)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "IdMovimiento", SqlDbType = SqlDbType.Int, Value = renglonMovimiento.IdMovimiento});
            parametros.Add(new SqlParameter { ParameterName = "Insumo", SqlDbType = SqlDbType.VarChar, Value = renglonMovimiento.Insumo});
            parametros.Add(new SqlParameter { ParameterName = "Cantidad", SqlDbType = SqlDbType.Decimal, Value = renglonMovimiento.Cantidad});
            parametros.Add(new SqlParameter { ParameterName = "Costo", SqlDbType = SqlDbType.Decimal, Value = renglonMovimiento.Costo});
            parametros.Add(new SqlParameter { ParameterName = "UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = renglonMovimiento.IdUsuarioRegistra  });

            try
            {
                DataSet ds = dac.Fill("sp_InsertRenglonMovimiento", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public List<GetRenglonesMovimientoModel> GetRenglonesMovimiento(int IdMovimiento)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
           parametros.Add(new SqlParameter { ParameterName = "IdMovimiento", SqlDbType = SqlDbType.Int, Value = IdMovimiento  });
            List<GetRenglonesMovimientoModel> lista = new List<GetRenglonesMovimientoModel>();

            try
            {
                DataSet ds = dac.Fill("sp_GetRenglonesMovimiento", parametros);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new GetRenglonesMovimientoModel
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            IdMovimiento = int.Parse(dr["IdMovimiento"].ToString()),
                            Insumo = dr["Insumo"].ToString(),
                            DescripcionInsumo = dr["DescripcionInsumo"].ToString(),
                            Cantidad = decimal.Parse(dr["Cantidad"].ToString()),
                            Costo = decimal.Parse(dr["Costo"].ToString()),
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

        public void UpdateRenglonMovimiento(UpdateRenglonMovimientoModel renglonMovimiento)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = renglonMovimiento.Id  });
            parametros.Add(new SqlParameter { ParameterName = "Insumo", SqlDbType = SqlDbType.VarChar, Value = renglonMovimiento.Insumo});
            parametros.Add(new SqlParameter { ParameterName = "Cantidad", SqlDbType = SqlDbType.Decimal, Value = renglonMovimiento.Cantidad});
            parametros.Add(new SqlParameter { ParameterName = "Costo", SqlDbType = SqlDbType.Decimal, Value = renglonMovimiento.Costo});
            parametros.Add(new SqlParameter { ParameterName = "UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = renglonMovimiento.IdUsuarioRegistra  });
            parametros.Add(new SqlParameter { ParameterName = "Estatus", SqlDbType = SqlDbType.Int, Value = renglonMovimiento.Estatus  });

            try
            {
                DataSet ds = dac.Fill("sp_UpdateRenglonMovimiento", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void DeleteRenglonMovimiento(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = Id  });

            try
            {
                DataSet ds = dac.Fill("sp_DeleteRenglonMovimiento", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }


    }


}