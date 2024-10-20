using PersonalManagementProject.Shared.CrossCuttingConcerns.Logging.Serilog.Configurations;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace PersonalManagementProject.Shared.CrossCuttingConcerns.Logging.Serilog.Logger;

public class ElasticSearchLogger : LoggerServiceBase
{
    public ElasticSearchLogger(IConfiguration configuration)
    {
        ElasticSearchConfiguration? logConfiguration = configuration
            .GetSection("SeriLogConfigurations:ElasticSearchConfiguration")
            .Get<ElasticSearchConfiguration>();

        Logger = new LoggerConfiguration().WriteTo
            .Elasticsearch(
                new ElasticsearchSinkOptions(new Uri(logConfiguration.ConnectionString))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                    CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true)
                }
            )
            .CreateLogger();
    }
}