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
    public class EntradaService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

        public EntradaService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
            connection = settings.ConnectionString;

            _webHostEnvironment = webHostEnvironment;
        }

        public List<GetEntradaModel> GetEntradas()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            GetAlumnoModel persona = new GetAlumnoModel();

            List<GetEntradaModel> lista = new List<GetEntradaModel>();
            try
            {
                parametros = new ArrayList();
                DataSet ds = dac.Fill("sp_GetEntradas", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new GetEntradaModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        Proveedor = dataRow["Proveedor"].ToString(),
                        Sucursal = dataRow["Sucursal"].ToString(),
                        Total = decimal.Parse(dataRow["Total"].ToString()),
                        Factura = dataRow["Factura"].ToString(),
                        FechaEntrada = dataRow["FechaEntrada"].ToString(),
                        Estatus = int.Parse(dataRow["Estatus"].ToString()),
                        UsuarioRegistra = dataRow["UsuarioRegistra"].ToString(),
                        FechaRegistro= dataRow["FechaRegistro"].ToString()
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            return lista;
        }

        public int InsertEntrada(InsertEntradaModel Entrada)
        {
            int IdEntrada;
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();

            parametros.Add(new SqlParameter { ParameterName = "@IdProveedor", SqlDbType = System.Data.SqlDbType.Int, Value = Entrada.IdProveedor });
            parametros.Add(new SqlParameter { ParameterName = "@IdSucursal", SqlDbType = System.Data.SqlDbType.Int, Value = Entrada.IdSucursal});
            parametros.Add(new SqlParameter { ParameterName = "@Factura", SqlDbType = System.Data.SqlDbType.VarChar, Value = Entrada.Factura});
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = System.Data.SqlDbType.Int, Value = Entrada.UsuarioRegistra });

            try
            {
                DataSet ds = dac.Fill("sp_InsertEntrada", parametros);
                IdEntrada = ds.Tables[0].AsEnumerable().Select(dataRow => int.Parse(dataRow["IdEntrada"].ToString())).ToList()[0];

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return IdEntrada;
        }

        public void UpdateEntrada(UpdateEntradaModel Entrada)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = System.Data.SqlDbType.Int, Value = Entrada.Id });
            parametros.Add(new SqlParameter { ParameterName = "@IdProveedor", SqlDbType = System.Data.SqlDbType.Int, Value = Entrada.IdProveedor });
            parametros.Add(new SqlParameter { ParameterName = "@IdSucursal", SqlDbType = System.Data.SqlDbType.Int, Value = Entrada.IdSucursal});
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = System.Data.SqlDbType.Int, Value = Entrada.Estatus});
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = System.Data.SqlDbType.Int, Value = Entrada.UsuarioRegistra });

            try
            {
                DataSet ds = dac.Fill("sp_UpdateEntrada", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void DeleteEntrada(int Id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Id", SqlDbType = SqlDbType.Int, Value = Id  });

            try
            {
                DataSet ds = dac.Fill("sp_DeleteEntrada", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
             
    }
}