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
    public class EntradaController:ControllerBase
    {
        private readonly EntradaService _entradaService;
        private readonly ILogger<EntradaController> _logger;

        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public EntradaController(EntradaService entradaservice, ILogger<EntradaController> logger, IJwtAuthenticationService authService)
        {
            _entradaService = entradaservice;
            _logger = logger;
            _authService = authService;
        }
            
        [HttpPost("InsertEntrada")]
        public IActionResult InsertEntrada([FromBody] InsertEntradaModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Entrada registrado con éxito" ;
                objectResponse.response =_entradaService.InsertEntrada(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetAllEntradas")]
        public IActionResult GetAllEntradaes()
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Entradaes obtenidos con éxito";
                var resultado = _entradaService.GetEntradas();
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

        [HttpGet("ExportarExcelEntradas")]
        public IActionResult ExportarExcel()
        {
            var data = GetEntradasData();

            XLWorkbook wb = new XLWorkbook();
            MemoryStream ms = new MemoryStream();

            wb.AddWorksheet(data, "Entradas").Columns().AdjustToContents();
            wb.SaveAs(ms);


            return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","Entradas.xlsx");
        }

        private DataTable GetEntradasData()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Entradas";
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Proveedor", typeof(string));
            dt.Columns.Add("Sucursal", typeof(string));
            dt.Columns.Add("Total", typeof(decimal));
            dt.Columns.Add("Factura", typeof(string));
            dt.Columns.Add("Fecha Entrada", typeof(string));
            dt.Columns.Add("Estatus", typeof(int));
            dt.Columns.Add("Usuario Registra", typeof(string));
            dt.Columns.Add("Fecha Registro", typeof(string));


            List<GetEntradaModel> lista = this._entradaService.GetEntradas();
            if (lista.Count > 0)
            {
                foreach(GetEntradaModel entrada in lista)
                {
                    dt.Rows.Add(entrada.Id, entrada.Proveedor, entrada.Sucursal,entrada.Total,entrada.Factura, entrada.FechaEntrada,entrada.Estatus, entrada.UsuarioRegistra, entrada.FechaRegistro);
                }
            }
            return dt;
        }


        [HttpPut("UpdateEntrada")]
        public IActionResult UpdateEntrada([FromBody] UpdateEntradaModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Entrada actualizado con éxito" ;
                _entradaService.UpdateEntrada(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteEntrada")]
        public IActionResult DeleteEntrada([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Entrada eliminado con éxito" ;
                _entradaService.DeleteEntrada(Id);
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