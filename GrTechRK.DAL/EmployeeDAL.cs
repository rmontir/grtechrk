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
    public class EmployeeDAL : IEmployeeDAL
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeDAL(
            ApplicationDbContext dbContext
        )
        {
            _dbContext = dbContext;
        }

        public async Task<ImmutableHashSet<Employee>> GetAllAsync(CancellationToken cancellationToken)
        {
            List<Employee> rows = await _dbContext.Employees.Include(e => e.Company).ToListAsync(cancellationToken).ConfigureAwait(false);
            return rows.ToImmutableHashSet();
        }

        public async Task<Employee> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Employees.Include(e => e.Company).SingleAsync(e => e.Id == id, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            if (employee.Id.HasValue) throw new InvalidOperationException("Employee already has an Id");

            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return await GetAsync(employee.Id.Value, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            Employee row = await _dbContext.Employees.SingleAsync(e => e.Id == employee.Id).ConfigureAwait(false);

            row.FirstName = employee.FirstName;
            row.LastName = employee.LastName;
            row.CompanyId = employee.CompanyId;
            row.Email = employee.Email;
            row.Phone = employee.Phone;

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return await GetAsync(row.Id.Value, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id)
        {
            Employee employee = await GetAsync(id, CancellationToken.None).ConfigureAwait(false);
            _dbContext.Employees.Remove(employee);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
