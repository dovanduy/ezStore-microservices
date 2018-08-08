﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microservice.Core.Interfaces;
using Microservice.Logging.API.Application.Queries;
using Microservice.Logging.API.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Logging.API.Controllers
{
    [Route("api/[controller]")]
    public class LoggingController : Controller
    {
        private readonly ILoggingQueries _loggingQueries;
        protected readonly ICommandBus CommandBus;

        public LoggingController(ILoggingQueries loggingQueries, ICommandBus commandBus)
        {
            CommandBus = commandBus;
            _loggingQueries = loggingQueries;
        }

        // GET api/values
        [HttpGet]
        [Route("ExceptionLogs")]
        public Task<List<LogViewModel>> ExceptionLogs()
        {
            return Task.FromResult(_loggingQueries.GetExceptionLogs().Result);
        }

        [HttpGet]
        [Route("AuditLogs")]
        public Task<List<LogViewModel>> AuditLogs()
        {
            return Task.FromResult(_loggingQueries.GetAuditLogs().Result);
        }
    }
}