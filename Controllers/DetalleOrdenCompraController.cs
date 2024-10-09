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
    public class DetalleOrdenCompraController : ControllerBase
    {
        private readonly DetalleOrdenCompraService _DetalleOrdenCompraService;
        private readonly ILogger<DetalleOrdenCompraController> _logger;
  
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

         public DetalleOrdenCompraController(DetalleOrdenCompraService DetalleOrdenCompraService, ILogger<DetalleOrdenCompraController> logger, IJwtAuthenticationService authService) {
            _DetalleOrdenCompraService = DetalleOrdenCompraService;
            _logger = logger;
       
            _authService = authService;
            // Configura la ruta base donde se almacenan los archivos.
            // Asegúrate de ajustar la ruta según tu estructura de directorios.

            
            
        }

        [HttpPost("InsertDetalleOrdenCompra")]
        public IActionResult InsertDetalleOrdenCompra([FromBody] InsertDetalleOrdenCompraModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "DetalleOrdenCompra registrada correctamente";
                _DetalleOrdenCompraService.InsertDetalleOrdenCompra(req);

            }

            catch (System.Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetDetalleOrdenCompras")]
        public IActionResult GetDetalleOrdenCompra([FromQuery] int IdOrdenCompra)
        {
            var objectResponse = Helper.GetStructResponse();

            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "DetalleOrdenCompras cargados con exito";
                var resultado = _DetalleOrdenCompraService.GetDetalleOrdenCompras(IdOrdenCompra);
                
                

                // Llamando a la función y recibiendo los dos valores.
                
                 objectResponse.response = resultado;
            }

            catch (System.Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpPut("UpdateDetalleOrdenCompra")]
        public IActionResult UpdateDetalleOrdenCompra([FromBody] UpdateDetalleOrdenCompraModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "DetalleOrdenCompra actualizada correctamente";
                _DetalleOrdenCompraService.UpdateDetalleOrdenCompra(req);

            }

            catch (System.Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

         [HttpDelete("DeleteDetalleOrdenCompra")]
        public IActionResult DeleteDetalleOrdenCompra([FromQuery] int Id )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "DetalleOrdenCompra eliminada correctamente";
                _DetalleOrdenCompraService.DeleteDetalleOrdenCompra(Id);

            }

            catch (System.Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }   
    }
}