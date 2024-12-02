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

        // [HttpGet("ExportarExcelRenglonTraspasos")]
        // public IActionResult ExportarExcel([FromQuery] int IdTraspaso)
        // {
        //     var data = GetRenglonTraspasosData(IdTraspaso);

        //     XLWorkbook wb = new XLWorkbook();
        //     MemoryStream ms = new MemoryStream();

        //     wb.AddWorksheet(data, "RenglonTraspasos").Columns().AdjustToContents();
        //     wb.SaveAs(ms);


        //     return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","RenglonTraspasos.xlsx");
        // }

        // private DataTable GetRenglonTraspasosData(int IdTraspaso)
        // {
        //     DataTable dt = new DataTable();
        //     dt.TableName = "RenglonTraspasos";
        //     dt.Columns.Add("Id", typeof(int));
        //     dt.Columns.Add("Insumo", typeof(string));
        //     dt.Columns.Add("Descripción Insumo", typeof(string));
        //     dt.Columns.Add("Cantidad", typeof(decimal));
        //     dt.Columns.Add("Costo", typeof(decimal));
        //     dt.Columns.Add("Estatus", typeof(int));
        //     dt.Columns.Add("Fecha Registro", typeof(string));
        //     dt.Columns.Add("Id Usuario Registra", typeof(int));
        //     dt.Columns.Add("Usuario Registra", typeof(string));


        //     List<GetRenglonesTraspasoModel> lista = this._renglonTraspasoService.GetRenglonesTraspaso(IdTraspaso);
        //     if (lista.Count > 0)
        //     {
        //         foreach(GetRenglonesTraspasoModel renglonTraspaso in lista)
        //         {
        //             dt.Rows.Add(renglonTraspaso.Id, renglonTraspaso.Insumo, renglonTraspaso.DescripcionInsumo, renglonTraspaso.Cantidad,renglonTraspaso.Costo, renglonTraspaso.Estatus, renglonTraspaso.FechaRegistro,renglonTraspaso.IdUsuarioRegistra, renglonTraspaso.UsuarioRegistra);
        //         }
        //     }
        //     return dt;
        // }

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