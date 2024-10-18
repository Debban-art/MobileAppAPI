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
    public class AlmacenController: ControllerBase
    {
        private readonly AlmacenService _almacenService;
        private readonly ILogger<AlmacenController> _logger;

        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public AlmacenController(AlmacenService almacenservice, ILogger<AlmacenController> logger, IJwtAuthenticationService authService)
        {
            _almacenService = almacenservice;
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("InsertAlmacen")]
        public IActionResult InsertAlmacen([FromBody] InsertAlmacenModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Almacen registrado con éxito" ;
                _almacenService.InsertAlmacen(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetAllAlmacenes")]
        public IActionResult GetAllAlmacenes()
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Almacenes obtenidos con éxito";
                var resultado = _almacenService.GetAllAlmacenes();
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

        [HttpGet("ExportarExcelAlmacenes")]
        public IActionResult ExportarExcel()
        {
            var data = GetAlmacenesData();

            XLWorkbook wb = new XLWorkbook();
            MemoryStream ms = new MemoryStream();

            wb.AddWorksheet(data, "Almacenes").Columns().AdjustToContents();
            wb.SaveAs(ms);


            return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","Almacenes.xlsx");
        }

        private DataTable GetAlmacenesData()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Almacenes";
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Dirección", typeof(string));
            dt.Columns.Add("Estatus", typeof(int));
            dt.Columns.Add("Usuario Registra", typeof(string));
            dt.Columns.Add("Fecha Registro", typeof(string));


            List<GetAlmacenesModel> lista = this._almacenService.GetAllAlmacenes();
            if (lista.Count > 0)
            {
                foreach(GetAlmacenesModel almacen in lista)
                {
                    dt.Rows.Add(almacen.Almacen_Id, almacen.Almacen_Nombre, almacen.Almacen_Direccion, almacen.Almacen_Estatus, almacen.Usuario_Registra, almacen.Fecha_Registro);
                }
            }
            return dt;
        }


        [HttpPut("UpdateAlmacen")]
        public IActionResult UpdateAlmacen([FromBody] UpdateAlmacenModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Almacen actualizado con éxito" ;
                _almacenService.UpdateAlmacen(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteAlmacen")]
        public IActionResult DeleteAlmacen([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Almacen eliminado con éxito" ;
                _almacenService.DeleteAlmacen(Id);
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