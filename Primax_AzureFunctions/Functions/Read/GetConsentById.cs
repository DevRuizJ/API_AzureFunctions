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

namespace Primax_AzureFunctions.Functions.Read
{
    public class GetConsentById
    {
        private readonly ILogger<SaveConsent> _logger;
        private readonly IConsentRepo _consent;

        public GetConsentById(ILogger<SaveConsent> log, IConsentRepo repo) 
        {
            _logger = log;
            _consent = repo;
        }

        [FunctionName("GetConsentById")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "consent/{id}")] HttpRequest req,
            string id)
        {

            IActionResult returnValue = null;

            try
            {
                var acquiescence = await _consent.GetById(id);
            }
            catch (Exception ex)
            {
                returnValue = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return returnValue;
        }
    }
}
