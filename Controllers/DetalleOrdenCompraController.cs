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