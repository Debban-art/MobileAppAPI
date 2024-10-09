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
    public class OrdenCompraController:ControllerBase
    {
        private readonly OrdenCompraService _ordenCompraService;
        private readonly ILogger<OrdenCompraController> _logger;

        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public OrdenCompraController(OrdenCompraService OrdenCompraService, ILogger<OrdenCompraController> logger, IJwtAuthenticationService authService)
        {
            _ordenCompraService = OrdenCompraService;
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("InsertOrdenCompra")]
        public IActionResult InsertOrdenCompra([FromBody]InsertOrdenCompraModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try{
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Orden de Compra creada correctamente";
                objectResponse.response = _ordenCompraService.InsertOrdenCompra(req);
            }
            catch(Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetOrdenCompra")]
        public IActionResult GetOrdenCompra()
        {
            var objectResponse = Helper.GetStructResponse();
            try{
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Ordenes de Compras cargadas exitósamente";
                objectResponse.response = _ordenCompraService.GetOrdenCompra();
            }
            catch(Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpPut("UpdateOrdenCompra")]
        public IActionResult UpdateOrdenCompra([FromBody]UpdateOrdenCompraModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Orden de Compra actualizada correctamente";
                _ordenCompraService.UpdateOrdenCompra(req);
            }
            catch(Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteOrdenCompra")]
        public IActionResult DeleteOrdenCompra([FromQuery]int id)
        {
            var objectResponse = Helper.GetStructResponse();
            try{
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Orden de Compra eliminada correctamente";
                _ordenCompraService.DeleteOrdenCompra(id);
            }
            catch(Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }
    }
}