using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace FunctionApp31
{
    public class Function1
    {
        private readonly IMyService _myService;
        private readonly MyClient _myClient;
        private readonly IHostingEnvironment _env;

        public Function1(IMyService myService, MyClient myClient, IHostingEnvironment env)
        {
            _myService = myService;
            _myClient = myClient;
            _env = env;
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogWarning($"ENV::::::::::::::::::::::::::: {_env.EnvironmentName}");

            log.LogInformation($"IMyService ID(Scoped): {_myService.MyId}");
            log.LogInformation($"MyClient ID(Singleton): {_myClient.MyId}");
            log.LogInformation($"MyClient ConnectionString: {_myClient.ConnectionString}");

            var result = _myClient.GetSampleConfig(_env.EnvironmentName);
            log.LogInformation($"result: {result}");
            return new OkObjectResult(result);
        }
    }
}