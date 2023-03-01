using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Application.Interface.Security.Jwt;
using Application.Entity;

namespace Primax_AzureFunctions.Functions.Auth
{
    public class Authorization
    {
        private readonly ILogger<Authorization> _logger;
        private readonly IJwtGenerator _jwt;

        public Authorization(ILogger<Authorization> log, IJwtGenerator repo)
        {
            _logger = log;
            _jwt = repo;
        }

        [FunctionName("Authorization")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {

            IActionResult returnValue = null;
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject<CredentialsEntity>(requestBody);

                CredentialsEntity credentials = new()
                {
                    Username= data.Username,
                    Password= data.Password,
                };

                var result = await _jwt.CreateToken(credentials);

                returnValue = new OkObjectResult(result);
            }
            catch(Exception ex)
            {
            }

            return returnValue; 
        }
    }
}
