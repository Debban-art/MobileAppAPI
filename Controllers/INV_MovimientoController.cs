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
    public class INV_MovimientoController : ControllerBase
    {
        private readonly INV_MovimientoService _INV_MovimientoService;
        private readonly ILogger<INV_MovimientoController> _logger;
  
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

         public INV_MovimientoController(INV_MovimientoService INV_MovimientoService, ILogger<INV_MovimientoController> logger, IJwtAuthenticationService authService) {
            _INV_MovimientoService = INV_MovimientoService;
            _logger = logger;
       
            _authService = authService;
            // Configura la ruta base donde se almacenan los archivos.
            // Asegúrate de ajustar la ruta según tu estructura de directorios.

            
            
        }

        [HttpPost("InsertINV_Movimiento")]
        public IActionResult InsertINV_Movimiento([FromBody] InsertINV_MovimientoModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Movimiento registrado correctamente";
                objectResponse.response = _INV_MovimientoService.InsertINV_Movimiento(req);

            }

            catch (System.Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetINV_Movimientos")]
        public IActionResult GetINV_Movimiento() 
        {
            var objectResponse = Helper.GetStructResponse();

            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Movimientos cargados con exito";
                var resultado = _INV_MovimientoService.GetINV_Movimientos();
                
                

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

        [HttpPut("UpdateINV_Movimiento")]
        public IActionResult UpdateINV_Movimiento([FromBody] UpdateINV_MovimientoModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Movimiento actualizado correctamente";
                _INV_MovimientoService.UpdateINV_Movimiento(req);

            }

            catch (System.Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

         [HttpDelete("DeleteINV_Movimiento")]
        public IActionResult DeleteINV_Movimiento([FromQuery] int Id )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Movimiento eliminado correctamente";
                _INV_MovimientoService.DeleteINV_Movimiento(Id);

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