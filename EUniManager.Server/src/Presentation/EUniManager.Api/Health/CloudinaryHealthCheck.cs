using System.Net;

using CloudinaryDotNet.Actions;

using EUniManager.Application.Models.Cloudinary.Interfaces;

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EUniManager.Api.Health;

public class CloudinaryHealthCheck : IHealthCheck
{
    private readonly ICloudinaryService _cloudinaryService;
    
    public CloudinaryHealthCheck(ICloudinaryService cloudinaryService)
    {
        _cloudinaryService = cloudinaryService;
    }
    
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, 
        CancellationToken cancellationToken = new())
    {
        PingResult pingResult = await _cloudinaryService.PingAsync(cancellationToken);

        return pingResult.StatusCode switch
        {
            HttpStatusCode.OK => HealthCheckResult.Healthy(),
            _ => HealthCheckResult.Degraded("Unable to ping Cloudinary!")
        };
    }
}