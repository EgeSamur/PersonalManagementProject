namespace PersonalManagementProject.Shared.CrossCuttingConcerns.Logging.Serilog.Configurations;

public class ElasticSearchConfiguration
{
    public string ConnectionString { get; set; }

    public ElasticSearchConfiguration()
    {
        ConnectionString = string.Empty;
    }
}