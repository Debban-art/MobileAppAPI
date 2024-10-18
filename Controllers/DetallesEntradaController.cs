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
using ClosedXML.Excel;
using System.Data;
using System.Collections.Generic;

namespace reportesApi.Controllers
{
    [Route("api")]
    public class DetallesEntradaController:ControllerBase
    {
        private readonly DetallesEntradaService _detallesEntradaService;
        private readonly ILogger<DetallesEntradaController> _logger;

        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public DetallesEntradaController(DetallesEntradaService detallesEntradaservice, ILogger<DetallesEntradaController> logger, IJwtAuthenticationService authService)
        {
            _detallesEntradaService = detallesEntradaservice;
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("InsertDetallesEntrada")]
        public IActionResult InsertDetallesEntrada([FromBody] InsertDetallesEntradaModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Entrada registrado con éxito" ;
                _detallesEntradaService.InsertDetallesEntrada(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }
            

        [HttpGet("GetDetallesEntradas")]
        public IActionResult GetDetallesEntradaes([FromQuery] int IdEntrada)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Detalles de Entradas obtenidos con éxito";
                var resultado = _detallesEntradaService.GetDetallesEntradas(IdEntrada);
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

        [HttpGet("ExportarExcelDetallesEntradas")]
        public IActionResult ExportarExcel([FromQuery] int IdEntrada)
        {
            var data = GetDetallesEntradasData(IdEntrada);

            XLWorkbook wb = new XLWorkbook();
            MemoryStream ms = new MemoryStream();

            wb.AddWorksheet(data, "DetallesEntrada").Columns().AdjustToContents();
            wb.SaveAs(ms);


            return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","DetallesEntrada.xlsx");
        }

        private DataTable GetDetallesEntradasData(int IdEntrada)
        {
            DataTable dt = new DataTable();
            dt.TableName = "DetallesEntradas";
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Id Entrada", typeof(int));
            dt.Columns.Add("Insumo", typeof(string));
            dt.Columns.Add("Cantidad", typeof(decimal));
            dt.Columns.Add("Sin Cargo", typeof(decimal));
            dt.Columns.Add("Costo", typeof(decimal));
            dt.Columns.Add("Estatus", typeof(int));
            dt.Columns.Add("Usuario Registra", typeof(string));
            dt.Columns.Add("Fecha Registro", typeof(string));


            List<GetDetallesEntradaModel> lista = this._detallesEntradaService.GetDetallesEntradas(IdEntrada);
            if (lista.Count > 0)
            {
                foreach(GetDetallesEntradaModel detallesEntrada in lista)
                {
                    dt.Rows.Add(detallesEntrada.Id, detallesEntrada.IdEntrada,detallesEntrada.Insumo, detallesEntrada.Cantidad,detallesEntrada.SinCargo,detallesEntrada.Costo, detallesEntrada.Estatus, detallesEntrada.UsuarioRegistra, detallesEntrada.FechaRegistro);
                }
            }
            return dt;
        }


        [HttpPut("UpdateDetallesEntrada")]
        public IActionResult UpdateDetallesEntrada([FromBody] UpdateDetallesEntradaModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Detalles de entrada actualizado con éxito" ;
                _detallesEntradaService.UpdateDetallesEntrada(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteDetallesEntrada")]
        public IActionResult DeleteDetallesEntrada([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Detalles de entrada eliminados con éxito" ;
                _detallesEntradaService.DeleteDetallesEntrada(Id);
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