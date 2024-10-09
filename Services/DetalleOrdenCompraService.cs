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
    public class DetalleOrdenCompraService
    {
        private string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

        public DetalleOrdenCompraService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
            connection = settings.ConnectionString;
            _webHostEnvironment = webHostEnvironment;
        }

        public void InsertDetalleOrdenCompra(InsertDetalleOrdenCompraModel detalleOrdenCompra)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter {ParameterName = "IdOrdenCompra",SqlDbType = SqlDbType.Int, Value = detalleOrdenCompra.IdOrdenCompra});
            parametros.Add(new SqlParameter {ParameterName = "Insumo", SqlDbType = SqlDbType.VarChar, Value = detalleOrdenCompra.Insumo});
            parametros.Add(new SqlParameter {ParameterName = "Cantidad", SqlDbType = SqlDbType.Decimal, Value = detalleOrdenCompra.Cantidad});
            parametros.Add(new SqlParameter {ParameterName = "CantidadRecibida", SqlDbType = SqlDbType.Decimal, Value = detalleOrdenCompra.CantidadRecibida});
            parametros.Add(new SqlParameter {ParameterName = "Costo", SqlDbType = SqlDbType.Decimal, Value = detalleOrdenCompra.Costo});
            parametros.Add(new SqlParameter {ParameterName = "UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = detalleOrdenCompra.UsuarioRegistra});

            try
            {
                dac.ExecuteNonQuery("sp_InsertDetalleOrdenCompra", parametros);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public List<GetDetallesOrdenCompraModel> GetDetalleOrdenCompra(int IdOrdenCompra)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            List<GetDetallesOrdenCompraModel> lista = new List<GetDetallesOrdenCompraModel>();
            parametros = new ArrayList();

            parametros.Add(new SqlParameter {ParameterName = "IdOrdenCompra", SqlDbType = SqlDbType.Int, Value = IdOrdenCompra});

            try
            {
                DataSet ds = dac.Fill("sp_GetDetalleOrdenCompra", parametros);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new GetDetallesOrdenCompraModel{
                            Id = int.Parse(dr["Id"].ToString()),
                            IdOrdenCompra = int.Parse(dr["IdOrdenCompra"].ToString()),
                            Insumo = dr["Insumo"].ToString(),
                            Cantidad = float.Parse(dr["Cantidad"].ToString()),
                            CantidadRecibida = float.Parse(dr["CantidadRecibida"].ToString()),
                            Costo = float.Parse(dr["Costo"].ToString()),
                            CostoRenglon = float.Parse(dr["CostoRenglon"].ToString()),
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

        public void UpdateDetalleOrdenCompra(UpdateDetalleOrdenCompraModel detalleOrdenCompra)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter{ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = detalleOrdenCompra.Id});
            parametros.Add(new SqlParameter {ParameterName = "IdOrdenCompra", SqlDbType = SqlDbType.Int, Value = detalleOrdenCompra.IdOrdenCompra});
            parametros.Add(new SqlParameter {ParameterName = "Insumo", SqlDbType = SqlDbType.VarChar, Value = detalleOrdenCompra.Insumo});
            parametros.Add(new SqlParameter {ParameterName = "Cantidad", SqlDbType = SqlDbType.Decimal, Value = detalleOrdenCompra.Cantidad});
            parametros.Add(new SqlParameter {ParameterName = "CantidadRecibida", SqlDbType = SqlDbType.Decimal, Value = detalleOrdenCompra.CantidadRecibida});
            parametros.Add(new SqlParameter {ParameterName = "Costo", SqlDbType = SqlDbType.Decimal, Value = detalleOrdenCompra.Costo});
            parametros.Add(new SqlParameter {ParameterName = "Estatus", SqlDbType = SqlDbType.Int, Value = detalleOrdenCompra.Estatus});
            parametros.Add(new SqlParameter {ParameterName = "UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = detalleOrdenCompra.UsuarioRegistra});

            try
            {
                dac.ExecuteNonQuery("sp_UpdateDetalleOrdenCompra", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void DeleteDetalleOrdenCompra(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter{ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = Id});

            try
            {
                dac.ExecuteNonQuery("sp_DeleteDetalleOrdenCompra", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}