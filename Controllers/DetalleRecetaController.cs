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
    public class DetalleRecetaController: ControllerBase
    {
        private readonly DetalleRecetaService _detalleRecetaService;
        private readonly ILogger<DetalleRecetaController> _logger;
        
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public DetalleRecetaController(DetalleRecetaService detallerecetaservice, ILogger<DetalleRecetaController> logger, IJwtAuthenticationService authService)
        {
            _detalleRecetaService = detallerecetaservice;
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("InsertDetalleReceta")]
        public IActionResult InsertDetalleReceta([FromBody] InsertDetalleRecetaModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Detalle Receta Insertado Correctamente";
                _detalleRecetaService.InsertDetalleReceta(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetDetallesReceta")]
        public IActionResult GetDetallesReceta([FromQuery] int IdReceta)
        {
            var objectResponse = Helper.GetStructResponse();

            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Detalles-Receta caragdos exitósamente";
                objectResponse.response = _detalleRecetaService.GetDetallesReceta(IdReceta);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("ReporteExcelDetallesReceta")]
        public IActionResult ExportarExcel([FromQuery] int IdReceta)
        {
            var data = GetDetallesRecetasData(IdReceta);

            XLWorkbook wb = new XLWorkbook();
            MemoryStream ms = new MemoryStream();

            wb.AddWorksheet(data, "DetallesReceta").Columns().AdjustToContents();
            wb.SaveAs(ms);


            return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","DetallesReceta.xlsx");
        }

        private DataTable GetDetallesRecetasData(int IdReceta)
        {
            DataTable dt = new DataTable();
            dt.TableName = "DetallesRecetas";
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Código Insumo", typeof(string));
            dt.Columns.Add("Insumo", typeof(string));
            dt.Columns.Add("Cantidad", typeof(float));
            dt.Columns.Add("Estatus", typeof(int));
            dt.Columns.Add("Usuario Registra", typeof(string));
            dt.Columns.Add("Fecha Registro", typeof(string));


            List<GetDetallesRecetaModel> lista = this._detalleRecetaService.GetDetallesReceta(IdReceta);
            if (lista.Count > 0)
            {
                foreach(GetDetallesRecetaModel detallesReceta in lista)
                {
                    dt.Rows.Add(detallesReceta.Id,detallesReceta.CodigoInsumo, detallesReceta.Insumo,detallesReceta.Cantidad, detallesReceta.Estatus, detallesReceta.UsuarioRegistra, detallesReceta.FechaRegistro);
                }
            }
            return dt;
        }


        [HttpPut("UpdateDetalleReceta")]
        public IActionResult UpdateDetalleReceta([FromBody] UpdateDetalleRecetaModel req)
        {
            var objectResponse = Helper.GetStructResponse();

            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Detalle-Receta actualizado correctamente";
                _detalleRecetaService.UpdateDetalleReceta(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);

        }

        [HttpDelete("DeleteDetalleReceta")]
        public IActionResult DeleteDetalleReceta([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Detalle-Receta eliminado correctamente";
                _detalleRecetaService.DeleteDetalleReceta(Id);
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