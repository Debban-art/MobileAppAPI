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
    public class InsumoService
    {
        private string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

        public InsumoService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
            connection = settings.ConnectionString;
            _webHostEnvironment = webHostEnvironment;
        }

        public void InsertInsumo(InsertInsumoModel Insumo)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Insumo", SqlDbType = SqlDbType.VarChar, Value = Insumo.Insumo  });
            parametros.Add(new SqlParameter { ParameterName = "DescripcionInsumo", SqlDbType = SqlDbType.VarChar, Value = Insumo.Insumo_Descripcion  });
            parametros.Add(new SqlParameter { ParameterName = "Costo", SqlDbType = SqlDbType.Decimal, Value = Insumo.Insumo_Costo  });
            parametros.Add(new SqlParameter { ParameterName = "UnidadMedida", SqlDbType = SqlDbType.Int, Value = Insumo.Insumo_UnidadMedida  });
            parametros.Add(new SqlParameter { ParameterName = "InsumoUp", SqlDbType = SqlDbType.VarChar, Value = Insumo.Insumo_InsumoUp});
            parametros.Add(new SqlParameter { ParameterName = "UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = Insumo.Usuario_Registra  });

            try
            {
                DataSet ds = dac.Fill("sp_InsertInsumo", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public List<GetInsumosModel> GetAllInsumos()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            List<GetInsumosModel> lista = new List<GetInsumosModel>();

            try
            {
                DataSet ds = dac.Fill("sp_GetAllInsumos", parametros);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new GetInsumosModel
                        {
                            Insumo_Id = int.Parse(dr["Id"].ToString()),
                            Insumo = dr["Insumo"].ToString(),
                            Insumo_Descripcion = dr["DescripcionInsumo"].ToString(),
                            Insumo_Costo = float.Parse(dr["Costo"].ToString()),
                            Insumo_UnidadMedida = int.Parse(dr["UnidadMedida"].ToString()),
                            Insumo_InsumoUp = dr["InsumoUp"].ToString(),
                            Insumo_Estatus = int.Parse(dr["Estatus"].ToString()),
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

        public void UpdateInsumo(UpdateInsumoModel Insumo)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = Insumo.Insumo_Id  });
            parametros.Add(new SqlParameter { ParameterName = "Insumo", SqlDbType = SqlDbType.VarChar, Value = Insumo.Insumo  });
            parametros.Add(new SqlParameter { ParameterName = "DescripcionInsumo", SqlDbType = SqlDbType.VarChar, Value = Insumo.Insumo_Descripcion  });
            parametros.Add(new SqlParameter { ParameterName = "Costo", SqlDbType = SqlDbType.Decimal, Value = Insumo.Insumo_Costo  });
            parametros.Add(new SqlParameter { ParameterName = "UnidadMedida", SqlDbType = SqlDbType.Int, Value = Insumo.Insumo_UnidadMedida  });
            parametros.Add(new SqlParameter { ParameterName = "InsumoUp", SqlDbType = SqlDbType.VarChar, Value = Insumo.Insumo_InsumoUp});
            parametros.Add(new SqlParameter { ParameterName = "Estatus", SqlDbType = SqlDbType.Int, Value = Insumo.Insumo_Estatus});
            parametros.Add(new SqlParameter { ParameterName = "UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = Insumo.Usuario_Registra  });

            try
            {
                DataSet ds = dac.Fill("sp_UpdateInsumo", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void DeleteInsumo(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = Id  });

            try
            {
                DataSet ds = dac.Fill("sp_DeleteInsumo", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }


    }


}