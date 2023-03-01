using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Application.Interface;
using Primax_AzureFunctions.Functions.Create;
using System.Collections.Generic;
using Application.Entity;

namespace Primax_AzureFunctions.Functions.Read
{
    public class GetConsentList
    {
        private readonly ILogger<GetConsentList> _logger;
        private readonly IConsentRepo _consent;

        public GetConsentList(ILogger<GetConsentList> log, IConsentRepo repo)
        {
            _logger = log;
            _consent = repo;
        }

        [FunctionName("GetConsentList")]
        public async Task<List<AcquiescenceEntity>> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            List<AcquiescenceEntity> returnValue = null;

            try 
            {
                returnValue = await _consent.GetList();
            }
            catch(Exception ex) 
            {

            }

            return returnValue;
        }
    }
}
