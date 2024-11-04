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
    public class INV_RenglonesMovimientoController : ControllerBase
    {
        private readonly INV_RenglonesMovimientoService _INV_RenglonesMovimientoService;
        private readonly ILogger<INV_RenglonesMovimientoController> _logger;
  
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

         public INV_RenglonesMovimientoController(INV_RenglonesMovimientoService INV_RenglonesMovimientoService, ILogger<INV_RenglonesMovimientoController> logger, IJwtAuthenticationService authService) {
            _INV_RenglonesMovimientoService = INV_RenglonesMovimientoService;
            _logger = logger;
       
            _authService = authService;
            // Configura la ruta base donde se almacenan los archivos.
            // Asegúrate de ajustar la ruta según tu estructura de directorios.

            
            
        }

        [HttpPost("InsertINV_RenglonesMovimiento")]
        public IActionResult InsertINV_RenglonesMovimiento([FromBody] InsertINV_RenglonesMovimientoModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Renglon registrado correctamente";
                _INV_RenglonesMovimientoService.InsertINV_RenglonesMovimiento(req);

            }

            catch (System.Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetINV_RenglonesMovimientos")]
        public IActionResult GetINV_RenglonesMovimiento([FromQuery] int IdMovimiento)
        {
            var objectResponse = Helper.GetStructResponse();

            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Renglon cargado con exito";
                var resultado = _INV_RenglonesMovimientoService.GetINV_RenglonesMovimientos(IdMovimiento);
                
                

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

        [HttpPut("UpdateINV_RenglonesMovimiento")]
        public IActionResult UpdateINV_RenglonesMovimiento([FromBody] UpdateINV_RenglonesMovimientoModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Renglon Movimiento actualizado correctamente";
                _INV_RenglonesMovimientoService.UpdateINV_RenglonesMovimiento(req);

            }

            catch (System.Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

         [HttpDelete("DeleteINV_RenglonesMovimiento")]
        public IActionResult DeleteINV_RenglonesMovimiento([FromQuery] int Id )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Renglon Movimiento eliminado correctamente";
                _INV_RenglonesMovimientoService.DeleteINV_RenglonesMovimiento(Id);

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