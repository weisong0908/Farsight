using System.Threading;
using System.Threading.Tasks;
using Farsight.Backend.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Farsight.Backend.HealthChecks
{
    public class DbContextHealthCheck : IHealthCheck
    {
        private readonly FarsightBackendDbContext _dbContext;

        public DbContextHealthCheck(FarsightBackendDbContext dbContext)
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