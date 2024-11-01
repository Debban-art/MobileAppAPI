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
    public class ExistenciasController: ControllerBase
    {
        private readonly ExistenciasService _existenciaService;
        private readonly ILogger<ExistenciasController> _logger;

        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public ExistenciasController(ExistenciasService existenciaservice, ILogger<ExistenciasController> logger, IJwtAuthenticationService authService)
        {
            _existenciaService = existenciaservice;
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("InsertExistencia")]
        public IActionResult InsertExistencia([FromBody] InsertExistenciaModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Existencia registrada con éxito" ;
                _existenciaService.InsertExistencia(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetExistencias")]
        public IActionResult GetExistencias()
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Existencias obtenidas con éxito";
                var resultado = _existenciaService.GetExistencias();
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

        [HttpGet("ExportarExcelExistencias")]
        public IActionResult ExportarExcel()
        {
            var data = GetExistenciaesData();

            XLWorkbook wb = new XLWorkbook();
            MemoryStream ms = new MemoryStream();

            wb.AddWorksheet(data, "Existencias").Columns().AdjustToContents();
            wb.SaveAs(ms);


            return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","Existencias.xlsx");
        }

        private DataTable GetExistenciaesData()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Existencias";
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Fecha", typeof(string));
            dt.Columns.Add("Insumo", typeof(string));
            dt.Columns.Add("Descripción Insumo", typeof(string));
            dt.Columns.Add("Cantidad", typeof(decimal));
            dt.Columns.Add("Almacen", typeof(string));
            dt.Columns.Add("Estatus", typeof(int));
            dt.Columns.Add("Usuario Registra", typeof(string));
            dt.Columns.Add("Fecha Registro", typeof(string));


            List<GetExistenciasModel> lista = this._existenciaService.GetExistencias();
            if (lista.Count > 0)
            {
                foreach(GetExistenciasModel existencia in lista)
                {
                    dt.Rows.Add(existencia.Id, existencia.Fecha, existencia.Insumo, existencia.DescripcionInsumo,existencia.Cantidad, existencia.Almacen, existencia.Estatus,existencia.Usuario, existencia.FechaRegistro);
                }
            }
            return dt;
        }

        [HttpPut("UpdateExistencia")]
        public IActionResult UpdateExistencia([FromBody] UpdateExistenciaModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Existencia actualizada con éxito" ;
                _existenciaService.UpdateExistencia(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteExistencia")]
        public IActionResult DeleteExistencia([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Existencia eliminada con éxito" ;
                _existenciaService.DeleteExistencia(Id);
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