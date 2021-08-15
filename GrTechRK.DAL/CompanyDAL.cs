using GrTechRK.DAL.Interfaces;
using GrTechRK.Database;
using GrTechRK.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace GrTechRK.DAL
{
    public class CompanyDAL : ICompanyDAL
    {
        private readonly ApplicationDbContext _dbContext;

        public CompanyDAL(
            ApplicationDbContext dbContext
        )
        {
            _dbContext = dbContext;
        }

        public async Task<ImmutableHashSet<Company>> GetAllAsync(CancellationToken cancellationToken)
        {
            List<Company> rows = await _dbContext.Companies.ToListAsync(cancellationToken).ConfigureAwait(false);
            return rows.ToImmutableHashSet();
        }

        public async Task<Company> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Companies.SingleAsync(c => c.Id == id, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Company> AddAsync(Company company)
        {
            if (company.Id.HasValue) throw new InvalidOperationException("Company already has an Id");

            _dbContext.Companies.Add(company);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return await GetAsync(company.Id.Value, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<Company> UpdateAsync(Company company)
        {
            Company row = await _dbContext.Companies.SingleAsync(c => c.Id == company.Id).ConfigureAwait(false);

            row.Name = company.Name;
            row.Email = company.Email;
            row.Website = company.Website;

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return row;
        }

        public async Task<Company> UpdateLogoAsync(int id, byte[] logo)
        {
            Company row = await _dbContext.Companies.SingleAsync(c => c.Id == id).ConfigureAwait(false);

            row.Logo = logo;

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return row;
        }

        public async Task DeleteAsync(int id)
        {
            Company company = await GetAsync(id, CancellationToken.None).ConfigureAwait(false);
            _dbContext.Companies.Remove(company);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
