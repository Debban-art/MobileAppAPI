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
using DocumentFormat.OpenXml.Office2010.Excel;

namespace reportesApi.Controllers
{
    [Route("api")]
    public class RenglonMovimientoController: ControllerBase
    {
        private readonly RenglonMovimientoService _renglonMovimientoService;
        private readonly ILogger<RenglonMovimientoController> _logger;

        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public RenglonMovimientoController(RenglonMovimientoService renglonMovimientoservice, ILogger<RenglonMovimientoController> logger, IJwtAuthenticationService authService)
        {
            _renglonMovimientoService = renglonMovimientoservice;
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("InsertRenglonMovimiento")]
        public IActionResult InsertRenglonMovimiento([FromBody] InsertRenglonMovimientoModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "RenglonMovimiento registrado con éxito" ;
                _renglonMovimientoService.InsertRenglonMovimiento(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetRenglonesMovimiento")]
        public IActionResult GetRenglonesMovimiento([FromQuery] int IdMovimiento)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Renglones - Movimiento obtenidos con éxito";
                var resultado = _renglonMovimientoService.GetRenglonesMovimiento(IdMovimiento);
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

        [HttpGet("ExportarExcelRenglonMovimientos")]
        public IActionResult ExportarExcel([FromQuery] int IdMovimiento)
        {
            var data = GetRenglonMovimientosData(IdMovimiento);

            XLWorkbook wb = new XLWorkbook();
            MemoryStream ms = new MemoryStream();

            wb.AddWorksheet(data, "RenglonMovimientos").Columns().AdjustToContents();
            wb.SaveAs(ms);


            return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","RenglonMovimientos.xlsx");
        }

        private DataTable GetRenglonMovimientosData(int IdMovimiento)
        {
            DataTable dt = new DataTable();
            dt.TableName = "RenglonMovimientos";
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Insumo", typeof(string));
            dt.Columns.Add("Descripción Insumo", typeof(string));
            dt.Columns.Add("Cantidad", typeof(decimal));
            dt.Columns.Add("Costo", typeof(decimal));
            dt.Columns.Add("Estatus", typeof(int));
            dt.Columns.Add("Fecha Registro", typeof(string));
            dt.Columns.Add("Id Usuario Registra", typeof(int));
            dt.Columns.Add("Usuario Registra", typeof(string));


            List<GetRenglonesMovimientoModel> lista = this._renglonMovimientoService.GetRenglonesMovimiento(IdMovimiento);
            if (lista.Count > 0)
            {
                foreach(GetRenglonesMovimientoModel renglonMovimiento in lista)
                {
                    dt.Rows.Add(renglonMovimiento.Id, renglonMovimiento.Insumo, renglonMovimiento.DescripcionInsumo, renglonMovimiento.Cantidad,renglonMovimiento.Costo, renglonMovimiento.Estatus, renglonMovimiento.FechaRegistro,renglonMovimiento.IdUsuarioRegistra, renglonMovimiento.UsuarioRegistra);
                }
            }
            return dt;
        }

        [HttpGet("GetReporteMovimientosInventarios")]
        public IActionResult GetReporteMovimientosInventarios([FromQuery] string FechaInicio, string FechaFin, int IdAlmacen)
        {
            var data = GetReporteMovimientosInventariosData(FechaInicio, FechaFin, IdAlmacen);
            
            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("ReporteMovimientos");
                
                // Título con rango de fechas
                string titulo = $"Reporte de Movimientos de {FechaInicio} a {FechaFin}";
                ws.Cell(1, 1).Value = titulo;
                ws.Range(1, 1, 1, data.Columns.Count).Merge().Style.Font.SetBold().Font.FontSize = 16;
                
                // Copiar DataTable al worksheet
                ws.Cell(3, 1).InsertTable(data); // Dejar dos filas de espacio para el título
                ws.Columns().AdjustToContents();

                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteMovimientosInventario.xlsx");
                }
            }
        }

        private DataTable GetReporteMovimientosInventariosData(string FechaInicio, string FechaFin, int IdAlmacen)
        {
            DataTable dt = new DataTable();
            dt.TableName = "ReporteMovimientos";
            dt.Columns.Add("Insumo", typeof(string));
            dt.Columns.Add("Descripción Insumo", typeof(string));
            dt.Columns.Add("Tipo Movimiento", typeof(string));
            dt.Columns.Add("Cantidad", typeof(decimal));
            dt.Columns.Add("Costo", typeof(decimal));
            dt.Columns.Add("Total Renglón", typeof(decimal));
            dt.Columns.Add("Usuario Registra", typeof(string));

            // Llamada al servicio para obtener datos
            var lista = _renglonMovimientoService.GetMovimientosReporte(FechaInicio, FechaFin, IdAlmacen);
            foreach (var movimiento in lista)
            {
                dt.Rows.Add(movimiento.Insumo, movimiento.DescripcionInsumo, movimiento.TipoMovimiento, movimiento.Cantidad, movimiento.Costo, movimiento.TotalRenglon, movimiento.UsuarioRegistra);
            }
            return dt;
        }

        [HttpPut("UpdateRenglonMovimiento")]
        public IActionResult UpdateRenglonMovimiento([FromBody] UpdateRenglonMovimientoModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "RenglonMovimiento actualizado con éxito" ;
                _renglonMovimientoService.UpdateRenglonMovimiento(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteRenglonMovimiento")]
        public IActionResult DeleteRenglonMovimiento([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "RenglonMovimiento eliminado con éxito" ;
                _renglonMovimientoService.DeleteRenglonMovimiento(Id);
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