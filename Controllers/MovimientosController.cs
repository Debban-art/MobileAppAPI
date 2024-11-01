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
    public class MovimientoController: ControllerBase
    {
        private readonly MovimientoService _movimientoService;
        private readonly ILogger<MovimientoController> _logger;

        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public MovimientoController(MovimientoService movimientoservice, ILogger<MovimientoController> logger, IJwtAuthenticationService authService)
        {
            _movimientoService = movimientoservice;
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("InsertMovimiento")]
        public IActionResult InsertMovimiento([FromBody] InsertMovimientoModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Movimiento registrado con éxito" ;
                _movimientoService.InsertMovimiento(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetMovimientos")]
        public IActionResult GetAllMovimientos()
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Movimientos obtenidos con éxito";
                var resultado = _movimientoService.GetMovimientos();
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

        [HttpGet("ExportarExcelMovimientos")]
        public IActionResult ExportarExcel()
        {
            var data = GetMovimientosData();

            XLWorkbook wb = new XLWorkbook();
            MemoryStream ms = new MemoryStream();

            wb.AddWorksheet(data, "Movimientos").Columns().AdjustToContents();
            wb.SaveAs(ms);


            return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","Movimientos.xlsx");
        }

        private DataTable GetMovimientosData()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Movimientos";
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Id Tipo-Movimiento", typeof(int));
            dt.Columns.Add("Tipo-Movimiento", typeof(string));
            dt.Columns.Add("IdAlmacen", typeof(int));
            dt.Columns.Add("Almacen", typeof(string));
            dt.Columns.Add("Fecha", typeof(string));
            dt.Columns.Add("Estatus", typeof(int));
            dt.Columns.Add("Id Usuario", typeof(string));
            dt.Columns.Add("Usuario", typeof(string));


            List<GetMovimientosModel> lista = this._movimientoService.GetMovimientos();
            if (lista.Count > 0)
            {
                foreach(GetMovimientosModel movimiento in lista)
                {
                    dt.Rows.Add(movimiento.Id, movimiento.IdTipoMovimiento, movimiento.TipoMovimiento, movimiento.IdAlmacen,movimiento.Almacen, movimiento.Fecha, movimiento.Estatus,movimiento.IdUsuario, movimiento.Usuario);
                }
            }
            return dt;
        }

        [HttpPut("UpdateMovimiento")]
        public IActionResult UpdateMovimiento([FromBody] UpdateMovimientoModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Movimiento actualizado con éxito" ;
                _movimientoService.UpdateMovimiento(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteMovimiento")]
        public IActionResult DeleteMovimiento([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Movimiento eliminado con éxito" ;
                _movimientoService.DeleteMovimiento(Id);
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