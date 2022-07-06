using System.Threading.Tasks;
using Ta9_Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Neo4jClient;

namespace Ta9_Assignment.Repositories 
{
    public interface IEmployeeRepository
    {
        // get - all the Employees from DB
        Task<List<Employee>> AllEmployees();
        
        // get - Employee by id
        Task<Employee> EmployeeById(int id);

        // post - add new Employee to db from body content
        Task<Result.ResultCode> Create(int id, Employee emp);

        // put - update Employee to db from body content
        Task<Result.ResultCode> Update(int id, Employee emp);

        // delete - Employee to db from body content
        Task<Result.ResultCode> Delete(int id);

        // post - create a relation between an Employee to Department :  Employee <- hasEmployee - Department
        Task<Result.ResultCode> AssignToDepartment(int empId, int depId);
    }

    // Implement IEmployeeRepository methods
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
                                .DetachDelete("e")
                                .ExecuteWithoutResultsAsync();
            return Result.ResultCode.SUCSSES;
        }

        public async Task<Result.ResultCode> AssignToDepartment(int empId, int depId)
        {
            await _client.Cypher.Match("(d:Department), (e:Employee)")
                                    .Where((Department d, Employee e) => d.id == depId && e.id == empId)
                                    .Create("(d)-[r:hasEmployee]->(e)")
                                    .ExecuteWithoutResultsAsync();

            return Result.ResultCode.SUCSSES;
        }
    }
}