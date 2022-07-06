using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ta9_Assignment.Models;
using Ta9_Assignment.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ta9_Assignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        // create an instance of the IEmployeeRepository interface
        // who execute the methods from EmployeeRepository class -> services.AddScoped on Startup.cs
        private readonly IEmployeeRepository _employeeRepository;
        
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<List<Employee>> GetEmployees()
        {
            return await _employeeRepository.AllEmployees();
        }

        [HttpGet("{id}")]
        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _employeeRepository.EmployeeById(id);
        }

        [HttpPost]
        public async Task<Result.ResultCode> Create(int id, [FromBody]Employee emp)
        {
            return await _employeeRepository.Create(id,emp);
        }

        [HttpPut("{id}")]
        public async Task<Result.ResultCode> Update(int id, [FromBody]Employee emp)
        {
             return await _employeeRepository.Update(id,emp);
        }

        [HttpDelete("{id}")]
        public async Task<Result.ResultCode> Delete(int id)
        {
            return await _employeeRepository.Delete(id);
        }

        [HttpPost("{empId}/assign-employee/{depId}/")]
        public async Task<Result.ResultCode> AssignToDepartment(int empId, int depId)
        {
            return await _employeeRepository.AssignToDepartment(empId,depId);
        }
    }
}