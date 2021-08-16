using AutoMapper;
using GrTechRK.BSL.Common;
using GrTechRK.BSL.Interfaces;
using GrTechRK.DAL.Interfaces;
using GrTechRK.Database.Models;
using GrTechRK.DTO;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GrTechRK.BSL
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyDAL _companyDAL;
        private readonly IMapper _mapper;

        public CompanyService(
            ICompanyDAL companyDAL,
            IMapper mapper
        )
        {
            _companyDAL = companyDAL;
            _mapper = mapper;
        }

        public async Task<ImmutableHashSet<CompanyDto>> GetAllCompaniesAsync(CancellationToken cancellationToken)
        {
            ImmutableHashSet<Company> companies = await _companyDAL.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return companies.Select(c => _mapper.Map<CompanyDto>(c)).ToImmutableHashSet();
        }

        public async Task<CompanyDto> GetAsync(int id, CancellationToken cancellationToken)
        {
            Company company = await _companyDAL.GetAsync(id, cancellationToken).ConfigureAwait(false);
            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<CompanyDto> AddAsync(Company company)
        {
            if (company.Id.HasValue) throw new ArgumentException("Company already has an Id");
            if (string.IsNullOrWhiteSpace(company.Name)) throw new ArgumentException("Name is required");
            if (EmailValidator.IsValidEmail(company.Email)) throw new ArgumentException("Invalid email format");

            company = await _companyDAL.AddAsync(company).ConfigureAwait(false);
            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<CompanyDto> UpdateAsync(Company company)
        {
            if (!company.Id.HasValue) throw new ArgumentException("Company does not have an Id");
            if (string.IsNullOrWhiteSpace(company.Name)) throw new ArgumentException("Name is required");
            if (EmailValidator.IsValidEmail(company.Email)) throw new ArgumentException("Invalid email format");

            company = await _companyDAL.UpdateAsync(company).ConfigureAwait(false);
            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<CompanyDto> UpdateLogoAsync(int id, byte[] logo)
        {
            Company company = await _companyDAL.UpdateLogoAsync(id, logo).ConfigureAwait(false);
            return _mapper.Map<CompanyDto>(company);
        }

        public async Task DeleteAsync(int id)
        {
            await _companyDAL.DeleteAsync(id).ConfigureAwait(false);
        }
    }
}
