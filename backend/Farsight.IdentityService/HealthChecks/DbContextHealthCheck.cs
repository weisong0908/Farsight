using System.Threading;
using System.Threading.Tasks;
using Farsight.IdentityService.Persistence;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Farsight.IdentityService.HealthChecks
{
    public class DbContextHealthCheck : IHealthCheck
    {
        private readonly FarsightIdentityServiceDbContext _dbContext;

        public DbContextHealthCheck(FarsightIdentityServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var healthCheckResultHealthy = await _dbContext.Database.CanConnectAsync();

            if (healthCheckResultHealthy)
            {
                return HealthCheckResult.Healthy();
            }

            return HealthCheckResult.Unhealthy();
        }
    }
}