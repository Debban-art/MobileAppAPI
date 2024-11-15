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
    public class TraspasoController: ControllerBase
    {
        private readonly TraspasoService _traspasoService;
        private readonly ILogger<TraspasoController> _logger;

        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public TraspasoController(TraspasoService traspasoservice, ILogger<TraspasoController> logger, IJwtAuthenticationService authService)
        {
            _traspasoService = traspasoservice;
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("InsertTraspaso")]
        public IActionResult InsertTraspaso([FromBody] InsertTraspasoModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Traspaso registrado con éxito" ;
                objectResponse.response =_traspasoService.InsertTraspaso(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetTraspasos")]
        public IActionResult GetAllTraspasos([FromQuery] GetTraspasoRequest req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Traspasos obtenidos con éxito";
                var resultado = _traspasoService.GetTraspasos(req);
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

        // [HttpGet("ExportarExcelTraspasos")]
        // public IActionResult ExportarExcel()
        // {
        //     var data = GetTraspasosData();

        //     XLWorkbook wb = new XLWorkbook();
        //     MemoryStream ms = new MemoryStream();

        //     wb.AddWorksheet(data, "Traspasos").Columns().AdjustToContents();
        //     wb.SaveAs(ms);


        //     return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","Traspasos.xlsx");
        // }

        // private DataTable GetTraspasosData()
        // {
        //     DataTable dt = new DataTable();
        //     dt.TableName = "Traspasos";
        //     dt.Columns.Add("Id", typeof(int));
        //     dt.Columns.Add("Id Tipo-Traspaso", typeof(int));
        //     dt.Columns.Add("Tipo-Traspaso", typeof(string));
        //     dt.Columns.Add("IdAlmacen", typeof(int));
        //     dt.Columns.Add("Almacen", typeof(string));
        //     dt.Columns.Add("Fecha", typeof(string));
        //     dt.Columns.Add("Estatus", typeof(int));
        //     dt.Columns.Add("Id Usuario", typeof(string));
        //     dt.Columns.Add("Usuario", typeof(string));


        //     List<GetTraspasosModel> lista = this._traspasoService.GetTraspasos();
        //     if (lista.Count > 0)
        //     {
        //         foreach(GetTraspasosModel traspaso in lista)
        //         {
        //             dt.Rows.Add(traspaso.Id, traspaso.IdTipoTraspaso, traspaso.TipoTraspaso, traspaso.IdAlmacen,traspaso.Almacen, traspaso.Fecha, traspaso.Estatus,traspaso.IdUsuario, traspaso.Usuario);
        //         }
        //     }
        //     return dt;
        // }

        [HttpPut("UpdateTraspasoEstatus")]
        public IActionResult UpdateTraspasoEstatus([FromBody] UpdateTraspasoEstatusModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Estatus de traspaso actualizado con éxito" ;
                _traspasoService.UpdateTraspasoEstatus(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpPut("UpdateTraspaso")]
        public IActionResult UpdateTraspaso([FromBody] UpdateTraspasoModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Traspaso actualizado con éxito" ;
                _traspasoService.UpdateTraspaso(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteTraspaso")]
        public IActionResult DeleteTraspaso([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Traspaso eliminado con éxito" ;
                _traspasoService.DeleteTraspaso(Id);
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