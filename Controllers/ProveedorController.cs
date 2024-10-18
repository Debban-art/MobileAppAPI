using System;
using Microsoft.AspNetCore.Mvc;
using reportesApi.Services;
using reportesApi.Utilities;
using reportesApi.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using reportesApi.Helpers;
using Microsoft.AspNetCore.Hosting;
using ClosedXML.Excel;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace reportesApi.Controllers
{
    [Route("api")]
    public class ProveedorController: ControllerBase
    {
        private readonly ProveedorService _proveedorService;
        private readonly ILogger<ProveedorController> _logger;

        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public ProveedorController(ProveedorService proveedorservice, ILogger<ProveedorController> logger, IJwtAuthenticationService authService)
        {
            _proveedorService = proveedorservice;
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("InsertProveedor")]
        public IActionResult InsertProveedor([FromBody] InsertProveedorModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Proveedor registrado con éxito" ;
                _proveedorService.InsertProveedor(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetAllProveedores")]
        public IActionResult GetAllProveedores()
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Proveedores obtenidos con éxito";
                var resultado = _proveedorService.GetAllProveedores();
                objectResponse.response = resultado;
            }
            catch(Exception ex)
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

            wb.AddWorksheet(data, "Proveedores").Columns().AdjustToContents();
            wb.SaveAs(ms);


            return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","Proveedores.xlsx");
        }

        private DataTable GetProveedoresData()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Proveedores";
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Dirección", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("RFC", typeof(string));
            dt.Columns.Add("Plazo Pago", typeof(int));
            dt.Columns.Add("Porcentaje Retención", typeof(float));
            dt.Columns.Add("Estatus", typeof(int));
            dt.Columns.Add("Usuario Registra", typeof(string));
            dt.Columns.Add("Fecha Registro", typeof(string));


            List<GetProveedoresModel> lista = this._proveedorService.GetAllProveedores();
            if (lista.Count > 0)
            {
                foreach(GetProveedoresModel proveedor in lista)
                {
                    dt.Rows.Add(proveedor.Proveedor_Id, proveedor.Proveedor_Nombre, proveedor.Proveedor_Direccion, proveedor.Proveedor_Email,proveedor.Proveedor_RFC, proveedor.Proveedor_PlazoPago, proveedor.Proveedor_PorcentajeRetencion,proveedor.Proveedor_Estatus, proveedor.Usuario_Registra, proveedor.Fecha_Registro);
                }
            }
            return dt;
        }

        [HttpPut("UpdateProveedor")]
        public IActionResult UpdateProveedor([FromBody] UpdateProveedorModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Proveedor actualizado con éxito" ;
                _proveedorService.UpdateProveedor(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteProveedor")]
        public IActionResult DeleteProveedor([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Proveedor eliminado con éxito" ;
                _proveedorService.DeleteProveedor(Id);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }
    }
}