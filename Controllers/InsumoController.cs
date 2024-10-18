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
    public class InsumoController: ControllerBase
    {
        private readonly InsumoService _insumoService;
        private readonly ILogger<InsumoController> _logger;

        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public InsumoController(InsumoService insumoservice, ILogger<InsumoController> logger, IJwtAuthenticationService authService)
        {
            _insumoService = insumoservice;
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("InsertInsumo")]
        public IActionResult InsertInsumo([FromBody] InsertInsumoModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Insumo registrado con éxito" ;
                _insumoService.InsertInsumo(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetAllInsumos")]
        public IActionResult GetAllInsumos()
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Insumos obtenidos con éxito";
                var resultado = _insumoService.GetAllInsumos();
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

        [HttpGet("ExportarExcelInsumos")]
        public IActionResult ExportarExcel()
        {
            var data = GetInsumosData();

            XLWorkbook wb = new XLWorkbook();
            MemoryStream ms = new MemoryStream();

            wb.AddWorksheet(data, "Insumos").Columns().AdjustToContents();
            wb.SaveAs(ms);


            return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","Insumos.xlsx");
        }

        private DataTable GetInsumosData()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Insumos";
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Insumo", typeof(string));
            dt.Columns.Add("DescripcionInsumo", typeof(string));
            dt.Columns.Add("Costo", typeof(float));
            dt.Columns.Add("UnidadMedida", typeof(int));
            dt.Columns.Add("InsumoUp", typeof(string));
            dt.Columns.Add("Estatus", typeof(int));
            dt.Columns.Add("UsuarioRegistra", typeof(string));
            dt.Columns.Add("FechaRegistro", typeof(string));

            List<GetInsumosModel> lista = this._insumoService.GetAllInsumos();
            if (lista.Count > 0)
            {
                foreach(GetInsumosModel insumo in lista)
                {
                    dt.Rows.Add(insumo.Insumo_Id, insumo.Insumo, insumo.Insumo_Descripcion, insumo.Insumo_Costo, insumo.Insumo_UnidadMedida, insumo.Insumo_InsumoUp, insumo.Insumo_Estatus, insumo.Usuario_Registra, insumo.Fecha_Registro);
                }
            }
            return dt;
        }

        [HttpPut("UpdateInsumo")]
        public IActionResult UpdateInsumo([FromBody] UpdateInsumoModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Insumo actualizado con éxito" ;
                _insumoService.UpdateInsumo(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteInsumo")]
        public IActionResult DeleteInsumo([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Insumo eliminado con éxito" ;
                _insumoService.DeleteInsumo(Id);
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