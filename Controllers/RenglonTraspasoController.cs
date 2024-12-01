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
using System.ComponentModel.DataAnnotations;

namespace reportesApi.Controllers
{
    [Route("api")]
    public class RenglonTraspasoController: ControllerBase
    {
        private readonly RenglonTraspasoService _renglonTraspasoService;
        private readonly ILogger<RenglonTraspasoController> _logger;

        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public RenglonTraspasoController(RenglonTraspasoService renglonTraspasoservice, ILogger<RenglonTraspasoController> logger, IJwtAuthenticationService authService)
        {
            _renglonTraspasoService = renglonTraspasoservice;
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("InsertRenglonTraspaso")]
        public IActionResult InsertRenglonTraspaso([FromBody] InsertRenglonTraspasoModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Renglon de Traspaso registrado con éxito" ;
                _renglonTraspasoService.InsertRenglonTraspaso(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetRenglonesTraspaso")]
        public IActionResult GetRenglonesTraspaso([FromQuery] int IdTraspaso)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Renglones - Traspaso obtenidos con éxito";
                var resultado = _renglonTraspasoService.GetRenglonesTraspaso(IdTraspaso);
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

        [HttpGet("ReporteExcelInsumosTraspasoSalida")]
        public IActionResult GetInsumosTraspasoSalida([FromQuery, Required] int IdAlmacen, [FromQuery, Required]string FechaInicio, [FromQuery, Required]string FechaFin)
        {
            var data = GetRenglonTraspasosData(1,IdAlmacen, FechaInicio, FechaFin);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("CodigosTraspasosSalidaAlmacen" + IdAlmacen);
                string titulo = $"Códigos en Traspasos Salida del Almacén #{IdAlmacen} {FechaInicio} - {FechaFin} ";
                ws.Cell(1, 1).Value = titulo;

                ws.Range(1,1,1, data.Columns.Count).Merge();
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(2, 1).InsertTable(data);
                ws.Columns().AdjustToContents();

                MemoryStream ms = new MemoryStream();
                wb.SaveAs(ms);
                return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",$"Insumos_Traspasos_Salida_AlmacenNo{IdAlmacen}.xlsx");
            }
        }

        
        [HttpGet("ReporteExcelInsumosTraspasoEntrada")]
        public IActionResult GetInsumosTraspasoEntrada([FromQuery, Required] int IdAlmacen, [FromQuery, Required]string FechaInicio, [FromQuery, Required]string FechaFin)
        {
            var data = GetRenglonTraspasosData(2,IdAlmacen, FechaInicio, FechaFin);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("CodigosTraspasosEntradaAlmacen" + IdAlmacen);
                string titulo = $"Códigos en Traspasos Entrada del Almacén #{IdAlmacen} {FechaInicio} - {FechaFin} ";
                ws.Cell(1, 1).Value = titulo;

                ws.Range(1,1,1, data.Columns.Count).Merge();
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(2, 1).InsertTable(data);
                ws.Columns().AdjustToContents();

                MemoryStream ms = new MemoryStream();
                wb.SaveAs(ms);
                return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",$"Insumos_Traspasos_Entrada_AlmacenNo{IdAlmacen}.xlsx");
            }
        }

        private DataTable GetRenglonTraspasosData(int TipoTraspaso,int IdAlmacen, string FechaInicio, string FechaFin)
        {
            DataTable dt = new DataTable();
            dt.TableName = "Insumos traspasos";
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Insumo", typeof(string));
            dt.Columns.Add("Cantidad", typeof(float));
            dt.Columns.Add( TipoTraspaso == 1 ? "Fecha Salida" : "Fecha Entrega", typeof(string));
            dt.Columns.Add("Fecha Registro", typeof(string));
            dt.Columns.Add("Usuario Registra", typeof(string));
            dt.Columns.Add("Estatus", typeof(int));

            var serviceCall = TipoTraspaso == 1 ? this._renglonTraspasoService.GetInsumosTraspasoSalida(IdAlmacen, FechaInicio, FechaFin) : this._renglonTraspasoService.GetInsumosTraspasoEntrada(IdAlmacen, FechaInicio, FechaFin);
            List<GetInsumosTraspasoModel> lista = serviceCall;
            if (lista.Count > 0)
            {
                foreach(GetInsumosTraspasoModel renglonTraspaso in lista)
                {
                    dt.Rows.Add(renglonTraspaso.Id, renglonTraspaso.Insumo, renglonTraspaso.Cantidad,renglonTraspaso.FechaMovimiento, renglonTraspaso.FechaRegistro, renglonTraspaso.UsuarioRegistra,renglonTraspaso.Estatus);
                }
            }
            return dt;
        }

        [HttpPut("UpdateRenglonTraspaso")]
        public IActionResult UpdateRenglonTraspaso([FromBody] UpdateRenglonTraspasoModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Renglon de traspaso actualizado con éxito" ;
                _renglonTraspasoService.UpdateRenglonTraspaso(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteRenglonTraspaso")]
        public IActionResult DeleteRenglonTraspaso([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Renglon de traspaso eliminado con éxito" ;
                _renglonTraspasoService.DeleteRenglonTraspaso(Id);
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