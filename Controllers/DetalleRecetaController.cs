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
    public class DetalleRecetaController : ControllerBase
    {
        private readonly DetalleRecetaService _DetalleRecetaService;
        private readonly ILogger<DetalleRecetaController> _logger;
  
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

         public DetalleRecetaController(DetalleRecetaService DetalleRecetaService, ILogger<DetalleRecetaController> logger, IJwtAuthenticationService authService) {
            _DetalleRecetaService = DetalleRecetaService;
            _logger = logger;
       
            _authService = authService;
            // Configura la ruta base donde se almacenan los archivos.
            // Asegúrate de ajustar la ruta según tu estructura de directorios.

            
            
        }

        [HttpPost("InsertDetalleReceta")]
        public IActionResult InsertDetalleReceta([FromBody] InsertDetalleRecetaModel req )
        {
            
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "DetalleReceta registrada correctamente";
                _DetalleRecetaService.InsertDetalleReceta(req);

            }

            catch (System.Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetDetalleRecetas")]
        public IActionResult GetDetalleReceta([FromQuery] int IdReceta)
        {
            var objectResponse = Helper.GetStructResponse();

            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "DetalleRecetas cargados con exito";
                var resultado = _DetalleRecetaService.GetDetalleRecetas(IdReceta);
                
                

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

        [HttpGet("ReporteExcelDetallesReceta")]
        public IActionResult ExportarExcel([FromQuery] int IdReceta)
        {
            var data = GetDetallesRecetasData(IdReceta);
            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Detalle_Receta");
                string titulo = $"Detalles de Receta #{IdReceta}";
                ws.Cell(1, 1).Value = titulo;

                ws.Range(1,1,1, data.Columns.Count).Merge();
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(2, 1).InsertTable(data);
                ws.Columns().AdjustToContents();

                MemoryStream ms = new MemoryStream();
                wb.SaveAs(ms);
                return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",$"Detalles_Receta_{IdReceta}.xlsx");

            }
        }

        private DataTable GetDetallesRecetasData(int IdReceta)
        {
            DataTable dt = new DataTable();
            dt.TableName = "DetallesRecetas";
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Código Insumo", typeof(string));
            dt.Columns.Add("Insumo", typeof(string));
            dt.Columns.Add("Cantidad", typeof(float));
            dt.Columns.Add("Estatus", typeof(int));
            dt.Columns.Add("Usuario Registra", typeof(string));
            dt.Columns.Add("Fecha Registro", typeof(string));


            List<GetDetallesRecetaModel> lista = this._detalleRecetaService.GetDetallesReceta(IdReceta);
            if (lista.Count > 0)
            {
                foreach(GetDetallesRecetaModel detallesReceta in lista)
                {
                    dt.Rows.Add(detallesReceta.Id,detallesReceta.CodigoInsumo, detallesReceta.Insumo,detallesReceta.Cantidad, detallesReceta.Estatus, detallesReceta.UsuarioRegistra, detallesReceta.FechaRegistro);
                }
            }
            return dt;
        }


        [HttpPut("UpdateDetalleReceta")]
        public IActionResult UpdateDetalleReceta([FromBody] UpdateDetalleRecetaModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "DetalleReceta actualizada correctamente";
                _DetalleRecetaService.UpdateDetalleReceta(req);

            }

            catch (System.Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

         [HttpDelete("DeleteDetalleReceta")]
        public IActionResult DeleteDetalleReceta([FromQuery] int Id )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "DetalleReceta eliminada correctamente";
                _DetalleRecetaService.DeleteDetalleReceta(Id);

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