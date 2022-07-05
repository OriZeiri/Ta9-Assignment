using System.Threading.Tasks;
using Ta9_Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Neo4jClient;

namespace Ta9_Assignment.Repositories 
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> AllDepartments();
        Task<Department> DepartmentById(int id);
        Task<Result.ResultCode> Create(int id, Department dep);
        Task<Result.ResultCode> Update(int id, Department dep);
        Task<Result.ResultCode> Delete(int id);
        Task<List<Employee>> AllEmployeesAtDepartment(int depId);
    }

    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IGraphClient _client;
        public DepartmentRepository(IGraphClient client)
        {
            this._client = client;
        }

        public async Task<List<Department>> AllDepartments()
        {
            var departments = await _client.Cypher.Match("(n:Department)")
                                                  .Return(n => n.As<Department>()).ResultsAsync;
            return departments.ToList();
        }

        public async Task<Department> DepartmentById(int id)
        {
            var department = await _client.Cypher.Match("(d:Department)")
                                                  .Where((Department d) => d.id == id)
                                                  .Return(d => d.As<Department>()).ResultsAsync;
            return department.LastOrDefault();
        }

        public async Task<Result.ResultCode> Create(int id, Department dep)
        {
            await _client.Cypher.Create("(d:Department $dep)")
                                .WithParam("dep",dep)
                                .ExecuteWithoutResultsAsync();
            return Result.ResultCode.SUCSSES;
        }

        public async Task<Result.ResultCode> Update(int id, [FromBody]Department dep)
        {
            await _client.Cypher.Match("(d:Department)")
                                .Where((Department d) => d.id == id)
                                .Set("d = $dep")
                                .WithParam("dep",dep)
                                .ExecuteWithoutResultsAsync();
            return Result.ResultCode.SUCSSES;
        }

        public async Task<Result.ResultCode> Delete(int id)
        {
            await _client.Cypher.Match("(d:Department)")
                                .Where((Department d) => d.id == id)
                                .DetachDelete("d")
                                .ExecuteWithoutResultsAsync();
            return Result.ResultCode.SUCSSES;
        }

        public async Task<List<Employee>> AllEmployeesAtDepartment(int depId)
        {
            //MATCH (d:Department)-[r:hasEmployee]->(e:Employee) WHERE(d.id = 4) RETURN e
            var Employee = await _client.Cypher.Match("(d:Department)-[r:hasEmployee]->(e:Employee)")
                                          .Where((Department d) => d.id == depId)
                                          .Return(e => e.As<Employee>()).ResultsAsync;
            return Employee.ToList();
            
        }
    }
}