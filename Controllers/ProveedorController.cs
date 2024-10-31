using System;
using Microsoft.AspNetCore.Mvc;
using reportesApi.Services;
using reportesApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using reportesApi.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using reportesApi.Helpers;
using Newtonsoft.Json;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using System.Collections.Generic;
using ClosedXML.Excel;


namespace reportesApi.Controllers
{
    [Route("api")]
    public class ProveedorController : ControllerBase
    {
         private readonly ProveedorService _ProveedorService;
        private readonly ILogger<ProveedorController> _logger;
  
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public ProveedorController(ProveedorService ProveedorService, ILogger<ProveedorController> logger, IJwtAuthenticationService authService) {
            _ProveedorService = ProveedorService;
            _logger = logger;
       
            _authService = authService;
            // Configura la ruta base donde se almacenan los archivos.
            // Asegúrate de ajustar la ruta según tu estructura de directorios.

            
            
        }

        [HttpPost("InsertProveedor")]
        public IActionResult InsertProveedor([FromBody] InsertProveedorModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Proveedor registrado correctamente";
                _ProveedorService.InsertProveedor(req);

            }

            catch (System.Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetProveedores")]
        public IActionResult GetProveedor()
        {
            var objectResponse = Helper.GetStructResponse();

            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Proveedores cargados con exito";
                var resultado = _ProveedorService.GetProveedores();
                
                

                // Llamando a la función y recibiendo los dos valores.
                
                 objectResponse.response = resultado;
            }

            catch (System.Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

         [HttpGet("ExportarExcelProveedores")]
        public IActionResult ExportarExcel()
        {
            var data = GetProveedoresData();

            XLWorkbook wb = new XLWorkbook();
            MemoryStream ms = new MemoryStream();

            wb.AddWorksheet(data, "ProveedorService").Columns().AdjustToContents();
            wb.SaveAs(ms);

            return File(ms.ToArray(),"aplication/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Proveedores.xlsx");
        }

        private DataTable GetProveedoresData()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Proveedores";
            dt.Columns.Add("Id",typeof(int));
            dt.Columns.Add("Nombre",typeof(string));
            dt.Columns.Add("Direccion",typeof(string));
            dt.Columns.Add("Email",typeof(string));
            dt.Columns.Add("RFC",typeof(string));
            dt.Columns.Add("PlazoPago",typeof(int));
            dt.Columns.Add("PorcentajeRetencion",typeof(float));
            dt.Columns.Add("Estatus",typeof(int));
            dt.Columns.Add("UsuarioRegistra",typeof(string));
            dt.Columns.Add("FechaRegistro",typeof(string));

            List <GetProveedorModel> lista = this._ProveedorService.GetProveedores();
            if (lista.Count > 0)
            {
                foreach (GetProveedorModel Proveedor in lista)
                {
                    dt.Rows.Add(Proveedor.Id, Proveedor.Nombre, Proveedor.Direccion, Proveedor.Email, Proveedor.RFC, Proveedor.PlazoPago, 
                    Proveedor.PorcentajeRetencion ,Proveedor.Estatus,  Proveedor.UsuarioRegistra, Proveedor.FechaRegistro);
                } 
            }
            return dt;
        }

        [HttpPut("UpdateProveedor")]
        public IActionResult UpdateProveedor([FromBody] UpdateProveedorModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Proveedor actualizado correctamente";
                _ProveedorService.UpdateProveedor(req);

            }

            catch (System.Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

         [HttpDelete("DeleteProveedor")]
        public IActionResult DeleteProveedor([FromQuery] int Id )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Proveedor eliminado correctamente";
                _ProveedorService.DeleteProveedor(Id);

            }

            catch (System.Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }   
            
    }
}
