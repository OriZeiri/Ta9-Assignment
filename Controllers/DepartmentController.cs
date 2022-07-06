using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ta9_Assignment.Models;
using Ta9_Assignment.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ta9_Assignment.Controllers{

    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase 
    {
        // create an instance of the IDepartmentRepository interface
        // who execute the methods from  DepartmentRepository class -> services.AddScoped on Startup.cs
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            this._departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<List<Department>> GetDepartments()
        {
            return await _departmentRepository.AllDepartments();
        }

        [HttpGet("{id}")]
        public async Task<Department> GetDepartmentById(int id)
        {
            return await _departmentRepository.DepartmentById(id);
        }

        [HttpPost]
        public async Task<Result.ResultCode> Create(int id, [FromBody]Department dep)
        {
            return await _departmentRepository.Create(id,dep);
        }

        [HttpPut("{id}")]
        public async Task<Result.ResultCode> Update(int id, [FromBody]Department dep)
        {
             return await _departmentRepository.Update(id,dep);
        }

        [HttpDelete("{id}")]
        public async Task<Result.ResultCode> Delete(int id)
        {
            return await _departmentRepository.Delete(id);
        }
        [HttpGet("{depId}/employees")]
        public async Task<List<Employee>> AllEmployeesAtDepartment(int depId)
        {
            return await _departmentRepository.AllEmployeesAtDepartment(depId);
        }
    }
}