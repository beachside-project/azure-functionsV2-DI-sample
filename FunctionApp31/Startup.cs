using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

[assembly: FunctionsStartup(typeof(FunctionApp31.Startup))]

namespace FunctionApp31
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var serviceProvider = builder.Services.BuildServiceProvider();
            var env = serviceProvider.GetRequiredService<IHostingEnvironment>(); // if define the `EnvironmentName`, Set environment variable to value of the "AZURE_FUNCTIONS_ENVIRONMENT" key
            var defaultConfig = serviceProvider.GetRequiredService<IConfiguration>();

            var appDirectory = serviceProvider.GetRequiredService<IOptions<ExecutionContextOptions>>().Value.AppDirectory;

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(appDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            var customConfig = configBuilder.AddConfiguration(defaultConfig).Build();

            // 必要に応じて appsettings の内容を IConfiguration から取得
            builder.Services.Configure<SampleConfig>(customConfig.GetSection("SampleConfig"));

            builder.Services.AddSingleton(sp =>
            {
                var sampleConfig = sp.GetService<IOptions<SampleConfig>>().Value;
                //ここら辺は適当に。
                //return new MyClient(Environment.GetEnvironmentVariable("MyClientConnectionString"), sampleConfig);
                return new MyClient("AppDirectory:" + appDirectory + "; environment name: " + env.EnvironmentName, sampleConfig);
            });

            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IMyService, MyService>();
        }
    }
}