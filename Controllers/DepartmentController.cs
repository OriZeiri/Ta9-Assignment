using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neo4jClient;

namespace Controllers{

    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase 
    {
        private readonly IGraphClient _client;
        public DepartmentController(IGraphClient client)
        {
            this._client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var departments = await _client.Cypher.Match("(d:Department)")
                                                  .Return(n => n.As<Department>()).ResultsAsync;
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var departments = await _client.Cypher.Match("(d:Department)")
                                                  .Where((Department d) => d._id == id)
                                                  .Return(d => d.As<Department>()).ResultsAsync;
            return Ok(departments.LastOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id, [FromBody]Department dep)
        {
            await _client.Cypher.Create("(d:Department $dep)")
                                .WithParam("dep",dep)
                                .ExecuteWithoutResultsAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]Department dep)
        {
            await _client.Cypher.Match("(d:Department)")
                                .Where((Department d) => d._id == id)
                                .Set("d = $dep")
                                .WithParam("dep",dep)
                                .ExecuteWithoutResultsAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _client.Cypher.Match("(d:Department)")
                                .Where((Department d) => d._id == id)
                                .Delete("d")
                                .ExecuteWithoutResultsAsync();
            return Ok();
        }
    }
}