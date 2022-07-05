using System.Threading.Tasks;
using Ta9_Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Neo4jClient;

namespace Ta9_Assignment.Repositories 
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> AllEmployees();
        Task<Employee> EmployeeById(int id);
        Task<Result.ResultCode> Create(int id, Employee emp);
        Task<Result.ResultCode> Update(int id, Employee emp);
        Task<Result.ResultCode> Delete(int id);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IGraphClient _client;
        public EmployeeRepository(IGraphClient client)
        {
            this._client = client;
        }

        public async Task<List<Employee>> AllEmployees()
        {
            var employees = await _client.Cypher.Match("(e:Employee)")
                                                  .Return(e => e.As<Employee>()).ResultsAsync;
            return employees.ToList();
        }

        public async Task<Employee> EmployeeById(int id)
        {
            var emp = await _client.Cypher.Match("(e:Employee)")
                                                  .Where((Employee e) => e.id == id)
                                                  .Return(e => e.As<Employee>()).ResultsAsync;
            return emp.LastOrDefault();
        }

        public async Task<Result.ResultCode> Create(int id, Employee emp)
        {
            await _client.Cypher.Create("(e:Employee $emp)")
                                .WithParam("emp",emp)
                                .ExecuteWithoutResultsAsync();
            return Result.ResultCode.SUCSSES;
        }

        public async Task<Result.ResultCode> Update(int id, Employee emp)
        {
            await _client.Cypher.Match("(e:Employee $emp)")
                                .Where((Employee e) => e.id == id)
                                .Set("e = $emp")
                                .WithParam("emp",emp)
                                .ExecuteWithoutResultsAsync();
            return Result.ResultCode.SUCSSES;
        }

        public async Task<Result.ResultCode> Delete(int id)
        {
            await _client.Cypher.Match("(e:Employee)")
                                .Where((Employee e) => e.id == id)
                                .Delete("e")
                                .ExecuteWithoutResultsAsync();
            return Result.ResultCode.SUCSSES;
        }
    }
}