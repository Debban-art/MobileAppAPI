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
using System.Data;
using System.Collections.Generic;
using ClosedXML.Excel;

namespace reportesApi.Controllers
{
    [Route("api")]
    public class RecetaController : ControllerBase
    {
        private readonly RecetaService _RecetaService;
        private readonly ILogger<RecetaController> _logger;
  
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

         public RecetaController(RecetaService RecetaService, ILogger<RecetaController> logger, IJwtAuthenticationService authService) {
            _RecetaService = RecetaService;
            _logger = logger;
       
            _authService = authService;
            // Configura la ruta base donde se almacenan los archivos.
            // Asegúrate de ajustar la ruta según tu estructura de directorios.

            
            
        }

        [HttpPost("InsertReceta")]
        public IActionResult InsertReceta([FromBody] InsertRecetaModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Receta registrada correctamente";
                objectResponse.response = _RecetaService.InsertReceta(req);

            }

            catch (System.Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetRecetas")]
        public IActionResult GetReceta() 
        {
            var objectResponse = Helper.GetStructResponse();

            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Recetas cargados con exito";
                var resultado = _RecetaService.GetRecetas();
                
                

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

        [HttpGet("ExportarExcel")]
        public IActionResult ExportarExcel()
        {
            var data = GetRecetasData();

            XLWorkbook wb = new XLWorkbook();
            MemoryStream ms = new MemoryStream();

            wb.AddWorksheet(data, "RecetaService").Columns().AdjustToContents();
            wb.SaveAs(ms);

            return File(ms.ToArray(),"aplication/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Recetas.xlsx");
        }

        private DataTable GetRecetasData()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Recetas";
            dt.Columns.Add("Id",typeof(int));
            dt.Columns.Add("Nombre",typeof(string));
            dt.Columns.Add("Estatus",typeof(int));
            dt.Columns.Add("FechaCreacion",typeof(string));
            dt.Columns.Add("UsuarioRegistra",typeof(string));
            dt.Columns.Add("FechaRegistro",typeof(string));

            List <GetRecetaModel> lista = this._RecetaService.GetRecetas();
            if (lista.Count > 0)
            {
                foreach (GetRecetaModel Receta in lista)
                {
                    dt.Rows.Add(Receta.Id, Receta.Nombre, Receta.Estatus, Receta.FechaCreacion, Receta.UsuarioRegistra, Receta.FechaRegistro);
                } 
            }
            return dt;
        }

        [HttpPut("UpdateReceta")]
        public IActionResult UpdateReceta([FromBody] UpdateRecetaModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Receta actualizada correctamente";
                _RecetaService.UpdateReceta(req);

            }

            catch (System.Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

         [HttpDelete("DeleteReceta")]
        public IActionResult DeleteReceta([FromQuery] int Id )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Receta eliminada correctamente";
                _RecetaService.DeleteReceta(Id);

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