using System.Threading.Tasks;

namespace Farsight.Backend.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FarsightBackendDbContext _dbContext;

        public UnitOfWork(FarsightBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}