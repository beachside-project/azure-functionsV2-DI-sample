using System;
using Newtonsoft.Json;

namespace FunctionApp31
{
    public class MyClient
    {
        private readonly SampleConfig _sampleConfig;
        public Guid MyId { get; } = Guid.NewGuid();
        public string ConnectionString { get; }

        public MyClient(string connectionString, SampleConfig sampleConfig)
        {
            _sampleConfig = sampleConfig;
            ConnectionString = connectionString;
        }

        public string GetSampleConfig(string environmentName)
        {
            _sampleConfig.EnvironmentName = environmentName;
            return JsonConvert.SerializeObject(_sampleConfig);
        }
    }
}