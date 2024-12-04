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

        [HttpGet("ReporteExcelRecetas")]
        public IActionResult ExportarExcel(string FechaInicio, string FechaFin)
        {
            var data = GetRecetasData(FechaInicio, FechaFin);
            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Recetas");
                string titulo = $"Recetas creadas {FechaInicio} - {FechaFin} ";
                ws.Cell(1, 1).Value = titulo;

                ws.Range(1,1,1, data.Columns.Count).Merge();
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(2, 1).InsertTable(data);
                ws.Columns().AdjustToContents();

                MemoryStream ms = new MemoryStream();
                wb.SaveAs(ms);
                return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","Reporte_Recetas.xlsx");
            }

        }

        private DataTable GetRecetasData(string FechaInicio, string FechaFin)
        {
            DataTable dt = new DataTable();
            dt.TableName = "Recetas";
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Insumo", typeof(string));
            dt.Columns.Add("Cantidad", typeof(float));
            dt.Columns.Add("Fecha Creación", typeof(string));
            dt.Columns.Add("Estatus", typeof(int));
            dt.Columns.Add("Usuario Registra", typeof(string));
            dt.Columns.Add("Fecha Registro", typeof(string));

            List<GetReporteRecetasModel> lista = this._RecetaService.GetReporteRecetas(FechaInicio, FechaFin);
            if (lista.Count > 0)
            {
                foreach(GetReporteRecetasModel receta in lista)
                {
                    dt.Rows.Add(receta.Id, receta.Nombre, receta.Insumo, receta.Cantidad, receta.Fecha_Creacion, receta.Estatus, receta.Usuario_Registra, receta.Fecha_Registro);
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