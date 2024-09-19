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
    public class ProveedorService
    {
        private string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();

        public ProveedorService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
            connection = settings.ConnectionString;
            _webHostEnvironment = webHostEnvironment;
        }

        public void InsertProveedor(InsertProveedorModel proveedor)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            
            parametros.Add(new SqlParameter { ParameterName = "Nombre", SqlDbType = SqlDbType.VarChar, Value = proveedor.Proveedor_Nombre  });
            parametros.Add(new SqlParameter { ParameterName = "Direccion", SqlDbType = SqlDbType.VarChar, Value = proveedor.Proveedor_Direccion  });
            parametros.Add(new SqlParameter { ParameterName = "Email", SqlDbType = SqlDbType.VarChar, Value = proveedor.Proveedor_Email  });
            parametros.Add(new SqlParameter { ParameterName = "RFC", SqlDbType = SqlDbType.VarChar, Value = proveedor.Proveedor_RFC  });
            parametros.Add(new SqlParameter { ParameterName = "PlazoPago", SqlDbType = SqlDbType.Int, Value = proveedor.Proveedor_PlazoPago  });
            parametros.Add(new SqlParameter { ParameterName = "PorcentajeRetencion", SqlDbType = SqlDbType.Decimal, Value = proveedor.Proveedor_PorcentajeRetencion  });
            parametros.Add(new SqlParameter { ParameterName = "UsuarioRegistra", SqlDbType = SqlDbType.VarChar, Value = proveedor.Usuario_Registra  });

            try
            {
                DataSet ds = dac.Fill("sp_InsertProveedor", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}