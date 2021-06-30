using System.Threading.Tasks;

namespace Farsight.Backend.Persistence
{
    public interface IUnitOfWork
    {
        Task SaveChanges();
    }
}