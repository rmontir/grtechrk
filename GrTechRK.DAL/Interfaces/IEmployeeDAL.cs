using GrTechRK.Database.Models;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace GrTechRK.DAL.Interfaces
{
    public interface IEmployeeDAL
    {
        Task<ImmutableHashSet<Employee>> GetAllAsync(CancellationToken cancellationToken);
        Task<Employee> GetAsync(int id, CancellationToken cancellationToken);

        Task<Employee> AddAsync(Employee employee);
        Task<Employee> UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
    }
}
