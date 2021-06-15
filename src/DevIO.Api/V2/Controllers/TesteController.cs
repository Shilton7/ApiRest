using DevIO.Api.Controllers;
using DevIO.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DevIO.Api.V2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/teste")]
    public class TesteController : MainController
    {
        private readonly ILogger _logger;

        public TesteController(INotificador notificador, IUser user, ILogger<TesteController> logger) : base(notificador, user)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Valor()
        {

            //throw new Exception("Error");

            _logger.LogTrace("Log de Trace em Development");
            _logger.LogDebug("Log de Debug em Development");

            _logger.LogInformation("Log de Informação em Production");
            _logger.LogWarning("Log de Warning/Aviso em Production");
            _logger.LogError("Log de Erro em Production");
            _logger.LogCritical("Log de Problema Critico em Production");


            return "Sou a api version V2";
        }
    }
}
