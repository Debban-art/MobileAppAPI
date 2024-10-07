using System;
using Microsoft.AspNetCore.Mvc;
using reportesApi.Services;
using reportesApi.Utilities;
using reportesApi.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using reportesApi.Helpers;
using Microsoft.AspNetCore.Hosting;

namespace reportesApi.Controllers
{
    [Route("api")]
    public class RecetaController: ControllerBase
    {
        private readonly RecetaService _recetaService;
        private readonly ILogger<RecetaController> _logger;

        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public RecetaController(RecetaService recetaservice, ILogger<RecetaController> logger, IJwtAuthenticationService authService)
        {
            _recetaService = recetaservice;
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("InsertReceta")]
        public IActionResult InsertReceta([FromBody] InsertRecetaModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Receta registrada con éxito" ;
                _recetaService.InsertReceta(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetRecetas")]
        public IActionResult GetRecetas()
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Recetas obtenidas con éxito";
                var resultado = _recetaService.GetRecetas();
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

    }

    


}