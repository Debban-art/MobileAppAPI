using System;
using Microsoft.AspNetCore.Mvc;
using reportesApi.Services;
using reportesApi.Utilities;
using reportesApi.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using reportesApi.Helpers;
using Microsoft.AspNetCore.Hosting;
using ClosedXML.Excel;
using System.IO;
using System.Data;
using System.Collections.Generic;

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

        [HttpGet("ExportarExcelRecetas")]
        public IActionResult ExportarExcel()
        {
            var data = GetRecetasData();

            XLWorkbook wb = new XLWorkbook();
            MemoryStream ms = new MemoryStream();

            wb.AddWorksheet(data, "Recetas").Columns().AdjustToContents();
            wb.SaveAs(ms);


            return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","Recetas.xlsx");
        }

        private DataTable GetRecetasData()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Recetas";
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Fecha Creación", typeof(string));
            dt.Columns.Add("Estatus", typeof(int));
            dt.Columns.Add("Usuario Registra", typeof(string));
            dt.Columns.Add("Fecha Registro", typeof(string));


            List<GetRecetasModel> lista = this._recetaService.GetRecetas();
            if (lista.Count > 0)
            {
                foreach(GetRecetasModel receta in lista)
                {
                    dt.Rows.Add(receta.Id, receta.Nombre, receta.Fecha_Creacion, receta.Estatus, receta.Usuario_Registra, receta.Fecha_Registro);
                }
            }
            return dt;
        }

        [HttpPut("UpdateReceta")]
        public IActionResult UpdateReceta([FromBody] UpdateRecetaModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Receta actualizada con éxito" ;
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
        public IActionResult DeleteProveedor([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Receta eliminada con éxito" ;
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