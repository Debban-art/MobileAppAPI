using System;
using Microsoft.AspNetCore.Mvc;
using reportesApi.Services;
using reportesApi.Utilities;
using reportesApi.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using reportesApi.Helpers;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml; // Necesario para EPPlus
using System.IO;     // Necesario para MemoryStream

namespace reportesApi.Controllers
{
    [Route("api")]
    public class RecetaController : ControllerBase
    {
        private readonly RecetaService _recetaService;
        private readonly ILogger<RecetaController> _logger;

        public RecetaController(RecetaService recetaService, ILogger<RecetaController> logger)
        {
            _recetaService = recetaService;
            _logger = logger;
        }

        [HttpPost("InsertReceta")]
        public IActionResult InsertReceta([FromBody] InsertRecetaModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Receta registrada con éxito";
                objectResponse.response = _recetaService.InsertReceta(req);
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
        public IActionResult GetRecetas(bool ExportarAExcel = false)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                if (ExportarAExcel)
                {
                    // Generar el archivo Excel utilizando el servicio
                    var excelStream = _recetaService.GetRecetasExcel();
                    var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    var fileName = "RecetasExcel.xlsx";

                    return File(excelStream.ToArray(), contentType, fileName);
                }
                else
                {
                    var recetas = _recetaService.GetRecetas();
                    objectResponse.StatusCode = (int)HttpStatusCode.OK;
                    objectResponse.success = true;
                    objectResponse.message = "Recetas obtenidas con éxito";
                    objectResponse.response = recetas;
                }
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpPut("UpdateReceta")]
        public IActionResult UpdateReceta([FromBody] UpdateRecetaModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Receta actualizada con éxito";
                _recetaService.UpdateReceta(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteReceta")]
        public IActionResult DeleteReceta([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Receta eliminada con éxito";
                _recetaService.DeleteReceta(Id);
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

