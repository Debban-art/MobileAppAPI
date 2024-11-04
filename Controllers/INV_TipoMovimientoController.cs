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
    public class INV_TipoMovimientoController : ControllerBase
    {
         private readonly INV_TipoMovimientoService _INV_TipoMovimientoService;
        private readonly ILogger<INV_TipoMovimientoController> _logger;

        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

         public INV_TipoMovimientoController(INV_TipoMovimientoService INV_TipoMovimientoService, ILogger<INV_TipoMovimientoController> logger, IJwtAuthenticationService authService) 
         {
            _INV_TipoMovimientoService = INV_TipoMovimientoService;
            _logger = logger;
       
            _authService = authService;
            // Configura la ruta base donde se almacenan los archivos.
            // Asegúrate de ajustar la ruta según tu estructura de directorios.
        }

        [HttpPost("InsertINV_TipoMovimiento")]
        public IActionResult InsertINV_TipoMovimiento([FromBody] InsertINV_TipoMovimientoModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Tipo de movimiento registrado correctamente";
                _INV_TipoMovimientoService.InsertINV_TipoMovimiento(req);

            }

            catch (System.Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }  

        [HttpGet("GetINV_TipoMovimiento")]
        public IActionResult GetINV_TipoMovimiento()
        {
            var objectResponse = Helper.GetStructResponse();

            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Tipos de movimientos cargados con exito";
                var resultado = _INV_TipoMovimientoService.GetINV_TipoMovimiento();
                
                

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

        [HttpPut("UpdateINV_TipoMovimiento")]
        public IActionResult UpdateINV_TipoMovimiento([FromBody] UpdateINV_TipoMovimientoModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Tipo de movimiento actualizado correctamente";
                _INV_TipoMovimientoService.UpdateINV_TipoMovimiento(req);

            }

            catch (System.Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteINV_TipoMovimiento")]
        public IActionResult DeleteINV_TipoMovimiento([FromQuery] int Id )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Tipo de movimiento eliminado correctamente";
                _INV_TipoMovimientoService.DeleteINV_TipoMovimiento(Id);

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