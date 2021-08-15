using GrTechRK.Database.Models;
using GrTechRK.DTO;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace GrTechRK.BSL.Interfaces
{
    public interface IEmployeeService
    {
        Task<ImmutableHashSet<EmployeeDto>> GetAllEmployeesAsync(CancellationToken cancellationToken);
        Task<EmployeeDto> GetAsync(int id, CancellationToken cancellationToken);

        Task<EmployeeDto> AddAsync(Employee employee);
        Task<EmployeeDto> UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
    }
}
