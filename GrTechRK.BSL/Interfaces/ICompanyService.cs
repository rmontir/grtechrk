using GrTechRK.Database.Models;
using GrTechRK.DTO;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace GrTechRK.BSL.Interfaces
{
    public interface ICompanyService
    {
        Task<ImmutableHashSet<CompanyDto>> GetAllCompaniesAsync(CancellationToken cancellationToken);
        Task<CompanyDto> GetAsync(int id, CancellationToken cancellationToken);

        Task<CompanyDto> AddAsync(Company company);
        Task<CompanyDto> UpdateAsync(Company company);
        Task<CompanyDto> UpdateLogoAsync(int id, byte[] logo);
        Task DeleteAsync(int id);
    }
}
