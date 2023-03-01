using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Application.Entity;
using Application.Interface;
using Azure.Core;
using System.Collections.Generic;
using JWT.Builder;
using JWT.Algorithms;
using Application.Interface.Security.Jwt;

namespace Primax_AzureFunctions.Functions.Create
{
    public class SaveConsent
    {

        private readonly ILogger<SaveConsent> _logger;
        private readonly IConsentRepo _consent;
        private readonly IJwtValidation _jwt;

        public SaveConsent(ILogger<SaveConsent> log, IConsentRepo repo, IJwtValidation jwt)
        {
            _logger = log;
            _consent = repo;
            _jwt = jwt;
        }

        [FunctionName("SaveConsent")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "consent/create")] HttpRequest req)
        {

            IActionResult returnValue = null;

            try
            {
                if (req.Headers is null)
                    throw new NullReferenceException();
                else if (req.Body is null)
                    throw new NullReferenceException();


                var requ = req.Headers["Authorization"].ToString().StartsWith("Bearer ");

                var token = req.Headers["Authorization"].ToString()[string.Format("Bearer ").Length..];

                var validate = _jwt.ValidateJwt(token);

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);

                AcquiescenceEntity acquiescence = new() 
                {
                    Id = Guid.NewGuid().ToString(),
                    DocumentNumber = data["document_number"],
                    DocumentType = data["document_type"],
                    CustomerName = data["customer_name"],
                    CustomerLastname = data["customer_lastname"],
                    AcquiescenceDate = DateTime.Parse(data["acquiescence_date"].ToString()),
                    Authorization = bool.Parse(data["authorization"].ToString()),
                    AcquiescenceValidity = DateTime.Parse(data["acquiescence_validity"].ToString()),
                    AcquiescenceVersion = data["acquiescence_version"],
                    SourceApp = data["source_app"],
                    AppIP = data["app_ip"],
                    SystemUser = data["system_user"],
                    ProcessDate = DateTime.Parse(data["process_date"].ToString())
                };

                string result = await _consent.Create(acquiescence);
                returnValue = new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                returnValue = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return returnValue;
        }
    }
}
