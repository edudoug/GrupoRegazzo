using Microsoft.Extensions.Configuration;

public class FeatureFlagsAdapter : IFeatureFlagPort
{
    private readonly IConfiguration _configuration;

    public FeatureFlagsAdapter(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public bool IsTaxReformEnabled()
        => _configuration.GetValue<bool>("FeatureFlags:TaxReform");
}