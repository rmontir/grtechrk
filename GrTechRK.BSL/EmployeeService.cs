using AutoMapper;
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
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeDAL _employeeDAL;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;

        public EmployeeService(
            IEmployeeDAL employeeDAL,
            IMailService mailService,
            IMapper mapper
        )
        {
            _employeeDAL = employeeDAL;
            _mailService = mailService;
            _mapper = mapper;
        }

        public async Task<ImmutableHashSet<EmployeeDto>> GetAllEmployeesAsync(CancellationToken cancellationToken)
        {
            ImmutableHashSet<Employee> employees = await _employeeDAL.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return employees.Select(e => _mapper.Map<EmployeeDto>(e)).ToImmutableHashSet();
        }

        public async Task<EmployeeDto> GetAsync(int id, CancellationToken cancellationToken)
        {
            Employee employee = await _employeeDAL.GetAsync(id, cancellationToken).ConfigureAwait(false);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> AddAsync(Employee employee)
        {
            if (employee.Id.HasValue) throw new ArgumentException("Employee already has an Id");
            if (!string.IsNullOrWhiteSpace(employee.Phone) && employee.Phone.Any(e => char.IsLetter(e))) throw new ArgumentException("Invalid phone number");

            employee = await _employeeDAL.AddAsync(employee).ConfigureAwait(false);
            try
            {
                _mailService.SendAsyc(employee.Company.Email, employee);
            }
            catch { }
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> UpdateAsync(Employee employee)
        {
            if (!employee.Id.HasValue) throw new ArgumentException("Employee does not have an Id");

            employee = await _employeeDAL.UpdateAsync(employee).ConfigureAwait(false);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task DeleteAsync(int id)
        {
            await _employeeDAL.DeleteAsync(id).ConfigureAwait(false);
        }
    }
}
