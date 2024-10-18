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
    public class DetalleOrdenCompraController: ControllerBase
    {
        private readonly DetalleOrdenCompraService _detalleOrdenCompraService;
        private readonly ILogger<DetalleOrdenCompraController> _logger;
        
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public DetalleOrdenCompraController(DetalleOrdenCompraService detalleordenCompraservice, ILogger<DetalleOrdenCompraController> logger, IJwtAuthenticationService authService)
        {
            _detalleOrdenCompraService = detalleordenCompraservice;
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("InsertDetalleOrdenCompra")]
        public IActionResult InsertDetalleOrdenCompra([FromBody] InsertDetalleOrdenCompraModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Detalle Orden-Compra Insertado Correctamente";
                _detalleOrdenCompraService.InsertDetalleOrdenCompra(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetDetallesOrdenCompra")]
        public IActionResult GetDetallesOrdenCompra([FromQuery] int IdOrdenCompra)
        {
            var objectResponse = Helper.GetStructResponse();

            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Detalles Orden-Compra caragdos exitósamente";
                objectResponse.response = _detalleOrdenCompraService.GetDetalleOrdenCompra(IdOrdenCompra);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("ExportarExcelDetallesOrdenCompra")]
        public IActionResult ExportarExcel([FromQuery] int IdOrdenCompra)
        {
            var data = GetDetallesOrdenCompraData(IdOrdenCompra);

            XLWorkbook wb = new XLWorkbook();
            MemoryStream ms = new MemoryStream();

            wb.AddWorksheet(data, "Detalles Orden Compra").Columns().AdjustToContents();
            wb.SaveAs(ms);


            return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","Detalles Orden Compra.xlsx");
        }

        private DataTable GetDetallesOrdenCompraData(int IdOrdenCompra)
        {
            DataTable dt = new DataTable();
            dt.TableName = "Detalles Orden Compra";
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Id Orden Compra", typeof(int));
            dt.Columns.Add("Insumo", typeof(string));
            dt.Columns.Add("Cantidad", typeof(float));
            dt.Columns.Add("Cantidad Recibida", typeof(float));
            dt.Columns.Add("Costo", typeof(float));
            dt.Columns.Add("Costo Renglón", typeof(float));
            dt.Columns.Add("Estatus", typeof(int));
            dt.Columns.Add("Fecha Registro", typeof(string));
            dt.Columns.Add("Usuario Registra", typeof(string));


            List<GetDetallesOrdenCompraModel> lista = this._detalleOrdenCompraService.GetDetalleOrdenCompra(IdOrdenCompra);
            if (lista.Count > 0)
            {
                foreach(GetDetallesOrdenCompraModel detalle in lista)
                {
                    dt.Rows.Add(detalle.Id, detalle.IdOrdenCompra, detalle.Insumo, detalle.Cantidad, detalle.CantidadRecibida, detalle.Costo, detalle.CostoRenglon, detalle.Estatus, detalle.FechaRegistro, detalle.UsuarioRegistra);
                }
            }
            return dt;
        }


        [HttpPut("UpdateDetalleOrdenCompra")]
        public IActionResult UpdateDetalleOrdenCompra([FromBody] UpdateDetalleOrdenCompraModel req)
        {
            var objectResponse = Helper.GetStructResponse();

            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Detalle Orden-Compra actualizado correctamente";
                _detalleOrdenCompraService.UpdateDetalleOrdenCompra(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);

        }

        [HttpDelete("DeleteDetalleOrdenCompra")]
        public IActionResult DeleteDetalleOrdenCompra([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Detalle Orden-Compra eliminado correctamente";
                _detalleOrdenCompraService.DeleteDetalleOrdenCompra(Id);
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