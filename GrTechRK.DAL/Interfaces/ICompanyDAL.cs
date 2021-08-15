using GrTechRK.Database.Models;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace GrTechRK.DAL.Interfaces
{
    public interface ICompanyDAL
    {
        Task<ImmutableHashSet<Company>> GetAllAsync(CancellationToken cancellationToken);
        Task<Company> GetAsync(int id, CancellationToken cancellationToken);

        Task<Company> AddAsync(Company company);
        Task<Company> UpdateAsync(Company company);
        Task<Company> UpdateLogoAsync(int id, byte[] logo);
        Task DeleteAsync(int id);
    }
}
