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
    public class TiposMovimientoController: ControllerBase
    {
        private readonly TiposMovimientoService _tiposMovimientoService;
        private readonly ILogger<TiposMovimientoController> _logger;

        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public TiposMovimientoController(TiposMovimientoService tiposMovimientoservice, ILogger<TiposMovimientoController> logger, IJwtAuthenticationService authService)
        {
            _tiposMovimientoService = tiposMovimientoservice;
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("InsertTipoMovimiento")]
        public IActionResult InsertTipoMovimiento([FromBody] InsertTipoMovimientoModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "TipoMovimiento registrado con éxito" ;
                _tiposMovimientoService.InsertTipoMovimiento(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetTiposMovimiento")]
        public IActionResult GetTiposMovimientoes()
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "TipoMovimientoes obtenidos con éxito";
                var resultado = _tiposMovimientoService.GetTiposMovimientos();
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

        [HttpGet("ExportarExcelTiposMovimiento")]
        public IActionResult ExportarExcel()
        {
            var data = GetTipoMovimientoesData();

            XLWorkbook wb = new XLWorkbook();
            MemoryStream ms = new MemoryStream();

            wb.AddWorksheet(data, "TiposMovimiento").Columns().AdjustToContents();
            wb.SaveAs(ms);


            return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","TiposMovimiento.xlsx");
        }

        private DataTable GetTipoMovimientoesData()
        {
            DataTable dt = new DataTable();
            dt.TableName = "TipoMovimientoes";
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Entrada-Salida", typeof(int));
            dt.Columns.Add("Estatus", typeof(int));
            dt.Columns.Add("Usuario Registra", typeof(string));
            dt.Columns.Add("Fecha Registro", typeof(string));


            List<GetTiposMovimientoModel> lista = this._tiposMovimientoService.GetTiposMovimientos();
            if (lista.Count > 0)
            {
                foreach(GetTiposMovimientoModel tipoMovimiento in lista)
                {
                    dt.Rows.Add(tipoMovimiento.Id, tipoMovimiento.Nombre, tipoMovimiento.EntradaSalida ,tipoMovimiento.Estatus, tipoMovimiento.UsuarioRegistra, tipoMovimiento.FechaRegistro);
                }
            }
            return dt;
        }

        [HttpPut("UpdateTipoMovimiento")]
        public IActionResult UpdateTipoMovimiento([FromBody] UpdateTipoMovimientoModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "TipoMovimiento actualizado con éxito" ;
                _tiposMovimientoService.UpdateTipoMovimiento(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteTipoMovimiento")]
        public IActionResult DeleteTipoMovimiento([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "TipoMovimiento eliminado con éxito" ;
                _tiposMovimientoService.DeleteTipoMovimiento(Id);
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