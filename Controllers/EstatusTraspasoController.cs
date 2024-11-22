// using System;
// using Microsoft.AspNetCore.Mvc;
// using reportesApi.Services;
// using reportesApi.Utilities;
// using reportesApi.Models;
// using Microsoft.Extensions.Logging;
// using System.Net;
// using reportesApi.Helpers;
// using Microsoft.AspNetCore.Hosting;
// using ClosedXML.Excel;
// using System.IO;
// using System.Data;
// using System.Collections.Generic;

// namespace reportesApi.Controllers
// {
//     [Route("api")]
//     public class EstatusTraspasoController: ControllerBase
//     {
//         private readonly EstatusTraspasoService _estatusTraspasoService;
//         private readonly ILogger<EstatusTraspasoController> _logger;

//         private readonly IJwtAuthenticationService _authService;
//         private readonly IWebHostEnvironment _hostingEnvironment;

//         Encrypt enc = new Encrypt();

//         public EstatusTraspasoController(EstatusTraspasoService estatusTraspasoservice, ILogger<EstatusTraspasoController> logger, IJwtAuthenticationService authService)
//         {
//             _estatusTraspasoService = estatusTraspasoservice;
//             _logger = logger;
//             _authService = authService;
//         }

//         [HttpPost("InsertEstatusTraspaso")]
//         public IActionResult InsertEstatusTraspaso([FromBody] InsertEstatusTraspasoModel req)
//         {
//             var objectResponse = Helper.GetStructResponse();
//             try
//             {
//                 objectResponse.StatusCode = (int)HttpStatusCode.Created;
//                 objectResponse.success = true;
//                 objectResponse.message = "Estatus de traspaso registrado con éxito" ;
//                 _estatusTraspasoService.InsertEstatusTraspaso(req);
//             }
//             catch (Exception ex)
//             {
//                 objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
//                 objectResponse.success = false;
//                 objectResponse.message = ex.Message;
//             }

//             return new JsonResult(objectResponse);
//         }

//         [HttpGet("GetEstatusTraspaso")]
//         public IActionResult GetEstatusTraspasoes()
//         {
//             var objectResponse = Helper.GetStructResponse();
//             try
//             {
//                 objectResponse.StatusCode = (int)HttpStatusCode.OK;
//                 objectResponse.success = true;
//                 objectResponse.message = "Estatus obtenidos con éxito";
//                 var resultado = _estatusTraspasoService.GetEstatusTraspaso();
//                 objectResponse.response = resultado;
//             }
//             catch(Exception ex)
//             {
//                 objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
//                 objectResponse.success = false;
//                 objectResponse.message = ex.Message;
//             }

//             return new JsonResult(objectResponse);
//         }


//         [HttpPut("UpdateEstatusTraspaso")]
//         public IActionResult UpdateEstatusTraspaso([FromBody] UpdateEstatusTraspasoModel req)
//         {
//             var objectResponse = Helper.GetStructResponse();
//             try
//             {
//                 objectResponse.StatusCode = (int)HttpStatusCode.Created;
//                 objectResponse.success = true;
//                 objectResponse.message = "Estatus de traspaso actualizado con éxito" ;
//                 _estatusTraspasoService.UpdateEstatusTraspaso(req);
//             }
//             catch (Exception ex)
//             {
//                 objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
//                 objectResponse.success = false;
//                 objectResponse.message = ex.Message;
//             }

//             return new JsonResult(objectResponse);
//         }

//         [HttpDelete("DeleteEstatusTraspaso")]
//         public IActionResult DeleteEstatusTraspaso([FromQuery] int Id)
//         {
//             var objectResponse = Helper.GetStructResponse();
//             try
//             {
//                 objectResponse.StatusCode = (int)HttpStatusCode.Created;
//                 objectResponse.success = true;
//                 objectResponse.message = "Estatus de traspaso eliminado con éxito" ;
//                 _estatusTraspasoService.DeleteEstatusTraspaso(Id);
//             }
//             catch (Exception ex)
//             {
//                 objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
//                 objectResponse.success = false;
//                 objectResponse.message = ex.Message;
//             }

//             return new JsonResult(objectResponse);
//         }
//     }
// }